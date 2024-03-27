using Kentico.PageBuilder.Web.Mvc;
using Kentico.Xperience.Admin.Base.FormAnnotations;

namespace Kentico.Xperience.Disqus.Widgets;

/// <summary>
/// The configurable properties for the Disqus widget.
/// </summary>
public class DisqusWidgetProperties : IWidgetProperties
{
    /// <summary>
    /// The CSS class(es) added to the Disqus widget's containing DIV.
    /// </summary>
    [TextInputComponent(Label = "Css classes", ExplanationText = "Enter any number of CSS classes to apply to the Disqus thread, e.g. 'comments blue'")]
    public string? CssClass { get; set; } = "disqus-thread";


    /// <summary>
    /// An unique string identifying the current page. If empty, it will be generated based on the page's WebPageItemID.
    /// </summary>
    [TextInputComponent(Label = "Disqus page identifier", ExplanationText = "An unique string identifying the current page. If empty, it will be generated based on the page's WebPageItemID.")]
    public string? PageIdentifier { get; set; }


    /// <summary>
    /// A custom title for the created Disqus thread.
    /// </summary>
    [TextInputComponent(Label = "Disqus title", ExplanationText = "A custom title for the created Disqus thread.")]
    public string? Title { get; set; }


    /// <summary>
    /// Option to use disqus comment counts.
    /// </summary>
    [CheckBoxComponent(Label = "Display disqus comment count", ExplanationText = "Choose whether you want to add Disqus comment counts. You will need to add '#disqus_thread' to your article URLs. See Disqus documentation for this.")]
    public bool DisplayCommentCount { get; set; }
}
