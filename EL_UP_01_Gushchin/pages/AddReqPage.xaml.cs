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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EL_UP_01_Gushchin.pages
{
    /// <summary>
    /// Логика взаимодействия для AddReqPage.xaml
    /// </summary>
    public partial class AddReqPage : Page
    {
        private Requests _currentRequest = new Requests();
        public AddReqPage(Requests selectedRequest)
        {
            InitializeComponent();

            if (selectedRequest != null)
                _currentRequest = selectedRequest;
            cmbPriority.ItemsSource = Entities.GetContext().Priorities.Select(x => x.name).ToList();
            cmbEquipment.ItemsSource = Entities.GetContext().Equipment.Select(x => x.name).ToList();
            cmbProblem.ItemsSource = Entities.GetContext().Problems.Select(x => x.name).ToList();
            cmbClients.ItemsSource = Entities.GetContext().Clients.Select(x => x.second_name + " " + x.first_name + " " + x.last_name).ToList();
            cmbWorker.ItemsSource = Entities.GetContext().Workers.Select(x => x.second_name + " " + x.first_name + " " + x.lastname).ToList();
            cmbStatus.ItemsSource = Entities.GetContext().Statuses.Select(x => x.name).ToList();

            DataContext = _currentRequest;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {


            DateTime datetimeCreated;
            DateTime datetimeClosed;
            try
            {
                datetimeCreated = DateTime.Parse(TextBoxDTCreated.Text);
                datetimeClosed = DateTime.Parse(TextBoxDTClosed.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }


            int priorityID = Entities.GetContext().Priorities.Where(x => x.name == cmbPriority.SelectedValue.ToString()).FirstOrDefault().id;
            int equipmentID = Entities.GetContext().Equipment.Where(x => x.name == cmbEquipment.SelectedValue.ToString()).FirstOrDefault().id;
            int problemID = Entities.GetContext().Problems.Where(x => x.name == cmbProblem.SelectedValue.ToString()).FirstOrDefault().id;
            int clientID = Entities.GetContext().Clients.Where(x => (x.second_name + " " + x.first_name + " " + x.last_name) == cmbClients.SelectedValue.ToString()).FirstOrDefault().id;
            int workerID = Entities.GetContext().Workers.Where(x => (x.second_name + " " + x.first_name + " " + x.lastname) == cmbWorker.SelectedValue.ToString()).FirstOrDefault().id;
            int statusID = Entities.GetContext().Statuses.Where(x => x.name == cmbStatus.SelectedValue.ToString()).FirstOrDefault().id;

            var request = new Requests
            {
                datetime_created = datetimeCreated,
                datetime_closed = datetimeClosed,
                id_priority = priorityID,
                id_equipment = equipmentID,
                serial_number_of_equipment = TextBoxNumOfEquipment.Text,
                id_type_of_problem = problemID,
                description = TextBoxDescription.Text,
                id_client = clientID,
                id_worker = workerID,
                id_status = statusID,
            };



            //Делаем попытку записи данных в БД о новой заявке
            try
            {
                Entities.GetContext().Requests.Add(request);
                Entities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void TextBoxDTCreated_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBlockDTCrated.Visibility = Visibility.Visible;
            if (TextBoxDTCreated.Text.Length > 0)
            {
                TextBlockDTCrated.Visibility = Visibility.Hidden;
            }
        }

        private void TextBoxDTClosed_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBlockDTClosed.Visibility = Visibility.Visible;
            if (TextBoxDTClosed.Text.Length > 0)
            {
                TextBlockDTClosed.Visibility = Visibility.Hidden;
            }
        }

        private void TextBoxNumOfEquipment_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBlockNumOfEquipment.Visibility = Visibility.Visible;
            if (TextBoxNumOfEquipment.Text.Length > 0)
            {
                TextBlockNumOfEquipment.Visibility = Visibility.Hidden;
            }
        }

        private void TextBoxDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBlockDescription.Visibility = Visibility.Visible;
            if (TextBoxDescription.Text.Length > 0)
            {
                TextBlockDescription.Visibility = Visibility.Hidden;
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack) NavigationService.GoBack();
        }

        private void UpdateChart(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
