﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MediaArchieve.Client.Helpers
{
    public class BaseModel :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}