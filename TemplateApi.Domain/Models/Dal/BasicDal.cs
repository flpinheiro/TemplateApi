namespace TemplateApi.Domain.Models.Dal
{
    [ExcludeFromCodecoverage]
    public abstract class BasicDal<Key>
        where Key : IEquatable<Key>
    {
        public Key? Id { get; set; }
    }
}
