//using System;
//using System.Collections.Generic;
//using System.Linq;
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

//namespace UniversityAllExpelledWorkerView
//{
//    /// <summary>
//    /// Логика взаимодействия для GroupWindow.xaml
//    /// </summary>
//    public partial class GroupWindow : Window
//    {
//        [Dependency]
//        public IUnityContainer Container { get; set; }
//        public string GrdBookNum { set { gbn = value; } }

//        private string gbn;

//        public string Login { set { login = value; } }

//        private string login;

//        private readonly GroupLogic _logicGroup;

//        public bool isUpdating;

//        public GroupWindow(GroupLogic logic)
//        {
//            InitializeComponent();
//            this._logicGroup = logic;
//        }

//        private void GroupWindow_Loaded(object sender, RoutedEventArgs e)
//        {
//            if (!string.IsNullOrEmpty(gbn))
//            {
//                try
//                {
//                    var view = _logicGroup.Read(new GroupBindingModel { GradebookNumber = gbn })?[0];
//                    if (view != null)
//                    {
//                        if (isUpdating)
//                        {
//                            TextBoxGradBook.IsEnabled = false;
//                        }
//                        TextBoxGradBook.Text = view.GradebookNumber;
//                        TextBoxName.Text = view.Name;
//                    }
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
//                }
//            }
//        }

//        private void ButtonSave_Click(object sender, RoutedEventArgs e)
//        {
//            if (string.IsNullOrEmpty(TextBoxName.Text))
//            {
//                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
//                return;
//            }
//            try
//            {
//                _logicGroup.CreateOrUpdate(new GroupBindingModel
//                {
//                    GradebookNumber = TextBoxGradBook.Text,
//                    Name = TextBoxName.Text,
//                    DenearyLogin = login
//                }, isUpdating);
//                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
//                DialogResult = true;
//                Close();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//        }

//        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
//        {
//            DialogResult = false;
//            Close();
//        }

//    }
//}
