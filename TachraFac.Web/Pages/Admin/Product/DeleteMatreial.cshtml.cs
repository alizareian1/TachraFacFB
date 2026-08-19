using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TachraFac.Core.Services.Interfaces;
using TachraFac.Datalayer.Entities.Product;

namespace TachraFac.Web.Pages.Admin.Product
{
    public class DeleteMatreialModel : PageModel
    {
        private IProductService _productService;
        public DeleteMatreialModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public RawMaterial rawMaterial { get; set; }

        public void OnGet(int id)
        {
            rawMaterial = _productService.GetRawMaterialById(id);
        }

        public IActionResult OnPost(int id)
        {
            _productService.DeleteMatreialById(id);
            return RedirectToPage("Index");
        }
    }
}
