using SV21T1080058.DomainModels;

namespace SV21T1080058.Web.Models
{
    /// <summary>
    /// Kết quả tìm kiếm loại hàng
    /// </summary>
    public class CategorySearchResult : PaginationSearchResult
    {
        public required List<Category> Data { get; set; }
    }
}
