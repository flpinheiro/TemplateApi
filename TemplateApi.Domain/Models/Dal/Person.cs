namespace TemplateApi.Domain.Models.Dal
{
    public class Person : BasicDal<string>
    {
        public string? Name { get; set; }
        public string? SurName { get; set; }
        public string? BirthDay { get; set; }
    }
}
