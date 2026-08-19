using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TachraFac.Core.DTOs.Product;
using TachraFac.Core.Services.Interfaces;
using TachraFac.Datalayer.Context;
using TachraFac.Datalayer.Entities.Product;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace TachraFac.Core.Services
{
    public class ProductService:IProductService
    {
        private TachraContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductService(TachraContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public void AddRawMaterial(RawMaterial rawMaterial)
        {
            
            _context.RawMaterials.Add(rawMaterial);
            _context.SaveChangesAsync();            
        }

        public async Task<int> CreateProductAsync(ProductCreateViewModel model)
        {
            if (model.PhotoPaths != null && model.PhotoPaths.Count > 4)
            {
                throw new InvalidOperationException("حداکثر ۴ عکس مجاز است.");
            }
            // ۱. ساخت شیء محصول
            var product = new Product
            {
                ProductTitle = model.ProductTitle,
                Discription = model.Discription,
                ExpirationDate = model.ExpirationDate,
                Weight = model.Weight,
                price = model.Price,
                LikeCount = 0,
                Photos = new List<Photo>(),
                ProductMaterials = new List<ProductMaterial>()
            };

            // ۲. مدیریت آپلود عکس‌ها
            //if (model.PhotoPaths != null && model.PhotoPaths.Count > 0)
            //{
            //    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/products");
            //    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

            //    foreach (var file in model.PhotoPaths)
            //    {
            //        // ساخت نام منحصر به فرد برای فایل
            //        string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            //        using (var fileStream = new FileStream(filePath, FileMode.Create))
            //        {
            //            await file.CopyToAsync(fileStream);
            //        }

            //        // ذخیره مسیر نسبی در دیتابیس (برای نمایش راحت‌تر در HTML)
            //        product.Photos.Add(new Photo
            //        {
            //            PhotoProduct = "/uploads/products/" + uniqueFileName
            //        });
            //    }
            //}

            if (model.PhotoPaths != null && model.PhotoPaths.Count > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                const long maxFileSize = 5 * 1024 * 1024; // 5MB

                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/products");
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                foreach (var file in model.PhotoPaths)
                {
                    // اعتبارسنجی پسوند فایل
                    var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                    if (!allowedExtensions.Contains(extension))
                    {
                        throw new InvalidOperationException($"فرمت فایل «{file.FileName}» مجاز نیست. فقط JPG، PNG و WEBP قابل قبول است.");
                    }

                    // اعتبارسنجی حجم فایل
                    if (file.Length > maxFileSize)
                    {
                        throw new InvalidOperationException($"حجم فایل «{file.FileName}» بیشتر از حد مجاز (۵ مگابایت) است.");
                    }

                    // ساخت نام منحصر به فرد برای فایل
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    // ذخیره مسیر نسبی در دیتابیس (برای نمایش راحت‌تر در HTML)
                    product.Photos.Add(new Photo
                    {
                        PhotoProduct = "/uploads/products/" + uniqueFileName
                    });
                }
            }

            if (model.MaterialIds != null && model.MaterialIds.Any())
            {
                var validMaterialIds = await _context.RawMaterials
                    .Where(m => model.MaterialIds.Contains(m.MaterialId))
                    .Select(m => m.MaterialId)
                    .ToListAsync();

                product.ProductMaterials = validMaterialIds.Select(mid => new ProductMaterial
                {
                    MaterialId = mid
                }).ToList();
            }



            //  افزودن و ذخیره
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product.ProductId;

        }

        public void DeleteMatreialById(int id)
        {
            var matreial = GetRawMaterialById(id);
            if(matreial == null)
            {
                return;
            }
            _context.RawMaterials.Remove(matreial);
            _context.SaveChanges();
            return;

        }

        

        public List<RawMaterial> GetAllMatreial()
        {
            return _context.RawMaterials.ToList();
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(p => p.Photos)
                .Include(p => p.ProductMaterials)
                    .ThenInclude(pm => pm.rawMaterial)
                .ToListAsync();
        }

        public int GetMatreialById(int id)
        {
            return _context.RawMaterials.FirstOrDefault(m => m.MaterialId == id).MaterialId;
        }

        public RawMaterial GetRawMaterialById(int id)
        {
            return _context.RawMaterials.Find(id);
        }

        public void UpdateMatreial(RawMaterial rawMaterial)
        {
            var ExistMatreial = _context.RawMaterials.FirstOrDefault(m => m.MaterialId == rawMaterial.MaterialId);
            if (ExistMatreial != null) 
            {
                ExistMatreial.MaterialTitle = rawMaterial.MaterialTitle;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("ماده اولیه مورد نظر یافت نشد.");
            }
        }
    }
}
