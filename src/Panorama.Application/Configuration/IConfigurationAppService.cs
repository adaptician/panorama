using System.Threading.Tasks;
using Panorama.Configuration.Dto;

namespace Panorama.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
