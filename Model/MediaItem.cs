using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayer.Model
{
    public class MediaItem : ObservableObject, ICloneable
    {
        private Guid _id;
        private string _path;
        private string _title;
        private string _duration;
        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
        public string Path
        {
            get => _path;
            set => SetProperty(ref _path, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Duration
        {
            get => _duration;
            set => SetProperty(ref _duration, value);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
