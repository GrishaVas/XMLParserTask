using AutoMapper;
using XMLTask.DataProcessor.Infrastructure.Entities;

namespace XMLTask.DataProcessor.MapperProfiles
{
    public class CombinedStatusProfile : Profile
    {
        public CombinedStatusProfile()
        {
            CreateMap<Services.Models.XML.CombinedStatus, CombinedStatus>()
                .IncludeAllDerived();

            CreateMap<Services.Models.XML.CombinedOvenStatus, CombinedOvenStatus>();
            CreateMap<Services.Models.XML.CombinedPumpStatus, CombinedPumpStatus>();
            CreateMap<Services.Models.XML.CombinedSamplerStatus, CombinedSamplerStatus>();
        }
    }
}
