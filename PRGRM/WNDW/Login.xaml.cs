using Database;
using HP;
using PRGRM.WNDW;
using System.Windows;
using System.Windows.Input;
namespace PRGRM
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            UpdateCapsLockVisibility();
            Keyboard.AddKeyDownHandler(Application.Current.MainWindow, HandlerSub);
        }

        private void HandlerSub(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.CapsLock)
            {
                UpdateCapsLockVisibility();
            }
        }

        private void UpdateCapsLockVisibility()
        {
            capsLabel.Visibility = (Keyboard.GetKeyStates(Key.CapsLock) & KeyStates.Toggled) == KeyStates.Toggled
                ? Visibility.Visible
                : Visibility.Hidden;
        }

        private void BLogin(object sender, RoutedEventArgs e)
        {
            using (var db = new ApplicationContext())
            {
                var hashedPassword = sha256.HP(password.Password);

                var user = db.Authorization.FirstOrDefault(u => u.Login == login.Text && u.PassHash == hashedPassword);

                if (user != null)
                {
                    var window = new Main();
                    window.lplogin.Text = user.Login;

                    MessageBox.Show(
                        $"Добро пожаловать, {user.Login}",
                        "Severstal Infocom",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    window.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show(
                        "Введен неверный логин или пароль.",
                        "Severstal Infocom",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

        private void Login_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}