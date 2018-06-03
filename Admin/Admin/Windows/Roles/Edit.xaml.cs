using Admin.DataBase;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace Admin.Windows.Roles
{
    /// <summary>
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        private string[] _roleTest;
        public Edit(string[] roleTest)
        {
            InitializeComponent();
            _roleTest = roleTest;
            txtName.Text = _roleTest[3].TrimStart(' ');
            txtDisplayName.Text = _roleTest[5].TrimStart(' ');
            txtDescription.Text = _roleTest[7].TrimStart(' ');
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            using (llblanca_lara1Entities db = new llblanca_lara1Entities())
            {
                int id = Convert.ToInt32(_roleTest[1].TrimStart(' '));
                roles role = db.roles.Where(u => u.id == id).FirstOrDefault();
                db.roles.Attach(role);
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();

                role.name = txtName.Text;
                role.display_name = txtDisplayName.Text;
                role.description = txtDescription.Text;
                role.updated_at = DateTime.Now;
                try
                {
                    db.roles.Add(role);
                    MessageBox.Show($"Role {role.name} is update");
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
