using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaPlayer.Model
{
    public class PlayListHandle
    {
        public List<MediaItem> SelectDirectiory()
        {
            var mediaItensList = new List<MediaItem>();

            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                var basePath = fbd.SelectedPath;
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(basePath))
                {
                    var files = Directory.GetFiles(basePath, "*.*", SearchOption.AllDirectories).Where(s=>s.EndsWith(".mp3") || s.EndsWith(".mp4") || s.EndsWith(".mkv") || s.EndsWith(".wav"));

                    foreach (var file in files)
                    {
                        var mediaItem = new MediaItem()
                        {
                            Id = Guid.NewGuid(),
                            Path = file,
                            Title = Path.GetFileName(file),
                        };
                        mediaItensList.Add(mediaItem);
                    }
                }
            }
            return mediaItensList;
        }

        public List<MediaItem> SelectFiles()
        {
            var mediaItensList = new List<MediaItem>();

            using (var fbd = new OpenFileDialog())
            {
                fbd.Multiselect = true;
                fbd.Filter = "Audio File (*.mp3,*.mp4.*.wav)|*.mp3;*.mp4;*.wav |Video files (*.mp4,*.mkv)|mp4;*.mkv";
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && fbd.FileNames.Count() >0)
                {
                    foreach (var file in fbd.FileNames)
                    {
                        var mediaItem = new MediaItem()
                        {
                            Id = Guid.NewGuid(),
                            Path = file,
                            Title = Path.GetFileName(file),
                        };
                        mediaItensList.Add(mediaItem);
                    }
                }
            }
            return mediaItensList;
        }
    }
}
