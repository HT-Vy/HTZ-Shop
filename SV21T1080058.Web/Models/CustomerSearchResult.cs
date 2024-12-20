using SV21T1080058.DomainModels;
using SV21T1080058.Web.Models;

namespace SV21T1080058.Web.Models
{
    /// <summary>
    /// Kết quả tìm kiếm khách hàng
    /// </summary>
    public class CustomerSearchResult : PaginationSearchResult
    {
        public required List<Customer> Data { get; set; }
    }
}
