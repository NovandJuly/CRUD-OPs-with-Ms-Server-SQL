using CRUD_OPs.Model;
using CRUD_OPs.Repo;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for CRUDui.xaml
    /// </summary>
    public partial class CRUDui : Window
    {
        public CRUDui()
        {
            InitializeComponent();
            ReadClients();
        }

        private void ReadClients()
        {
            DataTable dataTable = new();

            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Email");
            dataTable.Columns.Add("Phone");
            dataTable.Columns.Add("Date");

            var repo = new ClientRepository();
            var clients = repo.GetClients();
            // MessageBox.Show("Rows loaded: " + dataTable.Rows.Count); //

            foreach (var client in clients)
            {
                var row = dataTable.NewRow();

                row["Id"] = client.Id;
                row["Name"] = client.FirstName + " " + client.LastName;
                row["Email"] = client.Email;
                row["Phone"] = client.Phone;
                row["Date"] = client.CreatedAt;

                dataTable.Rows.Add(row);
            }

            DataGridName.ItemsSource = dataTable.DefaultView;
        }

        private void AddButton_clicked(object sender, RoutedEventArgs e)
        {
            ClientManager clientManager = new();
            if (clientManager.ShowDialog() == true)
            {
                ReadClients();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridName.SelectedItem == null) return;

            DataRowView selectedRow = (DataRowView)DataGridName.SelectedItem;
            int clientId = Convert.ToInt32(selectedRow["Id"]);
            MessageBox.Show($"Selected Client ID: {clientId}", "Debug");

            var repo = new ClientRepository();
            var client = repo.GetClient(clientId); 
            MessageBox.Show($"Trying from GetClient: {client.Email}");

            if (client == null)
            {
                MessageBox.Show("Client not found.");
                return;
            }

            ClientManager clientManager = new();
            clientManager.EditClient(client);

            if (clientManager.ShowDialog() == true)
            {
                ReadClients();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)DataGridName.SelectedItem;
            int clientId = Convert.ToInt32(selectedRow["Id"]);

            MessageBoxResult dialogResult = MessageBox.Show("Are You Sure ?", "Delete Confirmation", MessageBoxButton.YesNo);

            if (dialogResult == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                var repo = new ClientRepository();
                repo.DeleteClient(clientId);

                ReadClients();
            }
        }
    }
}
