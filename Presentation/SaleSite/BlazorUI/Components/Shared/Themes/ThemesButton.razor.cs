using BlazorUI.Services;
using BlazorUI.Services.Layout;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorUI.Components.Shared.Themes;

public partial class ThemesButton
{
    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
    
    [Inject] private LayoutService LayoutService { get; set; } = default!;

}