namespace OperationsApi.Dtos
{
    public record ProfileDto
    {
        public double P1Bar { get; set; }

        public double P2Bar { get; set; }

        public int Cycles { get; set; }

        public double TempC { get; set; }
    }
}
