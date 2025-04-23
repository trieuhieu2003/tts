﻿using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using tts.Authorization;
using tts.Products.Dto;
using System;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;

using System.Threading.Tasks;
using AutoMapper.Internal.Mappers;

namespace tts.Products
{
    [AbpAuthorize]
    public class ProductAppService : ttsAppServiceBase, IProductAppService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRepository<Product> _productRepository;

        public ProductAppService(IRepository<Product> productRepository, IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        [AbpAuthorize(PermissionNames.Pages_Products_Create)]
        public async System.Threading.Tasks.Task Create(ProductListDto input)
        {
            // Xử lý upload ảnh nếu có
            string imagePath = null;
            if (input.ImageUrl != null && input.ImageUrl.Length > 0)
            {

                imagePath = await SaveImageAsync(input.ImageUrl);

            }
            // Map DTO sang entity 
            var product = new Product(
                input.Name.Trim(),
                input.Price,
                imagePath,
                input.Discount,
                input.CategoryId
            );

            // Thêm sản phẩm vào database
            await _productRepository.InsertAsync(product);
        }

        [AbpAuthorize]
        // phan trang product
        //public async Task<PagedResultDto<ProductDto>> GetProductPaged(PagedProductDto input)
        //{
        //    //input.MaxResultCount = input.MaxResultCount > 0 ? input.MaxResultCount : 15;
        //    var products = _productRepository.GetAllIncluding(p => p.Category);

        //    if (!string.IsNullOrWhiteSpace(input.Keyword))
        //    {
        //        products = products.Where(
        //            p => p.Name.Contains(input.Keyword) ||
        //            p.Price.ToString().Contains(input.Keyword) ||
        //            p.Discount.ToString().Contains(input.Keyword) ||
        //            p.Category.NameCategory.ToString().Contains(input.Keyword)
        //        );
        //    }

        //    var count = await products.CountAsync();

        //    //input.Sorting = "Id DESC";

        //    if (!string.IsNullOrWhiteSpace(input.Sorting))
        //    {
        //        products = products.OrderBy(input.Sorting);
        //    }
        //    else
        //    {
        //        products = products.OrderByDescending(p => p.CreationTime);
        //    }



        //    //var items = await products.OrderBy(input.Sorting).PageBy(input).ToListAsync();
        //    var items = await products.PageBy(input).ToListAsync();

        //    var result = items.Select(p => new ProductDto
        //    {
        //        Id = p.Id,
        //        Name = p.Name,
        //        Price = p.Price,
        //        ImageUrl = p.ImageUrl,
        //        Discount = p.Discount,
        //        CategoryId = p.CategoryId ?? 0,
        //        NameCategory = p.Category != null ? p.Category.NameCategory : "",
        //        CreationTime = p.CreationTime,
        //        LastModificationTime = p.LastModificationTime
        //    }).ToList();

        //    return new PagedResultDto<ProductDto>(count, result);

        //}

        public async Task<PagedResultDto<ProductDto>> GetProductPaged(PagedProductDto input)
        {
            //input.MaxResultCount = input.MaxResultCount > 0 ? input.MaxResultCount : 10;
            // Lấy truy vấn sản phẩm cơ bản có bao gồm thông tin danh mục
            var products = _productRepository.GetAllIncluding(p => p.Category).AsNoTracking();

            // Lọc theo từ khóa
            if (!string.IsNullOrWhiteSpace(input.Keyword))
            {
                var keyword = input.Keyword.ToLower().Trim();
                products = products.Where(p =>
                    p.Name.ToLower().Contains(keyword) ||
                    p.Price.ToString().Contains(keyword) ||
                    p.Discount.ToString().Contains(keyword) ||
                    p.Category.NameCategory.ToLower().Contains(keyword)
                );
            }

            // Lọc theo khoảng giá
            if (input.MinPrice.HasValue)
            {
                products = products.Where(p => p.Price >= input.MinPrice.Value);
            }

            if (input.MaxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= input.MaxPrice.Value);
            }

            // Lọc theo danh mục
            if (input.CategoryId.HasValue && input.CategoryId.Value > 0)
            {
                products = products.Where(p => p.CategoryId == input.CategoryId.Value);
            }

            // Tính tổng số lượng sản phẩm sau khi lọc
            var count = await products.CountAsync();

            // Lọc theo sắp xếp nếu có
            if (!string.IsNullOrWhiteSpace(input.Sorting))
            {
                if (input.Sorting.ToLower().Contains("namecategory"))
                {
                    var isDesc = input.Sorting.ToLower().EndsWith("desc");
                    products = isDesc
                        ? products.OrderByDescending(p => p.Category.NameCategory)
                        : products.OrderBy(p => p.Category.NameCategory);
                }
                else
                {
                    products = products.OrderBy(input.Sorting);
                }
            }
            else
            {
                products = products.OrderByDescending(p => p.CreationTime);
            }

            // Lấy kết quả phân trang
            var items = await products.PageBy(input).ToListAsync();

            // Chuyển kết quả thành danh sách ProductDto
            var result = items.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Discount = p.Discount,
                CategoryId = p.CategoryId ?? 0,
                NameCategory = p.Category != null ? p.Category.NameCategory : "",
                CreationTime = p.CreationTime,
                LastModificationTime = p.LastModificationTime
            }).ToList();

            // Trả về kết quả phân trang
            return new PagedResultDto<ProductDto>(count, result);
        }



        [AbpAuthorize(PermissionNames.Pages_Products_Edit)]
        public async Task Update(UpdateProductDto input)
        {
            var product = await _productRepository.FirstOrDefaultAsync((int)input.Id);

            product.Name = input.Name.Trim();
            product.Price = input.Price;
            product.Discount = input.Discount;
            product.CategoryId = input.CategoryId;


            if (input.ImageUrl != null)
            {
                product.ImageUrl = await SaveImageAsync(input.ImageUrl);
            }
            else
            {
                product.ImageUrl = input.ExistingImageUrl; // Giữ ảnh cũ nếu không có ảnh mới
            }

            await _productRepository.UpdateAsync(product);

        }
        [AbpAuthorize(PermissionNames.Pages_Products_Delete)]
        public async Task Delete(int id)
        {

            await _productRepository.DeleteAsync(id);
        }


        private async Task<string> SaveImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }
            // Đường dẫn thư mục lưu ảnh: wwwroot/uploads/products
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/products");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            // Tạo tên file duy nhất
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);
            // Lưu file ảnh vào thư mục
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            // Lưu đường dẫn tương đối vào database
            return $"/uploads/products/{fileName}"; // Trả về đường dẫn để lưu vào database
        }

        [AbpAuthorize]
        public async Task<ProductDto> GetProducts(int id)
        {
            var product = await _productRepository.GetAllIncluding(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return null;
            }

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Discount = product.Discount,
                CategoryId = product.CategoryId ?? 0,
                NameCategory = product.Category?.NameCategory ?? "",
                CreationTime = product.CreationTime,
                LastModificationTime = product.LastModificationTime
            };
        }
    }
}
