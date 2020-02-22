using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TM.Extinguisher
{
    public class FireService
    {
        private FireReportRepository _fireRepo;

        public FireService()
        {
            _fireRepo = new FireReportRepository();
        }

        public FireReport GetFireReport(int Id)
        {
            return _fireRepo.GetReport(Id);
        }

        public List<FireReport> GetFireReports()
        {
            return _fireRepo.GetReports();
        }
        public void UpdateFireReport(FireReport model)
        {
            _fireRepo.UpdateReport(model);
        }
        private FireLevel DecreaseFireLevel(FireLevel fireLevel)
        {
            switch (fireLevel) {
                case FireLevel.NONE:
                    throw new Exception("WASTING RESOURCES");
                case FireLevel.SMALL:
                    return FireLevel.NONE;
                case FireLevel.MEDIUM:
                    return FireLevel.SMALL;
                case FireLevel.LARGE:
                    return FireLevel.MEDIUM;
                case FireLevel.EPIC:
                    return FireLevel.LARGE;
                case FireLevel.DISASTER:
                    return FireLevel.EPIC;
                default:
                    return fireLevel;
            }
        }

        // TODO:    Decrease fire level every 5 seconds of watering
        //          If FireLevel = None make sure to stop watering                       
        public void UpdateWateringStatusReports()
        {
            var reports = _fireRepo.GetReports();
            var currentlyExtinguishing = reports.Where(r => r.Watering).FirstOrDefault();

            if (currentlyExtinguishing == null) return;

            var wateringDuration = (DateTime.Now - currentlyExtinguishing.LastUpdated).TotalSeconds; 

            if (wateringDuration >= 5)
            {
                currentlyExtinguishing.FireLevel = DecreaseFireLevel(currentlyExtinguishing.FireLevel);
                currentlyExtinguishing.LastUpdated = DateTime.Now;

                if (currentlyExtinguishing.FireLevel == FireLevel.NONE)
                {
                    currentlyExtinguishing.Watering = false;
                }

                UpdateFireReport(currentlyExtinguishing);
            }
        }


        // TODO: Can only extinguish 1 fire at a time
        public void ExtinguishFireReport(int Id)
        {

            //check if anything else being watered
            var reports = _fireRepo.GetReports();
            var currentlyExtinguishing = reports.Where(r => r.Watering).FirstOrDefault();
            var fireExtinguishOrder = reports.FirstOrDefault(r => r.Id == Id);

            // do we need to stop previous watering?
            if (currentlyExtinguishing != null && currentlyExtinguishing.Id != fireExtinguishOrder.Id) {
                currentlyExtinguishing.Watering = false;
                UpdateFireReport(currentlyExtinguishing);
            }

            fireExtinguishOrder.Watering = true;
            fireExtinguishOrder.LastUpdated = DateTime.Now;
            UpdateFireReport(fireExtinguishOrder);
        }

    }
}
