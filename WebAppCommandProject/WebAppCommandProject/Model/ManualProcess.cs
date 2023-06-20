using Microsoft.AspNetCore.SignalR;
using WebAppCommandProject.Hubs;

namespace WebAppCommandProject.Model
{
    public class ManualProcess
    {
        private bool _isRunning;
        private bool _isPaused;
        private object _lockObject;
        private string _workerMessage = string.Empty;


        private readonly IHubContext<WorkerHub> _hub_context;

        public ManualProcess(IHubContext<WorkerHub> hub_context)
        {
            _isRunning = false;
            _isPaused = false;
            _lockObject = new object();

            _hub_context = hub_context;
        }

        public void Start()
        {
            if (_isRunning)
            {
                _hub_context.Clients.All.SendAsync("Send", "Process is already running!", "Worker on background");
                Console.WriteLine("Process is already running.");
                return;
            }

            _isRunning = true;
            _isPaused = false;

            Thread processThread = new Thread(Execute);
            processThread.Start();
            _hub_context.Clients.All.SendAsync("Send", "Process started!", "Worker on background");
            Console.WriteLine("Process started.");
        }

        public void Stop()
        {
            if (!_isRunning)
            {
                _hub_context.Clients.All.SendAsync("Send", "Process is not running!", "Worker on background");
                Console.WriteLine("Process is not running.");
                return;
            }

            lock (_lockObject)
            {
                _isRunning = false;
                _isPaused = false;
            }
            _hub_context.Clients.All.SendAsync("Send", "Process stopped!", "Worker on background");
            Console.WriteLine("Process stopped.");
        }

        public void Pause()
        {
            if (!_isRunning)
            {
                _hub_context.Clients.All.SendAsync("Send", "Process is not running!", "Worker on background");
                Console.WriteLine("Process is not running.");
                return;
            }

            if (_isPaused)
            {
                _hub_context.Clients.All.SendAsync("Send", "Process is already paused!", "Worker on background");
                Console.WriteLine("Process is already paused.");
                return;
            }

            lock (_lockObject)
            {
                _isPaused = true;
            }
            _hub_context.Clients.All.SendAsync("Send", "Process paused!", "Worker on background");
            Console.WriteLine("Process paused.");
        }

        public void Continue()
        {
            if (!_isRunning)
            {
                _hub_context.Clients.All.SendAsync("Send", "Process is not running!", "Worker on background");
                Console.WriteLine("Process is not running.");
                return;
            }

            if (!_isPaused)
            {
                _hub_context.Clients.All.SendAsync("Send", "Process is not paused!", "Worker on background");
                Console.WriteLine("Process is not paused.");
                return;
            }

            lock (_lockObject)
            {
                _isPaused = false;
                Monitor.Pulse(_lockObject); // Signal the thread to continue
            }
            _hub_context.Clients.All.SendAsync("Send", "Process resumed!", "Worker on background");
            Console.WriteLine("Process resumed.");
        }

        private void Execute()
        {
            while (_isRunning)
            {
                lock (_lockObject)
                {
                    while (_isPaused)
                    {
                        Monitor.Wait(_lockObject); // Wait for the resume signal
                    }
                }

                // Do your process logic here
                Console.WriteLine("Process is running...");
                _hub_context.Clients.All.SendAsync("Send", $"Process is running... {_workerMessage}", "Worker on background");
                Thread.Sleep(1000); // Simulate some work, replace with your own code
            }
        }

        public void ChangeMessage(string messageNew)
        {
            _workerMessage = messageNew;
        }
    }
}
