using employeeApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace employeeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EmployeesContext db = new EmployeesContext();
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;

            
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // гарантируем, что база данных создана
            db.Database.EnsureCreated();
            // загружаем данные из БД
            //var list =   db.Employees.ToList();
            db.Employees.Load();
            // и устанавливаем данные в качестве контекста
            DataContext = db.Employees.Local.ToObservableCollection();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow EmployeeWindow = new AddEmployeeWindow(new Employee());
            if (EmployeeWindow.ShowDialog() == true)
            {              
                Employee employee = EmployeeWindow.Employee;
                employee.Id = Guid.NewGuid();
                db.Employees.Add(employee);
                db.SaveChanges();
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Employee? employee = employeeGrid.SelectedItem as Employee;
            // если ни одного объекта не выделено, выходим
            if (employee is null)
            {
                MessageBox.Show("Выделите сотрудника, которого хотите удалить");
                return;
            }
            
            try
            {
                db.Employees.Remove(employee);
                db.SaveChanges();
                MessageBox.Show("Сотрудник удален");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
