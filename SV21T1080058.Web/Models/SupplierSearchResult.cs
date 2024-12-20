using SV21T1080058.DomainModels;

namespace SV21T1080058.Web.Models
{
    /// <summary>
    /// Kết quả tìm kiếm Nhà cung cấp
    /// </summary>
    public class SupplierSearchResult : PaginationSearchResult
    {
        public required List<Supplier> Data { get; set; }
    }
}
