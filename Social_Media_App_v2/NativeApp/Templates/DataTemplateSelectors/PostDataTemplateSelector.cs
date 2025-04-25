using NativeApp.MVVM.ViewModels;

namespace NativeApp.Templates.DataTemplateSelectors;
public class PostDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate HasMediaDataTemplate { get; set; }
    public DataTemplate DoesntHaveMediaDataTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {   
        if((item as PostViewModel)?.Post.Media?.Count > 0)
        {
            return HasMediaDataTemplate;
        }

        return DoesntHaveMediaDataTemplate;
    }
}
