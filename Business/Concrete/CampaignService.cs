using Business.Abstract;
using Common.DTOs;
using Common.Helpers;
using Common.Helpers.AutoMapper;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.DataAccess;

namespace Business.Concrete
{
    public class CampaignService : ICampaignService
    {
        private IAutoMapperService _mapper;
        public CampaignService(IAutoMapperService mapper)
        {
            _mapper = mapper;
        }

        public ResultDTO<CampaignDTO> CreateCampaign(CampaignDTO campaignDTO)
        {
            ResultDTO< CampaignDTO> result = new ResultDTO<CampaignDTO>();
            try
            {
                var product = Context.Products.Find(x => x.ProductCode == campaignDTO.ProductCode);

                if (product == null)
                {
                    result.ServiceMessage = ServiceMessageHelper.GetExceptionMessage(ErrorType.NotFound, DataType.Product);
                    return result;
                }

                campaignDTO.ProductId = product.ProductId;

                //Ürün fiyatı üzerine kampanya indirim yüzdesi uygulanarak ürünün kampanya süresince satış fiyatı belirleniyor.
                product.UnitPrice = product.UnitPrice - (product.UnitPrice * campaignDTO.Limit / 100);

                var data = _mapper.Mapper.Map<Campaign>(campaignDTO);
                data.BeginDate = Context.timer;
                data.EndDate = Context.timer.AddHours(campaignDTO.Duration);
                data.IsActive = true;
                Context.Campaigns.Add(data);
                result.IsSucessed = true;
                result.ServiceMessage = $"Campaign is created; name {data.CampaignName}, product {data.ProductCode}, begin date {data.BeginDate},end date {data.EndDate} limit {data.Limit}, target sales count {data.TargetSalesCount} ";
            }
            catch
            {
                result.ServiceMessage = ServiceMessageHelper.GetExceptionMessage(ErrorType.UnexpectedError);
            }

            return result;
        }

        
        public ResultDTO<CampaignDTO> GetCampaignByName(string CampaignName)
        {
            var result = new ResultDTO<CampaignDTO>();

            try
            {
                var data = Context.Campaigns.Find(x => x.CampaignName == CampaignName);

                if (data == null)
                {
                    result.ServiceMessage = ServiceMessageHelper.GetExceptionMessage(ErrorType.NotFound, DataType.Campaign);
                }

                result.Data = _mapper.Mapper.Map<CampaignDTO>(data);
                result.Data.IsActive= EndCampaignsByEndDate(result.Data.CampaignName);
                result.IsSucessed = true;
                result.ServiceMessage= $"Campaign {result.Data.CampaignName} info; Status {result.Data.Status}, Target Sales {result.Data.TargetSalesCount}, Total Sales Could not calculated,Turnover : , Average Item Price {result.Data.Price} ";
            }
            catch
            {
                result.ServiceMessage = ServiceMessageHelper.GetExceptionMessage(ErrorType.UnexpectedError);
            }

            return result;
        }
        /// <summary>
        /// Süresi bitmiş kampanyaları pasife çeker
        /// </summary>
        /// <returns></returns>

        public bool EndCampaignsByEndDate(string CampaignName)
        {
            var now = DateTime.Now;
            var result = true;
            try
            {
                var campaigns = Context.Campaigns.Where(x => x.CampaignName == CampaignName && x.EndDate < Context.timer);

                if(campaigns.Any())
                {
                    result = false;
                }
                    
            }
            catch
            {

            }
            return result;
        }
    }
}
