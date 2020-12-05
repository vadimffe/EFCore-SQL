using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            UpdateGrid();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new SQLiteDBContext())
            {
                // Create
                Debug.WriteLine("Add New Employee: ");

                if (!string.IsNullOrEmpty(FirstName.Text) && !string.IsNullOrEmpty(LastName.Text) && !string.IsNullOrEmpty(Age.Text))
                {
                    db.Add<Employee>(new Employee { FirstName = FirstName.Text, LastName = LastName.Text, Age = int.Parse(Age.Text) });
                    db.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Input some data!");
                }

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

                if (DataGriddd.SelectedItems.Count == 1 &&!string.IsNullOrEmpty(FirstName.Text) && !string.IsNullOrEmpty(LastName.Text) && !string.IsNullOrEmpty(Age.Text))
                {
                    object item = DataGriddd.SelectedItem;
                    string ID = (DataGriddd.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;

                    var employee = db.Employee
                        .Where(d => d.Id == int.Parse(ID))
                        .OrderBy(b => b.Id)
                        .First();

                    Debug.WriteLine("The employee found: {0} {1} and is {2} years old.", employee.FirstName, employee.LastName, employee.Age);

                    // Update
                    Debug.WriteLine("Updating the employee first name and age.");

                    employee.FirstName = "Louis";
                    employee.Age = 90;

                    if (employee != null)
                    {
                        employee.FirstName = FirstName.Text;
                        employee.LastName = LastName.Text;
                        employee.Age = int.Parse(Age.Text);

                        //Debug.WriteLine("Newly updated employee is: {0} {1} and is {2} years old.", employee.FirstName, employee.LastName, employee.Age);

                        db.SaveChanges();

                    }
                }

                UpdateGrid();

            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            using (var db = new SQLiteDBContext())
            {
                var row = DataGriddd.SelectedItems[0];
                DataRowView castedRow = DataGriddd.SelectedItems[0] as DataRowView;

                //var employee = db.Employee
                //.OrderBy(b => b.Id)
                //.FirstOrDefault();

                var selectedItem = DataGriddd.SelectedItem;

                // Delete
                Debug.WriteLine("Delete the employee.");

                if (selectedItem != null)
                {
                    try
                    {
                        db.Remove(selectedItem);
                        db.SaveChanges();
                    }
                    catch (System.Exception)
                    {

                        // throw;
                    }
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

        private void DataGriddd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                object item = DataGriddd.SelectedItem;
                string FN = (DataGriddd.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                string LN = (DataGriddd.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
                string AGE = (DataGriddd.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;

                FirstName.Text = FN;
                LastName.Text = LN;
                Age.Text = AGE;
            }
            catch (System.Exception)
            {

                // throw;
            }
        }
    }
}
