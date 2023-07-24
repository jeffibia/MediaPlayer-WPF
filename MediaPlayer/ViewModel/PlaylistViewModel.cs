using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediaPlayer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MediaPlayer.ViewModel
{
    public class PlaylistViewModel : ObservableObject
    {
        public ObservableCollection<MediaItem> Playlist { get; set; }
        public event Action<MediaItem> PlayRequested;
        public event Action<MediaItem> PauseRequested;
        public event Action<MediaItem> StopRequested;

        private MediaItem? _selectedMedia;
        private string _appTitle = string.Empty;

        public const string AppName = "FRJ Media Player";


        public string AppTitle 
        {
            get 
            {
                if (_appTitle == string.Empty )
                {
                    return AppName;
                }
                else
                {
                    return AppName + " - Reproduzindo: " + _appTitle;
                }

            }
            set
            {
                SetProperty(ref _appTitle, value);
            }
        }

        public MediaItem SelectedMedia
        {
            get { return _selectedMedia; }
            set
            {
                SetProperty(ref _selectedMedia, value);
                RemoveMedia.NotifyCanExecuteChanged();
            }
        }


        public PlaylistViewModel()
        {
            Previous = new RelayCommand(PreviousMediaCommand);
            Play = new RelayCommand(PlayMediaCommand);
            Pause = new RelayCommand(PauseMediaCommand);
            Stop = new RelayCommand(StopMediaCommand);
            Next = new RelayCommand(NextMediaCommand);
            AddFolder = new RelayCommand(AddFolderCommand);
            AddFile = new RelayCommand(AddFileCommand);
            RemoveMedia = new RelayCommand(RemoveMediaCommand, CanRemoveCommand);

            Playlist = new ObservableCollection<MediaItem>();
        }

        public RelayCommand Previous { get; set; }
        public RelayCommand Play { get; set; }
        public RelayCommand Pause { get; set; }
        public RelayCommand Stop { get; set; }
        public RelayCommand Next { get; set; }

        public RelayCommand AddFolder { get; set; }
        public RelayCommand AddFile { get; set; }
        public RelayCommand RemoveMedia { get; set; }

        //Executa can deletar
        private bool CanRemoveCommand()
        {
            return this._selectedMedia != null;
        }


        private void AddFolderCommand()
        {
            PlayListHandle playListHandle = new PlayListHandle();
            var list = playListHandle.SelectDirectiory();
            foreach (var item in list)
            {
                Playlist.Add(item);
            }
        }

        private void AddFileCommand()
        {
            PlayListHandle playListHandle = new PlayListHandle();
            var list = playListHandle.SelectFiles();
            foreach (var item in list)
            {
                Playlist.Add(item);
            }
        }

        private void RemoveMediaCommand()
        {
            if (SelectedMedia != null)
            {
                Playlist.Remove(SelectedMedia);
            }
        }

        private void PreviousMediaCommand()
        {
            if(SelectedMedia!= null)
            {
                var index = Playlist.IndexOf(SelectedMedia);
                if (index == 0)
                {
                    SelectedMedia = Playlist[Playlist.Count - 1]; //Play the last media
                }
                else
                {
                    SelectedMedia = Playlist[index - 1]; //play previous one
                }
                AppTitle = SelectedMedia.Title;
                this.PlayRequested.Invoke(SelectedMedia);
            }
        }

        private void PlayMediaCommand()
        {
            if (this.PlayRequested != null && SelectedMedia != null)
            {
                AppTitle = SelectedMedia.Title;
                this.PlayRequested.Invoke(SelectedMedia);
            }
        }

        private void PauseMediaCommand()
        {
            if(this.PauseRequested != null && SelectedMedia != null)
            {
                this.PauseRequested.Invoke(SelectedMedia);
            }
        }

        private void StopMediaCommand()
        {
            if (this.StopRequested != null && SelectedMedia != null)
            {
                AppTitle = string.Empty;
                this.StopRequested.Invoke(SelectedMedia);
            }
        }

        public void NextMediaCommand()
        {
            if(SelectedMedia != null)
            {
                var index = Playlist.IndexOf(SelectedMedia);
                if (index + 1 > Playlist.Count - 1)
                {
                    SelectedMedia = Playlist[0];
                }
                else
                {
                    SelectedMedia = Playlist[index + 1];
                }
                AppTitle = SelectedMedia.Title;
                this.PlayRequested.Invoke(SelectedMedia);
            }
  
        }
    }
}
