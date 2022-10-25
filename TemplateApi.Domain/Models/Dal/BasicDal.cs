using System.Diagnostics.CodeAnalysis;

namespace TemplateApi.Domain.Models.Dal
{
    [ExcludeFromCodeCoverage]
    public abstract class BasicDal<Key>
        where Key : IEquatable<Key>
    {
        public BasicDal(Key id)
        {
            Id = id;
        }
        public Key Id { get; set; }
    }
}
