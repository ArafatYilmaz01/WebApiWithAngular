using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class Order : IEntity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int? CampaignId { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
