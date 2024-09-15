using EventBus;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EventBusWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker worker;
        EventBus.EventBus eventBus;
        Handler1 handler1 = new Handler1();
        Handler2 handler2 = new Handler2();
        Handler3 handler3 = new Handler3();
        StringBuilder logger = new StringBuilder();

        public MainWindow()
        {
            InitializeComponent();
            eventBus = new EventBus.EventBus();

            handler1.OnProgress += Handler_OnProgress;
            handler1.OnErrorOccurred += Handler_OnErrorOccurred;
            handler1.OnCompleted += Handler_OnCompleted;

            handler2.OnProgress += Handler_OnProgress;
            handler2.OnErrorOccurred += Handler_OnErrorOccurred;
            handler2.OnCompleted += Handler_OnCompleted;

            handler3.OnProgress += Handler_OnProgress;
            handler3.OnErrorOccurred += Handler_OnErrorOccurred;
            handler3.OnCompleted += Handler_OnCompleted;

            eventBus.Subscribe("handler1", handler1);
            eventBus.Subscribe("handler1_Completed", handler2);
            eventBus.Subscribe("handler2_Completed", handler3);


            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();

        }

        private void Handler_OnCompleted(object? sender, EventHandlerCompletedEventArgs e)
        {
            Dispatcher.InvokeAsync(new Action(() =>
            {
                logger.AppendLine($"Handler_OnCompleted: {e.Topic}");
                tbConsole.Text = logger.ToString();
            }));
            if (!string.IsNullOrWhiteSpace(e.Next))
            {
                eventBus.Publish(e.Next, e.Data);
            }
        }

        private void Handler_OnErrorOccurred(object? sender, EventHandlerFailedEventArgs e)
        {
            Dispatcher.InvokeAsync(new Action(() =>
            {
                logger.AppendLine($"Handler_OnErrorOccurred: {e.Topic}");
                tbConsole.Text = logger.ToString();
            }));
        }

        private void Handler_OnProgress(object? sender, EventHandlerProgressedEventArgs e)
        {
            Dispatcher.InvokeAsync(new Action(() =>
            {
                logger.AppendLine($"Handler_OnProgress: {e.Topic}");
                tbConsole.Text = logger.ToString();
            }));
        }

        private void Worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            eventBus.StartAsync();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            eventBus.Publish("handler1", new());
        }
    }
}