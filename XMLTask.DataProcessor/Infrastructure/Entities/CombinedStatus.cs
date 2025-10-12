using System.ComponentModel.DataAnnotations;
using XMLTask.Services.Models.Enums;

namespace XMLTask.DataProcessor.Infrastructure.Entities
{
    public class CombinedStatus
    {
        public Guid Id { get; set; }
        [MaxLength(1024)]
        public string ModuleState { get; set; }
        public bool IsBusy { get; set; }
        public bool IsReady { get; set; }
        public bool IsError { get; set; }
        public bool KeyLock { get; set; }

        public CombinedStatusType CombinedStatusType { get; set; }

        public Guid RapidControlStatusId { get; set; }
    }
}
