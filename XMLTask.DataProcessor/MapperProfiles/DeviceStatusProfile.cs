using AutoMapper;
using XMLTask.DataProcessor.Infrastructure.Entities;

namespace XMLTask.DataProcessor.MapperProfiles
{
    public class DeviceStatusProfile : Profile
    {
        public DeviceStatusProfile()
        {
            CreateMap<Services.Models.XML.DeviceStatus, DeviceStatus>();
        }
    }
}
