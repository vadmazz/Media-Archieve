using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using MediaArchieve.Client.ViewModel;
using MediaArchieve.Client.Helpers;
using MediaArchieve.Shared;
using MediaArchieve.Shared.Items;

namespace MediaArchieve.Client.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            var vm = DataContext as MainWindowViewModel;
            AsyncHelper.RunAsync(vm.GetFolderCollection);
            AsyncHelper.RunAsync(vm.GetItemCollection);
        }

        private void ButtonPower_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void listView_Click(object sender, MouseButtonEventArgs e)
        {
            var vm = DataContext as MainWindowViewModel;
            vm.SelectedItem = (Item)(sender as ListViewItem).DataContext;
        }
        
    }
}

 