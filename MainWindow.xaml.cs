using CommunityToolkit.Mvvm.Messaging;
using MediaPlayer.Model;
using System;
using System.Collections.Generic;
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

namespace MediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel.PlaylistViewModel vm;
        public MainWindow()
        {
            InitializeComponent();
            vm = new ViewModel.PlaylistViewModel();
            DataContext = vm;
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
                vm.SelectedMedia.Duration = "";
            };
            //Pause
            vm.PauseRequested += (e) =>
            {
                this.MediaPlayerElement.Pause();
            };
            //Media finished play
            this.MediaPlayerElement.MediaEnded += MediaPlayerElement_MediaEnded;
            //Media opened
            this.MediaPlayerElement.MediaOpened += MediaPlayerElement_MediaOpened;
        }

        private void MediaPlayerElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            var duration = this.MediaPlayerElement.NaturalDuration.TimeSpan.ToString();
            vm.SelectedMedia.Duration = "Tempo total: " + duration;
        }

        //detect that movie/audio finished, so go to next one
        private void MediaPlayerElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            vm.NextMediaCommand();
        }
    }
}
