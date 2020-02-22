using System;
using System.Collections.Generic;
using System.Linq;

namespace TM.Extinguisher
{
    public class FireReportRepository
    {
        public FireReportRepository()
        {
        }

        public List<FireReportModel> GetReports()
        {
            return ExtinguisherDb.Fires.Value.ToList();
        }

        public FireReportModel GetReport(int Id)
        {
            var _fires = ExtinguisherDb.Fires.Value.ToList();
            var fire = _fires.FirstOrDefault(f => f.Id == Id);
            if (fire == null) { FireNotFoundException(); }
            return fire;
        }

        public void UpdateReport(FireReportModel model)
        {
            var fire = ExtinguisherDb.Fires.Value.FirstOrDefault(f => f.Id == model.Id);

            fire.Watering = model.Watering;
            fire.LastUpdated = model.LastUpdated;
            fire.FireLevel = model.FireLevel;
        }

        private void FireNotFoundException()
        {
            throw new Exception("No Fire Found in databaes");
        }

    }
}
