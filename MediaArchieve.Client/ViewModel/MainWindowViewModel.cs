using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MediaArchieve.Client.Helpers;
using MediaArchieve.Client.Model.ServerSide;
using MediaArchieve.Shared;
using MediaArchieve.Shared.Items;

namespace MediaArchieve.Client.ViewModel
{
    public class MainWindowViewModel : BaseModel
    {
        public ObservableCollection<Folder> Folders { get; set; }
        public ObservableCollection<Item> Items { get; set; }
        public Visibility EditWindowVisibility { get; set; }
        public Folder SelectedFolder { get; set; }
        private Document _selectedItem; 
        public Document SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }
        public ICommand EditWindowCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        
        public MainWindowViewModel()
        {
            EditWindowVisibility = Visibility.Collapsed;
            EditWindowCommand = new RelayCommand(EditWindow);
            SelectionChangedCommand = new RelayCommand(items =>
            {
                var itemList = (items as ObservableCollection<object>).Cast<Item>().ToList();
            });
        }
        
        private void EditWindow(object obj)
        {
            if (EditWindowVisibility == Visibility.Collapsed)
                EditWindowVisibility = Visibility.Visible;
            else
                EditWindowVisibility = Visibility.Collapsed;
            OnPropertyChanged("EditWindowVisibility");
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
            var collection = await itemsService.GetAllItems(4);
            Items = new ObservableCollection<Item>(collection);
            OnPropertyChanged("Items");
        }
    }
}