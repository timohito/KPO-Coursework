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
using TimetableBusinessLogic.BindingModels;
using TimetableBusinessLogic.BusinessLogics;
using Unity;

namespace TimetableView
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private int? id;

        private DenearyLogic logic;

        public MainWindow(DenearyLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void MenuItemGroups_Click(object sender, RoutedEventArgs e)
        {
            //var window = Container.Resolve<WindowGroups>();
            //window.Id = (int)id;
            //window.ShowDialog();
        }

        private void MenuItemClassrooms_Click(object sender, RoutedEventArgs e)
        {
            //var window = Container.Resolve<WindowClassrooms>();
            //window.Id = (int)id;
            //window.ShowDialog();

        }
        private void MenuItemSubjects_Click(object sender, RoutedEventArgs e)
        {
            //var window = Container.Resolve<WindowSubjects>();
            //window.Id = (int)id;
            //window.ShowDialog();
        }

        private void MenuItemLectors_Click(object sender, RoutedEventArgs e)
        {
            //var window = Container.Resolve<WindowLectors>();
            //window.Id = (int)id;
            //window.ShowDialog();
        }

        private void MenuBindingLectors_Click(object sender, RoutedEventArgs e)
        {
            //var window = Container.Resolve<WindowBindingLectors>();
            //window.DenearyId = (int)id;
            //window.ShowDialog();

        }

        private void MenuTimetable_Click(object sender, RoutedEventArgs e)
        {
            //var window = Container.Resolve<WindowTimetable>();
            //window.ShowDialog();
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var Deneary = logic.Read(new DenearyBindingModel { Id = id })?[0];
            labelDeneary.Content = "Клиент: " + Deneary.Name;
        }
    }
}
