using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Common.DTOs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScenariosController : ControllerBase
    {
        private IOrderService _os;
         private IProductService _ps;
         private ICampaignService _cs;
        private ITimerService _ts;
        public ScenariosController(IOrderService os, IProductService ps, ICampaignService cs, ITimerService ts)
        {
            _os = os;
            _ps = ps;
            _cs = cs;
            _ts = ts;
        }
        [HttpGet(template: "getAll")]
        public IQueryable GetAll()
        {
            return GetAll();
        }
        [HttpGet("{selectedScenario}")]
        public List<string> RunTxtCommands(string selectedScenario)
        {
            List<List<string>> commandResults=ReadFile(selectedScenario+".txt");
            List<string> resultOfMethods = new List<string>();
            var productDTO = new ProductDTO();
            var orderDTO = new OrderDTO();
            var campaignDTO = new CampaignDTO();
            foreach (List<string> item in commandResults)
            {

                switch (item[0])
                {
                    case "create_product":
                        productDTO.ProductCode = item[1];
                        productDTO.UnitPrice = int.Parse(item[2]);
                        productDTO.UnitsInStock = int.Parse(item[3]);
                        resultOfMethods.Add(_ps.CreateProduct(productDTO).ServiceMessage);
                        break;
                    case "get_product_info":
                        resultOfMethods.Add(_ps.GetProductByCode(item[1]).ServiceMessage);
                        break;
                    case "create_order":
                        orderDTO.ProductCode = item[1];
                        orderDTO.Quantity = int.Parse(item[2]);
                        resultOfMethods.Add(_os.CreateOrder(orderDTO).ServiceMessage);
                        break;
                    case "create_campaign":
                        campaignDTO.CampaignName = item[1];
                        campaignDTO.ProductCode = item[2];
                        campaignDTO.Duration = int.Parse(item[3]);
                        campaignDTO.Limit = int.Parse(item[4]);
                        campaignDTO.TargetSalesCount = int.Parse(item[5]);
                        resultOfMethods.Add(_cs.CreateCampaign(campaignDTO).ServiceMessage);
                        break;
                    case "get_campaign_info":
                        campaignDTO.CampaignName = item[1];
                        resultOfMethods.Add(_cs.GetCampaignByName(campaignDTO.CampaignName).ServiceMessage);
                        break;
                    case "increase_time":
                        int duration = int.Parse(item[1]);
                        resultOfMethods.Add(_ts.IncreaseTime(duration));
                        break;
                    default:
                        break;
                }

            }
            return resultOfMethods;
        }
        
        public List<List<string>> ReadFile(string selectedScenario)
        {
           
            List<List<string>> commandList = new List<List<string>>();
            try
            {
                string[] lines = System.IO.File.ReadAllLines(selectedScenario);

                foreach (string line in lines)
                {
                    var commandArray = line.Split(null).Where(x => x != "").ToList();
                    commandList.Add(commandArray);
                }
            }
            catch
            {
                //throw e;

            }

            return commandList;
        }
    }
}
