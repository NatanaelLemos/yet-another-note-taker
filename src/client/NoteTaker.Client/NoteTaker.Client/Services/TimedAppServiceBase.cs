using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Timers;

namespace NoteTaker.Client.Services
{
    public class TimedAppServiceBase
    {
        private readonly Queue<ConfiguredTaskAwaitable> _taskQueue;
        private readonly Timer _updateTimer;

        public TimedAppServiceBase(int interval)
        {
            _taskQueue = new Queue<ConfiguredTaskAwaitable>();
            _updateTimer = new Timer(interval);
            _updateTimer.Elapsed += _updateTimer_Elapsed;
            _updateTimer.Start();
        }

        protected void Enqueue(Task task)
        {
            _taskQueue.Enqueue(task.ConfigureAwait(false));
        }

        private async void _updateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _updateTimer.Stop();

            try
            {
                if (!_taskQueue.Any())
                {
                    return;
                }

                var nextInLine = _taskQueue.Dequeue();
                await nextInLine;
            }
            finally
            {
                _updateTimer.Start();
            }
        }
    }
}
