using System.Windows.Controls;

namespace TaskManager_Lipina.View
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
            DataContext = Context;
        }
    }
}
