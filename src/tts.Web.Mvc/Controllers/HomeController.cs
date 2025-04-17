using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using tts.Controllers;

namespace tts.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : ttsControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
