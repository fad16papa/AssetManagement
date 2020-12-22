using AssetManagementWeb.Models.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagementWeb.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<object, AssetsDTO>();
            CreateMap<List<object>, UserAssetsDTO>();
        }
    }
}
