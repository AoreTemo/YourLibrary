using Microsoft.Build.Framework;

namespace YourLibrary.Helpers;

public class CloudinarySettings
{
    public const string CloudinarySettingsSectionName = "CloudinarySettings";
    [Required]
    public string CloudName { get; set;  }
    [Required]
    public string ApiKey { get; set; }
    [Required]
    public string ApiSecret { get; set; }
}