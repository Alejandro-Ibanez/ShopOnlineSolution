using ShopOnline.MODELS.Dtos;
namespace ShopOnline.WEB.Services.Contract
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<CartItemDto>> GetItems(int userId);
        Task<CartItemDto> AddItem(CartItemToAddDto cartItemToAddDto);
    }
}
