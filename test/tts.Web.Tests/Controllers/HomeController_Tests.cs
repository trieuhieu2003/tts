using System.Threading.Tasks;
using tts.Models.TokenAuth;
using tts.Web.Controllers;
using Shouldly;
using Xunit;

namespace tts.Web.Tests.Controllers
{
    public class HomeController_Tests: ttsWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}