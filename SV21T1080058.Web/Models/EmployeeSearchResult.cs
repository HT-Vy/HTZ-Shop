using SV21T1080058.DomainModels;
using SV21T1080058.Web.Models;

namespace SV21T1080058.Web.Models
{    
    /// <summary>
    /// Kết quả tìm kiếm nhân viên
    /// </summary>
    public class EmployeeSearchResult : PaginationSearchResult
    {
        public required List<Employee> Data { get; set;}
    }

}
