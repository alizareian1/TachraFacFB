using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TachraFac.Core.Services.Interfaces;
using TachraFac.Datalayer.Entities.Product;

namespace TachraFac.Web.Pages.Admin.Product
{
    public class EditMatreialModel : PageModel
    {
        private IProductService _productService;
        public EditMatreialModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public RawMaterial rawMaterial { get; set; }

        public void OnGet(int id)
        {
            rawMaterial = _productService.GetRawMaterialById(id);
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                _productService.UpdateMatreial(rawMaterial);
                TempData["Success"] = "???? ????? ?? ?????? ?????? ??.";
                return RedirectToPage("AddMatreial");
            }
            catch (Exception ex)
            {
                // ?. ?????? ??? ?? ???? ???? ???? ?? ???????
                ModelState.AddModelError(string.Empty, "????? ?? ?????????? ?? ???: " + ex.Message);
                return Page();
            }
        }
    }
}
