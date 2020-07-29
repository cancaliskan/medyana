using AutoMapper;

using Medyana.Application.Models;
using Medyana.Domain.Entities;

namespace Medyana.Application.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Clinic, ClinicModel>();
            CreateMap<ClinicModel, Clinic>();

            CreateMap<Equipment, EquipmentModel>();
            CreateMap<EquipmentModel, Equipment>();
        }
    }
}