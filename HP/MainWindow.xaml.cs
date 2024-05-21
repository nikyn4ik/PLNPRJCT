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
        private void BSaved(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Log.Text) || string.IsNullOrWhiteSpace(Pass.Password) || string.IsNullOrWhiteSpace(FIO.Text))
            {
                MessageBox.Show("Необходимо заполнить все данные", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            using (ApplicationContext db = new ApplicationContext())
                {
                    var log = Log.Text;
                    var pass = sha256.HP(Pass.Password);
                    var fio = FIO.Text;

                    var login_check = db.Authorization.FirstOrDefault(p => p.Login == log);
                    if (login_check != null)
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Error);
                        return; 
                    }
                    Authorization Authorization = new Authorization()
                    {
                        Login = log,
                        PassHash = pass,
                        FIO = fio
                    };

                    db.Authorization.Add(Authorization);
                    db.SaveChanges();

                    Log.Clear();
                    Pass.Clear();
                    FIO.Clear();

                    MessageBox.Show("Пользователь успешно добавлен!", "Severstal Infocom", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            }
        }
    }