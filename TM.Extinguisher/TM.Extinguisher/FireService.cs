using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TM.Extinguisher
{
    public class FireService
    {
        private FireReportRepository _fireRepo;
        private Stopwatch _runtime = new Stopwatch();

        public FireService()
        {
            _fireRepo = new FireReportRepository();
            _runtime.Start();
        }

        public FireReportModel GetFireReport(int Id)
        {
            return _fireRepo.GetReport(Id);
        }

        public StringBuilder GetFormattedFireReportStasus()
        {
            Console.SetCursorPosition(0, 0);
            var sb = new StringBuilder();
            sb.AppendLine($"Runtime: {_runtime.ElapsedMilliseconds / 1000} in seconds");
            foreach (var fire in _fireRepo.GetReports())
            {
                sb.AppendLine($"Fire#: {fire.Id}, fire level: {fire.FireLevel}, Watering: {fire.Watering}");
            }

            return sb;
        }

        public void UpdateFireReport(FireReportModel model)
        {
            _fireRepo.UpdateReport(model);
        }

        public void ExtinguishFireReport(int Id)
        {
            // TODO: Can only extinguish 1 fire at a time

            //check if anything else being watered
            var reports = _fireRepo.GetReports();
            var currentlyExtinguishing = reports.Where(r => r.Watering).FirstOrDefault();
            var fireExtinguishOrder = reports.FirstOrDefault(r => r.Id == Id);

            // do we need to stop previous watering?
            if (currentlyExtinguishing?.Id != fireExtinguishOrder.Id) {
                currentlyExtinguishing.Watering = false;
                UpdateFireReport(currentlyExtinguishing);
            }

            fireExtinguishOrder.Watering = true;
            fireExtinguishOrder.LastUpdated = DateTime.Now;
            UpdateFireReport(fireExtinguishOrder);
            
            // TODO: Every 5 seconds decrease fire level while watering
        }

        // returns same level if fire not decreased
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
                    return FireLevel.SMALL;
                case FireLevel.EPIC:
                    return FireLevel.SMALL;
                case FireLevel.DISASTER:
                    return FireLevel.SMALL;
                default:
                    return fireLevel;
            }
        }
    }
}
