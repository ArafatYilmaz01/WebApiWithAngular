using Common.DTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.DataAccess;

namespace Business.Abstract
{
    public interface IProductService
    {
        //IEnumerable<ProductDTO> GetProducts();
        ResultDTO<ProductDTO> CreateProduct(ProductDTO productDTO);
        //ProductDTO GetProductById(int productId);
        ResultDTO<ProductDTO> GetProductByCode(string productCode);
    }
}
