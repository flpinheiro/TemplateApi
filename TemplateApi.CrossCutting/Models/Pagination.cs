namespace TemplateApi.CrossCutting.Models
{
    public class Pagination
    {
        private int _pageSize;
        public int PageSize
        {
            get => _pageSize > 0 ? _pageSize : 10;
            init => _pageSize = value;
        }
        public int Page { get; init; } = 0;
    }

    public record PaginationResponse(int Pages, int Total);

    public static class PaginationResponseExtensions
    {
        public static PaginationResponse Paginate<T>(this IQueryable<T> query, Pagination pagination)
            where T : class
        {
            var total = query.Count();
            var pages = (int)Math.Ceiling((decimal)total / pagination.PageSize);

            return new PaginationResponse(pages, total);
        }
    }

    public static class PaginationExtensions
    {
        public static IQueryable<T> GetPaginated<T>(this IQueryable<T> query, Pagination pagination)
        where T : class
            => query.Skip(pagination.PageSize * (pagination.Page)).Take(pagination.PageSize).AsQueryable();
    }


}
