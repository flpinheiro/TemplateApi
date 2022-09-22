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
