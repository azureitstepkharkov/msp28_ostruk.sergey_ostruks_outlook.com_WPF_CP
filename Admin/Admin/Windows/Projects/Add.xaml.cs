using Admin.DataBase;
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

namespace Admin.Windows.Projects
{
    /// <summary>
    /// Interaction logic for Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        private int _id;
        public Add(int id)
        {
            InitializeComponent();
            _id = id;
            using (llblanca_lara1Entities db = new llblanca_lara1Entities())
            {
                var users = from u in db.users
                            join u_r in db.role_user on u.id equals u_r.user_id
                            join r in db.roles on u_r.role_id equals r.id
                            where r.display_name == "ProjectMan Role"
                            select new
                            {
                                Manager = u.name
                            };
                cmbManagers.ItemsSource = users.Select(role => role.Manager).ToList();
            }
        }
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            using (llblanca_lara1Entities db = new llblanca_lara1Entities())
            {
                projects project = new projects
                {
                    client_id = _id,
                    name = txtName.Text,
                    description = txtDescription.Text,
                    created_at = DateTime.Now,
                    status = txtStatus.Text,
                    //project_manager_id = cmbManagers.SelectedIndex + 1
                    project_manager_id = 9
                };
                try
                {
                    db.projects.Add(project);
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
