using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace GymSystemManagment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GYMDBEntities DB = new GYMDBEntities();
        public static DataGrid datagrid;
        public MainWindow()
        {
            InitializeComponent();
            Load();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource memberViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("memberViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // memberViewSource.Source = [generic data source]
            _ = ((System.Windows.Data.CollectionViewSource)(this.FindResource("memberViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // memberViewSource.Source = [generic data source]
        }

        private void memberDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Load(string searchName = "")
        {
            memberDataGrid.ItemsSource = DB.Members.ToList();
            datagrid = memberDataGrid;
            var query = DB.Members.AsQueryable();

            // Apply the search filter if a name is provided
            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(member => member.FirstName.Contains(searchName) || member.LastName.Contains(searchName));
            }

            // Update the memberDataGrid.ItemsSource
            memberDataGrid.ItemsSource = query.ToList();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (memberDataGrid.SelectedItem is Member selectedMember)
                {
                    int Id = (memberDataGrid.SelectedItem as Member).MemberID;
                    var deleteMember = DB.Members.SingleOrDefault(m => m.MemberID == Id);

                    if (deleteMember != null)
                    {
                        DB.Members.Remove(deleteMember);
                        DB.SaveChanges();
                        memberDataGrid.ItemsSource = DB.Members.ToList();
                    }
                    else
                    {
                        MessageBox.Show("لا يوجد شخص", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                //else
                //{
                //    MessageBox.Show("لم يحدد شخص", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {
            Member newMember = new Member();
            try
            {
                if (true)
                {
                    // Assuming FirstNameText, LastNameText, DatePickerJoinDate,
                    // PhoneNumberText, and MembershipStatusText are UI controls or variables.

                    // Check if the required fields are not empty
                    if (!string.IsNullOrEmpty(FirstNameText.Text) &&
                        !string.IsNullOrEmpty(LastNameText.Text) &&
                        DatePickerJoinDate.SelectedDate.HasValue &&
                        !string.IsNullOrEmpty(PhoneNumberText.Text) &&
                        !string.IsNullOrEmpty(MembershipStatusText.Text))
                    {
                        // Set properties for the newMember object
                        newMember.FirstName = FirstNameText.Text;
                        newMember.LastName = LastNameText.Text;
                        newMember.JoinDate = DatePickerJoinDate.SelectedDate.Value;
                        newMember.EndDate = DatePickerEndDate.SelectedDate.Value;
                        newMember.PhoneNumber = PhoneNumberText.Text;
                        newMember.MemberStatus = MembershipStatusText.Text;
                    }
                    else
                    {
                        MessageBox.Show("املئ الخانات كاملة", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                FirstNameText.Text = "";
                LastNameText.Text = "";
                PhoneNumberText.Text = "";
                MembershipStatusText.Text = "";
                DatePickerJoinDate.SelectedDate = DateTime.Today;
                DatePickerEndDate.SelectedDate =DateTime.Today ;


                DB.Members.Add(newMember);
                DB.SaveChanges();
                MainWindow.datagrid.ItemsSource = DB.Members.ToList();
            }
            catch (Exception)
            {

                MessageBox.Show("لم يضف الشخص", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (memberDataGrid.SelectedItem is Member selectedMember)
                {
                    int Id = selectedMember.MemberID;
                    UpdatePage Upage = new UpdatePage(Id, DB);

                    if (Upage != null)
                    {
                        Upage.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("لايوجد شخص ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text;
            Load(searchText);
        }
    }
    public class DatePassedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime endDate)
            {
                return endDate.Date < DateTime.Now.Date;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}

