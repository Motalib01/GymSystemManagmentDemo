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

namespace GymSystemManagment
{
    /// <summary>
    /// Interaction logic for UpdatePage.xaml
    /// </summary>
    public partial class UpdatePage : Window
    {
        GYMDBEntities DB = new GYMDBEntities();
        
        int Id;
        public UpdatePage(int memberId ,GYMDBEntities db)
        {
            InitializeComponent();
           
            DB = db;
            Id = memberId;
            LoadMemberData();
            
        }
        private void LoadMemberData()
        {
            try
            {
                var member = DB.Members.FirstOrDefault(m => m.MemberID == Id);
                if (member != null)
                {
                    // Fill text boxes with the member's information
                    FirstNameText.Text = member.FirstName;
                    LastNameText.Text = member.LastName;
                    PhoneNumberText.Text = member.PhoneNumber;
                    MembershipStatusText.Text = member.MemberStatus;
                    DatePickerJoinDate.SelectedDate = member.JoinDate;
                    DatePickerEndDate.SelectedDate = member.EndDate;
                }
                else
                {
                    MessageBox.Show($"لايوجد شخص بهذا {Id}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل البيانات: {ex.Message}");
            }
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            Member updateMember = (from m in DB.Members
                                   where m.MemberID == Id
                                   select m).Single();
            updateMember.FirstName = FirstNameText.Text;
            updateMember.LastName = LastNameText.Text;
            updateMember.PhoneNumber = PhoneNumberText.Text;
            updateMember.MemberStatus = MembershipStatusText.Text;
            updateMember.JoinDate = DatePickerJoinDate.SelectedDate.Value;
            updateMember.EndDate = DatePickerEndDate.SelectedDate.Value;


            DB.SaveChanges();
            MainWindow.datagrid.ItemsSource = DB.Members.ToList();
            this.Hide();
        }
    }
}
