using ShopOnline.MODELS.Dtos;

namespace ShopOnline.WEB.Services.Contract
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetItems();
        Task<ProductDto> GetItem(int id);
    }
}
