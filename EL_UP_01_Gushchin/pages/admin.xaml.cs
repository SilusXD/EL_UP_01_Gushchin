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
    /// Логика взаимодействия для admin.xaml
    /// </summary>
    public partial class admin : Page
    {
        public admin()
        {
            InitializeComponent();
            DataGridReq.ItemsSource = Entities.GetContext().Requests.ToList();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Обновление таблицы с данными о заявках при каждой перезагрузке станицы

            if (Visibility == Visibility.Visible)
            {
                Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                DataGridReq.ItemsSource = Entities.GetContext().Requests.ToList();
            }
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            var requestsForRemoving = DataGridReq.SelectedItems.Cast<Requests>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить записи в количестве {requestsForRemoving.Count()} элементов?", "Внимание",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Entities.GetContext().Requests.RemoveRange(requestsForRemoving);
                    Entities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");

                    DataGridReq.ItemsSource = Entities.GetContext().Requests.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddReqPage(null));
        }

        private void ButtonReport_Click(object sender, RoutedEventArgs e)
        {
            int totalRequests = Entities.GetContext().Requests.Count();
            int completedRequests = Entities.GetContext().Requests.Count(r => r.id_status == 2);
            int passedRequests = Entities.GetContext().Requests.Count(r => r.id_status == 5);
            string reportMessage = $"Общее количество заявок: {totalRequests}\n" + $"Выполненных заявок: {completedRequests}\n" + $"Сдано: {passedRequests}";
            MessageBox.Show(reportMessage, "Отчет", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void UpdateRequests()
        {
            //загружаем все заявки в список
            var currentRequest = Entities.GetContext().Requests.ToList();

            //осуществляем сортировку в зависимости от выбора статуса
            if (cmbStatus.SelectedIndex == 0)
                DataGridReq.ItemsSource = currentRequest.Where(x => x.id_status == 1).ToList();
            else if (cmbStatus.SelectedIndex == 1)
                DataGridReq.ItemsSource = currentRequest.Where(x => x.id_status == 2).ToList();
            else if (cmbStatus.SelectedIndex == 2)
                DataGridReq.ItemsSource = currentRequest.Where(x => x.id_status == 3).ToList();
            else if (cmbStatus.SelectedIndex == 3)
                DataGridReq.ItemsSource = currentRequest.Where(x => x.id_status == 4).ToList();
            else if (cmbStatus.SelectedIndex == 4)
                DataGridReq.ItemsSource = currentRequest.Where(x => x.id_status == 5).ToList();
        }

        private void cmbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateRequests();
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            cmbStatus.SelectedIndex = -1;
            DataGridReq.ItemsSource = Entities.GetContext().Requests.ToList();
        }
    }
}
