using System;
using System.Collections.Generic;
using System.Linq;

namespace TM.Extinguisher
{
    public class FireReportRepository
    {
        private readonly object UpdateLock = new object();

        public List<FireReport> GetReports()
        {
            return ExtinguisherDb.Fires.Value.ToList();
        }

        public FireReport GetReport(int Id)
        {
            var fire = ExtinguisherDb.Fires.Value.ToList().FirstOrDefault(f => f.Id == Id);
            if (fire == null) { FireNotFoundException(); }
            return fire;
        }

        public void UpdateReport(FireReport model)
        {
            lock (UpdateLock)
            {
                var fire = ExtinguisherDb.Fires.Value.FirstOrDefault(f => f.Id == model.Id);

                fire.Watering = model.Watering;
                fire.LastUpdated = model.LastUpdated;
                fire.FireLevel = model.FireLevel;
            }
        }

        private void FireNotFoundException()
        {
            throw new Exception("No Fire Found in databaes");
        }

    }
}
