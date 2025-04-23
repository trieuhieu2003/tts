using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using tts.Controllers;
using tts.Products;
using tts.Web.Models;
using tts.Products.Dto;

namespace tts.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : ttsControllerBase
    {
        private readonly IProductAppService _productAppService;

        public HomeController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        public async Task<ActionResult> Index()
        {
            var products = await _productAppService.GetProductPaged(new PagedProductDto());
            return View(products.Items);
        }

        public async Task<ActionResult> Viewdetails(int id)
        {
            var product = await _productAppService.GetProducts(id);
            return View(product);
        }

        public ActionResult PayMent()
        {
            return View();
        }
    }
}
