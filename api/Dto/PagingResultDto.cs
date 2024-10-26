using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dto
{
    public class PagingResultDto<T>
    {
        public List<T> ResultItems { get; set; } = new List<T>();
        public int TotalRecords { get; set; }    // Tổng số bản ghi
        public int CurrentPage { get; set; }      // Số trang hiện tại
        public int PageSize { get; set; }        // Số lượng bản ghi trên mỗi trang

        public PagingResultDto(List<T> items, int totalCount, int pageSize, int currentPage)
        {
            ResultItems = items;
            TotalRecords = totalCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
        }
    }
}