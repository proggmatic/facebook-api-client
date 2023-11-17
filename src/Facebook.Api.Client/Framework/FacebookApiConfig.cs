namespace Facebook;

public class FacebookApiConfig
{
    public string? ApiVersion { get; set; }

    public string AppId { get; set; } = null!;

    public string AppSecret { get; set; } = null!;

    public string AppRedirectUrl { get; set; } = null!;
}