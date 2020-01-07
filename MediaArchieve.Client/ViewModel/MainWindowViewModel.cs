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
        public ObservableCollection<Item> Items { get; set; }
        
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
        
        public async Task GetItemCollection()
        {
            var itemsService = new ItemsService();
            var collection = await itemsService.GetAllItems();
            Items = new ObservableCollection<Item>(collection);
            OnPropertyChanged("Items");
        }
    }
}