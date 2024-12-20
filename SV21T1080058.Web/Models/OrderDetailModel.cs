using SV21T1080058.DomainModels;

namespace SV21T1080058.Web.Models
{
    public class OrderDetailModel
    {
        public Order Order { get; set; }
        public List<OrderDetail> Details { get; set; }
    }
}
