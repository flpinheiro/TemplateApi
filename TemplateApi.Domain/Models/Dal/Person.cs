using TemplateApi.Domain.Interfaces;

namespace TemplateApi.Domain.Models.Dal
{
    public class Person : BasicDal<string>, IAuditable
    {
        public string? Name { get; set; }
        public string? SurName { get; set; }
        public string? BirthDay { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool IsUpdated { get; set; } = false;
    }
}
