using System.Threading.Tasks;
using Panorama.Models.TokenAuth;
using Panorama.Web.Controllers;
using Shouldly;
using Xunit;

namespace Panorama.Web.Tests.Controllers
{
    public class HomeController_Tests: PanoramaWebTestBase
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