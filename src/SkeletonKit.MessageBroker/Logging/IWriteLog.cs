namespace SkeletonKit.MessageBroker.Logging
{
    public interface IWriteLog
    {
        int Level { get; set; }
        string Category { get; set; }
        string Source { get; set; }
        string Message { get; set; }
        List<object> Data { get; set; }
        string User { get; set; }
        DateTime CreatedOn { get; set; }
        string CorrelationId { get; set; }
        Dictionary<string, object> ContextInfo { get; set; }
    }
}
