namespace EventFeedback.Exceptions
{
    
        public class FeedbackAlreadyExists : ApplicationException
        {
            public FeedbackAlreadyExists() { }
            public FeedbackAlreadyExists(string message) : base(message) { }
        }
    
}
