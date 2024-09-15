using System.Collections.Concurrent;

namespace EventBus
{
    public class EventBus : IEventBus
    {
        private Dictionary<string, List<IEventBusHandler>> _registration = new Dictionary<string, List<IEventBusHandler>>();
        private ConcurrentQueue<EventBusData> _eventQueue = new ConcurrentQueue<EventBusData>();
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public void Subscribe(string topic, IEventBusHandler handler)
        {
            if (!_registration.ContainsKey(topic))
            {
                _registration[topic] = new List<IEventBusHandler>();
            }
            _registration[topic].Add(handler);

        }

        public void Unsubscribe(string topic, IEventBusHandler handler)
        {
            if (_registration.ContainsKey(topic))
            {
                _registration[topic].Remove(handler);
            }
        }

        public void Publish(string topic, object data)
        {
            _eventQueue.Enqueue(new EventBusData(topic, data));
        }

        public void StartAsync()
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                if (_eventQueue.TryDequeue(out EventBusData? e) && e is not null)
                {
                    if (_registration.TryGetValue(e.Topic, out List<IEventBusHandler>? handlers) && handlers is not null)
                    {
                        foreach (var handler in handlers)
                        {
                            Task.Run(() => handler.Handle(e.Topic, e.Data));
                        }
                    }
                }
            }
        }

        public void StopAsync()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}
