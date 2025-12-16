using System.Text;

namespace CFBROrders.SDK.Data_Models
{
    public class DBOperationResult : IOperationResult
    {
        public bool IsError { get; set; }
        public long RecordId { get; set; }
        public long Records { get; set; }
        public string CustomMessage { get; set; }
        public Exception OperationException { get; set; }
        public string ExceptionType { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string TargetSite { get; set; }
        public string StackTrace { get; set; }
        public bool IsInnerException { get; set; }
        public string InnerExceptionType { get; set; }
        public string InnerExceptionMessage { get; set; }
        public string InnerExceptionSource { get; set; }
        public string InnerExceptionStackTrace { get; set; }
        public string InnerExceptionTargetSite { get; set; }

        public DBOperationResult()
        {
            Reset();
        }

        public DBOperationResult(Exception e)
        {
            GetException(e);
        }

        public DBOperationResult(DBOperationResult o)
        {
            Reset();
            IsError = o.IsError;
            OperationException = o.OperationException;
            ExceptionType = o.ExceptionType;
            Message = o.Message;
            Source = o.Source;
            TargetSite = o.TargetSite;
            StackTrace = o.StackTrace;
            IsInnerException = o.IsInnerException;
            InnerExceptionType = o.InnerExceptionType;
            InnerExceptionMessage = o.InnerExceptionMessage;
            InnerExceptionSource = o.InnerExceptionSource;
            InnerExceptionStackTrace = o.InnerExceptionStackTrace;
            InnerExceptionTargetSite = o.InnerExceptionTargetSite;
        }

        private string Show()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("IsError: {0}\r\n", IsError);
            sb.AppendFormat("Records: {0}\r\n", Records);
            sb.AppendFormat("Record Id: {0}\r\n", RecordId);
            sb.AppendFormat("Custom Message: {0}\r\n", CustomMessage);
            sb.AppendLine();
            sb.AppendLine("EXCEPTION TYPE:");
            sb.AppendLine(ExceptionType);
            sb.AppendLine("MESSAGE:");
            sb.AppendLine(Message);
            sb.AppendLine("SOURCE:");
            sb.AppendLine(Source);
            sb.AppendLine("TARGETSITE:");
            sb.AppendLine(TargetSite);
            sb.AppendLine("STACKTRACE:");
            sb.AppendLine(StackTrace);

            if (IsInnerException)
            {
                sb.AppendLine();
                sb.AppendLine("INNER EXCEPTION");
                sb.AppendLine("INNER EXCEPTION TYPE:");
                sb.AppendLine(InnerExceptionType);
                sb.AppendLine("INNER EXCEPTION MESSAGE:");
                sb.AppendLine(InnerExceptionMessage);
                sb.AppendLine("INNER EXCEPTION SOURCE:");
                sb.AppendLine(InnerExceptionSource);
                sb.AppendLine("INNER EXCEPTION TARGETSITE:");
                sb.AppendLine(InnerExceptionTargetSite);
                sb.AppendLine("INNER EXCEPTION STACKTRACE:");
                sb.AppendLine(InnerExceptionStackTrace);
            }

            return sb.ToString();
        }

        public void GetException(Exception ex)
        {
            Reset();
            IsError = true;
            OperationException = ex;
            ExceptionType = ex.GetType().ToString();
            Message = ex.Message;
            Source = ex.Source;
            TargetSite = ex.TargetSite?.ToString() ?? "";
            StackTrace = ex.StackTrace;

            if (ex.InnerException != null)
            {
                IsInnerException = true;
                InnerExceptionType = ex.InnerException.GetType().ToString();
                InnerExceptionMessage = ex.InnerException.Message;
                InnerExceptionSource = ex.InnerException.Source;
                InnerExceptionTargetSite = ex.InnerException.TargetSite?.ToString() ?? "";
                InnerExceptionStackTrace = ex.InnerException.StackTrace ?? "";
            }
        }

        public string ToErrorString() => Show();
        public string ToHtmlErrorString() => $"<pre>{Show()}</pre>";

        public void Reset()
        {
            IsError = false;
            Message = "";
            Source = "";
            TargetSite = "";
            StackTrace = "";
            RecordId = 0;
            Records = 0;
            OperationException = null;
            CustomMessage = "";
            IsInnerException = false;
            InnerExceptionType = "";
            InnerExceptionMessage = "";
            InnerExceptionSource = "";
            InnerExceptionStackTrace = "";
            InnerExceptionTargetSite = "";
        }
    }
}
