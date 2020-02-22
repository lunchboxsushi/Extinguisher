using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace TM.Extinguisher
{
    public class UIThread
    {
        private Stopwatch _runtime = new Stopwatch();

        private FireService _fireService;
        public UIThread(FireService fireService)
        {
            _fireService = fireService;
            _runtime.Start();
        }

        public void UpdateUI()
        {
            for (; ; )
            {
                // Dispaly a list of the currently reported fires
                var fireReports = _fireService.GetFireReports();
                var formattedFireReports = this.FormatFireReportStasus(fireReports);

                // check watering status
                _fireService.UpdateWateringStatusReports();
                IOService.Display(formattedFireReports);
                Thread.Sleep(250);
            }

        }

        private StringBuilder FormatFireReportStasus(List<FireReport> model)
        {
            Console.SetCursorPosition(0, 0);
            var sb = new StringBuilder();
            sb.AppendLine($"Runtime: {_runtime.ElapsedMilliseconds / 1000} in seconds");
            foreach (var fire in _fireService.GetFireReports())
            {
                sb.AppendLine($"Fire#: {fire.Id}, fire level: {fire.FireLevel}\t Watering: {fire.Watering}\t Last Update: {fire.LastUpdated}                         ");
            }

            return sb;
        }

        // Error output
        private static void ShowInvalidInput(int inputRequest)
        {
            Console.Clear();
            Console.WriteLine($"No fire reported at Location #: {inputRequest}");
        }

    }
}
