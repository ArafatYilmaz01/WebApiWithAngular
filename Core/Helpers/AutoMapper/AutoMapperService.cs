using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Helpers.AutoMapper
{
   public class AutoMapperService:IAutoMapperService
    {
        public IMapper Mapper
        {
            get { return ObjectMapper.Mapper; }
        }
    }
}
