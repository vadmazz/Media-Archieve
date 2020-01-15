using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MediaArchieve.Client.Helpers;
using MediaArchieve.Client.Model;
using MediaArchieve.Client.Model.ServerSide;
using MediaArchieve.Shared;

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

        public string Icon { get; set; } = "FileDocumentEdit";
        public ObservableCollection<Item> Items { get; set; }
        public Visibility EditWindowVisibility { get; set; }
        public Visibility EditLabelVisibility { get
        {
            if (EditFolderVisibility == Visibility.Collapsed) return Visibility.Visible;
            else return Visibility.Collapsed;
        } }
        public Visibility EditFolderVisibility { get; set; }
        private Item _selectedItem;

        public Item SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged("SelectedItem"); }
        }
        public ICommand CancelEditItemCommand { get; }
        public ICommand DeleteItemCommand { get; }
        public ICommand RefreshItemsCommand { get; }
        public ICommand ClearItemsCommand { get; }
        public ICommand AcceptEditItemCommand { get; }
        public ICommand EditWindowCommand { get; }
        public ICommand EditFolderCommand { get; }
        public ICommand CreateItemCommand { get; set; }
        public ICommand UpdateFolderCommand { get; }
        
        ItemsService _itemsService = new ItemsService();
        FoldersService _foldersService = new FoldersService();
        ItemFactory _itemFactory = new ItemFactory();

        public MainWindowViewModel()
        {
            EditWindowVisibility  = Visibility.Collapsed;
            EditFolderVisibility  = Visibility.Collapsed;
            EditWindowCommand     = new RelayCommand(ShowEditItemWindow);
            EditFolderCommand     = new RelayCommand(ShowEditFolderWindow);
            DeleteItemCommand     = new RelayCommand(DeleteItem);
            CancelEditItemCommand = new RelayCommand(CancelEditItem);    
            AcceptEditItemCommand = new RelayCommand(AcceptEditItem);    
            CreateItemCommand     = new RelayCommand(CreateItem);
            RefreshItemsCommand   = new RelayCommand(RefreshItems);    
            ClearItemsCommand     = new RelayCommand(ClearItems);    
            UpdateFolderCommand   = new RelayCommand(UpdateFolder);    
        }

        private void ShowEditItemWindow(object n)
        {
            if (EditWindowVisibility == Visibility.Collapsed)
                EditWindowVisibility = Visibility.Visible;
            else
                EditWindowVisibility = Visibility.Collapsed;
            OnPropertyChanged("EditWindowVisibility");
        }
        private void ShowEditFolderWindow(object n)
        {
            if (EditFolderVisibility == Visibility.Collapsed)
                EditFolderVisibility = Visibility.Visible;
            else
                EditFolderVisibility = Visibility.Collapsed;
            OnPropertyChanged("EditFolderVisibility");
            OnPropertyChanged("EditLabelVisibility");
        }

        private async void UpdateFolder(object obj)
        {
            await _foldersService.UpdateFolder(SelectedFolder);
            await GetFolderCollection();
            ShowEditFolderWindow(null);
        }
        private async void ClearItems(object obj)
        {
            foreach (var item in Items)
                await _itemsService.DeleteItem(item);
            await GetItemCollection();
        }
        private async void RefreshItems(object obj) =>
            await GetItemCollection();

        private async void CreateItem(object obj)
        {
            var str = obj as String;
            if (str == null)
                throw new ArgumentException();
            //преобразуем строку из параметра в ItemType
            var item = _itemFactory.CreateItem((ItemType)Enum.Parse(typeof(ItemType), str));
            if (_selectedFolder == null)
                MessageBox.Show("Не выбрана папка");
            else
            {
                await _itemsService.CreateItem(item, _selectedFolder.Id);
                await GetItemCollection();
            }
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
            ShowEditItemWindow(null);
        }
        
        private async void AcceptEditItem(object obj)
        {
            await _itemsService.UpdateItem(SelectedItem);
            SelectedItem = null;
            await GetItemCollection();
            ShowEditItemWindow(null);

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