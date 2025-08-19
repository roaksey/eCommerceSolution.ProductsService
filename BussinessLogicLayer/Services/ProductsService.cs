

using AutoMapper;
using BussinessLogicLayer.DTO;
using BussinessLogicLayer.ServiceContracts;
using DataAccessLayer.Entities;
using DataAccessLayer.Repository;
using FluentValidation;
using FluentValidation.Results;
using System.Linq.Expressions;

namespace BussinessLogicLayer.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IValidator<ProductAddRequest> _productAddRequestValidator;
        private readonly IValidator<ProductUpdateRequest> _productUpdateRequestValidator;
        private readonly IMapper _mapper;
        private readonly ProductRepository _productRepository;
        public ProductsService(IValidator<ProductAddRequest> productAddRequestValidator,
            IValidator<ProductUpdateRequest> productUpdateRequestValidator,
                IMapper mapper, ProductRepository productRepository
            )
        {
            _productAddRequestValidator = productAddRequestValidator;
            _productUpdateRequestValidator = productUpdateRequestValidator;
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public async Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest)
        {
           if(productAddRequest == null)
            {
                throw new ArgumentNullException(nameof(productAddRequest), "Product add request cannot be null");
            }
            // Validate the product add request
            var validationResult = await _productAddRequestValidator.ValidateAsync(productAddRequest);
            if (!validationResult.IsValid)
            {
               string errors = string.Join(",",validationResult.Errors.Select(temp => temp.ErrorMessage));           
                throw new ArgumentException(errors);
            }
            //Attempt to add the product
            Product productInput = _mapper.Map<Product>(productAddRequest);
            Product? addedProduct = await _productRepository.AddProduct(productInput);
            if (addedProduct == null)
            {
               return null;
            }
            ProductResponse addedProductResponse=_mapper.Map<ProductResponse>(addedProduct);
            return addedProductResponse;
        }

        public async Task<bool> DeleteProduct(Guid productID)
        {
            Product? existingProduct = await _productRepository.GetProductByCondition(temp => temp.ProductID == productID);
            if (existingProduct == null)
            {
                return false;
            }
            //Attempt to delete the product
            bool isDeleted = await _productRepository.DeleteProduct(productID);
            return isDeleted;
        }

        public async Task<ProductResponse?> GetProductByConditon(Expression<Func<Product, bool>> conditoinExpression)
        {
            Product? product = await _productRepository.GetProductByCondition(conditoinExpression);
            if (product == null)
            {
                return null;
            }
            ProductResponse productResponse = _mapper.Map<ProductResponse>(product);
            return productResponse;
        }

        public async Task<List<ProductResponse>> GetProducts()
        {
            IEnumerable<Product> products = await _productRepository.GetProducts();
            List<ProductResponse> productResponses = _mapper.Map<List<ProductResponse>>(products);
            return productResponses.ToList();
        }

        public async Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression)
        {
            IEnumerable<Product?> products = await _productRepository.GetProductsByCondition(conditionExpression);
            List<ProductResponse?> productResponses = _mapper.Map<List<ProductResponse?>>(products);
            return productResponses.ToList();
        }

        public async Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest)
        {
           Product? existingProduct =  await _productRepository.GetProductByCondition(temp => temp.ProductID == productUpdateRequest.ProductID);
            if (existingProduct == null)
            {
                throw new ArgumentException("Product not found for the given ID");
            }
            // Validate the product update request
            ValidationResult validationResult = await _productUpdateRequestValidator.ValidateAsync(productUpdateRequest);
            if (!validationResult.IsValid)
            {
                string errors = string.Join(",", validationResult.Errors.Select(temp => temp.ErrorMessage));
                throw new ArgumentException(errors);
            }
            Product product = _mapper.Map<Product>(productUpdateRequest);
            Product? updatedProduct = await _productRepository.UpdateProduct(product);
            if (updatedProduct == null)
            {
                return null;
            }
            ProductResponse updatedProductResponse = _mapper.Map<ProductResponse>(updatedProduct);
            return updatedProductResponse;
        }
    }
}
