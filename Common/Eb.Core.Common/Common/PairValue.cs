namespace EmergencyButton.Core.Common
{
    public class PairValue<TFirst, TSecond>
    {
        public PairValue(TFirst first, TSecond second)
        {
            First = first;
            Second = second;
        }

        public TFirst First { get; set; }
        public TSecond Second { get; set; }
    }
}