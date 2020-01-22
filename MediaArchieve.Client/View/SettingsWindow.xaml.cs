using System.Windows;
using MediaArchieve.Client.Model.ServerSide;
using MediaArchieve.Client.ViewModel;

namespace MediaArchieve.Client.View
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow(ServerConnection connection)
        {
            InitializeComponent();
            DataContext = new SettingsWindowViewModel(connection);
        }
        
        private void ButtonPower_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
        private void Minimize_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}