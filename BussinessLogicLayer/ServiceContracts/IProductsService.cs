using BussinessLogicLayer.DTO;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.ServiceContracts;

public interface IProductsService
{
    /// <summary>
    /// Retrives the list of products from the product repository
    /// </summary>
    /// <returns>List<ProductResponse></Product></returns>
    Task<List<ProductResponse>> GetProducts();
    /// <summary>
    /// Retrives list of products matching with given condition
    /// </summary>
    /// <param name="conditionExpression">Expression that requires condition to check</param>
    /// <returns>Return matching products</returns>
    Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression);
    /// <summary>
    /// Return single product that matches with given condition 
    /// </summary>
    /// <param name="conditoinExpression"></param>
    /// <returns>Return Product</returns>
    Task<ProductResponse?> GetProductByConditon(Expression<Func<Product, bool>> conditoinExpression);
    /// <summary>
    /// Add(inserts) product into the table using products repository
    /// </summary>
    /// <param name="productAddRequest">Product to insert</param>
    /// <returns> Product after inserting or null if unsuccessful</returns>
    Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest);
    /// <summary>
    /// Product to be update 
    /// </summary>
    /// <param name="productUpdateRequest"></param>
    /// <returns>Updated Product</returns>
    Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest);
    /// <summary>
    /// Deletes existing product based on given product id 
    /// </summary>
    /// <param name="productID">Product Id to search and delete</param>
    /// <returns> True is successfull else false </returns>
    Task<bool> DeleteProduct(Guid productID);


}

