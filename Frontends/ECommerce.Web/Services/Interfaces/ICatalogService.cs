using ECommerce.Web.Models.Catalog;

namespace ECommerce.Web.Services.Interfaces;
public interface ICatalogService
{
    #region Product
    Task<List<ProductViewModel>> GettAllProductsAsync();
    Task<List<ProductViewModel>> GetAllProductByStoreIdAsync(int StoreId);
    Task<ProductViewModel> GetProductByIdAsync();
    Task<bool> CreateProductAsync(ProductCreateInput productCreateInput);
    Task<bool> UpdateProductAsync(ProductUpdateInput productUpdateInput);
    Task<bool> DeleteProductAsync(string productId);
    #endregion

    #region ProductVariation
    Task<List<ProductViewModel>> GettAllProductVariationsAsync();
    Task<ProductViewModel> GetProductVariationByIdAsync(string Id);
    Task<bool> CreateProductVariationAsync(ProductVariationCreateInput productVariationCreateInput);
    Task<bool> UpdateProductVariationAsync(ProductVariationUpdateInput productVariationUpdateInput);
    Task<bool> DeleteProductVariationAsync(string Id);
    #endregion

    #region Category
    Task<List<ProductViewModel>> GettAllCategoriessAsync();
    Task<ProductViewModel> GetCategoryByIdAsync(string Id);
    Task<bool> CreateCategoryAsync(CategoryCreateInput productCreateInput);
    Task<bool> UpdateCategoryAsync(CategoryUpdateInput productUpdateInput);
    Task<bool> DeleteCategoryAsync(string Id);
    #endregion
}
