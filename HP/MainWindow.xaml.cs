using Database.MDLS;
using Database;
using System.Windows;

namespace HP
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Authorization Authorization = new Authorization();

                if (string.IsNullOrWhiteSpace(Log.Text) || string.IsNullOrWhiteSpace(Pass.Password) || string.IsNullOrWhiteSpace(FIO.Text))
                {
                    MessageBox.Show("Необходимо заполнить все данные",
                        "Severstal Infocom");
                }
                else
                {
                    var log = Log.Text;
                    var pass = sha256.HP(Pass.Password);
                    var fio = FIO.Text;
                    if (CheckLog())
                    {
                        return;
                    }
                    Authorization.Login = log;
                    Authorization.PassHash = pass;
                    Authorization.FIO = fio;
                    db.Authorization.Add(Authorization);
                    db.SaveChanges();
                    Log.Clear();
                    Pass.Clear();
                    FIO.Clear();
                    MessageBox.Show("Пользователь успешно добавлен!",
                        "Severstal Infocom");
                }
            }
        }

        private Boolean CheckLog()
        {
            var log = Log.Text;
            using (ApplicationContext db = new ApplicationContext())
            {
                var login_check = db.Authorization.Where(p => p.Login == log).ToList();
                if (login_check.Count > 0)
                {
                    MessageBox.Show($"Пользователь с таким логином уже существует!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Error);
                    return true;
                }
                else return false;
            }
        }
    }
}