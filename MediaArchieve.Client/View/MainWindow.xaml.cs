using System.Windows;
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
        }

        private void ButtonPower_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Sample5_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventargs)
        {
            throw new System.NotImplementedException();
        }
    }
}
 