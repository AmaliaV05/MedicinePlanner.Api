using AutoMapper;
using MedicinePlanner.BusinessLogic.DTOs;
using MedicinePlanner.Data.Models;

namespace MedicinePlanner.Api.Helpers.MappingProfile
{
    public class MappingStock : Profile
    {
        public MappingStock()
        {
            CreateMap<LoadingStock, LoadingStockDTO>();
            CreateMap<UnloadingStock, UnloadingStockDTO>();
        }
    }
}
