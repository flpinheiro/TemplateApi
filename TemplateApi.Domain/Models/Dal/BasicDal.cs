namespace TemplateApi.Domain.Models.Dal
{
    public abstract class BasicDal<Key>
        where Key : IEquatable<Key>
    {
        public Key? Id { get; set; }
    }
}
