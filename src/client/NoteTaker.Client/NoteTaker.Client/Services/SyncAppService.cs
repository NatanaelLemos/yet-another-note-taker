using System;
using NoteTaker.Domain.Data;
using System.Timers;

namespace NoteTaker.Client.Services
{
    public class SyncAppService : ISyncAppService
    {
        private readonly INotesRepository _notesRepository;
        private readonly Timer _updateTimer;

        public SyncAppService(INotesRepository notesRepository)
        {
            _notesRepository = notesRepository;

            _updateTimer = new Timer(1000);
            _updateTimer.Elapsed += _updateTimer_Elapsed;
        }

        public void StartListeners()
        {
            _updateTimer.Start();
        }

        private void _updateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _updateTimer.Stop();

            try
            {
                Console.WriteLine("test XD");
            }
            finally
            {
                _updateTimer.Start();
            }
        }
    }
}
