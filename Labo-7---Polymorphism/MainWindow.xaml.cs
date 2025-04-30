using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using Labo_7___Polymorphism.Entities;
using Labo_7___Polymorphism.Data;

namespace Labo_7___Polymorphism;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    Store<Machine> machineStore = new Store<Machine>();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void ImportButton_Click(object sender, RoutedEventArgs e)
    {
        string fileName;
        OpenFileDialog ofd = new OpenFileDialog
        {
            InitialDirectory = "C:\\Mac\\Home\\Documents\\School\\C# Advanced\\Oefeningen\\7 - Polymorphism\\Labo\\Labo-7---Polymorphism"
        };
        if (ofd.ShowDialog() == true)
        {
            fileName = ofd.FileName;

            using (StreamReader sr = new StreamReader(fileName))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string[] tempData = sr.ReadLine().Split(',');

                    if (tempData[0] == "L")
                    {
                        string name = tempData[1];
                        double.TryParse(tempData[2], out double width);
                        double.TryParse(tempData[3], out double length);
                        double.TryParse(tempData[4], out double costPerMinute);
                        double.TryParse(tempData[5], out double accuracy);

                        LaserCutter laserCutter = new LaserCutter(name, width, length, costPerMinute, accuracy);
                        machineStore.AddItem(laserCutter);
                    }
                    else if (tempData[0] == "R")
                    {
                        string name = tempData[1];
                        double.TryParse(tempData[2], out double width);
                        double.TryParse(tempData[3], out double length);
                        double.TryParse(tempData[4], out double costPerMinute);

                        Router router = new Router(name, width, length, costPerMinute);
                        machineStore.AddItem(router);
                    }
                    else if (tempData[0] == "G")
                    {
                        string name = tempData[1];

                        General general = new General(name);
                        machineStore.AddItem(general);
                    }
                }
            }
        }

        UpdateListBox();
        clearButton.IsEnabled = true;
        sortButton.IsEnabled = true;
        filterButton.IsEnabled = true;
    }

    private void RemoveButton_Click(object sender, RoutedEventArgs e)
    {
        machineStore.RemoveItem(itemsListBox.SelectedItem as Machine);
        UpdateListBox();
        itemsListBox.SelectedIndex = -1;
        useButton.IsEnabled = false;
        removeButton.IsEnabled = false;
    }

    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        machineStore.ClearAllItems();
        UpdateListBox();
        sortButton.IsEnabled = false;
        filterButton.IsEnabled = false;
        clearButton.IsEnabled = false;
        removeButton.IsEnabled = false;
        useButton.IsEnabled = false;
    }

    private void UseButton_Click(object sender, RoutedEventArgs e)
    {
        if (inputTextBox.Text.Length > 0 && itemsListBox.SelectedIndex >= 0)
        {
            Int32.TryParse(inputTextBox.Text, out int minutes);
            Machine machine = itemsListBox.SelectedItem as Machine;
            machine.Use(minutes);
        }
        UpdateListBox();
        itemsListBox.SelectedIndex = -1;
        useButton.IsEnabled = false;
        removeButton.IsEnabled = false;
    }

    private void SortButton_Click(object sender, RoutedEventArgs e)
    {
        machineStore.SortItems((x, y) => x.Name.CompareTo(y.Name));
        UpdateListBox();
    }

    private void FilterButton_Click(object sender, RoutedEventArgs e)
    {
        if (inputTextBox.Text.Length > 0 && itemsListBox.SelectedIndex < 0)
        {
            itemsListBox.ItemsSource = null;
            itemsListBox.ItemsSource = machineStore.FilterItems(machine => machine.Name.IndexOf(inputTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
    }

    private void itemsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (itemsListBox.SelectedIndex >= 0)
        {
            Machine machine = itemsListBox.SelectedItem as Machine;

            if (machine.LifeSpan > 0)
            {
                useButton.IsEnabled = true;
            }
            else
            {
                useButton.IsEnabled = false;
            }
        }

        removeButton.IsEnabled = true;
    }

    private void UpdateListBox()
    {
        itemsListBox.ItemsSource = null;
        itemsListBox.ItemsSource = machineStore.GetAllItems();
    }
}