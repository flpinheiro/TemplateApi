using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text;

namespace TemplateApi.CrossCutting.Extensions
{
    public static class ExcelExtensions
    {
        public const string ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public const string ContentTypeCsv = "text/csv";
        /// <summary>
        /// Creates a <see cref="FileStreamResult"/> which constains an excel file created from a <see cref="XLWorkbook"/>
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static FileStreamResult CreateFile(this XLWorkbook workbook, string fileName)
        {

            if (!fileName.EndsWith(".xlsx")) fileName += ".xlsx";
            var memoryStream = new MemoryStream();
            workbook.SaveAs(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(memoryStream, ContentType)
            {
                LastModified = DateTime.Now,
                FileDownloadName = fileName
            };
        }

        /// <summary>
        /// Return a csv file into the <see cref="FileStreamResult"/> from the <see cref="DataTable"/>
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static FileStreamResult DeliverCsvFile(this DataTable table, string fileName)
        {
            if (!fileName.EndsWith(".csv")) fileName += ".csv";

            var data = new StringBuilder();


            for (var i = 0; i < table.Columns.Count; i++)
            {
                data.Append(table.Columns[i].ColumnName);
                if (i < table.Columns.Count - 1)
                    data.Append(",");

            }
            data.AppendLine();
            foreach (DataRow item in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(item[i]))
                    {
                        var value = item[i].ToString();
                        if (!string.IsNullOrEmpty(value) && value.Contains(',')) value = string.Format("\"{0}\"", value);
                        data.Append(value);
                    }
                    if (i < table.Columns.Count - 1) data.Append(',');
                }
                data.AppendLine();
            }

            var memoryStream = new MemoryStream();
            var stream = new StreamWriter(memoryStream);
            stream.Write(data.ToString());
            stream.Flush();

            memoryStream.Seek(0, SeekOrigin.Begin);

            return new FileStreamResult(memoryStream, ContentTypeCsv)
            {
                LastModified = DateTime.Now,
                FileDownloadName = fileName,
            };

        }

        /// <summary>
        /// Return a <see cref="FileStreamResult"/> which contains an Excel File
        /// </summary>
        /// <param name="table"><see cref="DataTable"/> To be converted into an excel file </param>
        /// <param name="fileName">File nema</param>
        /// <returns></returns>
        public static FileStreamResult DeliverExcelFile(this DataTable table, string fileName) =>
            table.DataTableToXLWorkbook().CreateFile(fileName);

        /// <summary>
        /// Return an excel File into the <see cref="FileStreamResult"/> from the <see cref="DataSet"/>
        /// </summary>
        /// <param name="tableSet"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static FileStreamResult DeliverExcelFile(this DataSet tableSet, string fileName) =>
            tableSet.DataTableToXLWorkbook().CreateFile(fileName);

        /// <summary>
        /// Receive a <see cref="DataTable"/> and converto to <see cref="XLWorkbook"/>
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static XLWorkbook DataTableToXLWorkbook(this DataTable table)
        {
            var wbook = new XLWorkbook();
            var ws = wbook.Worksheets.Add(table);
           // ws.Columns().AdjustToContents();
            ws.SheetView.FreezeRows(1);
            return wbook;
        }

        /// <summary>
        /// Receive a <see cref="DataTable"/> and converto to <see cref="XLWorkbook"/>
        /// </summary>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        public static XLWorkbook DataTableToXLWorkbook(this DataSet dataSet)
        {
            var wbook = new XLWorkbook();
            foreach (DataTable table in dataSet.Tables)
            {
                var ws = wbook.Worksheets.Add(table, table.TableName);
               // ws.Columns().AdjustToContents();
                ws.SheetView.FreezeRows(1);
            }
            return wbook;
        }
    }
}
