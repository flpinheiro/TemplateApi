using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateApi.Domain.Models.Dal
{
    public class Person : BasicDal<string>
    {
        public string? Name { get; set; }
        public string? SurName { get; set; }
        public string? BirthDay { get; set; }
    }
}
