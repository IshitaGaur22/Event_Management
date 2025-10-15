namespace EventFeedback.Exceptions
{
    public class FeedbackNotFound : ApplicationException
    {
        public FeedbackNotFound() { }
        public FeedbackNotFound(string message) : base(message) { }
    }

    public class InvalidSortFieldException : ApplicationException
    {
        public InvalidSortFieldException(string message) : base(message) { }
    }

}
