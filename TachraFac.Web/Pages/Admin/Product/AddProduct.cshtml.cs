using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TachraFac.Core.DTOs.Product;
using TachraFac.Core.Services.Interfaces;
using TachraFac.Datalayer.Entities.Product;

namespace TachraFac.Web.Pages.Admin.Product
{
    public class AddProductModel : PageModel
    {
        private readonly IProductService _productService;
        public AddProductModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public ProductCreateViewModel productVM { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            try
            {
                await _productService.CreateProductAsync(productVM);
                return RedirectToPage("./Index"); // ??? ?? ?????? ?? ???? ??????
            }
           
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "???: " + ex.Message);
                return Page();
            }
        }
    }
}
