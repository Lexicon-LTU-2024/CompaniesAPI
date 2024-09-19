using Microsoft.EntityFrameworkCore;

namespace Companies.API.Paging
{
    public class PagedList<T> 
    {
        public IEnumerable<T> Companies { get; }
        public MetaData MetaData { get; }

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData
                (
                    currentPage: pageNumber,
                    totalPages: (int)Math.Ceiling(count / (double)pageSize),
                    pageSize: pageSize,
                    totalCount: count
                );

            Companies = items;
        }

        public static async Task<PagedList<T>> CreateAsync(
            IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();

            var items = await source.Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
