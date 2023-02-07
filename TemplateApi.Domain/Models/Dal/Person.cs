using TemplateApi.Domain.Interfaces;

namespace TemplateApi.Domain.Models.Dal
{
    public class Person : BasicDal<Guid>, IAuditable
    {
        
        public Person(Guid Id, string Name, string SurName, string BirthDay, string CPF, string Email) : base(Id)
        {
            this.Name = Name;
            this.SurName = SurName;
            this.BirthDay = BirthDay;
            this.CPF = CPF;
            this.Email = Email;
        }

        public string Name { get; set; }
        public string SurName { get; set; }
        public string BirthDay { get; set; }
        public string CPF { get; private set; }
        public string Email { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool IsUpdated { get; set; } = false;
    }
}
