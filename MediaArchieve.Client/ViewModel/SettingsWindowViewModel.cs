using System;
using System.Windows;
using System.Windows.Input;
using MediaArchieve.Client.Helpers;
using MediaArchieve.Client.Model.ServerSide;

namespace MediaArchieve.Client.ViewModel
{
    public class SettingsWindowViewModel : BaseModel
    {
        public string Host { get; set; } 
        public string Url { get; set; } 
        public string Port { get; set; } 
        public ICommand CheckConnectionCommand { get; }
        
        private ServerConnection _connection;
        public SettingsWindowViewModel(ServerConnection connection)
        {
            _connection = connection;
            CheckConnectionCommand = new RelayCommand(CheckConnection);
            Host = ServerSettings.Host;
            Port = ServerSettings.Port.ToString();
        }

        private void CheckConnection(object obj)
        {
            ServerSettings.Host = Host;
            ServerSettings.Port = int.Parse(Port);
            try
            {
                _connection.CheckConnection();
                MessageBox.Show("Установлено успешно");
            }
            catch (InvalidConnectionException)
            {
                MessageBox.Show("Невозможно установить соединение. Проверьте IP и Host");
            }
            catch (Exception)
            {
                MessageBox.Show("Неизвестная ошибка");
            }
        }
    }
}