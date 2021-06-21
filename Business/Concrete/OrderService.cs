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
    public class OrderService : IOrderService
    {
        private IAutoMapperService _mapper;
        public OrderService(IAutoMapperService mapper)
        {
            _mapper = mapper;
        }
        public ResultDTO<OrderDTO> CreateOrder(OrderDTO orderDTO)
        {
            ResultDTO<OrderDTO> result = new ResultDTO<OrderDTO>();

            try
            {
                var product = Context.Products.Find(x => x.ProductCode == orderDTO.ProductCode);

                //Sipariş geçilen ilgili ürün var mı?

                if (product == null)
                {
                    result.ServiceMessage = ServiceMessageHelper.GetExceptionMessage(ErrorType.NotFound, DataType.Product);
                }
                var campaign = Context.Campaigns.Find(x => x.ProductId == product.ProductId && x.EndDate < DateTime.Now);
                // Sipariş geçilmek istenilen ürün ile ilgili aktif bir kampanya var mı?
                if (campaign == null)
                {
                    //Kampanya yoksa ürünün fiyatını al

                    orderDTO.UnitPrice = product.UnitPrice;
                    orderDTO.TotalPrice = product.UnitPrice * orderDTO.Quantity;
                }
                else // Kampanya varsa siparişe kampanya fiyatını uygula
                {
                    orderDTO.UnitPrice = campaign.Price;
                    orderDTO.TotalPrice = campaign.Price * orderDTO.Quantity;
                    orderDTO.Discount = campaign.DiscountPercent;
                    orderDTO.CampaignId = campaign.CampaignId;
                }

                orderDTO.ProductId = product.ProductId;

                var data = _mapper.Mapper.Map<Order>(orderDTO);
                Context.Orders.Add(data);

                //Siparişi geçilen ürünün stok bilgisini güncelle

                product.UnitsInStock = product.UnitsInStock - orderDTO.Quantity;
                var updatedProduct = Context.Products.Find(x => x.ProductId == product.ProductId);
                updatedProduct = product;
                result.IsSucessed = true;
                result.ServiceMessage = $"Order is created; Product Code : {orderDTO.ProductCode} , Quantity : {orderDTO.Quantity}";
            }
            catch (Exception)
            {
                result.ServiceMessage = ServiceMessageHelper.GetExceptionMessage(ErrorType.NotFound, DataType.Product);
            }

            return result;
        }
    }
}

