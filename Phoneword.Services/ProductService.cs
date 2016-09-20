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

namespace Phoneword.Services
{
    public class ProductService : IProductService
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public ProductService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public ProductEntity CreateProduct(ProductEntity productEntity)
        {
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<ProductEntity, Product>());
                var product = Mapper.Map<ProductEntity, Product>(productEntity);
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
                    throw new Exception("Product id is invalid");

                using (var scope = new TransactionScope())
                {
                    var product = _unitOfWork.ProductRepository.GetByID(productId);
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
                throw ex;
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
                throw new Exception("Products not found");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProductEntity GetProductById(int productId)
        {
            try
            {
                var product = _unitOfWork.ProductRepository.GetByID(productId);
                if (product != null)
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductEntity>());
                    var productModel = Mapper.Map<Product, ProductEntity>(product);
                    return productModel;
                }
                throw new Exception(string.Format("No product found for id {0}", productId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProductEntity UpdateProduct(int productId, ProductEntity productEntity)
        {
            try
            {
                if (productId <= 0)
                    throw new Exception("Product id is invalid");
                else if (productEntity == null)
                    throw new Exception("Product data is invalid");

                var product = _unitOfWork.ProductRepository.GetByID(productId);
                using (var scope = new TransactionScope())
                {
                    product.ProductName = productEntity.ProductName;
                    _unitOfWork.ProductRepository.Update(product);
                    _unitOfWork.Save();
                    scope.Complete();
                }
                return GetProductById(productId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
