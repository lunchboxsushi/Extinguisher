using System;
using System.Text;
using System.Threading;
namespace TM.Extinguisher
{
    /*  
     * As a Fire Fighter Operations Manager, we're required to order FireFighters 
     * to be dispatched to different location and Extinguish the fires.
     * 
     * TODO: Implement watering logic so that only 1 fire can be extinguished at a time
     */
    class Program
    {
        public static void Main(string[] args)
        {
            var run = true;
            var _fireService = new FireService();

            InitStatusThread(_fireService);

            while (run)
            {
                var fire = UserInputCycle(_fireService);
                if (fire == null) continue;

                // TODO: Implement Extinguish fire logic
                _fireService.ExtinguishFireReport(fire.Id);

            }
        }

        private static void InitStatusThread(FireService _fireService)
        {
            var uiThread = new UIThread(_fireService);
            var statusUpdateThread = new Thread(new ThreadStart(uiThread.UpdateUI));
            statusUpdateThread.Start();
            Thread.Sleep(1000);
        }

        private static FireReport UserInputCycle(FireService _fireService)
        {
            Console.SetCursorPosition(0, 6);
            var sb = new StringBuilder();
            sb.Append("          ");
            sb.Append("          ");
            sb.Append("          ");
            sb.AppendLine("          ");
            sb.AppendLine("          ");
            sb.AppendLine("          ");
            Console.Write(sb);
            Console.SetCursorPosition(0, 6);

            // Get input command to send FireFighters to Extinguish fire
            var inputVal = IOService.ReadSelectedFireReport();

            // value valid selection
            if (!inputVal.HasValue) { return null; }

            // verify valid fire selected is reported
            var fireReport = _fireService.GetFireReport(inputVal.Value);
            if (fireReport == null)
            {
                ShowInvalidInput(inputVal.Value);
                return null;
            }

            return fireReport;
        }

        // Error output
        private static void ShowInvalidInput(int inputRequest)
        {
            Console.Clear();
            Console.WriteLine($"No fire reported at Location #: {inputRequest}");
        }
    }
}
