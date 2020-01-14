using System;
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
        public ICommand AddItemCommand { get; }
        public ICommand DeleteItemCommand { get; }
        public ICommand RefreshItemsCommand { get; }
        public ICommand ClearItemsCommand { get; }
        public ICommand AcceptEditItemCommand { get; }
        public ICommand EditWindowCommand { get; }
        public ICommand EditFolderCommand { get; }
        
        ItemsService _itemsService = new ItemsService();
        FoldersService _foldersService = new FoldersService();

        public MainWindowViewModel()
        {
            EditWindowVisibility  = Visibility.Collapsed;
            EditFolderVisibility  = Visibility.Collapsed;
            EditWindowCommand     = new RelayCommand(ShowEditItemWindow);
            EditFolderCommand     = new RelayCommand(ShowEditFolderWindow);
            DeleteItemCommand     = new RelayCommand(DeleteItem);
            CancelEditItemCommand = new RelayCommand(CancelEditItem);    
            AcceptEditItemCommand = new RelayCommand(AcceptEditItem);    
            AddItemCommand        = new RelayCommand(AddItem);    
            RefreshItemsCommand   = new RelayCommand(RefreshItems);    
            ClearItemsCommand     = new RelayCommand(ClearItems);    
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
        private async void ClearItems(object obj)
        {
            foreach (var item in Items)
                await _itemsService.DeleteItem(item);
            await GetItemCollection();
        }
        private async void RefreshItems(object obj) =>
            await GetItemCollection();

        private async void AddItem(object obj)
        {
            if (_selectedFolder == null)
                MessageBox.Show("Не выбрана папка");
            else
            {
                var newItem = new Item {Name = "Новая папка", Description = "Добавьте описание"};
                await _itemsService.CreateItem(newItem, _selectedFolder.Id);
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
            ChangeVisibility(EditWindowVisibility);
        }
        
        private async void AcceptEditItem(object obj)
        {
            await _itemsService.UpdateItem(SelectedItem);
            SelectedItem = null;
            ChangeVisibility(EditWindowVisibility);
            await GetItemCollection();
        }

        private void ChangeVisibility(Visibility visibility)
        {
            if (visibility == Visibility.Collapsed)
                visibility = Visibility.Visible;
            else
                visibility = Visibility.Collapsed;
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