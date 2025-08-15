using CRUD_OPs.Model;
using CRUD_OPs.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CRUD_OPs
{
    /// <summary>
    /// Interaction logic for ClientManager.xaml
    /// </summary>
    public partial class ClientManager : Window
    {
        public ClientManager()
        {
            InitializeComponent();
        }
        private int clientID = 0; 
        public void EditClient(Client client)
        { 
            CreateEditLabel.Content = "EDIT CLIENT";
            IdTBox.Visibility = Visibility.Visible;
            IdTBox.IsReadOnly = true;
            IdTBox.Background = new SolidColorBrush(Colors.LightGray);

            IdTBox.Text = client.Id.ToString();
            FirstnameTBox.Text = client.FirstName;
            LastNameTBox.Text = client.LastName;
            EmailTBox.Text = client.Email;
            AddressTBox.Text = client.Address;
            PhoneTBox.Text = client.Phone;
            clientID = client.Id;

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            var repo = new ClientRepository();
            Client client = new();
            client.Id = clientID;
            client.FirstName = FirstnameTBox.Text;
            client.LastName = LastNameTBox.Text;
            client.Email = EmailTBox.Text;
            client.Phone = PhoneTBox.Text;
            client.Address = AddressTBox.Text;

            if (clientID == 0)
            {
                repo.CreateClient(client);
            }
            else
                repo.UpdateClient(client);

            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
