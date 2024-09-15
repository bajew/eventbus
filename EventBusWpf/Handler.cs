using EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EventBusWpf
{
    public class Handler1 : IEventBusHandler
    {

        public event EventHandler<EventHandlerCompletedEventArgs>? OnCompleted;
        public event EventHandler<EventHandlerFailedEventArgs>? OnErrorOccurred;
        public event EventHandler<EventHandlerProgressedEventArgs>? OnProgress;

        public void Handle(string topic, object data)
        {
            try
            {
                OnProgress?.Invoke(this, new EventHandlerProgressedEventArgs(topic, data));
                //TODO: Implement
                Thread.Sleep(1000);
                OnCompleted?.Invoke(this, new EventHandlerCompletedEventArgs(topic, data, "handler1_Completed"));
            }
            catch (Exception ex)
            {
                OnErrorOccurred?.Invoke(this, new EventHandlerFailedEventArgs(topic, ex));
            }
        }
      
    }
    public class Handler2 : IEventBusHandler
    {
        public event EventHandler<EventHandlerCompletedEventArgs>? OnCompleted;
        public event EventHandler<EventHandlerFailedEventArgs>? OnErrorOccurred;
        public event EventHandler<EventHandlerProgressedEventArgs>? OnProgress;

        public void Handle(string topic, object data)
        {
            try
            {
                OnProgress?.Invoke(this, new EventHandlerProgressedEventArgs(topic, data));
                //TODO: Implement
                Thread.Sleep(2000);

                OnCompleted?.Invoke(this, new EventHandlerCompletedEventArgs(topic, data, "handler2_Completed"));
            }
            catch (Exception ex)
            {
                OnErrorOccurred?.Invoke(this, new EventHandlerFailedEventArgs(topic, ex));
            }
        }
   
    }
    public class Handler3 : IEventBusHandler
    {
        public event EventHandler<EventHandlerCompletedEventArgs>? OnCompleted;
        public event EventHandler<EventHandlerFailedEventArgs>? OnErrorOccurred;
        public event EventHandler<EventHandlerProgressedEventArgs>? OnProgress;

        public void Handle(string topic, object data)
        {
            try
            {
                OnProgress?.Invoke(this, new EventHandlerProgressedEventArgs(topic, data));
                Thread.Sleep(3000);

                //TODO: Implement
                OnCompleted?.Invoke(this, new EventHandlerCompletedEventArgs(topic, data, "handler3_Completed"));
            }
            catch (Exception ex)
            {
                OnErrorOccurred?.Invoke(this, new EventHandlerFailedEventArgs(topic, ex));
            }
        }
   
    }
}
