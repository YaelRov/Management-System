using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
/// Interaction logic for EngineerListWindow.xaml
/// </summary>

public partial class EngineerListWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public BO.FilterByEngineerExperience Experience { get; set; } = BO.FilterByEngineerExperience.All;
    public IEnumerable<BO.Engineer?> EngineerList
    {
        get { return (IEnumerable<BO.Engineer>)GetValue(EngineerListProperty); }
        set { SetValue(EngineerListProperty, value); }
    }

    // Using a DependencyProperty as the backing store for EngineerList.
    public static readonly DependencyProperty EngineerListProperty =DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.Engineer>),
                                                                    typeof(EngineerListWindow), new PropertyMetadata(null));
    
    private void cbEngineerSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        EngineerList = (Experience == BO.FilterByEngineerExperience.All) ?
        s_bl?.Engineer.ReadAll()! : s_bl?.Engineer.ReadAll(item => (int)item.Level == (int)Experience)!;
    }
    public EngineerListWindow(EngineerWindow engWin)
    {
        InitializeComponent();
        EngineerWindow.reloadList += this.reloadList!;
        EngineerList = s_bl?.Engineer.ReadAll()!;
    }

    private void onClickAddEng(object sender, RoutedEventArgs e)
    {
        new EngineerWindow().ShowDialog();
    }

    private void ClickOnSingleEng(object sender, MouseButtonEventArgs e)
    {
        BO.Engineer? Eng = (sender as ListView)?.SelectedItem as BO.Engineer;
        new EngineerWindow(Eng!.Id).ShowDialog();
    }

    private void reloadList(object sender, EventArgs e)
    {
        EngineerList = s_bl?.Engineer.ReadAll()!;
    }
}
