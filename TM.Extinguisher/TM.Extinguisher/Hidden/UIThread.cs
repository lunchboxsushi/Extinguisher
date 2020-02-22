using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TM.Extinguisher
{
    public class UIThread
    {
        private FireService _fireService;
        public UIThread(FireService fireService)
        {
            _fireService = fireService;
        }

        public void UpdateUI()
        {
            for (; ; )
            {
                // Dispaly a list of the currently reported fires
                var fireStatuses = _fireService.GetFormattedFireReportStasus();
                IOService.Display(fireStatuses);
                Thread.Sleep(1000);
            }

        }

        // Error output
        private static void ShowInvalidInput(int inputRequest)
        {
            Console.Clear();
            Console.WriteLine($"No fire reported at Location #: {inputRequest}");
        }

    }
}
