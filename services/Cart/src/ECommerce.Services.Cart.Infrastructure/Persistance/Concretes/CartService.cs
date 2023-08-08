
using System.Text.Json;

namespace ECommerce.Services.Cart.Infrastructure.Persistance.Concretes;
public class CartService : ICartService
{
    private readonly IRedisService _redisService;

    public CartService(IRedisService redisService)
    {
        _redisService = redisService;
    }


    public async Task<Response<CartDto>> GetCart(string userId)
    {
        var cartExists = await _redisService.GetDb().StringGetAsync(userId);

        if (String.IsNullOrEmpty(cartExists))
        {
            return Response<CartDto>.Failure("Cart not found", 404);
        }
        
        return Response<CartDto>.Success(JsonSerializer.Deserialize<CartDto>(cartExists), 200);
    }

    public async Task<Response<bool>> SaveOrUpdate(CartDto cartDto)
    {
        var status = await _redisService.GetDb().StringSetAsync(cartDto.UserId, JsonSerializer.Serialize(cartDto));

        return status ? Response<bool>.Success(204) : Response<bool>.Failure("Cart could not be saved or updated", 500);
    }
    public async Task<Response<bool>> Delete(string userId)
    {
        var status = await _redisService.GetDb().KeyDeleteAsync(userId);
        return status ? Response<bool>.Success(204) : Response<bool>.Failure("Cart not found", 404);

    }
}