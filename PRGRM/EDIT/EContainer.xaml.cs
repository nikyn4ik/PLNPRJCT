using Database;
using Database.MDLS;
using System.Windows;
using System.Windows.Controls;

namespace PRGRM.ADD
{
    public partial class EContainer : Window
    {
        private readonly ApplicationContext _dbContext;
        public EContainer()
        {
            InitializeComponent();
            _dbContext = new ApplicationContext();
            LoadTypeModelData();
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

            if (!DateTime.TryParse(DatePicker.Text, out DateTime DTContainer))
            {
                MessageBox.Show("Введите корректную дату.", "Severstal Infocom", MessageBoxButton.OK);
                return;
            }
            if (DTContainer < DateTime.Today)
            {
                MessageBox.Show("Дата не может быть меньше текущей даты.", "Severstal Infocom", MessageBoxButton.OK);
                return;
            }
            if (!string.IsNullOrEmpty(selectedTypeModel) && !string.IsNullOrEmpty(selectedMarkContainer))
            {
                var newContainer = new Container
                {
                    TypeModel = selectedTypeModel,
                    MarkContainer = selectedMarkContainer,
                    DTContainer = DateTime.Now
                };

                _dbContext.Container.Add(newContainer);
                _dbContext.SaveChanges();
                MessageBox.Show("Сохранено!", "Severstal Infocom", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Проверьте, введены ли все данные.", "Severstal Infocom", (MessageBoxButton)MessageBoxImage.Error);
        }
        }
    }
}