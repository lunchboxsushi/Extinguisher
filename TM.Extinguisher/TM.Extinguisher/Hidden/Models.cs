using System;

namespace TM.Extinguisher
{
    public class FireReportModel
    {
        public int Id { get; set; }
        public bool Watering { get; set; }
        public DateTime LastUpdated { get; set; }
        public FireLevel FireLevel { get; set; }
    }

    public enum FireLevel
    {
        NONE,
        SMALL,
        MEDIUM,
        LARGE,
        EPIC,
        DISASTER
    }
}
