using AutoMapper;
using XMLTask.DataProcessor.Infrastructure.Entities;

namespace XMLTask.DataProcessor.MapperProfiles
{
    public class RapidControlStatusProfile : Profile
    {
        public RapidControlStatusProfile()
        {
            CreateMap<Services.Models.XML.RapidControlStatus, RapidControlStatus>();
        }
    }
}
