using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateApi.CrossCutting.Enums;

namespace TemplateApi.Domain.Interfaces
{
    public interface ISortableQuery<TSortBy>
        where TSortBy : struct
    {
        SortAsEnum SortAs { get; set; }
        TSortBy SortBy { get; set; }
    }
    public interface ISortablePeopleQuery : ISortableQuery<PersonEnum> { }
}
