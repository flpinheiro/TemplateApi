﻿namespace TemplateApi.CrossCutting.Models
{
    public class Pagination
    {
        public int PageSize { get; set; } = 10;
        public int Page { get; set; } = 1;
    }

    public record PaginationResponse(int Pages,int Total);

    public static class PaginationResponseExtensions
    {
        public static PaginationResponse Paginate<T>(this IQueryable<T> query, Pagination pagination)
            where T : class
        {
            var total = query.Count();
            var pages = (int) Math.Ceiling((decimal)total / pagination.PageSize);

            return new PaginationResponse(pages, total);
        }
    }

    public static class PaginationExtensions
    {
        public static IQueryable<T> GetPaginated<T>(this IQueryable<T> query, Pagination pagination)
        where T : class
            => query.Skip(pagination.PageSize * (pagination.Page - 1)).Take(pagination.PageSize).AsQueryable();
    }
}
