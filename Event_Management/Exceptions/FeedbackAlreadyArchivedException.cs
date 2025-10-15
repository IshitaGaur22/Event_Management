namespace EventFeedback.Exceptions
{
    public class FeedbackAlreadyArchivedException : Exception
    {
        public FeedbackAlreadyArchivedException(string message) : base(message) { }
    }

    public class FeedbackNotArchivedException : Exception
    {
        public FeedbackNotArchivedException(string message) : base(message) { }
    }
}
