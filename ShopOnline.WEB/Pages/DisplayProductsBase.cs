using Microsoft.AspNetCore.Components;
using ShopOnline.MODELS.Dtos;

namespace ShopOnline.WEB.Pages
{
    public class DisplayProductsBase : ComponentBase
    {
        [Parameter]
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
