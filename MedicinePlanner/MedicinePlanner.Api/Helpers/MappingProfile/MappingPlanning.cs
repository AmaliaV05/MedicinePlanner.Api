using AutoMapper;
using MedicinePlanner.BusinessLogic.DTOs;
using MedicinePlanner.Data.Models;

namespace MedicinePlanner.Api.Helpers.MappingProfile
{
    public class MappingPlanning : Profile
    {
        public MappingPlanning()
        {
            CreateMap<Planning, PlanningDTO>().ReverseMap();
            CreateMap<DailyPlanning, DailyPlanningDTO>().ReverseMap();
        }
    }
}
