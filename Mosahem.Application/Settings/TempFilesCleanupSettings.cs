namespace Mosahem.Application.Settings
{
    public class TempFilesCleanupSettings
    {
        public bool Enabled { get; set; } = true;
        public int RunEveryHours { get; set; } = 24;
    }
}
