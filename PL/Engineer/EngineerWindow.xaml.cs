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
    public static event EventHandler reloadList;

    /// <summary>
    /// current engineer property
    /// </summary>
    public BO.Engineer CurrentEngineer
    {
        get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
        set { SetValue(CurrentEngineerProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CurrentEngineer.
    public static readonly DependencyProperty CurrentEngineerProperty =
        DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(EngineerWindow));


    /// <summary>
    /// open an empty window for new engineer and with details for existing one
    /// </summary>
    /// <param name="id"></param>
    public EngineerWindow(int id = 0)
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

    /// <summary>
    /// button for finish adding and updating
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
    {
        string actionType = ((Button)sender).Content.ToString()!;
        MessageBoxResult close;
        try
        {
            //Add engineer
            if (actionType == "Add")
            {
                s_bl.Engineer.Create(CurrentEngineer);
                close = MessageBox.Show($"{CurrentEngineer.Name} was added successfully");
            }
            //Update engineer
            else
            {
                s_bl.Engineer.Update(CurrentEngineer);
                close = MessageBox.Show($"{CurrentEngineer.Name} was updated successfully");
            }
            if (close == MessageBoxResult.OK || close == MessageBoxResult.None)
            {
                this.Close();
                if(reloadList is not null)
                {
                    reloadList(this, EventArgs.Empty);
                }
            }
        }
        catch (Exception ex) { MessageBox.Show(ex.ToString()); }

    }
}
