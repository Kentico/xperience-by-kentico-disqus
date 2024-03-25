using System;
using CMS.Core;
using CMS.DataEngine;
using CMS.Helpers;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Xperience.Disqus.Components;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

[assembly: CMS.AssemblyDiscoverable]
[assembly: RegisterWidget(DisqusComponentWidget.IDENTIFIER,
    typeof(DisqusComponentWidget),
    "Disqus comments",
    typeof(DisqusComponentWidgetProperties),
    Description = "Enables commenting, ratings, and reactions on Xperience pages.",
    IconClass = "icon-bubbles")]

namespace Kentico.Xperience.Disqus.Components;

/// <summary>
/// Class which constructs the <see cref="DisqusComponentWidgetViewModel"/> and renders the widget.
/// </summary>
public class DisqusComponentWidget : ViewComponent
{
    /// <summary>
    /// The internal identifier of the Disqus widget.
    /// </summary>
    public const string IDENTIFIER = "Kentico.Xperience.DisqusComponent";

    private readonly IConfiguration configuration;
    private readonly IEventLogService eventLogService;

    /// <summary>
    /// Initializes a new instance of the <see cref="DisqusComponentWidget"/> class.
    /// </summary>
    public DisqusComponentWidget(
        IConfiguration configuration,
        IEventLogService eventLogService
    )
    {
        this.eventLogService = eventLogService;
        this.configuration = configuration;
    }

    /// <summary>
    /// Populates the <see cref="DisqusComponentWidgetViewModel"/> and returns the appropriate view.
    /// </summary>
    /// <param name="widgetProperties">User populated properties from the page builder or view.</param>
    public IViewComponentResult Invoke(ComponentViewModel<DisqusComponentWidgetProperties> widgetProperties)
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
                LogWidgetLoadError($"{nameof(DisqusComponentWidgetProperties.PageIdentifier)} is null or empty and {nameof(ComponentViewModel.Page)} is null. The {nameof(DisqusComponentWidgetViewModel.Identifier)} can not be set. Please set the identifier or use a {nameof(ComponentViewModel.Page)}.");
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

        return View("~/Components/_DisqusComponentWidget.cshtml", new DisqusComponentWidgetViewModel()
        {
            Identifier = identifier,
            Site = options.SiteShortName,
            Url = pageUrl,
            Title = widgetProperties.Properties.Title ?? "",
            CssClass = widgetProperties.Properties.CssClass ?? "",
            DisplayCommentCount = widgetProperties.Properties.DisplayCommentCount
        });
    }

    private void LogWidgetLoadError(string description) =>
        eventLogService.LogError(
            nameof(DisqusComponentWidget),
            nameof(Invoke),
            description,
            new LoggingPolicy(TimeSpan.FromMinutes(1))
        );
}
