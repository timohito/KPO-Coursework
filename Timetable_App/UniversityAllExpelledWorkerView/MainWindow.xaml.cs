//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using Unity;
//using TimetableBusinessLogic.BindingModels;
//using TimetableBusinessLogic.BusinessLogics;
//using TimetableBusinessLogic.ViewModels;

//namespace UniversityAllExpelledWorkerView
//{
//    /// <summary>
//    /// Логика взаимодействия для MainWindow.xaml
//    /// </summary>
//    /// <summary>
//    /// Логика взаимодействия для MainWindow.xaml
//    /// </summary>
//    public partial class MainWindow : Window
//    {
//        [Dependency]
//        public IUnityContainer Container { get; set; }

//        public string Login { set { login = value; } }

//        private string login;

//        private readonly DenearyLogic _logicDeneary;
//        public MainWindow(DenearyLogic logic)
//        {
//            InitializeComponent();
//            this._logicDeneary = logic;
//        }

//        private void MenuItemPlans_Click(object sender, RoutedEventArgs e)
//        {
//            var window = Container.Resolve<TimetablesWindow>();
//            window.Login = login;
//            window.ShowDialog();
//        }

//        private void MenuItemGroups_Click(object sender, RoutedEventArgs e)
//        {
//            var window = Container.Resolve<GroupsWindow>();
//            window.Login = login;
//            window.ShowDialog();
//        }



//        private void MenuItemReport_Click(object sender, RoutedEventArgs e)
//        {
//            var window = Container.Resolve<ReportWindow>();
//            window.Login = login;
//            window.ShowDialog();
//        }


//        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
//        {
//            var _currentUser = _logicDeneary.Read(new DenearyBindingModel { Login = login })?[0];
//            labelUser.Content = $"Деканат \"{_currentUser.Name}\"";
//        }

//        private void ButtonAuth_Click(object sender, RoutedEventArgs e)
//        {
//            var window = Container.Resolve<AuthorizationWindow>();
//            window.ShowDialog();
//        }

//        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
//        {
//            var window = Container.Resolve<RegistrationWindow>();
//            window.ShowDialog();
//        }

//    }
//}
