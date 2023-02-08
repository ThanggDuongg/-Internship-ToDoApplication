using Microsoft.Extensions.Hosting;

namespace TodoApplication.Models.DTOs
{
    public class PaginatedDTO<T>
    {
        public PageInfo PageInfo { get; set; }

        public IEnumerable<T> Items { get; set; }

        public PaginatedDTO(IEnumerable<T> items, int count, int pageNumber, int itemsPerPage)
        {
            PageInfo = new PageInfo
            {
                CurrentPage = pageNumber,
                ItemsPerPage = itemsPerPage,
                TotalPages = (int)Math.Ceiling(count / (double)itemsPerPage),
                TotalItems = count
            };
            this.Items = items;
        }

        public static PaginatedDTO<T> ToPaginatedPost(
            IQueryable<T> items, int pageNumber, int itemsPerPage)
        {
            var count = items.Count();
            var chunk = items.Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage);
            return new PaginatedDTO<T>(chunk, count, pageNumber, itemsPerPage);
        }
    }
}
