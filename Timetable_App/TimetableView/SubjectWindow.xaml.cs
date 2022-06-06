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
using TimetableBusinessLogic.BindingModels;
using TimetableBusinessLogic.BusinessLogics;
using Unity;

namespace TimetableView
{
    /// <summary>
    /// Логика взаимодействия для SubjectWindow.xaml
    /// </summary>
    public partial class SubjectWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private int? id;


        private readonly SubjectLogic _logicSubject;

        public SubjectWindow(SubjectLogic logic)
        {
            InitializeComponent();
            this._logicSubject = logic;
        }

        private void SubjectWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var view = _logicSubject.Read(new SubjectBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        TextBoxName.Text = view.Name;
                        TextBoxHours.Text = view.Hours.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                _logicSubject.CreateOrUpdate(new SubjectBindingModel
                {
                    Id = id,
                    Name = TextBoxName.Text,
                    Hours = Convert.ToInt32(TextBoxHours.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
