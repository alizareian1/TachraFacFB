using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TachraFac.Core.Services.Interfaces;
using TachraFac.Datalayer.Entities.Product;

namespace TachraFac.Web.Pages.Admin.Product
{
    public class AddMatreialModel : PageModel
    {
        private IProductService _productService;
        public AddMatreialModel(IProductService productService)
        {
            _productService = productService;   
        }

        [BindProperty]
        public RawMaterial rawMaterial { get; set; }

        public void OnGet()
        {
            ViewData["ProductMatreial"] = _productService.GetAllMatreial();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ViewData["ProductMatreial"] = _productService.GetAllMatreial();
                return Page();
            }
            _productService.AddRawMaterial(rawMaterial);
            return RedirectToPage("AddMatreial");
        }
    }
}
