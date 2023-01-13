using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace TemplateApi.CrossCutting.Models;

public class PagedListQuery
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public static class PagedList
{
    public static PagedList<TModel> Empty<TModel>() => new();
}

public class PagedList<T> : IPagedList<T>
{
    private readonly IList<T> subset;

    public PagedList() : this(Enumerable.Empty<T>(), 0, 0) { }
    public PagedList(IEnumerable<T> items, PagedListQuery query): this(items, query.PageNumber, query.PageSize) { }
    public PagedList(IEnumerable<T> items, int pageNumber, int pageSize)
    {
        subset = items as IList<T> ?? new List<T>(items);
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(Count / (double)pageSize);
    }

    public int PageNumber { get; }

    public int TotalPages { get; }

    public bool IsFirstPage => PageNumber == 1;

    public bool IsLastPage => PageNumber == TotalPages;

    public int Count => subset.Count;

    public T this[int index] => subset[index];

    public IEnumerator<T> GetEnumerator() => subset.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => subset.GetEnumerator();
}

public interface IPagedList<out T> : IReadOnlyList<T>
{
    public int PageNumber { get; }

    public int TotalPages { get; }

    public bool IsFirstPage { get; }

    public bool IsLastPage { get; }
}
public static class PagedListQueryableExtensions
{
    public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, PagedListQuery query)
        => source.ToPagedList(query.PageNumber, query.PageSize);
    public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageNumber, int pageSize)
    {
        if (source.Any())
        {
            var items = source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return new(items, pageNumber, pageSize);
        }

        return PagedList.Empty<T>();
    }

    public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, PagedListQuery query, CancellationToken token = default)
        => await source.ToPagedListAsync(query.PageNumber, query.PageSize, token);

    public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize, CancellationToken token = default)
    {
        if (source.Any())
        {
            var items = await source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(token);
            return new(items, pageNumber, pageSize);
        }

        return PagedList.Empty<T>();
    }
}

