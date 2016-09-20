using Phoneword.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoneword.Services
{
    /// <summary>
    /// Product Service Contract
    /// </summary>
    public interface IProductService
    {
        ProductEntity GetProductById(int productId);
        IEnumerable<ProductEntity> GetAllProducts();
        ProductEntity CreateProduct(ProductEntity productEntity);
        ProductEntity UpdateProduct(int productId, ProductEntity productEntity);
        bool DeleteProduct(int productId);
    }
}
