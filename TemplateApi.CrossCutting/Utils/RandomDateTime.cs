namespace TemplateApi.CrossCutting.Utils
{
    public static class RandomDateTime
    {
        private static readonly DateTime start = new(1970, 1, 1);
        private static readonly Random gen = new();
        private static readonly int range = (DateTime.Today - start).Days;

        public static DateTime Next()
        {
            return start.AddDays(gen.Next(range)).AddHours(gen.Next(0, 24)).AddMinutes(gen.Next(0, 60)).AddSeconds(gen.Next(0, 60));
        }

        public static DateOnly NextDateOnly()
        {
            return DateOnly.FromDateTime(Next());
        }

    }

}
