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
        private Folder _selectedFolder;

        public Folder SelectedFolder
        {
            get { return _selectedFolder; }
            set { _selectedFolder = value; OnPropertyChanged("SelectedFolder"); AsyncHelper.RunAsync(GetItemCollection);}
        }

        public ObservableCollection<Item> Items { get; set; }
        public Visibility EditWindowVisibility { get; set; }
        private Item _selectedItem;

        public Item SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged("SelectedItem"); }
        }
        public ICommand CancelEditItemCommand { get; set; }
        public ICommand AddItemCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }
        public ICommand RefreshItemsCommand { get; set; }
        public ICommand ClearItemsCommand { get; set; }
        public ICommand AcceptEditItemCommand { get; set; }
        public ICommand EditWindowCommand { get; set; }
        
        
        ItemsService _itemsService = new ItemsService();
        FoldersService _foldersService = new FoldersService();
        public MainWindowViewModel()
        {
            EditWindowVisibility  = Visibility.Collapsed;
            EditWindowCommand     = new RelayCommand(EditWindow);
            DeleteItemCommand     = new RelayCommand(DeleteItem);
            CancelEditItemCommand = new RelayCommand(CancelEditItem);    
            AcceptEditItemCommand = new RelayCommand(AcceptEditItem);    
            AddItemCommand        = new RelayCommand(AddItem);    
            RefreshItemsCommand   = new RelayCommand(RefreshItems);    
            ClearItemsCommand     = new RelayCommand(ClearItems);    
        }

        private async void ClearItems(object obj)
        {
            foreach (var item in Items)
                await _itemsService.DeleteItem(item);
            await GetItemCollection();
        }

        private async void RefreshItems(object obj) =>
            await GetItemCollection();

        private void AddItem(object obj)
        {
            
        }

        private async void DeleteItem(object obj)
        {
            await _itemsService.DeleteItem(SelectedItem);
            await GetItemCollection();
            SelectedItem = null;
        }

        private async void CancelEditItem(object obj)
        {
            await GetItemCollection();
            SelectedItem = null;
            EditWindowCommand.Execute(null);
        }
        
        private async void AcceptEditItem(object obj)
        {
            await _itemsService.UpdateItem(SelectedItem);
            SelectedItem = null;
            EditWindowCommand.Execute(null);
            await GetItemCollection();
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
            var collection = await _foldersService.GetAllFolders();
            Folders = new ObservableCollection<Folder>(collection);
            OnPropertyChanged("Folders");
        }
        
        public async Task GetItemCollection()
        {
            if (_selectedFolder != null)
            {
                var collection = await _itemsService.GetAllItems(_selectedFolder.Id);
                Items = new ObservableCollection<Item>(collection);
                OnPropertyChanged("Items");    
            }
            
        }
    }
}