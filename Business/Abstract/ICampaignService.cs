using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICampaignService
    {
        ResultDTO<CampaignDTO> GetCampaignByName(string CampaignName);
        ResultDTO<CampaignDTO> CreateCampaign(CampaignDTO Campaign);
        bool EndCampaignsByEndDate(string CampaignId);
    }
}
