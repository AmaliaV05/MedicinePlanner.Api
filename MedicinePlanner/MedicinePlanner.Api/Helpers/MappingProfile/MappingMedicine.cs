using AutoMapper;
using MedicinePlanner.BusinessLogic.DTOs;
using MedicinePlanner.Data.Models;

namespace MedicinePlanner.Api.Helpers.MappingProfile
{
    public class MappingMedicine : Profile
    {
        public MappingMedicine()
        {
            CreateMap<Medicine, MedicineDTO>().ReverseMap();
        }
    }
}
