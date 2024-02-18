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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DalApi;
using PL.Engineer;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ListViewButton_Click(object sender, RoutedEventArgs e)
    {
        new EngineerListWindow().Show();
    }

    private void InitializedDBButton_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult mbConfirmInit = MessageBox.Show("To initialize the DB?",
                                                        "Confirm Initialize",
                                                        MessageBoxButton.OKCancel,
                                                        MessageBoxImage.Question,
                                                        MessageBoxResult.Cancel);
        switch (mbConfirmInit)
        {
            case MessageBoxResult.None:
                break;
            case MessageBoxResult.OK:
                //לקרוא לפונקציה InitializeDB
                break;
            case MessageBoxResult.Cancel:
                break;
            default:
                break;
        }
    }

    private void ResetDBButton_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult mbConfirmReset = MessageBox.Show("To reset the DB?",
                                                            "Confirm Reset",
                                                             MessageBoxButton.OKCancel,
                                                             MessageBoxImage.Question,
                                                             MessageBoxResult.Cancel);
        switch (mbConfirmReset)
        {
            case MessageBoxResult.None:
                break;
            case MessageBoxResult.OK:
                //לקרוא לפונקציה ResetDB
                break;
            case MessageBoxResult.Cancel:
                break;
            default:
                break;
        }
    }
}
