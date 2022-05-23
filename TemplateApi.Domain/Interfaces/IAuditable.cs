using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateApi.Domain.Interfaces
{
    public interface IAuditable
    {
        DateTime CreateDate { get; set; }
        DateTime? UpdateDate { get; set; }
    }
}
