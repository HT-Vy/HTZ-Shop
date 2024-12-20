using SV21T1080058.DomainModels;

namespace SV21T1080058.Web.Models

{
    /// <summary>
    /// Lớp cơ sở cho các kết quả tìm kiếm, hiển thị dữ liệu dưới dạng phân trang
    /// </summary>
    public class PaginationSearchResult
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; }
        public string SearchValue { get; set; } = "";

        

        public int RowCount { get; set; } = 0;

        public int PageCount
        {
            get
            {
                if (PageSize == 0) //Không phân trang
                    return 1;
                int n = RowCount / PageSize;
                if (RowCount % PageSize > 0)
                    n += 1;
                return n;
            }
        }
    }
    
    
    

    


    
}
