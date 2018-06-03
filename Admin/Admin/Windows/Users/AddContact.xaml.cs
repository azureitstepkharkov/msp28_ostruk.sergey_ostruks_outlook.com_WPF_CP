using Admin.DataBase;
using System;
using System.Linq;
using System.Windows;

namespace Admin.Windows.Users
{
    /// <summary>
    /// Interaction logic for AddContact.xaml
    /// </summary>
    public partial class AddContact : Window
    {
        private int _id;
        public AddContact(int id)
        {
            InitializeComponent();
            _id = id;
            using (llblanca_lara1Entities db = new llblanca_lara1Entities())
            {
                var type = from u in db.contact_types
                           select new
                           {
                               Id = u.id,
                               Type = u.contact_type
                           };
                cmbType.ItemsSource = type.Select(role => role.Type).ToList();
            }
        }
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            using (llblanca_lara1Entities db = new llblanca_lara1Entities())
            {
                contacts contact = new contacts
                {
                    value = txtContact.Text,
                    user_id = _id,
                    type_id = cmbType.SelectedIndex + 1
                };
                try
                {
                    db.contacts.Add(contact);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            this.Close();
        }
    }
}
