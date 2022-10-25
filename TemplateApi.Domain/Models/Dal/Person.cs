using TemplateApi.Domain.Interfaces;

namespace TemplateApi.Domain.Models.Dal
{
    public class Person : BasicDal<string>, IAuditable
    {
        public Person(string Id, string Name, string SurName, string BirthDay, string CPF): base(Id)
        {
            this.Name = Name;
            this.SurName = SurName;
            this.BirthDay = BirthDay;
            this.CPF = CPF;
        }

        public string Name { get; set; }
        public string SurName { get; set; }
        public string BirthDay { get; set; }
        public string CPF { get; private set; }
        public DateTime LastUpdate { get; set; }
        public bool IsUpdated { get; set; } = false;
    }
}
