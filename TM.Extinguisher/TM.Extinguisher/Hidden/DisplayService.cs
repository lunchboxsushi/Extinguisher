using System;
using System.Text;

namespace TM.Extinguisher
{
    public static class IOService
    {
        public static void Display(StringBuilder sb)
        {
            Console.WriteLine(sb);
        }

        public static int? ReadSelectedFireReport()
        {
            Console.Write("Select Fire Reported to extinguish: ");
            var inputVal = Console.ReadLine();

            try
            {
                return Convert.ToInt32(inputVal);
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine("Input must be numeric");
                return null;
            }
        }


    }
}
