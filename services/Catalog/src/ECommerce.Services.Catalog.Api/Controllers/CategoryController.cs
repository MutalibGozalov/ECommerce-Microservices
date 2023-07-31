using ECommerce.Services.Catalog.Application.Categories.Commands.CreateCategory;
using ECommerce.Services.Catalog.Application.Categories.Commands.UpdateCategory;
using ECommerce.Services.Catalog.Application.Categories.Commands.DeleteCategory;
using ECommerce.Services.Catalog.Application.Categories.Queries;
using Microsoft.AspNetCore.Mvc;


namespace ECommerce.Services.Catalog.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CategoryController : CustomBaseController
{
    private ISender? _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await Mediator.Send(new GetCategoriesQuery());
        return CreateActionResultInstance(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await Mediator.Send(new GetCategoryByIdQuery(id));
        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryCommand categoryCommand)
    {
        var response = await Mediator.Send(categoryCommand);
        return CreateActionResultInstance(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateCategoryCommand updateCategoryCommand)
    {
        var response = await Mediator.Send(updateCategoryCommand);
        return CreateActionResultInstance(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(DeleteCategoryCommand deleteCategoryCommand)
    {
        var response = await Mediator.Send(deleteCategoryCommand);
        return CreateActionResultInstance(response);
    }

}