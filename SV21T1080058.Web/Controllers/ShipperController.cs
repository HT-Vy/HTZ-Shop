using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SV21T1080058.BusinessLayers;
using SV21T1080058.DomainModels;
using SV21T1080058.Web.Models;
using System.Buffers;

namespace SV21T1080058.Web.Controllers
{
    [Authorize]
    public class ShipperController : Controller
    {
        const int PAGE_SIZE = 20;
        private const string SEARCH_CONDITION = "shipper_search";
        public IActionResult Index()
        {
            PaginationSearchInput? input = ApplicationContext.GetSessionData<PaginationSearchInput>(SEARCH_CONDITION);
            if (input == null)
            {
                input = new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = ""
                };
            }
            return View(input);
        }

        public IActionResult Search(PaginationSearchInput input)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfShippers(out rowCount, input.Page, input.PageSize, input.SearchValue ?? "");
            var model = new ShipperSearchResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue ?? "",
                RowCount = rowCount,
                Data = data
            };
            ApplicationContext.SetSessionData(SEARCH_CONDITION, input);
            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.Title = "Bổ sung Người giao hàng";
            Shipper shipper = new Shipper()
            {
                ShipperID = 0
            };
            return View("Edit", shipper);
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Title = "Cập nhật thông tin Người giao hàng";
            Shipper? shipper = CommonDataService.GetShipper(id);
            if (shipper == null)
                return RedirectToAction("Index");
            return View(shipper);
        }

        [HttpPost]
        public IActionResult Save(Shipper data)
        {
            //TODO: Kiểm tra dữ liệu đầu vào có hợp lệ không
            ViewBag.Title = data.ShipperID == 0 ? "Bổ sung Người giao hàng" : "Cập nhật thông tin Người giao hàng";
            //TODO: Kiểm tra dữ liệu đầu vào có hợp lệ không
            if (string.IsNullOrWhiteSpace(data.ShipperName))
                ModelState.AddModelError(nameof(data.ShipperName), "Tên Người giao hàng không được để trống");
            if (string.IsNullOrWhiteSpace(data.Phone))
                ModelState.AddModelError(nameof(data.Phone), "Số điện thoại không được để trống");
            
            //Nếu tồn tại lỗi thì trả dữ liệu về lại cho View  người sử dụng nhập lại cho đúng
            if (!ModelState.IsValid)
            {
                return View("Edit", data);
            }

            //Gọi chức năng xử lí dưới tầng tác nghiệp nếu quá trình kiểm soát lỗi không phát triển lỗi đầu vào
            if (data.ShipperID == 0)
            {
                //bổ sung
                CommonDataService.AddShipper(data);
            }
            else
            {
                //chỉnh sửa
                CommonDataService.UpdateShipper(data);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id = 0)
        //Nếu lời gọi là POST thì thực hiện xóa
        {
            if (Request.Method == "POST")
            {
                CommonDataService.DeleteShipper(id);
                return RedirectToAction("Index");
            }
            //Nếu lời gọi là GET thì hiển thị thông tin khách hàng cần xóa
            var shipper = CommonDataService.GetShipper(id);
            if (shipper == null)
                return RedirectToAction("Index");
            ViewBag.AllowDelete = !CommonDataService.IsUsedShipper(id);
            return View(shipper);
        }
    }
}
