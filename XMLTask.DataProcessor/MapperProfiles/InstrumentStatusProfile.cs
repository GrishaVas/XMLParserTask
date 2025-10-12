using AutoMapper;
using XMLTask.DataProcessor.Infrastructure.Entities;

namespace XMLTask.DataProcessor.MapperProfiles
{
    public class InstrumentStatusProfile : Profile
    {
        public InstrumentStatusProfile()
        {
            CreateMap<Services.Models.XML.InstrumentStatus, InstrumentStatus>();
        }
    }
}
