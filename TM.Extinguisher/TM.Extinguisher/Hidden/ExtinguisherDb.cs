using System;
using System.Collections.Generic;

namespace TM.Extinguisher
{
    public static class ExtinguisherDb
    {

        private static bool IsInit = false;
        public static Lazy<List<FireReportModel>> Fires => new Lazy<List<FireReportModel>>(() => Initialize());

        private static List<FireReportModel> Initialize()
        {
            var result = new List<FireReportModel>();

            Array values = Enum.GetValues(typeof(FireLevel));
            Random random = new Random();

            if (!IsInit)
            {
                for (var i = 1; i <= 5; i++)
                {
                    result.Add(new FireReportModel
                    {
                        Id = i,
                        FireLevel = (FireLevel)values.GetValue(random.Next(values.Length)),
                }); ;
                }
            }

            return result;
        }
    }
}
