using System.Text.Json;
using ECommerce.Shared.Dtos;
using ECommerce.Web.Helpers;
using ECommerce.Web.Models;
using ECommerce.Web.Models.Catalog;
using ECommerce.Web.Services.Interfaces;

namespace ECommerce.Web.Services;
public class CatalogService : ICatalogService
{
    private readonly HttpClient _httpClient;
    private readonly IPhotoStockService _photoStockService;
    private readonly PhotoHelper _photoHelper;

    public CatalogService(HttpClient httpClient, IPhotoStockService photoStockService, PhotoHelper photoHelper)
    {
        _httpClient = httpClient;
        _photoStockService = photoStockService;
        _photoHelper = photoHelper;
    }

    #region Product

    //Queries

    public async Task<List<ProductViewModel>> GetAllProductsAsync()
    {
        var response = await _httpClient.GetAsync("product/GetAll");

        if (response.IsSuccessStatusCode is false)
        {
            return null;
        }

        var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<ProductViewModel>>>();
        
        responseSuccess.Data.ForEach(data => {
            data.Image = _photoHelper.GetPhotoUrl(data.Image);
        });

        return responseSuccess.Data;
    }
    public async Task<ProductViewModel> GetProductByIdAsync(string id)
    {
        var response = await _httpClient.GetAsync($"Product/GetById/{id}");

        if (response.IsSuccessStatusCode is false)
        {
            return null;
        }

        var responseSuccess = await response.Content.ReadFromJsonAsync<Response<ProductViewModel>>();
        return responseSuccess.Data;
    }
    public async Task<List<ProductViewModel>> GetAllProductByStoreIdAsync(int storeId)
    {
        var response = await _httpClient.GetAsync($"product/GetByStoreId/{storeId}");

        if (response.IsSuccessStatusCode is false)
        {
            return null;
        }

        var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<ProductViewModel>>>();
        return responseSuccess.Data;
    }


    //Commands

    public async Task<bool> CreateProductAsync(ProductCreateInput productCreateInput)
    {
        var photoServiceResult = await _photoStockService.UploadPhoto(productCreateInput.PhotoFormFile);
        if (photoServiceResult is not null)
        {
            productCreateInput.Image = photoServiceResult.Url;
        }


        var response = await _httpClient.PostAsJsonAsync("product/create", productCreateInput);
        return response.IsSuccessStatusCode;
    }
    public async Task<bool> UpdateProductAsync(ProductUpdateInput productUpdateInput)
    {
        var response = await _httpClient.PutAsJsonAsync("product/update", productUpdateInput);

        return response.IsSuccessStatusCode;
    }
    public async Task<bool> DeleteProductAsync(string productId)
    {
        var response = await _httpClient.DeleteAsync($"product/delete/{productId}");

        return response.IsSuccessStatusCode;
    }
    #endregion

    #region Category
    public async Task<List<CategoryViewModel>> GetAllCategoriesAsync()
    {
        var response = await _httpClient.GetAsync("category/getall");

        if (response.IsSuccessStatusCode is false)
        {
            return null;
        }

        var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();
        return responseSuccess.Data;
    }


    public async Task<CategoryViewModel> GetCategoryByIdAsync(string id)
    {
        var response = await _httpClient.GetAsync($"category/GetById/{id}");

        if (response.IsSuccessStatusCode is false)
        {
            return null;
        }

        var responseSuccess = await response.Content.ReadFromJsonAsync<Response<CategoryViewModel>>();
        return responseSuccess.Data;
    }

    public async Task<bool> CreateCategoryAsync(CategoryCreateInput categoryCreateInput)
    {
        var response = await _httpClient.PostAsJsonAsync("category/create", categoryCreateInput);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateCategoryAsync(CategoryUpdateInput categoryUpdateInput)
    {
        var response = await _httpClient.PutAsJsonAsync($"category/update", categoryUpdateInput);

        return response.IsSuccessStatusCode;
    }


    public async Task<bool> DeleteCategoryAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"category/delete/{id}");

        return response.IsSuccessStatusCode;
    }

    #endregion



    #region ProductVariation
    public async Task<List<ProductVariatoinViewModel>> GetAllProductVariationsAsync()
    {
        var response = await _httpClient.GetAsync("productvariation/getall");

        if (response.IsSuccessStatusCode is false)
        {
            return null;
        }

        var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<ProductVariatoinViewModel>>>();
        return responseSuccess.Data;
    }

    public async Task<ProductVariatoinViewModel> GetProductVariationByIdAsync(string id)
    {
        var response = await _httpClient.GetAsync($"ProductVariation/GetById/{id}");

        if (response.IsSuccessStatusCode is false)
        {
            return null;
        }

        var responseSuccess = await response.Content.ReadFromJsonAsync<Response<ProductVariatoinViewModel>>();
        return responseSuccess.Data;
    }

    public async Task<bool> CreateProductVariationAsync(ProductVariationCreateInput productVariationCreateInput)
    {
        var response = await _httpClient.PostAsJsonAsync("ProductVariation/create", productVariationCreateInput);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateProductVariationAsync(ProductVariationUpdateInput productVariationUpdateInput)
    {
        var response = await _httpClient.PutAsJsonAsync("ProductVariation/update", productVariationUpdateInput); 

        return response.IsSuccessStatusCode;
    }


    public async Task<bool> DeleteProductVariationAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"ProductVariation/delete/{id}");

        return response.IsSuccessStatusCode;
    }

    #endregion
}