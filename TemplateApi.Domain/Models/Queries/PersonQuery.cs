using TemplateApi.CrossCutting.Enums;
using TemplateApi.Domain.Interfaces;

namespace TemplateApi.Domain.Models.Queries
{
    public class PersonQuery : ISortablePeopleQuery
    {
        public string? Name { get; set; }
        public string? Cpf { get; set; }
        public SortAsEnum SortAs { get; set; } = SortAsEnum.Asc;
        public PersonEnum SortBy { get; set; } = PersonEnum.Name;
    }
}
