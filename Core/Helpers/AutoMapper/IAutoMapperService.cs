using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Helpers.AutoMapper
{
    public interface IAutoMapperService
    {
        IMapper Mapper { get; }
    }
}
