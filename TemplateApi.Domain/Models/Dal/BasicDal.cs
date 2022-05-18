using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateApi.Domain.Models.Dal
{
    public abstract class BasicDal<Key>
        where Key : IEquatable<Key>
    {
        public Key? Id { get; set; }
    }
}
