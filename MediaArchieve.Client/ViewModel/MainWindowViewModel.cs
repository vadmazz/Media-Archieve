using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using MediaArchieve.Client.Helpers;
using MediaArchieve.Client.Model.ServerSide;
using MediaArchieve.Shared;

namespace MediaArchieve.Client.ViewModel
{
    public class MainWindowViewModel : BaseModel
    {
        public ObservableCollection<Folder> Folders { get; set; }


        public MainWindowViewModel()
        {

        }

        public async Task GetFolderCollection()
        {
            FoldersService foldersService = new FoldersService();
            var collection = await foldersService.GetAllFolders();
            Folders = new ObservableCollection<Folder>(collection);
            OnPropertyChanged("Folders");
        }
        
    }
}