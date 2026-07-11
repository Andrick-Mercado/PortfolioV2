using MudBlazor;

namespace PersonalPortfolio.Library.Domain.Mappers;

public static class IconMapper
{
    public static string GetIcon(Icon icon)
    {
        return icon switch
        {
            Icon.AccountCircle => Icons.Material.Filled.AccountCircle,
            Icon.Lightbulb => Icons.Material.Filled.Lightbulb,
            Icon.Construction => Icons.Material.Filled.Construction,
            Icon.AutoGraph => Icons.Material.Filled.AutoGraph,
            Icon.InsertDriveFile => Icons.Material.Filled.InsertDriveFile,
            Icon.Work => Icons.Material.Filled.Work,
            Icon.School => Icons.Material.Filled.School,
            Icon.Code => Icons.Material.Filled.Code,
            Icon.SportsEsports => Icons.Material.Filled.SportsEsports,
            Icon.Cloud => Icons.Material.Filled.Cloud,
            Icon.Storage => Icons.Material.Filled.Storage,
            Icon.Groups => Icons.Material.Filled.Groups,
            Icon.Terminal => Icons.Material.Filled.Terminal,
            Icon.Web => Icons.Material.Filled.Web,
            Icon.RocketLaunch => Icons.Material.Filled.RocketLaunch,
            _ => Icons.Material.Filled.DeviceUnknown
        };
    }
}