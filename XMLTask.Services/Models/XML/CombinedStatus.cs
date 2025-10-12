using System.Text.Json.Serialization;
using System.Xml.Serialization;
using XMLTask.Services.Models.Enums;

namespace XMLTask.Services.Models.XML
{
    [XmlInclude(typeof(CombinedSamplerStatus))]
    [XmlInclude(typeof(CombinedOvenStatus))]
    [XmlInclude(typeof(CombinedPumpStatus))]
    [JsonDerivedType(typeof(CombinedStatus), typeDiscriminator: "Base")]
    [JsonDerivedType(typeof(CombinedSamplerStatus), typeDiscriminator: "Sampler")]
    [JsonDerivedType(typeof(CombinedOvenStatus), typeDiscriminator: "Oven")]
    [JsonDerivedType(typeof(CombinedPumpStatus), typeDiscriminator: "Pump")]
    public class CombinedStatus
    {
        [XmlElement(ElementName = "ModuleState")]
        public ModuleState ModuleState { get; set; }

        [XmlElement(ElementName = "IsBusy")]
        public bool IsBusy { get; set; }

        [XmlElement(ElementName = "IsReady")]
        public bool IsReady { get; set; }

        [XmlElement(ElementName = "IsError")]
        public bool IsError { get; set; }

        [XmlElement(ElementName = "KeyLock")]
        public bool KeyLock { get; set; }
    }
}
