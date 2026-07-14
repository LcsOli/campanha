namespace Campaign.Shared.DataBaseContext.Entities.Period
{
    public class Period
    {
        public int Id { get; private set; }
        public short Year { get; private set; }
        public string Month { get; private set; } = default!;
        public DateTime InitIn { get; private set; }
        public DateTime EndIn { get; private set; }

        public Period(short year, string month, DateTime initIn, DateTime endIn)
        {
            Year = year;
            Month = month;
            EndIn = endIn;
            InitIn = initIn;
        }
    }
}
