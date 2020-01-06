using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MediaArchieve.Client.Helpers;
using MediaArchieve.Client.Model.ServerSide;
using MediaArchieve.Shared;

namespace MediaArchieve.Client.ViewModel
{
    public class MainWindowViewModel : BaseModel
    {
        public ObservableCollection<Folder> Folders { get; set; }
        public ObservableCollection<Folder> EditingFolder { get; set; } 
        public ICommand EditFolderCommand { get; set; } 

        private void EditFolder(object obj)
        {
            EditingFolder = new ObservableCollection<Folder>();
            EditingFolder.Add(Folders.FirstOrDefault());
            OnPropertyChanged("EditFolderName");
        }

        public MainWindowViewModel()
        {
            EditFolderCommand = new RelayCommand(EditFolder);
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