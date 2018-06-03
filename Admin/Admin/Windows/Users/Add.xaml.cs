using Admin.DataBase;
using System;
using System.Linq;
using System.Windows;

namespace Admin
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        public Add()
        {
            InitializeComponent();
            using (llblanca_lara1Entities db = new llblanca_lara1Entities())
            {
                cmbRoles.ItemsSource = db.roles.Select(role => role.display_name).ToList();
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            using (llblanca_lara1Entities db = new llblanca_lara1Entities())
            {                
                if(txtPassword.Password != txtCPassword.Password)
                {
                    MessageBox.Show("Passwords do not match!");
                    return;
                }
                users user = new users
                {
                    name = txtName.Text,
                    email = txtEmail.Text,
                    password = txtPassword.Password,
                    status = "active",
                    remember_token = "",
                    created_at = DateTime.Now
                };
                try
                {
                    db.users.Add(user);
                    db.SaveChanges();
                    int id = user.id;
                    role_user r_u = new role_user()
                    {
                        role_id = cmbRoles.SelectedIndex + 1,
                        user_id = id
                    };
                    db.role_user.Add(r_u);
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
