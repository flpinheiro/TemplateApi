using TemplateApi.CrossCutting.Constants;

namespace TemplateApi.CrossCutting.Exceptions
{
    public class PersonNotFoundException : Exception
    {
        public PersonNotFoundException() : base(ErrorMessage.PersonNotFound) { }
    }
}
