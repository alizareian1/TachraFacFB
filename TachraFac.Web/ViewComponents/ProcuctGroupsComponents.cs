using Microsoft.AspNetCore.Mvc;
using TachraFac.Core.Services.Interfaces;

namespace TachraFac.Web.ViewComponents
{
    public class ProcuctGroupsComponents:ViewComponent
    {
        private IProductService _productService;
        public ProcuctGroupsComponents(IProductService productService)
        {
            _productService = productService;
        }
        //public async Task<IViewComponentResult> InvokeAsync()
        //{
        //    return await Task.FromResult((IViewComponentResult)View("ProductGroup",));
        //}
    }
}
