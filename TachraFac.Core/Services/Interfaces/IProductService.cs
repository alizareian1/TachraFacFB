using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TachraFac.Datalayer.Entities.Product;
using TachraFac.Core.DTOs.Product;


namespace TachraFac.Core.Services.Interfaces
{
    public interface IProductService
    {
        #region Matreial
        void AddRawMaterial(RawMaterial rawMaterial);
        List<RawMaterial> GetAllMatreial();
        RawMaterial GetRawMaterialById(int id);
        void UpdateMatreial(RawMaterial rawMaterial);
        int GetMatreialById(int id);
        void DeleteMatreialById(int id);
        #endregion


        #region Product
        Task<List<Product>> GetAllProductsAsync();
        Task<int> CreateProductAsync(ProductCreateViewModel model);
        #endregion
    }
}
