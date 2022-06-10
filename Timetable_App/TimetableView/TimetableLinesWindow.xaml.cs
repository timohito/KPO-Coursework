using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
    /// Логика взаимодействия для TimetableLinesWindow.xaml
    /// </summary>
    public partial class TimetableLinesWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly TimetableLogic logic;
        private readonly GroupLogic logicGroup;

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

        public TimetableLinesWindow(TimetableLogic logic, GroupLogic groupLogic)
        {
            InitializeComponent();
            this.logic = logic;
            this.logicGroup = groupLogic;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<TimetableLineWindow>();


            if (window.ShowDialog().Value)
            {
                LoadData();
            }
        }

        private void ButtonUpd_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridPlans.SelectedCells.Count != 0)
            {
                var window = Container.Resolve<TimetableLineWindow>();
                //window.Login = login;
                var record = (TimetableViewModel)DataGridPlans.SelectedCells[0].Item;
                window.Id = record.Id.Value;
                if (window.ShowDialog().Value)
                {
                    LoadData();
                }

            }
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridPlans.SelectedCells.Count != 0)
            {
                var result = MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo,
               MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var cellInfo = DataGridPlans.SelectedCells[0];
                        TimetableViewModel content = (TimetableViewModel)(cellInfo.Item);
                        int id = (int)content.Id;
                        logic.Delete(new TimetableBindingModel { Id = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                       MessageBoxImage.Error);
                    }
                    LoadData();
                }
            }
        }

        private void ButtonRef_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            var list = logic.Read(new TimetableBindingModel { GroupId = (ComboBoxGroups.SelectedItem as GroupViewModel).Id.Value });
            if (list != null)
            {
                DataGridPlans.ItemsSource = list;
                DataGridPlans.Columns[0].Visibility = Visibility.Hidden;
                DataGridPlans.Columns[3].Visibility = Visibility.Hidden;
                DataGridPlans.Columns[5].Visibility = Visibility.Hidden;
                DataGridPlans.Columns[7].Visibility = Visibility.Hidden;
                DataGridPlans.Columns[8].Visibility = Visibility.Hidden;
                DataGridPlans.Columns[10].Visibility = Visibility.Hidden;
                DataGridPlans.Columns[12].Visibility = Visibility.Hidden;
            }
        }

        private void TimetableLines_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxGroups.ItemsSource = logicGroup.Read(null);
            ComboBoxGroups.SelectedIndex = 0;
            if (id.HasValue)
            {
                try
                {
                    ComboBoxGroups.SelectedItem = SetValue(groupId);
                    var view = logic.Read(new TimetableBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        ComboBoxGroups.SelectedItem = view.GroupName;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            //if (listGroups != null)
            //{
            //    ComboBoxGroups.ItemsSource = listGroups;
            //}
            //ComboBoxGroups.SelectedIndex = 0;
            LoadData();
        }

        /// <summary>
        /// Данные для привязки DisplayName к названиям столбцов
        /// </summary>
        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string displayName = GetPropertyDisplayName(e.PropertyDescriptor);
            if (!string.IsNullOrEmpty(displayName))
            {
                e.Column.Header = displayName;
            }
            new DataGridLength(1, DataGridLengthUnitType.Star);

        }
        /// <summary>
        /// метод привязки DisplayName к названию столбца
        /// </summary>
        public static string GetPropertyDisplayName(object descriptor)
        {

            PropertyDescriptor pd = descriptor as PropertyDescriptor;
            if (pd != null)
            {
                // Check for DisplayName attribute and set the column header accordingly
                DisplayNameAttribute displayName = pd.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;
                if (displayName != null && displayName != DisplayNameAttribute.Default)
                {
                    return displayName.DisplayName;
                }

            }
            else
            {
                PropertyInfo pi = descriptor as PropertyInfo;
                if (pi != null)
                {
                    // Check for DisplayName attribute and set the column header accordingly
                    Object[] attributes = pi.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                    for (int i = 0; i < attributes.Length; ++i)
                    {
                        DisplayNameAttribute displayName = attributes[i] as DisplayNameAttribute;
                        if (displayName != null && displayName != DisplayNameAttribute.Default)
                        {
                            return displayName.DisplayName;
                        }
                    }
                }
            }
            return null;
        }

        private GroupViewModel SetValue(int value)
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

        private void ComboBoxGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }
    }
}
