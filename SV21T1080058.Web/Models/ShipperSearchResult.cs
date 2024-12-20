using SV21T1080058.DomainModels;

namespace SV21T1080058.Web.Models
{
    /// <summary>
    /// Kết quả tìm kiếm Người giao hàng
    /// </summary>
    public class ShipperSearchResult : PaginationSearchResult
    {
        public required List<Shipper> Data { get; set; }
    }
}
