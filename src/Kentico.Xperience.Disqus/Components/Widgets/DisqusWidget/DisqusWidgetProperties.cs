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
    [TextInputComponent(Label = "{$DisqusResources.Admin_Disqus_Properties_CssClass_Label$}", ExplanationText = "{$DisqusResources.Admin_Disqus_Properties_CssClass_ExplanationText$}")]
    public string? CssClass { get; set; } = "disqus-thread";


    /// <summary>
    /// An unique string identifying the current page. If empty, it will be generated based on the page's WebPageItemID.
    /// </summary>
    [TextInputComponent(Label = "{$DisqusResources.Admin_Disqus_Properties_PageIdentifier_Label$}", ExplanationText = "{$DisqusResources.Admin_Disqus_Properties_PageIdentifier_ExplanationText$}")]
    public string? PageIdentifier { get; set; }


    /// <summary>
    /// A custom title for the created Disqus thread.
    /// </summary>
    [TextInputComponent(Label = "{$DisqusResources.Admin_Disqus_Properties_Title_Label$}", ExplanationText = "{$DisqusResources.Admin_Disqus_Properties_Title_ExplanationText$}")]
    public string? Title { get; set; }


    /// <summary>
    /// Option to use disqus comment counts.
    /// </summary>
    [CheckBoxComponent(Label = "{$DisqusResources.Admin_Disqus_Properties_DisplayCommentCount_Label$}", ExplanationText = "{$DisqusResources.Admin_Disqus_Properties_DisplayCommentCount_ExplanationText$}")]
    public bool DisplayCommentCount { get; set; }
}
