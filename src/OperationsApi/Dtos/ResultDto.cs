namespace OperationsApi.Dtos
{
    public record ResultDto
    {
        public double PeakPressureBar { get; set; }

        public double PressureDropBar { get; set; }

        public bool LeakDetected { get; set; }
    }
}
