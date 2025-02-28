﻿using SV21T1080058.DataLayers;
using SV21T1080058.DataLayers.SQLServer;
using SV21T1080058.DomainModels;
using SV21T1080058.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV21T1080058.BusinessLayers
{
    public static class ProductDataService
    {
        private static readonly IProductDAL productDB;

        static ProductDataService()
        {
            productDB = new ProductDAL(Configuration.ConnectionString);
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách mặt hàng (không phân trang)
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Product> ListProducts(string searchValue = "")
        {
            return productDB.List().ToList();
        }

        /// <summary>
        /// Tìm kiếm và lấy danh sách mặt hàng dưới dạng phân trang
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="categoryId"></param>
        /// <param name="supplierId"></param>
        ///
        /// <returns></returns>
        public static List<Product> ListProducts(out int rowCount, int page = 1, int pageSize = 0,
                                                string searchValue = "", int categoryId = 0, int supplierId = 0,
                                                decimal minPrice = 0, decimal maxPrice = 0)
        {
            rowCount = productDB.Count(searchValue, categoryId, supplierId, minPrice, maxPrice);
            return productDB.List(page, pageSize, searchValue, categoryId, supplierId, minPrice, maxPrice).ToList();
        }

        /// <summary>
        /// Lấy thông tin 1 mặt hàng theo mã mặt hàng
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static Product? GetProduct(int productId)
        {
            return productDB.Get(productId);
        }
        /// <summary>
        /// Bổ sung một sản phẩm mới
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddProduct(Product data)
        {
            return productDB.Add(data);
        }
        /// <summary>
        /// Cập nhật thông tin sản phẩm
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateProduct(Product data)
        {
            return productDB.Update(data);
        }
        /// <summary>
        /// Xóa 1 sản phẩm nếu không có dữ liệu liên quan
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static bool DeleteProduct(int productId)
        {
            if (productDB.IsUsed(productId))
                return false;
            return productDB.Delete(productId);
        }
        /// <summary>
        /// Kiểm tra xem sản phẩm có mã id hiện tại có dữ liệu liên quan không
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static bool InUsedProduct(int productId)
        {
            return productDB.IsUsed(productId);
        }

        /// <summary>
        /// Lấy danh sách ảnh của mỗi sản phẩm
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static List<ProductPhoto> ListPhotos(int productID)
        {
            return productDB.ListPhotos(productID).ToList();
        }
        /// <summary>
        /// Lấy thông tin ảnh
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        public static ProductPhoto? GetPhoto(long photoID)
        {
            return productDB.GetPhoto(photoID);
        }
        /// <summary>
        /// Thêm mới ảnh
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static long AddPhoto(ProductPhoto data)
        {
            return productDB.AddPhoto(data);
        }
        /// <summary>
        /// Cập nhật ảnh
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdatePhoto(ProductPhoto data)
        {
            return productDB.UpdatePhoto(data);
        }
        /// <summary>
        /// Xóa ảnh
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        public static bool DeletePhoto(long photoID)
        {
            return productDB.DeletePhoto(photoID);
        }
        /// <summary>
        /// lấy danh sách thuộc tính của mỗi sản phẩm
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static List<ProductAttribute> ListAttributes(int productID)
        {
            return productDB.ListAttributes(productID).ToList();
        }
        /// <summary>
        /// Lấy thông tin thuộc tính
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        public static ProductAttribute? GetAttribute(long attributeID)
        {
            return productDB.GetAttribute(attributeID);
        }
        /// <summary>
        /// Thêm mới ảnh
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static long AddAttribute(ProductAttribute data)
        {
            return productDB.AddAttribute(data);
        }
        /// <summary>
        /// Cập nhật ảnh
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateAttribute(ProductAttribute data)
        {
            return productDB.UpdateAttribute(data);
        }
        /// <summary>
        /// Xóa ảnh
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        public static bool DeleteAttribute(long attributeID)
        {
            return productDB.DeleteAttribute(attributeID);
        }
    }
}