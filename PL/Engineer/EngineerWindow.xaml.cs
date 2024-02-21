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

namespace PL.Engineer;

/// <summary>
/// Interaction logic for EngineerWindow.xaml
/// </summary>
public partial class EngineerWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public BO.EngineerExperience Level { get; set; } = BO.EngineerExperience.Novice;

    public BO.Engineer CurrentEngineer
    {
        get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
        set { SetValue(CurrentEngineerProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CurrentEngineer.
    public static readonly DependencyProperty CurrentEngineerProperty =
        DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(EngineerWindow));



    public EngineerWindow(int id=0)
    {
        InitializeComponent();
        if (id == 0)
        {
            CurrentEngineer = new BO.Engineer();
        }
        else
        {
            CurrentEngineer = s_bl.Engineer.Read(id)!;
        }
    }
    private void cbLevelSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      
    }

    private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
    {

    }
}
