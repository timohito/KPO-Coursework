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
using Unity;

namespace TimetableView
{
    /// <summary>
    /// Логика взаимодействия для TimetableLineWindow.xaml
    /// </summary>
    public partial class TimetableLineWindow : Window
    {
        private readonly TimetableLogic logicTimetable;
        private readonly GroupLogic logicGroup;
        private readonly ClassroomLogic logicClassroom;
        private readonly LectorLogic logicLector;
        private readonly SubjectLogic logicSubject;

        public int Id { set { id = value; } }
        private int? id;

        public int GroupId
        {
            get { return (ComboBoxGroups.SelectedItem as GroupViewModel).Id.Value; }
            set
            {
                groupId = value;
            }
        }

        private int groupId;

        public int ClassroomId
        {
            get { return (ComboBoxClassrooms.SelectedItem as ClassroomViewModel).Id; }
            set
            {
                classroomId = value;
            }
        }

        private int classroomId;

        public int LectorId
        {
            get { return (ComboBoxLector.SelectedItem as LectorViewModel).Id; }
            set
            {
                lectorId = value;
            }
        }

        private int lectorId;

        public int SubjectId
        {
            get { return (ComboBoxSubject.SelectedItem as SubjectViewModel).Id.Value; }
            set
            {
                subjectId = value;
            }
        }

        private int subjectId;

        [Dependency]
        public IUnityContainer Container { get; set; }

        public TimetableLineWindow(GroupLogic logicGroup, ClassroomLogic logicClassroom, LectorLogic logicLector, SubjectLogic logicSubject, TimetableLogic logicTimetable)
        {
            InitializeComponent();
            this.logicTimetable = logicTimetable;
            this.logicGroup = logicGroup;
            this.logicClassroom = logicClassroom;
            this.logicSubject = logicSubject;
            this.logicLector = logicLector;
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxGroups.SelectedValue == null)
            {
                MessageBox.Show("Выберите группу", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ComboBoxClassrooms.SelectedValue == null)
            {
                MessageBox.Show("Выберите аудиторию", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ComboBoxLector.SelectedValue == null)
            {
                MessageBox.Show("Выберите преподавателя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ComboBoxSubject.SelectedValue == null)
            {
                MessageBox.Show("Выберите предмет", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //if (ComboBoxDay.SelectedValue == null)
            //{
            //    MessageBox.Show("Выберите день", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
            //if (ComboBoxClass.SelectedValue == null)
            //{
            //    MessageBox.Show("Выберите номер пары", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
            try
            {
                int LectorSubjectId = logicTimetable.FindLectorSubjectIdByForeignKeys(LectorId, SubjectId);/// как-то так...
                logicTimetable.CreateOrUpdate(new TimetableBindingModel
                {
                    Id = id,
                    ClassroomId = ClassroomId,
                    GroupId = GroupId,
                    LectorSubject_LectorId = LectorId,
                    LectorSubject_SubjectId = SubjectId,
                    LectorSubjectId = LectorSubjectId
                    //Class = 
                });
                 

                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void TimetableLineWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var listGroups = logicGroup.Read(null);
            if (listGroups != null)
            {
                ComboBoxGroups.ItemsSource = listGroups;
            }
            var listClassrooms = logicClassroom.Read(null);
            if (listClassrooms != null)
            {
                ComboBoxClassrooms.ItemsSource = listClassrooms;
            }
            var listLectors = logicLector.Read(null);
            if (listLectors != null)
            {
                ComboBoxLector.ItemsSource = listLectors;
            }
            var listSubjects = logicSubject.Read(null);
            if (listSubjects != null)
            {
                ComboBoxSubject.ItemsSource = listSubjects;
            }
       
            if (id.HasValue)
            {
                try
                {
                    ComboBoxLector.SelectedItem = SetLectorValue(lectorId);
                    ComboBoxGroups.SelectedItem = SetGroupValue(groupId);
                    ComboBoxClassrooms.SelectedItem = SetClassroomValue(classroomId);
                    ComboBoxSubject.SelectedItem = SetSubjectValue(subjectId);
                    //дописать день и пару
                    var view = logicTimetable.Read(new TimetableBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        ComboBoxLector.SelectedItem = view.LectorName;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private GroupViewModel SetGroupValue(int value)
        {
            foreach (var item in ComboBoxGroups.Items)
            {
                if ((item as GroupViewModel).Id == value)
                {
                    return item as GroupViewModel;
                }
            }
            return null;
        }

        private ClassroomViewModel SetClassroomValue(int value)
        {
            foreach (var item in ComboBoxClassrooms.Items)
            {
                if ((item as ClassroomViewModel).Id == value)
                {
                    return item as ClassroomViewModel;
                }
            }
            return null;
        }

        private LectorViewModel SetLectorValue(int value)
        {
            foreach (var item in ComboBoxLector.Items)
            {
                if ((item as LectorViewModel).Id == value)
                {
                    return item as LectorViewModel;
                }
            }
            return null;
        }

        private SubjectViewModel SetSubjectValue(int value)
        {
            foreach (var item in ComboBoxSubject.Items)
            {
                if ((item as SubjectViewModel).Id == value)
                {
                    return item as SubjectViewModel;
                }
            }
            return null;
        }
    }
}
