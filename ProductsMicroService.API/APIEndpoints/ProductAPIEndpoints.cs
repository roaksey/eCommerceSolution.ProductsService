
using BussinessLogicLayer.DTO;
using BussinessLogicLayer.ServiceContracts;
using FluentValidation;
using FluentValidation.Results;

namespace ProductsMicroService.API.APIEndpoints
{
    public static class ProductAPIEndpoints
    {
        public static IEndpointRouteBuilder MapProductAPIEndpoints(this IEndpointRouteBuilder app)
        {
            //Get /api/products
            app.MapGet("/api/products", async (IProductsService productsService) =>
            {
                List<ProductResponse> products =  await productsService.GetProducts();
                return Results.Ok(products);
            });

            //Get /api/products/search/product-id
            app.MapGet("/api/products/search/product-id/{ProductID:guid}", async (IProductsService productsService, Guid ProductID) =>
            {
                ProductResponse? product = await productsService.GetProductByConditon(temp => temp.ProductID == ProductID);
                if (product == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(product);
            });

            //Get /api/products/search/product-name
            app.MapGet("/api/products/search/product-name/{SearchString}",async (IProductsService productService,string SearchString) =>
            {
                List<ProductResponse?> productsByProductName = await productService.GetProductsByCondition(temp =>
                temp.ProductName != null && temp.ProductName.Contains(SearchString,
                    StringComparison.OrdinalIgnoreCase));
                List<ProductResponse?> productsByCategory = await productService.GetProductsByCondition(temp =>
                 temp.Category != null && temp.Category.Contains(SearchString,StringComparison.OrdinalIgnoreCase));
                var products = productsByProductName.Union(productsByCategory);
                return Results.Ok(productsByProductName);
            });

            //Post /api/products
            app.MapPost("/api/products", async (IProductsService productsService
                ,IValidator<ProductAddRequest> productAddRequestValidator
                , ProductAddRequest productAddRequest) =>
            {
                //Validate the ProductAddRequest object using FluentValidation
                ValidationResult validationResult =  await productAddRequestValidator.ValidateAsync(productAddRequest);
                // check validation result
                if (!validationResult.IsValid)
                {
                    Dictionary<string, string[]> errors = validationResult.Errors
                     .GroupBy(temp=> temp.PropertyName)
                     .ToDictionary(grp => grp.Key,grp => grp.Select(temp => temp.ErrorMessage).ToArray());
                    return Results.ValidationProblem(errors);
                }
                ProductResponse? addedProductResponse = await productsService.AddProduct(productAddRequest);
                if (addedProductResponse != null)
                {
                    return Results.Created($"/api/products/search/product-id/{addedProductResponse.ProductID}",addedProductResponse);
                }
                else
                {
                    return Results.Problem("Error while adding product");
                }
               
            });

            //Put /api/products
            app.MapPut("api/products", async (IProductsService productsService
                ,IValidator<ProductUpdateRequest> productUpdateRequestValidator
                , ProductUpdateRequest productUpdateRequest) =>
            {
                //Validate the ProductUpdateRequest object using FluentValidation
                ValidationResult validationResult = await productUpdateRequestValidator.ValidateAsync(productUpdateRequest);
                // check validation result
                if (!validationResult.IsValid)
                {
                    Dictionary<string, string[]> errors = validationResult.Errors
                            .GroupBy(temp => temp.PropertyName)
                            .ToDictionary(grp=> grp.Key,grp => grp.Select(temp => temp.ErrorMessage).ToArray());
                }
                ProductResponse? updatedProductResponse = await productsService.UpdateProduct(productUpdateRequest);
                if (updatedProductResponse != null)
                {
                    return Results.Ok(updatedProductResponse);
                }
                else
                {
                    return Results.Problem("Product not found for update");
                }
            });

            //Delete /api/products/{productId}
            app.MapDelete("/api/products/{productId:guid}", async (IProductsService productService, Guid productId) =>
            {
                bool isDeleted = await productService.DeleteProduct(productId);
                if (!isDeleted)
                {
                    return Results.NotFound("Product not found for deletion");
                }
                return Results.NoContent();
            });
            return app;
        }
    }
}
