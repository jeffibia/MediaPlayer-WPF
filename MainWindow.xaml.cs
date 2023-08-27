using CommunityToolkit.Mvvm.Messaging;
using MediaPlayer.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace MediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel.PlaylistViewModel vm;
        TimeSpan _position;
        DispatcherTimer _timer = new DispatcherTimer();
        const string playlistName = "playlistData.frj";

        void ticktock(object sender, EventArgs e)
        {
            sliderSeek.Value = this.MediaPlayerElement.Position.TotalSeconds;
            vm.CurrentPosition = this.MediaPlayerElement.Position.ToString("hh\\:mm\\:ss");
        }

        public MainWindow()
        {
            InitializeComponent();

            vm = new ViewModel.PlaylistViewModel();
            DataContext = vm;

            _timer.Interval = TimeSpan.FromMilliseconds(1000);
            _timer.Tick += new EventHandler(ticktock);
            _timer.Start();

            try
            {
                string playListContent = File.ReadAllText(Directory.GetCurrentDirectory() + "\\" + playlistName, Encoding.UTF8);
                var mediaItem = JsonConvert.DeserializeObject<List<MediaItem>>(playListContent);
                //clear playlist before load new one.
                vm.Playlist.Clear();
                //load new playlist to screen
                if (mediaItem != null)
                {
                    foreach (var item in mediaItem)
                    {
                        vm.Playlist.Add(item);
                    }
                }
            }
            catch(Exception ex)
            {
                //Do nothing. Just can´t load latest playlist used.
            }

            //Play
            vm.PlayRequested += (e) =>
            {
                if(this.MediaPlayerElement.Source != new Uri(e.Path.ToString()))
                {
                    this.MediaPlayerElement.Source = new Uri(e.Path.ToString());
                }
                this.MediaPlayerElement.Play();
            };
            //Stop
            vm.StopRequested += (e) =>
            {
                this.MediaPlayerElement.Source = null;
                this.MediaPlayerElement.Stop();
                vm.SelectedMedia.Duration = "00:00:00";
            };
            //Pause
            vm.PauseRequested += (e) =>
            {
                this.MediaPlayerElement.Pause();
            };

            vm.FullScreenToggleRequested += (e) =>
            {
                if (e == false)
                {
                    gridVideo.SetValue(Grid.ColumnSpanProperty, 1);
                    gridSlider.SetValue(Grid.ColumnSpanProperty, 1);
                    lstPlaylist.Visibility = Visibility.Visible;
                    this.WindowState = WindowState.Normal;
                }
                else
                {
                    gridVideo.SetValue(Grid.ColumnSpanProperty, 2);
                    gridSlider.SetValue(Grid.ColumnSpanProperty, 2);
                    lstPlaylist.Visibility= Visibility.Collapsed;
                    this.WindowState = WindowState.Maximized;
                }
                
                this.MediaPlayerElement.Stretch = Stretch.Fill;
                this.UpdateLayout();

            };

            //Media finished play
            this.MediaPlayerElement.MediaEnded += MediaPlayerElement_MediaEnded;
            //Media opened
            this.MediaPlayerElement.MediaOpened += MediaPlayerElement_MediaOpened;
        }

        private void MediaPlayerElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            var duration = this.MediaPlayerElement.NaturalDuration.TimeSpan.ToString();
            vm.SelectedMedia.Duration = duration;
            _position = this.MediaPlayerElement.NaturalDuration.TimeSpan;
            sliderSeek.Minimum = 0;
            sliderSeek.Maximum = _position.TotalSeconds;
        }

        //detect that movie/audio finished, so go to next one
        private void MediaPlayerElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            vm.NextMediaCommand();
        }

        private void sliderSeek_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int pos = Convert.ToInt32(sliderSeek.Value);
            this.MediaPlayerElement.Position = new TimeSpan(0, 0, 0, pos, 0);
        }

        private void sliderSeek_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sliderSeek.IsMouseCaptureWithin)
            {
                int pos = Convert.ToInt32(sliderSeek.Value);
                this.MediaPlayerElement.Position = new TimeSpan(0, 0, 0, pos, 0);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                string playlistJson = JsonConvert.SerializeObject(vm.Playlist);
                System.IO.File.WriteAllText(Directory.GetCurrentDirectory() + "\\" + playlistName, playlistJson);
            }
            catch (Exception ex)
            {
                //Do nothing. It just didn't save current playlist (not an error, just user privilege problem)
            }
        }
    }
}
