using System.Diagnostics.CodeAnalysis;

namespace TemplateApi.Domain.Models.Dal
{
    [ExcludeFromCodeCoverage]
    public abstract class BasicDal<Key>
        where Key : IEquatable<Key>
    {
        public Key? Id { get; set; }
    }
}
