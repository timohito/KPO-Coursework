//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;
//using Unity;
//using TimetableBusinessLogic.BindingModels;
//using TimetableBusinessLogic.BusinessLogics;
//using TimetableBusinessLogic.ViewModels;

//namespace UniversityAllExpelledWorkerView
//{
//    /// <summary>
//    /// Логика взаимодействия для GroupsWindow.xaml
//    /// </summary>
//    public partial class GroupsWindow : Window
//    {
//        [Dependency]
//        public IUnityContainer Container { get; set; }

//        public string Login { set { login = value; } }

//        private string login;

//        private readonly GroupLogic logic;

//        public GroupsWindow(GroupLogic logic)
//        {
//            InitializeComponent();
//            this.logic = logic;
//        }

//        private void LoadData()
//        {
//            try
//            {
//                var list = logic.Read(new GroupBindingModel { DenearyLogin = login });
//                if (list != null)
//                {
//                    dataGrid.ItemsSource = list;
//                    dataGrid.Columns[2].Visibility = Visibility.Hidden;
//                    dataGrid.Columns[3].Visibility = Visibility.Hidden;
//                    dataGrid.Columns[4].Visibility = Visibility.Hidden;
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//        }

//        private void GroupsWindow_Loaded(object sender, RoutedEventArgs e)
//        {
//            LoadData();
//        }

//        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
//        {
//            var window = Container.Resolve<GroupWindow>();
//            window.Login = login;
//            if (window.ShowDialog().Value)
//            {
//                LoadData();
//            }
//        }

//        private void ButtonUpd_Click(object sender, RoutedEventArgs e)
//        {
//            if (dataGrid.SelectedCells.Count != 0)
//            {
//                var window = Container.Resolve<GroupWindow>();
//                window.GrdBookNum = ((GroupViewModel)dataGrid.SelectedCells[0].Item).GradebookNumber;
//                window.Login = login;
//                window.isUpdating = true;
//                if (window.ShowDialog().Value)
//                {
//                    LoadData();
//                }
//            }
//        }

//        private void ButtonDel_Click(object sender, RoutedEventArgs e)
//        {
//            if (dataGrid.SelectedCells.Count != 0)
//            {
//                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
//                {
//                    string id = ((GroupViewModel)dataGrid.SelectedCells[0].Item).GradebookNumber;
//                    try
//                    {
//                        logic.Delete(new GroupBindingModel { GradebookNumber = id });
//                    }
//                    catch (Exception ex)
//                    {
//                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
//                    }
//                    LoadData();
//                }
//            }
//        }

//        /// <summary>
//		/// Данные для привязки DisplayName к названиям столбцов
//		/// </summary>
//		private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
//        {
//            string displayName = GetPropertyDisplayName(e.PropertyDescriptor);
//            if (!string.IsNullOrEmpty(displayName))
//            {
//                e.Column.Header = displayName;
//            }
//        }

//        /// <summary>
//        /// метод привязки DisplayName к названию столбца
//        /// </summary>
//        public static string GetPropertyDisplayName(object descriptor)
//        {
//            if (descriptor is PropertyDescriptor pd)
//            {
//                // Check for DisplayName attribute and set the column header accordingly
//                if (pd.Attributes[typeof(DisplayNameAttribute)] is DisplayNameAttribute displayName && displayName != DisplayNameAttribute.Default)
//                {
//                    return displayName.DisplayName;
//                }
//            }
//            else
//            {
//                PropertyInfo pi = descriptor as PropertyInfo;
//                if (pi != null)
//                {
//                    // Check for DisplayName attribute and set the column header accordingly
//                    Object[] attributes = pi.GetCustomAttributes(typeof(DisplayNameAttribute), true);
//                    for (int i = 0; i < attributes.Length; ++i)
//                    {
//                        if (attributes[i] is DisplayNameAttribute displayName && displayName != DisplayNameAttribute.Default)
//                        {
//                            return displayName.DisplayName;
//                        }
//                    }
//                }
//            }
//            return null;
//        }

//        private void ButtonRef_Click(object sender, RoutedEventArgs e)
//        {
//            LoadData();
//        }
//    }
//}
