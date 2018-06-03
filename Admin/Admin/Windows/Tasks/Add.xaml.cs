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

namespace Admin.Windows.Tasks
{
    /// <summary>
    /// Interaction logic for Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        private int _projectId;
        private int _userId;
        public Add(int UserId, int ProjectId)
        {
            InitializeComponent();
            _projectId = ProjectId;
            _userId = UserId;
        }
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            using (llblanca_lara1Entities db = new llblanca_lara1Entities())
            {
                tasks task = new tasks
                {
                    project_id = _projectId,
                    name = txtName.Text,
                    description = txtDescription.Text,
                    created_at = DateTime.Now,
                    status = txtStatus.Text,
                    start = Convert.ToDateTime(txtStart.Text),
                    end = Convert.ToDateTime(txtEnd.Text)
                };
               
                try
                {
                    db.tasks.Add(task);
                    db.SaveChanges();
                    task_user taskUser = new task_user()
                    {
                        created_at = DateTime.Now,
                        id_task = task.id,
                        id_user = _userId
                    };
                    db.task_user.Add(taskUser);
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
