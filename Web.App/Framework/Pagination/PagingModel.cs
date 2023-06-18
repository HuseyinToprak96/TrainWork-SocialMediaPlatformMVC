using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.App.Framework.Pagination
{
    public class PagingModel<T>
    {
        public List<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / PageSize);
    }
}