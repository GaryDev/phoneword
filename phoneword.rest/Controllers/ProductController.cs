using AutoMapper;
using phoneword.rest.ActionFilters;
using phoneword.rest.Filters;
using Phoneword.Entities;
using Phoneword.Services;
using Phoneword.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace phoneword.rest.Controllers
{
    [AuthorizationRequired]
    [RoutePrefix("api/v1/product")]
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize product service instance
        /// </summary>
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        #endregion

        // GET api/product
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage Get()
        {
            try
            {
                var products = _productService.GetAllProducts();
                var productEntities = products as List<ProductEntity> ?? products.ToList();
                Mapper.Initialize(cfg => cfg.CreateMap<ProductEntity, ProductViewModel>());

                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<ProductEntity>, List<ProductViewModel>>(productEntities));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new ApiError { ErrorCode = "500", ErrorDescription = ex.Message });
            }
        }

        // GET api/product/5
        [HttpGet]
        [Route("{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var productEntity = _productService.GetProductById(id);

                Mapper.Initialize(cfg => cfg.CreateMap<ProductEntity, ProductViewModel>());
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<ProductEntity, ProductViewModel>(productEntity));                
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new ApiError { ErrorCode = "500", ErrorDescription = ex.Message });
            }
        }

        // POST api/product
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Post([FromBody] ProductViewModel product)
        {
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<ProductViewModel, ProductEntity>());
                var productEntity = Mapper.Map<ProductViewModel, ProductEntity>(product);
                var newProduct = _productService.CreateProduct(productEntity);

                Mapper.Initialize(cfg => cfg.CreateMap<ProductEntity, ProductViewModel>());
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<ProductEntity, ProductViewModel>(newProduct));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new ApiError { ErrorCode = "500", ErrorDescription = ex.Message });
            }
        }

        // PUT api/product/5
        [HttpPut]
        [Route("modify/{id:int}")]
        public HttpResponseMessage Put(int id, [FromBody]ProductViewModel product)
        {
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<ProductViewModel, ProductEntity>());
                var productEntity = Mapper.Map<ProductViewModel, ProductEntity>(product);
                var updatedProduct = _productService.UpdateProduct(id, productEntity);

                Mapper.Initialize(cfg => cfg.CreateMap<ProductEntity, ProductViewModel>());
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<ProductEntity, ProductViewModel>(updatedProduct));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new ApiError { ErrorCode = "500", ErrorDescription = ex.Message });
            }
        }

        // DELETE api/product/5
        [HttpDelete]
        [Route("delete/{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                _productService.DeleteProduct(id);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new ApiError { ErrorCode = "500", ErrorDescription = ex.Message });
            }
        }
    }
}
