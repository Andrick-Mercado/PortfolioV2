using Microsoft.AspNetCore.Components;
using PersonalPortfolio.Library.Domain;
using PersonalPortfolio.Library.Infrastructure;

namespace PersonalPortfolio.Library.Application.Components;

public partial class ProjectDetailPage
{
    private Card _project;
    private OtherPages _parentPage;
    private bool _hasLoaded;

    [Parameter] public string PageEndpoint { get; set; } = default!;
    [Parameter] public string Slug { get; set; } = default!;
    [Inject] private IWebsiteRepo WebsiteRepo { get; set; } = default!;

    protected override async Task OnParametersSetAsync()
    {
        var websiteData = await WebsiteRepo.GetWebsiteData();
        _parentPage = websiteData?.OtherPages
            .FirstOrDefault(x => x.Endpoint == PageEndpoint && x.PageFormat == CardType.Project);
        _project = _parentPage?.Cards
            .FirstOrDefault(c => string.Equals(c.Slug, Slug, StringComparison.OrdinalIgnoreCase));

        _hasLoaded = true;
        StateHasChanged();
    }

    // Normalize common external URLs (e.g. convert YouTube watch links to embed links)
    public string GetEmbedUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return url;

        var trimmed = url.Trim();

        // If already an embed url, return as-is
        if (trimmed.Contains("/embed/", StringComparison.OrdinalIgnoreCase))
            return trimmed;

        // YouTube short link: https://youtu.be/{id}
        if (trimmed.Contains("youtu.be/", StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                var parts = trimmed.Split('/');
                var last = parts.LastOrDefault();
                if (string.IsNullOrEmpty(last) is false)
                {
                    var id = last.Split('?')[0];
                    return $"https://www.youtube.com/embed/{id}";
                }
            }
            catch
            {
                // fall through
            }
        }

        // Standard YouTube link: https://www.youtube.com/watch?v={id}
        if (trimmed.Contains("watch?v=", StringComparison.OrdinalIgnoreCase))
        {
            var idx = trimmed.IndexOf("watch?v=", StringComparison.OrdinalIgnoreCase);
            var idPart = trimmed.Substring(idx + "watch?v=".Length);
            var amp = idPart.IndexOf('&');
            var id = amp >= 0 ? idPart.Substring(0, amp) : idPart;
            if (!string.IsNullOrEmpty(id))
                return $"https://www.youtube.com/embed/{id}";
        }

        // Default: return original url
        return trimmed;
    }
}