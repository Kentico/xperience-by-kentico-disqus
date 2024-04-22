using System;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using CMS.Core;
using CMS.DataEngine;
using CMS.Helpers;

using Kentico.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Xperience.Disqus.Widgets;

[assembly: CMS.AssemblyDiscoverable]
[assembly: RegisterWidget(DisqusWidget.IDENTIFIER,
    typeof(DisqusWidget),
    "Disqus comments",
    typeof(DisqusWidgetProperties),
    Description = "Enables commenting, ratings, and reactions on Xperience pages.",
    IconClass = "icon-bubbles")]

namespace Kentico.Xperience.Disqus.Widgets;

/// <summary>
/// Class which constructs the <see cref="DisqusWidgetViewModel"/> and renders the widget.
/// </summary>
public class DisqusWidget : ViewComponent
{
    /// <summary>
    /// The internal identifier of the Disqus widget.
    /// </summary>
    public const string IDENTIFIER = "Kentico.Xperience.DisqusComponent";

    private readonly IConfiguration configuration;
    private readonly IEventLogService eventLogService;
    private readonly IHttpContextAccessor accessor;

    /// <summary>
    /// Initializes a new instance of the <see cref="DisqusWidget"/> class.
    /// </summary>
    public DisqusWidget(
        IConfiguration configuration,
        IEventLogService eventLogService,
        IHttpContextAccessor accessor
    )
    {
        this.eventLogService = eventLogService;
        this.configuration = configuration;
        this.accessor = accessor;
    }


    /// <summary>
    /// Populates the <see cref="DisqusWidgetViewModel"/> and returns the appropriate view.
    /// </summary>
    /// <param name="widgetProperties">User populated properties from the page builder or view.</param>
    public IViewComponentResult Invoke(ComponentViewModel<DisqusWidgetProperties> widgetProperties)
    {
        if (widgetProperties == null)
        {
            LogWidgetLoadError("Widget properties were not provided.");
            return Content(string.Empty);
        }

        string pageUrl = HttpContext.Request.GetDisplayUrl();
        pageUrl = URLHelper.RemoveQuery(pageUrl);
        string identifier;

        if (widgetProperties.Page == null)
        {
            if (string.IsNullOrEmpty(widgetProperties.Properties.PageIdentifier))
            {
                LogWidgetLoadError($"{nameof(DisqusWidgetProperties.PageIdentifier)} is null or empty and {nameof(ComponentViewModel.Page)} is null. The {nameof(DisqusWidgetViewModel.Identifier)} can not be set. Please set the identifier or use a {nameof(ComponentViewModel.Page)}.");
                return Content(string.Empty);
            }

            identifier = widgetProperties.Properties.PageIdentifier;
        }
        else
        {
            identifier = widgetProperties.Page.WebPageItemID.ToString();
        }

        var options = configuration.GetSection(DisqusOptions.SECTION_NAME).Get<DisqusOptions>();

        if (string.IsNullOrEmpty(options?.SiteShortName))
        {
            LogWidgetLoadError($"{nameof(DisqusOptions.SiteShortName)} is null or empty. Please set the siteShortName option under the xperience.disqus section in your appsettings.json file.");
            return Content(string.Empty);
        }

        bool inKenticoAdmin = accessor.HttpContext.Kentico().PageBuilder().EditMode;

        return View("~/Components/Widgets/DisqusWidget/_DisqusWidget.cshtml", new DisqusWidgetViewModel()
        {
            Identifier = identifier,
            Site = options.SiteShortName,
            Url = pageUrl,
            Title = widgetProperties.Properties.Title ?? string.Empty,
            CssClass = widgetProperties.Properties.CssClass ?? string.Empty,
            DisplayCommentCount = widgetProperties.Properties.DisplayCommentCount,
            InKenticoAdmin = inKenticoAdmin
        });
    }

    private void LogWidgetLoadError(string description) =>
        eventLogService.LogError(
            nameof(DisqusWidget),
            nameof(Invoke),
            description,
            new LoggingPolicy(TimeSpan.FromMinutes(1))
        );
}
