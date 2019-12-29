using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using NoteTaker.Domain;

namespace NoteTaker.Client.Services
{
    public interface INotesAppService
    {
        ObservableCollection<Note> DataSource { get; }

        Task FetchAll();
    }
}
