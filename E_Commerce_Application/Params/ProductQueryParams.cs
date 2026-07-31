using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Application.Params
{
    public class ProductQueryParams
    {
        public int? brandID { get; set; }
        public int? typeID { get; set; }
        public string? searchValue { get; set; }
        public ProductSortingOptions Sort { get; set; }
        public int PageIndex { get; set; } = 1;
        private const int MaxPageSize = 10;
        private const int DefaultPageSize = 5;
        private int _pageSize = DefaultPageSize;
        public int Pagesize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : (value < 1 ? DefaultPageSize : value);
        }

    }
        
}
