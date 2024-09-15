namespace EventBus
{
    public class EventBusData
    {
        private string topic;
        private object data;
        public string Topic { get => topic; private set => topic = value; }
        public object Data { get => data; private set => data = value; }

        public EventBusData(string topic, object data)
        {
            ArgumentNullException.ThrowIfNull(topic, nameof(topic));
            ArgumentNullException.ThrowIfNull(data, nameof(data));

            this.topic = topic;
            this.data = data;
        }


    }
}
