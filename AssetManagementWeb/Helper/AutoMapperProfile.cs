using AssetManagementWeb.Models.DTO;
using AutoMapper;
using Domain;
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
            CreateMap<List<object>, UserLicense>();
            CreateMap<List<object>, UserAssets>();
            CreateMap<List<object>, AssetsLicense>();
        }
    }
}
