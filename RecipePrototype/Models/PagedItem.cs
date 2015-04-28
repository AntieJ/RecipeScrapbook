using System.Collections.Generic;

namespace RecipePrototype.Models
{
    public class PagedItem<T>
    {
        public IEnumerable<T> Payload { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }
}