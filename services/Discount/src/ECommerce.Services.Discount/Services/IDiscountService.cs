using ECommerce.Services.Discount.Dtos;
using ECommerce.Shared.Dtos;

namespace ECommerce.Services.Discount.Services;
public interface IDiscountService
{
    Task<Response<List<DiscountDto>>> GetAll();
    Task<Response<DiscountDto>> GetById(int Id);
    Task<Response<NoContent>> Create(DiscountDto discountDto);
    Task<Response<NoContent>> Update(DiscountDto discountDto);
    Task<Response<NoContent>> Delete(int id);
    Task<Response<DiscountDto>> GetByCodeAndUserId(string code, string userId);
}