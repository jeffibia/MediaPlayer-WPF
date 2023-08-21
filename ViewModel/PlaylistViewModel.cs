using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediaPlayer.Model;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace MediaPlayer.ViewModel
{
    public class PlaylistViewModel : ObservableObject
    {
        public ObservableCollection<MediaItem> Playlist { get; set; }
        public event Action<MediaItem> PlayRequested;
        public event Action<MediaItem> PauseRequested;
        public event Action<MediaItem> StopRequested;
        public event Action<bool> FullScreenToggleRequested;

        private MediaItem? _selectedMedia;
        private string _currentPosition;
        private string _appTitle = string.Empty;
        private bool _isFullscreen = false;

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

        public string CurrentPosition
        {
            get { return _currentPosition; }
            set
            {
                SetProperty(ref _currentPosition, value);
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
            SavePlaylist = new RelayCommand(SavePlaylistCommand);
            LoadPlaylist = new RelayCommand(LoadPlaylistCommand);
            FullScreenToggle = new RelayCommand(FullScreenToggleCommand);
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

        public RelayCommand SavePlaylist { get; set; }
        public RelayCommand LoadPlaylist { get; set; }
        public RelayCommand FullScreenToggle { get; set; }

        //Executa can deletar
        private bool CanRemoveCommand()
        {
            return this._selectedMedia != null;
        }

        private void FullScreenToggleCommand()
        {
            _isFullscreen = !_isFullscreen;
            this.FullScreenToggleRequested.Invoke(_isFullscreen);
        }

        private void SavePlaylistCommand()
        {
            var dialog = new System.Windows.Forms.SaveFileDialog();
            dialog.Filter = "Playlist File (*.frj)|*.frj";
            if (dialog.ShowDialog() == DialogResult.OK && dialog.FileName != string.Empty)
            {
                try
                {
                    string playlistJson = JsonConvert.SerializeObject(Playlist.ToArray());
                    System.IO.File.WriteAllText(dialog.FileName, playlistJson);
                    MessageBox.Show("Playlist salva com sucesso!");
                }catch(Exception ex)
                {
                    MessageBox.Show("Falha ao salvar playlist. Verifique as permissões da pasta para salvar o arquivo.");
                }
            }
        }

        private void LoadPlaylistCommand()
        {
            try
            {
                using (var fbd = new System.Windows.Forms.OpenFileDialog())
                {
                    fbd.Multiselect = false;
                    fbd.Filter = "Playlist File (*.frj)|*.frj";
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && fbd.FileName!=string.Empty)
                    {
                        string playListContent = File.ReadAllText(fbd.FileName, Encoding.UTF8);
                        var mediaItem = JsonConvert.DeserializeObject<List<MediaItem>>(playListContent);
                        //clear playlist before load new one.
                        Playlist.Clear();
                        //load new playlist to screen
                        if (mediaItem != null)
                        {
                            foreach (var item in mediaItem)
                            {
                                Playlist.Add(item);
                            }
                        }
                        MessageBox.Show("Playlist carregada com sucesso!");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Falha ao carregar Playlist.");
            }
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
