namespace XMLTask.DataProcessor.Infrastructure.Entities
{
    public class CombinedOvenStatus : CombinedStatus
    {
        public bool UseTemperatureControl { get; set; }
        public bool OvenOn { get; set; }
        public double TemperatureActual { get; set; }
        public double TemperatureRoom { get; set; }
        public int MaximumTemperatureLimit { get; set; }
        public int ValvePosition { get; set; }
        public int ValveRotations { get; set; }
        public bool Buzzer { get; set; }
    }
}
