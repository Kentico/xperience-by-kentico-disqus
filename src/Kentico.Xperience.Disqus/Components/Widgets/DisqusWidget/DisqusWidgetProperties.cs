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
    [TextInputComponent(Label = "{$admin.disqus.properties.cssClass.label$}", ExplanationText = "{$admin.disqus.properties.cssClass.explanationText$}")]
    public string? CssClass { get; set; } = "disqus-thread";


    /// <summary>
    /// An unique string identifying the current page. If empty, it will be generated based on the page's WebPageItemID.
    /// </summary>
    [TextInputComponent(Label = "{$admin.disqus.properties.pageIdentifier.label$}", ExplanationText = "{$admin.disqus.properties.pageIdentifier.explanationText$}")]
    public string? PageIdentifier { get; set; }


    /// <summary>
    /// A custom title for the created Disqus thread.
    /// </summary>
    [TextInputComponent(Label = "{$admin.disqus.properties.title.label$}", ExplanationText = "{$admin.disqus.properties.title.explanationText$}")]
    public string? Title { get; set; }


    /// <summary>
    /// Option to use disqus comment counts.
    /// </summary>
    [CheckBoxComponent(Label = "{$admin.disqus.properties.displayCommentCount.label$}", ExplanationText = "{$admin.disqus.properties.displayCommentCount.explanationText$}")]
    public bool DisplayCommentCount { get; set; }
}
