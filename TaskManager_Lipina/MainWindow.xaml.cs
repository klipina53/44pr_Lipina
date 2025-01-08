using System.Windows;
using TaskManager_Lipina.ViewModels;

namespace TaskManager_Lipina
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
        {
            public static MainWindow init;
            public MainWindow()
            {
                InitializeComponent();
                init = this;
                DataContext = new VM_Pages();
            }
        }
    
}
