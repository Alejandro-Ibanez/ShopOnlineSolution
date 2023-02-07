using Microsoft.EntityFrameworkCore;
using ShopOnline.API.Data;
using ShopOnline.API.Entities;
using ShopOnline.API.Repositories.Contracts;
using ShopOnline.MODELS.Dtos;

namespace ShopOnline.API.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ShopOnlineDbContext _shopOnlineDbContext;

        public ShoppingCartRepository(ShopOnlineDbContext shopOnlineDbContext)
        {
            _shopOnlineDbContext = shopOnlineDbContext;
        }

        private async Task<bool> CartItemExist(int cartId, int productId)
        {
            return await _shopOnlineDbContext.CartItems.AnyAsync(c => c.CartId == cartId && c.ProductId == productId);
        }
        public async Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto)
        {
            if (!await CartItemExist(cartItemToAddDto.CartId, cartItemToAddDto.ProductId))
            {
                var item = await (from product in _shopOnlineDbContext.Products
                                  where product.Id == cartItemToAddDto.ProductId
                                  select new CartItem
                                  {
                                      CartId = cartItemToAddDto.CartId,
                                      ProductId = product.Id,
                                      Qty = cartItemToAddDto.Qty
                                  }).SingleOrDefaultAsync();
                if (item != null)
                {
                    var result = await _shopOnlineDbContext.CartItems.AddAsync(item);
                    await _shopOnlineDbContext.SaveChangesAsync();
                    return result.Entity;
                }
            }


            return null;

        }

        public async Task<CartItem> DeleteItem(int id)
        {
            var item = await _shopOnlineDbContext.CartItems.FindAsync(id);
            if (item !=null)
            {
                _shopOnlineDbContext.CartItems.Remove(item);
                await _shopOnlineDbContext.SaveChangesAsync();
            }
            return item;
        }

        public async Task<CartItem> GetItem(int id)
        {
            return await (from cart in _shopOnlineDbContext.Carts
                          join cartItem in _shopOnlineDbContext.CartItems
                          on cart.Id equals cartItem.CartId
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Qty = cartItem.Qty,
                              CartId = cartItem.CartId
                          }).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CartItem>> GetItems(int userId)
        {

            return await (from cart in _shopOnlineDbContext.Carts
                          join cartItem in _shopOnlineDbContext.CartItems
                          on cart.Id equals cartItem.CartId
                          where cart.UserId == userId
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Qty = cartItem.Qty,
                              CartId = cartItem.CartId
                          }).ToListAsync();
        }

        public async Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            var item = await _shopOnlineDbContext.CartItems.FindAsync(id);

            if (item != null)
            {
                item.Qty = cartItemQtyUpdateDto.Qty;
                await _shopOnlineDbContext.SaveChangesAsync();
                return item;
            }

            return null;
        }
    }
}

