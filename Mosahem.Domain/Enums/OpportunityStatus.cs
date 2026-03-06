namespace mosahem.Domain.Enums
{
    [Flags]
    public enum OpportunityStatus
    {
        Open = 1 << 0,
        Closed = 1 << 1,
        Active = 1 << 2,
        Ended = 1 << 3,
        Stopped = 1 << 4
    }
}