using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using TachraFac.Core.Services.Interfaces;
using TachraFac.Datalayer.Entities.Product;

namespace TachraFac.Web.Pages.Admin.Product
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;
        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public List<TachraFac.Datalayer.Entities.Product.Product> Products { get; set; } = new();

        public async Task OnGetAsync()
        {
            Products = await _productService.GetAllProductsAsync();
        }
    }
}