using System.Collections.Generic;

namespace MCSABackend.Utilities
{
    public class Paged<T>
    {
        // Description: Array of items returned by the search
        // json tag:    items
        // json type:   int 
        public IEnumerable<T> Items { get; set; }

        // Description: Current page
        // json tag:    page
        // json type:   int   
        public int Page { get; set; } = 1;

        // Description: Number of items per page
        // json tag:    take
        // json type:   int 
        public int Take { get; set; } = 20;

        // Description: Total of items in the original query
        // json tag:    total
        // json type:   int 
        public int Total { get; set; }

    }
}
