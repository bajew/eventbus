namespace EventBus
{
    public interface IEventBusHandler 
    {
        public void Handle(string topic, object data);

        public event EventHandler<EventHandlerCompletedEventArgs>? OnCompleted;
        public event EventHandler<EventHandlerFailedEventArgs>? OnErrorOccurred;
        public event EventHandler<EventHandlerProgressedEventArgs>? OnProgress;
    }
    public class EventHandlerCompletedEventArgs : EventArgs
    {
        public EventHandlerCompletedEventArgs(string topic, object data, string? next = null)
        {
            Topic = topic;
            Data = data;
            Next = next;
        }

        public string Topic { get; private set; }
        public string? Next { get; private set; }
        public object Data { get; private set; }
    }
    public class EventHandlerFailedEventArgs : EventArgs
    {
        public EventHandlerFailedEventArgs(string topic, Exception error)
        {
            Topic = topic;
            Error = error;
        }

        public string Topic { get; set; }
        public Exception Error{ get; private set; }
    }
    public class EventHandlerProgressedEventArgs : EventArgs
    {
        public EventHandlerProgressedEventArgs(string topic, object data)
        {
            Topic = topic;
            Data = data;
        }

        public string Topic { get; set; }
        public object Data { get; private set; }
    }
}
