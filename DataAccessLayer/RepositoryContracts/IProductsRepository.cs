using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.DataAccessLayer.RepositoryContracts
{
    public  interface IProductsRepository
    {
        /// <summary>
        /// Retrieves all products asynchronously 
        /// </summary>
        /// <returns>Return all product from the table</returns>
        Task<IEnumerable<Product>> GetProducts();
        /// <summary>
        /// Retrieves all products based on the specified condition asynchronously
        /// </summary>
        /// <param name="conditionExpression">The condition for filter product</param>
        /// <returns>Returning a collection matching products </returns>
       Task<IEnumerable<Product?>> GetProductsByCondition(Expression<Func<Product?, bool>> conditionExpression);
        Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> conditonExpression);
        /// <summary>
        /// Add new product into product table asynchronously
        /// </summary>
        /// <param name="product">Product to be added</param>
        /// <returns>Returns the added product object or null if unsuccessful</returns>
        Task<Product?> AddProduct(Product product);

        /// <summary>
        /// Update product asynchronously
        /// </summary>
        /// <param name="product">Product to be updated</param>
        /// <returns>Returns the updated product object or null if unsuccessful</returns>
        Task<Product?> UpdateProduct(Product product);
        /// <summary>
        /// Deletes the product async
        /// </summary>
        /// <param name="productID"> the product id to be deleted</param>
        /// <returns> True if successful, false otherwise.</returns>
        Task<bool> DeleteProduct(Guid productID);
    }
}
