using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Windows;
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Met();
        }
        private async void Met()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(Text.Text);
            var responseStr = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject(responseStr,new JsonSerializerSettings{TypeNameHandling = TypeNameHandling.All});

            MessageBox.Show("sda");
        }

        private async void Post()
        {
            var client = new HttpClient();
            var c = new Clip();
            var obj = JsonConvert.SerializeObject(c,
                new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All});
            var cont = new StringContent(obj, Encoding.UTF8, "application/json");
            var req = await client.PostAsync(Text.Text, cont);
            MessageBox.Show(req.ToString());
        }
        private void Button1_OnClick(object sender, RoutedEventArgs e)
        {
            Post();
        }
    }
}
 