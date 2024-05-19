using Database;
using System.Windows;
using System.Windows.Controls;
using Database.MDLS;

namespace PRGRM.ADD
{
    public partial class EContainer : Window
    {
        private readonly ApplicationContext _dbContext;
        private readonly Container _selectedContainer;
        private readonly Action _refreshContainerGrid;

        public EContainer(Container selectedContainer)
        {
            InitializeComponent();
            _dbContext = new ApplicationContext();
            _selectedContainer = selectedContainer;
            DatePicker.DisplayDate = DateTime.Today;
            DatePicker.SelectedDate = DateTime.Today;
            LoadTypeModelData();
            TypeModel.SelectedItem = selectedContainer.TypeModel;
            MarkContainer.SelectedItem = selectedContainer.MarkContainer;
            DatePicker.SelectedDate = selectedContainer.DTContainer;
        }
        private void LoadTypeModelData()
        {
            var typeModels = _dbContext.ContainerPackage.Select(cp => cp.TypeModel).Distinct().ToList();
            TypeModel.ItemsSource = typeModels;
        }
        private void LoadMarkContainerData(string typeModel)
        {
            var markContainers = _dbContext.ContainerPackage
                .Where(cp => cp.TypeModel == typeModel)
                .Select(cp => cp.MarkContainer)
                .Distinct()
                .ToList();
            MarkContainer.ItemsSource = markContainers;
        }

        private void type_model_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypeModel.SelectedItem is string selectedTypeModel)
            {
                LoadMarkContainerData(selectedTypeModel);
                if (MarkContainer.Items.Count > 0)
                {
                    MarkContainer.SelectedIndex = 0;
                }
            }
        }
        private void BSaved(object sender, RoutedEventArgs e)
        {
            var selectedTypeModel = TypeModel.SelectedItem as string;
            var selectedMarkContainer = MarkContainer.SelectedItem as string;

            if (string.IsNullOrEmpty(selectedTypeModel) || string.IsNullOrEmpty(selectedMarkContainer))
            {
                MessageBox.Show("Проверьте, введены ли все данные.", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DateTime DTContainer;
            if (!DateTime.TryParse(DatePicker.Text, out DTContainer))
            {
                MessageBox.Show("Введите корректную дату.", "Severstal Infocom", MessageBoxButton.OK);
                return;
            }

            if (DTContainer < DateTime.Today)
            {
                MessageBox.Show("Дата не может быть меньше текущей даты.", "Severstal Infocom", MessageBoxButton.OK);
                return;
            }
            var selectedContainer = _dbContext.Container.FirstOrDefault(c => c.IdContainer == _selectedContainer.IdContainer);
            if (selectedContainer != null)
            {
                selectedContainer.TypeModel = selectedTypeModel;
                selectedContainer.MarkContainer = selectedMarkContainer;
                selectedContainer.DTContainer = DTContainer;

                _dbContext.SaveChanges();
                MessageBox.Show("Сохранено!", "Severstal Infocom", MessageBoxButton.OK);
                Close();
                _refreshContainerGrid?.Invoke();
            }
        }
    }
}