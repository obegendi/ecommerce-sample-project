namespace EcommerceSample.TimeSimulator
{
    public class SystemTimer
    {
        public int Hour { get; private set; }
        public int Day { get; private set; }
        public SystemTime GetTime()
        {
            var sysTime = new SystemTime
            {
                Hour = Hour,
                Day = Day,
                TimeInFormat = $"{Hour:D2}:00"
            };

            return sysTime;
        }

        public SystemTimer IncreaseTime(int hour)
        {
            Hour += hour;
            if (Hour > 23)
            {
                Hour -= 24;
                Day++;
            }

            return this;
        }
    }

}
