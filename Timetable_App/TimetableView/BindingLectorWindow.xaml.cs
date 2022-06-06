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
using TimetableBusinessLogic.ViewModels;

namespace TimetableView
{
    /// <summary>
    /// Логика взаимодействия для BindingLectorWindow.xaml
    /// </summary>
    public partial class BindingLectorWindow : Window
    {
        private readonly SubjectLogic _subjectLogic;
        private readonly LectorLogic _LectorLogic;

        public string Login { set { login = value; } }

        private string login;
        public BindingLectorWindow(SubjectLogic subjectLogic, LectorLogic LectorLogic)
        {
            InitializeComponent();
            _subjectLogic = subjectLogic;
            _LectorLogic = LectorLogic;
        }

        private void LoadData()
        {
            try
            {
                ListBoxSubjects.ItemsSource = _subjectLogic.Read(null);
                ComboBoxLectors.ItemsSource = _LectorLogic.Read(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BindingLectorWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void ButtonBinding_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxLectors.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите преподавателя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ListBoxSubjects.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите предмет", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var Lector = _LectorLogic.Read(new LectorBindingModel { Id = (ComboBoxLectors.SelectedItem as LectorViewModel).Id })?[0];
                var subject = _subjectLogic.Read(new SubjectBindingModel { Id = (ListBoxSubjects.SelectedItem as SubjectViewModel).Id })?[0];
                if (Lector == null)
                {
                    throw new Exception("Такой преподаватель не найден");
                }
                if (subject == null)
                {
                    throw new Exception("Такой предмет не найден");
                }
                if (Lector.Subjects.ContainsKey((int)subject.Id))
                {
                    throw new Exception("Преподаватель уже привязан к данному предмету");
                }
                _LectorLogic.BindingSubject(Lector.Id, (int)subject.Id);
                MessageBox.Show("Привязка прошла успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
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
