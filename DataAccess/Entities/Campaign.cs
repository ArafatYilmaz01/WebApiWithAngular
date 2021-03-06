using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class Campaign : IEntity
    {
        public int CampaignId { get; set; }
        public string CampaignName { get; set; }
        public string ProductCode { get; set; }
        public int ProductId { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPercent { get; set; }
        public int TargetSalesCount { get; set; }
        public decimal Limit { get; set; }
        public bool IsActive { get; set; }
    }
}
