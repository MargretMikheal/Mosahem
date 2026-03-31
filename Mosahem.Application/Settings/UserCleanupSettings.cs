namespace Mosahem.Application.Settings
{
    public class UserCleanupSettings
    {
        public bool Enabled { get; set; } = true;
        public int RetentionDays { get; set; } = 30;
        public int RunEveryHours { get; set; } = 24;
        public int BatchSize { get; set; } = 100;
    }
}
