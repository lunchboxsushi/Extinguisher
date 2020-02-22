using System;
using System.Collections.Generic;

namespace TM.Extinguisher
{
    public static class ExtinguisherDb
    {

        private static bool IsInit = false;
        private static List<FireReport> result = new List<FireReport>();
        public static Lazy<List<FireReport>> Fires => new Lazy<List<FireReport>>(() => Initialize());

        private static List<FireReport> Initialize()
        {

            Array values = Enum.GetValues(typeof(FireLevel));
            Random random = new Random();

            if (!IsInit)
            {
                for (var i = 1; i <= 5; i++)
                {
                    result.Add(new FireReport
                    {
                        Id = i,
                        FireLevel = (FireLevel)values.GetValue(random.Next(values.Length)),
                        LastUpdated = DateTime.Now
                    });
                }
            }
            IsInit = true; 

            return result;
        }
    }
}
