﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProductController : AuthorizedController
{
    private readonly IProductService _productService;

    public ProductController(IUserService userService, IProductService productService) : base(userService)
    {
        _productService = productService;
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<ProductDTO>>> GetById([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _productService.GetProduct(id)) :
            this.ErrorMessageResult<ProductDTO>(currentUser.Error);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<ProductDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _productService.GetProducts(pagination)) :
            this.ErrorMessageResult<PagedResponse<ProductDTO>>(currentUser.Error);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] ProductAddDTO product)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _productService.AddProduct(product, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] ProductUpdateDTO product)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _productService.UpdateProduct(product, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _productService.DeleteProduct(id)) :
            this.ErrorMessageResult(currentUser.Error);
    }
}
