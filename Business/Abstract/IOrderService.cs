using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IOrderService
    {
        public ResultDTO<OrderDTO> CreateOrder(OrderDTO orderDTO);
       // public List<OrderDTO> GetOrderListByCampaignId(int campaignId);

    }
}
