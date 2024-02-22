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
using BlApi;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    /// <summary>
    /// start running
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// view all the engineers
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ListViewButton_Click(object sender, RoutedEventArgs e)
    {
        new EngineerListWindow().Show();
    }

    /// <summary>
    /// initialize the database
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
                s_bl.InitializeDB();
                break;
            case MessageBoxResult.Cancel:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// reset the database
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
                s_bl.ResetDB();
                break;
            case MessageBoxResult.Cancel:
                break;
            default:
                break;
        }
    }
}
