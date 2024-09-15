namespace EventBus
{
    public interface IEventBus
    {
        public void Subscribe(string topic, IEventBusHandler handler);
        public void Unsubscribe(string topic, IEventBusHandler handler);
        public void Publish(string topic, object data);

        public void StartAsync();
        public void StopAsync();
    }
}
