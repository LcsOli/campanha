namespace Campaign.Shared.DataBaseContext.Entities.Period
{
    public class Period
    {
        public int Id { get; private set; }
        public short Year { get; private set; }
        public string Month { get; private set; } = default!;
        public DateTime Init { get; private set; }
        public DateTime End { get; private set; }

        public Period(short year, string month, DateTime init, DateTime end)
        {
            End = end;
            Year = year;
            Init = init;
            Month = month;
        }
    }
}
