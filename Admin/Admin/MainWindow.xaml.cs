using Admin.DataBase;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Admin
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _dr;
        private string[] test;
        private string[] projectTest;
        private string[] taskTest;
        private string[] roleTest;
        private string role_in_dataGrid;
        private string nameBeChange;

        public MainWindow()
        {
            InitializeComponent();
            fillGridView();
        }

        #region DataBase
        private void fillGridView()
        {
            DataContext = this;
            try
            {
                FillingFromDB();
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message);
            }
        }
        private void FillingFromDB()
        {
            using (llblanca_lara1Entities db = new llblanca_lara1Entities())
            {
                var users = from u in db.users
                            select new
                            {
                                Id = u.id,
                                Name = u.name,
                                Email = u.email,                               
                                Status = u.status,
                                Created = u.created_at,
                                Updated = u.updated_at,
                                Password = u.password
                            };
                var roles = from u in db.roles
                            select new
                            {
                                Id = u.id,
                                Name = u.name,
                                DisplayName = u.display_name,
                                Description = u.description,
                                Created = u.created_at,
                                Updated = u.updated_at
                            };
                gv_users.ItemsSource = users.ToList();
                gv_roles.ItemsSource = roles.ToList();
            }
        }
        private void FillingFromDBProjects(llblanca_lara1Entities db)
        {
            var projects = from project in db.projects
                           join users in db.users on project.client_id equals users.id
                           join managers in db.users on project.project_manager_id equals managers.id
                           where users.name == txtName.Text
                           select new
                           {
                               Id = project.id,
                               Project = project.name,
                               Status = project.status,
                               Description = project.description,
                               Manager = managers.name
                           };
            dg_UsersProjects.ItemsSource = projects.ToList();
        }
        private void fillingFromDBTasks(llblanca_lara1Entities db, projects project)
        {
            var tasks = from task in db.tasks
                        where task.project_id == project.id
                        select new
                        {
                            Id = task.id,
                            Name = task.name,
                            Description = task.description,
                            Project_id = task.project_id,
                            Start = task.start,
                            End = task.end,
                            Status = task.status,
                            Created = task.created_at,
                            Updated = task.updated_at
                        };
            dg_UsersTasks.ItemsSource = tasks.ToList();
        }
        #endregion

        #region Events
        private void GV_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            _dr = dg.SelectedItem.ToString();
            test = _dr.Split(new char[4] { '=', ',', '{', '}' }, StringSplitOptions.RemoveEmptyEntries);
            nameBeChange = test[3].TrimStart(' ');
            txtName.Text = test[3].TrimStart(' ');
            txtEmail.Text = test[5].TrimStart(' ');
            using (llblanca_lara1Entities db = new llblanca_lara1Entities())
            {
                users user = db.users.Where(u => u.name == nameBeChange).FirstOrDefault();
                role_user r_u = db.role_user.Where(u => u.user_id == user.id).FirstOrDefault();
                roles role = db.roles.Where(r => r.id == r_u.role_id).FirstOrDefault();
                var contacts = db.contacts.Join(db.contact_types, t => t.type_id, c => c.id, (t, c) => new {
                    value = t.value,
                    type = c.contact_type,
                    t.user_id
                }).Where(r => r.user_id == user.id).Select(p => p.type + ": " + p.value);
                cmbRoles.ItemsSource = db.roles.Select(r => r.display_name).ToList();
                cmbRoles.SelectedItem = db.roles.Where(u => u.display_name == role.display_name).FirstOrDefault().display_name;
                role_in_dataGrid = db.roles.Where(u => u.display_name == role.display_name).FirstOrDefault().display_name;
                lstContacts.ItemsSource = contacts.ToList();
                FillingFromDBProjects(db);
            }
        }
        private void GV_RoleMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            if (dg.SelectedItem == null)
                return;
            _dr = dg.SelectedItem.ToString();
            roleTest = _dr.Split(new char[4] { '=', ',', '{', '}' }, StringSplitOptions.RemoveEmptyEntries);
        }
        private void GV_ProjectMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            if (dg.SelectedItem == null)
                return;
            _dr = dg.SelectedItem.ToString();
            projectTest = _dr.Split(new char[4] { '=', ',', '{', '}' }, StringSplitOptions.RemoveEmptyEntries);
            string proj = projectTest[3].TrimStart(' ');
            using (llblanca_lara1Entities db = new llblanca_lara1Entities())
            {
                projects project = db.projects.Where(u => u.name == proj).FirstOrDefault();
                fillingFromDBTasks(db, project);
            }
        }
        private void GV_TaskMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            if (dg.SelectedItem == null)
                return;
            _dr = dg.SelectedItem.ToString();
            taskTest = _dr.Split(new char[4] { '=', ',', '{', '}' }, StringSplitOptions.RemoveEmptyEntries);
        }
        #endregion

        #region User
        private void btnUsers_Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Add add = new Add();
                add.ShowDialog();
                FillingFromDB();
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message);
            }
        }
        private void btnUsers_Delete_Click(object sender, RoutedEventArgs e)
        {
            string temp = test[3].TrimStart(' ');
            MessageBoxResult result = MessageBox.Show($"Вы точно уверены, что хотите удалить user {temp}?", "Delete", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                using (llblanca_lara1Entities db = new llblanca_lara1Entities())
                {
                    users user = db.users.Where(u => u.name == temp).FirstOrDefault();
                    var project = (from phone in db.projects
                              where phone.client_id == user.id
                              select phone).ToList();
                    if (project != null)
                    {
                        foreach(projects p in project)
                        {
                            var tas = (from t in db.tasks
                                        where t.project_id == p.id
                                        select t).ToList();
                            if (tas != null)
                            {
                                db.tasks.RemoveRange(tas);
                                db.SaveChanges();
                            }
                        }                       
                        db.projects.RemoveRange(project);
                        db.SaveChanges();
                    }                 
                    var contact = (from c in db.contacts
                                   where c.user_id == user.id
                                   select c).ToList();
                    if (contact != null)
                    {
                        db.contacts.RemoveRange(contact);
                        db.SaveChanges();
                    }
                    db.users.Remove(user);
                    db.SaveChanges();
                    MessageBox.Show($"User {temp} is delete");
                    FillingFromDB();
                }
            }
            else
            {
                return;
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            string name = String.Empty;
            try
            {
                using (llblanca_lara1Entities db = new llblanca_lara1Entities())
                {
                    users user = db.users.Where(u => u.name == nameBeChange).FirstOrDefault();
                    db.users.Attach(user);
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();

                    name = user.name;
                    user.name = txtName.Text;
                    user.email = txtEmail.Text;
                    user.updated_at = DateTime.Now;
                    user.password = txtPassword.Password;
                    roles role = db.roles.Where(r => r.display_name == role_in_dataGrid).FirstOrDefault();
                    role_user r_u = db.role_user.Where(u => u.user_id == user.id).Where(r => r.role_id == role.id).FirstOrDefault();
                    db.role_user.Attach(r_u);
                    db.Entry(r_u).State = EntityState.Modified;
                    db.SaveChanges();

                    r_u.user_id = user.id;
                    r_u.role_id = cmbRoles.SelectedIndex + 1;
                    db.users.Add(user);
                    db.role_user.Add(r_u);
                }
                MessageBox.Show($"User {name} is update");
                FillingFromDB();
            }
            catch(Exception ex)
            {
                MessageBox.Show("User is not selected");
            }    
        }
        private void btnAddContact_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int Id = Convert.ToInt32(test[1].TrimStart(' '));
                Admin.Windows.Users.AddContact form = new Admin.Windows.Users.AddContact(Id);
                form.ShowDialog();
                FillingFromDB();
            }
            catch (Exception msg)
            {
                MessageBox.Show("User is not selected");
            }
        }
        private void btnRemoveContact_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string value = lstContacts.SelectedValue.ToString().Split(':')[1].TrimStart(' ');
                MessageBoxResult result = MessageBox.Show($"Вы точно уверены, что хотите удалить Contact {value}?", "Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    using (llblanca_lara1Entities db = new llblanca_lara1Entities())
                    {
                        contacts contact = db.contacts.Where(c => c.value == value).FirstOrDefault();
                        db.contacts.Remove(contact);
                        db.SaveChanges();
                        MessageBox.Show($"Contact {contact} is delete");
                        FillingFromDB();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Contact is not selected");
            }
        }
        #endregion

        #region Role
        private void roles_Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Admin.Windows.Roles.Add add = new Admin.Windows.Roles.Add();
                add.ShowDialog();
                FillingFromDB();
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message);
            }
        }
        private void roles_Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Admin.Windows.Roles.Edit edit = new Admin.Windows.Roles.Edit(roleTest);
                edit.ShowDialog();
                FillingFromDB();
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message);
            }
        }
        private void roles_Delete_Click(object sender, RoutedEventArgs e)
        {
            string temp = roleTest[3].TrimStart(' ');
            MessageBoxResult result = MessageBox.Show($"Вы точно уверены, что хотите удалить role {temp}?", "Delete", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                using (llblanca_lara1Entities db = new llblanca_lara1Entities())
                {
                    roles role = db.roles.Where(u => u.name == temp).FirstOrDefault();
                    db.roles.Remove(role);
                    db.SaveChanges();
                    MessageBox.Show($"Role {temp} is delete");
                    FillingFromDB();
                }
            }
        }
        #endregion

        #region Project
        private void btnRemoveProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string project = projectTest[3].TrimStart(' ');
                MessageBoxResult result = MessageBox.Show($"Вы точно уверены, что хотите удалить проект {projectTest[3].TrimStart(' ')}?", "Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    using (llblanca_lara1Entities db = new llblanca_lara1Entities())
                    {
                        projects proj = db.projects.Where(p => p.name == project).FirstOrDefault();
                        var tas = (from t in db.tasks
                                   where t.project_id == proj.id
                                   select t).ToList();
                        if (tas != null)
                        {
                            db.tasks.RemoveRange(tas);
                            db.SaveChanges();
                        }
                        db.projects.Remove(proj);
                        db.SaveChanges();
                        MessageBox.Show($"Project {projectTest[3].TrimStart(' ')} is delete");
                        FillingFromDBProjects(db);
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void btnAddProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int Id = Convert.ToInt32(test[1].TrimStart(' '));
                Admin.Windows.Projects.Add form = new Admin.Windows.Projects.Add(Id);
                form.ShowDialog();
                using (llblanca_lara1Entities db = new llblanca_lara1Entities())
                {
                    FillingFromDBProjects(db);
                }
            }
            catch (Exception msg)
            {
                MessageBox.Show("User is not selected");
            }
        }
        #endregion

        #region Task
        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int Id = Convert.ToInt32(test[1].TrimStart(' '));
                int ProjectId = Convert.ToInt32(projectTest[1].TrimStart(' '));
                Admin.Windows.Tasks.Add form = new Admin.Windows.Tasks.Add(Id, ProjectId);
                form.ShowDialog();
                using (llblanca_lara1Entities db = new llblanca_lara1Entities())
                {
                    FillingFromDBProjects(db);
                }
            }
            catch (Exception msg)
            {
                MessageBox.Show("User/Project is not selected");
            }
        }
        private void btnRemoveTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int taskId = Convert.ToInt32(taskTest[1].TrimStart(' '));
                MessageBoxResult result = MessageBox.Show($"Вы точно уверены, что хотите удалить проект {taskTest[3].TrimStart(' ')}?", "Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    using (llblanca_lara1Entities db = new llblanca_lara1Entities())
                    {
                        tasks t = db.tasks.Where(p => p.id == taskId).FirstOrDefault();
                        task_user tas = db.task_user.Where(ts => ts.id_task == t.id).FirstOrDefault();
                        if (tas != null)
                        {
                            db.task_user.Remove(tas);
                            db.SaveChanges();
                        }
                        db.tasks.Remove(t);
                        db.SaveChanges();
                        MessageBox.Show($"Task {taskTest[3].TrimStart(' ')} is delete");
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Task is not selected");
            }
        }
        #endregion
    }
}
