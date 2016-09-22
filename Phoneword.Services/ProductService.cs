using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoneword.Entities;
using Phoneword.DataModel.UnitOfWork;
using AutoMapper;
using Phoneword.DataModel;
using System.Transactions;
using Phoneword.Services.ErrorHelper;
using System.Net;

namespace Phoneword.Services
{
    public class ProductService : IProductService
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public ProductService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ProductEntity CreateProduct(ProductEntity productEntity)
        {
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<ProductEntity, Product>());
                var product = Mapper.Map<ProductEntity, Product>(productEntity);
                product.UniqueId = Guid.NewGuid();
                product.ProductId = GetMaxProductId() + 1;
                using (var scope = new TransactionScope())
                {
                    _unitOfWork.ProductRepository.Insert(product);
                    _unitOfWork.Save();
                    scope.Complete();
                }
                return GetProductById(product.ProductId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteProduct(int productId)
        {
            var success = false;
            try
            {
                if (productId <= 0)
                    throw new ApiBizException(1001, "Product id is invalid", HttpStatusCode.BadRequest);

                using (var scope = new TransactionScope())
                {
                    var product = _unitOfWork.ProductRepository.Get(p => p.ProductId == productId);
                    if (product != null)
                    {
                        _unitOfWork.ProductRepository.Delete(product);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApiException { ErrorCode = 999, ErrorDescription = ex.Message, HttpStatus = HttpStatusCode.InternalServerError };
            }                        
            return success;
        }

        public IEnumerable<ProductEntity> GetAllProducts()
        {
            try
            {
                var products = _unitOfWork.ProductRepository.GetAll().ToList();
                if (products.Any())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductEntity>());
                    var productsModel = Mapper.Map<List<Product>, List<ProductEntity>>(products);
                    return productsModel;
                }
                throw new ApiDataException(1001, "Products not found", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                throw new ApiException { ErrorCode = 999, ErrorDescription = ex.Message, HttpStatus = HttpStatusCode.InternalServerError };
            }
        }

        public ProductEntity GetProductById(int productId)
        {
            try
            {
                var product = _unitOfWork.ProductRepository.Get(p => p.ProductId == productId);
                if (product != null)
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductEntity>());
                    var productModel = Mapper.Map<Product, ProductEntity>(product);
                    return productModel;
                }
                throw new ApiDataException(1001, string.Format("No product found for id {0}", productId), HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                throw new ApiException { ErrorCode = 999, ErrorDescription = ex.Message, HttpStatus = HttpStatusCode.InternalServerError };
            }
        }

        public ProductEntity UpdateProduct(int productId, ProductEntity productEntity)
        {
            try
            {
                if (productId <= 0)
                    throw new ApiBizException(1001, "Product id is invalid", HttpStatusCode.BadRequest);
                else if (productEntity == null)
                    throw new ApiBizException(1001, "Product data is invalid", HttpStatusCode.BadRequest);

                var product = _unitOfWork.ProductRepository.Get(p => p.ProductId == productId);
                if (product != null)
                {
                    using (var scope = new TransactionScope())
                    {
                        product.ProductName = productEntity.ProductName;
                        _unitOfWork.ProductRepository.Update(product);
                        _unitOfWork.Save();
                        scope.Complete();
                    }
                }                
                return GetProductById(productId);
            }
            catch (Exception ex)
            {
                throw new ApiException { ErrorCode = 999, ErrorDescription = ex.Message, HttpStatus = HttpStatusCode.InternalServerError };
            }
        }

        private int GetMaxProductId()
        {
            var products = _unitOfWork.ProductRepository.GetAll().ToList();
            if (products == null || !products.Any())
                return 0;

            int maxId = products.Max(p => p.ProductId);
            return maxId;
        }
    }
}
