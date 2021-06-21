using Business.Abstract;
using Common.Helpers.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.DataAccess;

namespace Business.Concrete
{
   public class TimerService : ITimerService
    {
        private CampaignService campaignService;

        private IAutoMapperService _mapper;
        public TimerService(IAutoMapperService mapper)
        {
            _mapper = mapper;
            campaignService = new CampaignService(_mapper);
        }

        public string IncreaseTime(int time)
        {
            Context.timer=Context.timer.AddHours(time);
            return "Time is " + Context.timer.ToShortTimeString();
        }
        
    }
}
