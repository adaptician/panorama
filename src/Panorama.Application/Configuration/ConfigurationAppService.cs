using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Panorama.Configuration.Dto;

namespace Panorama.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : PanoramaAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
