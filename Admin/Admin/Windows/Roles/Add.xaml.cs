using Admin.DataBase;
using System;
using System.Windows;

namespace Admin.Windows.Roles
{
    /// <summary>
    /// Interaction logic for Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        public Add()
        {
            InitializeComponent();
        }
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            using (llblanca_lara1Entities db = new llblanca_lara1Entities())
            {
                roles role = new roles
                {
                    name = txtName.Text,
                    display_name = txtDisplayName.Text,
                    description = txtDescription.Text,
                    created_at = DateTime.Now
                };
                try
                {
                    db.roles.Add(role);
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
