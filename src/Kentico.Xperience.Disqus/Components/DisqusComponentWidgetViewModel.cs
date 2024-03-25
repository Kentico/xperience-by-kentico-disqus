namespace Kentico.Xperience.Disqus.Components;

/// <summary>
/// The properties to be set when rendering the widget on a view.
/// </summary>
public class DisqusComponentWidgetViewModel
{
    /// <summary>
    /// CSS classes added to the containing DIV.
    /// </summary>
    public string CssClass
    {
        get;
        set;
    } = "";


    /// <summary>
    /// The unique identifier of the current page.
    /// </summary>
    public string Identifier
    {
        get;
        set;
    } = "";


    /// <summary>
    /// The absolute URL of the current page.
    /// </summary>
    public string Url
    {
        get;
        set;
    } = "";


    /// <summary>
    /// The name of the current page.
    /// </summary>
    public string Title
    {
        get;
        set;
    } = "";


    /// <summary>
    /// The short name of the Disqus site.
    /// </summary>
    public string Site
    {
        get;
        set;
    } = "";

    public bool DisplayCommentCount { get; set; } = false;
}
