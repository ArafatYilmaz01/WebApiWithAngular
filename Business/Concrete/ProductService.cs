using Business.Abstract;
using Common.DTOs;
using Common.Helpers;
using Common.Helpers.AutoMapper;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.DataAccess;

namespace Business.Concrete
{
    public class ProductService : IProductService
    {
        private IAutoMapperService _mapper;
        public ProductService( IAutoMapperService mapper)
        {
            _mapper = mapper;
        }
        public ResultDTO<ProductDTO> CreateProduct(ProductDTO productDTO)
        {
            ResultDTO<ProductDTO> result = new ResultDTO<ProductDTO>();
            try
            {
                var data = _mapper.Mapper.Map<Product>(productDTO);
                Context.Products.Add(data);
                result.IsSucessed = true;
                result.ServiceMessage=$"Product is created; Product Code : {productDTO.ProductCode} , Price : {productDTO.UnitPrice}, Stock : {productDTO.UnitsInStock}";           
            }
            catch
            {
                result.IsSucessed = false;
                result.ServiceMessage = ServiceMessageHelper.GetExceptionMessage(ErrorType.NotFound, DataType.Product);
            }

            return result;
        }

        public ResultDTO<ProductDTO> GetProductByCode(string productCode)
        {
            ResultDTO<ProductDTO> result = new ResultDTO<ProductDTO>();

            try
            {
                var data = Context.Products.Find(x => x.ProductCode == productCode);

                if (data == null)
                {
                    result.ServiceMessage = ServiceMessageHelper.GetExceptionMessage(ErrorType.NotFound, DataType.Product);
                    return result;
                }

                result.IsSucessed = true;
                result.ServiceMessage= $"Product {data.ProductCode} info; price {data.UnitPrice}, stock {data.UnitsInStock}";
            }
            catch
            {
                result.ServiceMessage = ServiceMessageHelper.GetExceptionMessage(ErrorType.NotFound, DataType.Product);
            }

            return result;
        }
    }
}
