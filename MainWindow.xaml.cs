using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace biblioDATA
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadComboBox();
        }

        private void LoadComboBox()
        {
            comboBox.Items.Add("Книги");
            comboBox.Items.Add("Автори");
            comboBox.Items.Add("Відвідувачі");
            comboBox.SelectedIndex = 0;
            comboBox.SelectionChanged += comboBox_SelectionChanged;
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            string selected = comboBox.SelectedItem.ToString();

            if (selected == "Книги")
                dataGrid.ItemsSource = DatabaseHelper.GetBooks(chkFilter.IsChecked == true).DefaultView;
            else if (selected == "Автори")
                dataGrid.ItemsSource = DatabaseHelper.GetAuthors().DefaultView;
            else if (selected == "Відвідувачі")
                dataGrid.ItemsSource = DatabaseHelper.GetVisitors().DefaultView;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string selected = comboBox.SelectedItem.ToString();
            string inputText = txtInput.Text.Trim();

            if (string.IsNullOrEmpty(inputText))
            {
                MessageBox.Show("Введіть значення!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (selected == "Книги")
                DatabaseHelper.AddBook(inputText);
            else if (selected == "Автори")
                DatabaseHelper.AddAuthor(inputText);
            else if (selected == "Відвідувачі")
                DatabaseHelper.AddVisitor(inputText, false);

            LoadData();
            txtInput.Clear();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem is DataRowView row)
            {
                int id = Convert.ToInt32(row["id"]);
                string newValue = txtInput.Text.Trim();

                if (string.IsNullOrEmpty(newValue))
                {
                    MessageBox.Show("Введіть нове значення!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string selected = comboBox.SelectedItem.ToString();

                if (selected == "Книги")
                    DatabaseHelper.UpdateBookTitle(id, newValue);
                else if (selected == "Автори")
                    DatabaseHelper.UpdateAuthor(id, newValue);
                else if (selected == "Відвідувачі")
                {
                    bool isDebtor = row["isDebtor"] != DBNull.Value && (bool)row["isDebtor"];
                    DatabaseHelper.UpdateVisitor(id, newValue, isDebtor);
                }

                LoadData();
                txtInput.Clear();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int id;

            if (dataGrid.SelectedItem is DataRowView row)
            {
                id = Convert.ToInt32(row["id"]);
            }
            else if (int.TryParse(txtInput.Text.Trim(), out id))
            {
            }
            else
            {
                MessageBox.Show("Виберіть запис або введіть коректний ID!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (comboBox.SelectedItem.ToString() == "Книги")
            {
                DatabaseHelper.DeleteBook(id);
            }
            else if (comboBox.SelectedItem.ToString() == "Автори")
            {
                bool deleted = DatabaseHelper.DeleteAuthor(id);
                if (!deleted)
                {
                    MessageBox.Show("Неможливо видалити автора, оскільки він має книги!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            else if (comboBox.SelectedItem.ToString() == "Відвідувачі")
            {
                DatabaseHelper.DeleteVisitor(id);
            }

            LoadData();
            txtInput.Clear();
        }


        private void Filter_Checked(object sender, RoutedEventArgs e)
        {
            if (comboBox.SelectedItem.ToString() == "Книги")
                dataGrid.ItemsSource = DatabaseHelper.GetBooks(onlyAvailable: true).DefaultView;
        }

        private void Filter_Unchecked(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
