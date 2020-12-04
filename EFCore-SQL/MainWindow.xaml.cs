using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;


namespace EFCore_SQL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new SQLiteDBContext())
            {
                // Create
                Debug.WriteLine("Add New Employee: ");
                db.Add<Employee>(new Employee { FirstName = "John", LastName = "Doe", Age = 55 });
                db.SaveChanges();

                Debug.WriteLine("Employee has been added sucessfully.");

                UpdateGrid();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (var db = new SQLiteDBContext())
            {
                // Read
                Debug.WriteLine("Querying table for that employee.");

                var employee = db.Employee
                    .OrderBy(b => b.Id)
                    .FirstOrDefault();

                if (employee != null)
                {
                    Debug.WriteLine("The employee found: {0} {1} and is {2} years old.", employee.FirstName, employee.LastName, employee.Age);
                }

                UpdateGrid();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using (var db = new SQLiteDBContext())
            {
                // Update
                Debug.WriteLine("Updating the employee first name and age.");

                var employee = db.Employee
                .OrderBy(b => b.Id)
                .FirstOrDefault();

                if (employee != null)
                {
                    employee.FirstName = "Louis";
                    employee.Age = 90;

                    Debug.WriteLine("Newly updated employee is: {0} {1} and is {2} years old.", employee.FirstName, employee.LastName, employee.Age);

                    db.SaveChanges();
                }

                UpdateGrid();

            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            using (var db = new SQLiteDBContext())
            {
                var employee = db.Employee
                .OrderBy(b => b.Id)
                .FirstOrDefault();

                // Delete
                Debug.WriteLine("Delete the employee.");

                if (employee != null)
                {
                    db.Remove(employee);
                    db.SaveChanges();
                }

                UpdateGrid();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            using (var db = new SQLiteDBContext())
            {
                var employee = from person in db.Employee
                               select person;

                List<Employee> queryResults = new List<Employee>();
                queryResults.AddRange(employee);

                DataGriddd.ItemsSource = queryResults;

            }
        }
    }
}
