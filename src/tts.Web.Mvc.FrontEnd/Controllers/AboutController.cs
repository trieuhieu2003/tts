using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using tts.Controllers;

namespace tts.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AboutController : ttsControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
