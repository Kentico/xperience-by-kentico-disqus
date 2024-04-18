namespace Kentico.Xperience.Disqus.Widgets;

/// <summary>
/// The properties to be set when rendering the widget on a view.
/// </summary>
public class DisqusWidgetViewModel
{
    /// <summary>
    /// CSS classes added to the containing DIV.
    /// </summary>
    public string CssClass
    {
        get;
        set;
    } = string.Empty;

    public bool InKenticoAdmin
    {
        get;
        set;
    }

    /// <summary>
    /// The unique identifier of the current page.
    /// </summary>
    public string Identifier
    {
        get;
        set;
    } = string.Empty;


    /// <summary>
    /// The absolute URL of the current page.
    /// </summary>
    public string Url
    {
        get;
        set;
    } = string.Empty;


    /// <summary>
    /// The name of the current page.
    /// </summary>
    public string Title
    {
        get;
        set;
    } = string.Empty;


    /// <summary>
    /// The short name of the Disqus site.
    /// </summary>
    public string Site
    {
        get;
        set;
    } = string.Empty;

    public bool DisplayCommentCount { get; set; } = false;
}
