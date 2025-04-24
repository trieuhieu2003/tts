using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using tts.Controllers;
using tts.Products;
using tts.Web.Models;
using tts.Products.Dto;
using System.Linq;

namespace tts.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : ttsControllerBase
    {
        private readonly IProductAppService _productAppService;
        //private readonly IOrderAppService _orderAppService;

        public HomeController(
            IProductAppService productAppService
            )
        {
            _productAppService = productAppService;
            ;
        }

        public async Task<ActionResult> Index()
        {
            var input = new PagedProductDto
            {
                MaxResultCount = 5
            };
            var products = await _productAppService.GetProductPaged(input);
            return View(products.Items);
        }

        public async Task<ActionResult> Viewdetails(int id)
        {
            var product = await _productAppService.GetProducts(id);
            return View(product);
        }

        public async Task<ActionResult> Order(int id, int quantity = 1)
        {
            var product = await _productAppService.GetProducts(id);
            return View(product);
        }

        public ActionResult Cart()
        {
            return View();
        }

        //    [HttpPost]
        //    public async Task<ActionResult> CreateOrder(CreateOrderDto input)
        //    {
        //        try
        //        {
        //            var order = await _orderAppService.CreateOrder(input);
        //            return Json(new { success = true, orderId = order.Id });
        //        }
        //        catch (System.Exception ex)
        //        {
        //            return Json(new { success = false, message = ex.Message });
        //        }
        //    }
        //}
    }
}
