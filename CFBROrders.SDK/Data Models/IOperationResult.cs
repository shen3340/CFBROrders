namespace CFBROrders.SDK.Data_Models
{
    public interface IOperationResult
    {
        bool IsError { get; set; }
        long Records { get; set; }
        long RecordId { get; set; }
        string CustomMessage { get; set; }
        Exception OperationException { get; set; }
        string ExceptionType { get; set; }
        string Message { get; set; }
        string Source { get; set; }
        string TargetSite { get; set; }
        string StackTrace { get; set; }
        bool IsInnerException { get; set; }
        string InnerExceptionType { get; set; }
        string InnerExceptionMessage { get; set; }
        string InnerExceptionSource { get; set; }
        string InnerExceptionStackTrace { get; set; }
        string InnerExceptionTargetSite { get; set; }

        void GetException(Exception ex);
        string ToErrorString();
        string ToHtmlErrorString();
        void Reset();
    }
}
