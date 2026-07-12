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
}
