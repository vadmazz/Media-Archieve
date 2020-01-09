using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using MediaArchieve.Client.ViewModel;
using MediaArchieve.Client.Helpers;

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
        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = DataContext as MainWindowViewModel;
            //vm.EditWindowCommand.Execute(null);
            
        }
    }
}
 