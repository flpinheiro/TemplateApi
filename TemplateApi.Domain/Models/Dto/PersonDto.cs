using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateApi.Domain.Models.Dto
{
    public class PersonDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? SurName { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
