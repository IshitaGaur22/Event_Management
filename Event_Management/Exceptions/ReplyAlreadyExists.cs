namespace EventFeedback.Exceptions
{
    public class ReplyAlreadyExists : ApplicationException
    {
        public ReplyAlreadyExists() { }
        public ReplyAlreadyExists(string message) : base(message) { }
    }
}
