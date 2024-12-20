using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SV21T1080058.BusinessLayers;
using SV21T1080058.DomainModels;
using SV21T1080058.Web.Models;

namespace SV21T1080058.Web.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        const int PAGE_SIZE = 20;
        private const string SEARCH_CONDITION = "category_search";
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
            var data = CommonDataService.ListOfCategories(out rowCount, input.Page, input.PageSize, input.SearchValue ?? "");
            var model = new CategorySearchResult()
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
            ViewBag.Title = "Bổ sung Loại hàng";
            Category category = new Category()
            {
                CategoryID = 0
            };
            return View("Edit", category);
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Title = "Cập nhật thông tin Loại hàng";
            Category? category = CommonDataService.GetCategory(id);
            if (category == null)
                return RedirectToAction("Index");
            return View(category);
        }

        [HttpPost]
        public IActionResult Save(Category data)
        {
            //TODO: Kiểm tra dữ liệu đầu vào có hợp lệ không
            ViewBag.Title = data.CategoryID == 0 ? "Bổ sung loại hàng" : "Cập nhật thông tin loại hàng";
            //TODO: Kiểm tra dữ liệu đầu vào có hợp lệ không
            if (string.IsNullOrWhiteSpace(data.CategoryName))
                ModelState.AddModelError(nameof(data.CategoryName), "Tên loại hàng không được để trống");
            

            data.Description = data.Description ?? "";
            

            //Nếu tồn tại lỗi thì trả dữ liệu về lại cho View  người sử dụng nhập lại cho đúng
            if (!ModelState.IsValid)
            {
                return View("Edit", data);
            }

            //Gọi chức năng xử lí dưới tầng tác nghiệp nếu quá trình kiểm soát lỗi không phát triển lỗi đầu vào
            if (data.CategoryID == 0)
            {
                //bổ sung
                CommonDataService.AddCategory(data);
            }
            else
            {
                //chỉnh sửa
                CommonDataService.UpdateCategory(data);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id = 0)
        //Nếu lời gọi là POST thì thực hiện xóa
        {
            if (Request.Method == "POST")
            {
                bool result = CommonDataService.DeleteCategory(id);
                return RedirectToAction("Index");
            }
            //Nếu lời gọi là GET thì hiển thị thông tin khách hàng cần xóa
            var category = CommonDataService.GetCategory(id);
            if (category == null)
                return RedirectToAction("Index");
            ViewBag.AllowDelete = !CommonDataService.IsUsedCategory(id);
            return View(category);
        }
    }
}
