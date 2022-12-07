using ForrestGump.Entities;
using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Brushes = System.Drawing.Brushes;
using Point = System.Drawing.Point;

namespace ForrestGump
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Forrest_GumpEntities db = new Forrest_GumpEntities();
        public MainWindow()
        {
            InitializeComponent();
            //   int i=1;
            //   foreach (var emploee in db.Emploees)
            //   {
            //       string path = @"D:\\Emploees\" + i + ".JPEG"; // Путь к картинкам + их названия
            //       if(i==6)
            //       {
            //            path = @"D:\\Emploees\" + i + ".JPG"; // Путь к картинкам + их названия
            //       }
            //       byte[] imageInBytes = System.IO.File.ReadAllBytes(path); // в массив байт
            //       emploee.Photo = imageInBytes; // массив байт в таблицу
            //       i++;
            //   }
            //   db.SaveChanges();
            //  i = 0;
        }
        private string text = String.Empty;
        private Bitmap CreateImage(int Width, int Height)
        {
            Random rnd = new Random();

            //Создадим изображение
            Bitmap result = new Bitmap(Width, Height);

            //Вычислим позицию текста
            int Xpos = rnd.Next(0, Width - 95);
            int Ypos = rnd.Next(15, Height - 25);

            //Добавим различные цвета
            System.Drawing.Brush[] colors = { Brushes.Black,
            Brushes.Red,
            Brushes.RoyalBlue,
            Brushes.Green,
            Brushes.DarkBlue};

            //Укажем где рисовать
            Graphics g = Graphics.FromImage((System.Drawing.Image)result);

            //Пусть фон картинки будет серым
            g.Clear(System.Drawing.Color.Gray);

            //Сгенерируем текст
            text = String.Empty;
            string ALF = "1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
            for (int i = 0; i < 5; ++i)
                text += ALF[rnd.Next(ALF.Length)];

            //Сгенерируем формат текста
            string[] st =
            {
                "Arial",
                "Times New Roman",
                "Lucida Calligraphy"
                };
            //Нарисуем сгенирируемый текст
            g.DrawString(text,
            new Font(st[rnd.Next(st.Length)], 15),
            colors[rnd.Next(colors.Length)],
            new PointF(Xpos, Ypos));

            //Добавим немного помех
            /////Линии из углов
            g.DrawLine(Pens.Black,
            new Point(0, 0),
            new Point(Width - 1, Height - 1));
            g.DrawLine(Pens.Black,
            new Point(0, Height - 1),
            new Point(Width - 1, 0));
            ////Белые точки
            for (int i = 0; i < Width; ++i)
                for (int j = 0; j < Height; ++j)
                    if (rnd.Next() % 20 == 0)
                        result.SetPixel(i, j, System.Drawing.Color.White);

            return result;
        }

        //If you get 'dllimport unknown'-, then add 'using System.Runtime.InteropServices;'
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        public ImageSource ImageSourceFromBitmap(Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { DeleteObject(handle); }
        }

        private void CaphBtn_Click(object sender, RoutedEventArgs e)
        {
            if (text == capchaTB.Text)
            {
                autorizationBtn.IsEnabled = true;
                ImCaph.Visibility = Visibility.Hidden;
                capchaTB.Visibility = Visibility.Hidden;
                CaphBtn.Visibility = Visibility.Hidden;
                capchaTB.Text = null;
                i = 0;
            }
            else
            {
                ImCaph.Source = ImageSourceFromBitmap(CreateImage(200, 50));
            }
        }

        int i = 0;



        private void Login(object sender, RoutedEventArgs e)
        {
            // Создание переменной типа Emploee
            Emploee incomingUser = null;

            // Внесение в переменную данных о пользователи, которые соответствуют условию
            incomingUser = db.Emploees.Where(b => b.E_mail == MailBx.Text).FirstOrDefault();
            
            // Проверка данных пользователя
            if (incomingUser != null)
            {
                if (incomingUser.Password == passwordBx.Password)
                {
                    ConnectionWindow connection = new ConnectionWindow();
                    App.Emploee_id.id = incomingUser.Id;

                    // Открытие нужной формы, в зависимости от должности пользователя
                    switch (incomingUser.Id_jobTitle)
                    {
                        case 1:
                            LoginHistory loginHistorySaller = new LoginHistory()
                            {
                                LoginDate = DateTime.Now,
                                Id_Employee = incomingUser.Id,
                                SuccessfulLogin = true,
                                Login = incomingUser.E_mail
                                
                            };
                            Forrest_GumpEntities context = new Forrest_GumpEntities();
                            context.LoginHistories.Add(loginHistorySaller);
                            context.SaveChanges();
                            Window g = new Home(ref incomingUser);
                            g.Show();
                            break;
                        case 2:
                            LoginHistory loginHistorySeniorSeller = new LoginHistory()
                            {
                                LoginDate = DateTime.Now,
                                Id_Employee = incomingUser.Id,
                                SuccessfulLogin = true,
                                Login = incomingUser.E_mail

                            };
                            Forrest_GumpEntities context2 = new Forrest_GumpEntities();
                            context2.LoginHistories.Add(loginHistorySeniorSeller);
                            context2.SaveChanges();
                            Window s = new HomeSeniorSeller(ref incomingUser);
                            s.Show();
                            break;
                        case 3:
                            LoginHistory loginHistoryAdministrator = new LoginHistory()
                            {
                                LoginDate = DateTime.Now,
                                Id_Employee = incomingUser.Id,
                                SuccessfulLogin = true,
                                Login = incomingUser.E_mail
                            };
                            Forrest_GumpEntities context3 = new Forrest_GumpEntities();
                            context3.LoginHistories.Add(loginHistoryAdministrator);
                            context3.SaveChanges();
                            Window a = new AdminProfile(ref incomingUser);
                            a.Show();
                            break;
                    }
                    Close();
                }
                else
                {
                    MessageBox.Show("Пароль введен неверно", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);

                    LoginHistory loginHistory = new LoginHistory()
                    {
                        LoginDate = DateTime.Now,
                        Id_Employee = incomingUser.Id,
                        Login = incomingUser.E_mail,
                        SuccessfulLogin = false
                    };
                    Forrest_GumpEntities context = new Forrest_GumpEntities();
                    context.LoginHistories.Add(loginHistory);
                    context.LoginHistories.Add(loginHistory);
                    context.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Такой пользователь не найден", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                i = i + 1;
                if (i == 3)
                {
                    ImCaph.Visibility = Visibility.Visible;
                    ImCaph.Source = ImageSourceFromBitmap(CreateImage(200, 50));
                    capchaTB.Visibility = Visibility.Visible;
                    CaphBtn.Visibility = Visibility.Visible;
                    autorizationBtn.IsEnabled = false;
                }
            }
        }

        private void UnWiewPasswordOrange(object sender, RoutedEventArgs e)
        {
            passwordView.Text = "";
            passwordView.Visibility = Visibility.Hidden;
            passwordBx.Visibility = Visibility.Visible;

        }
        private void viewPasswordOrange(object sender, RoutedEventArgs e)
        {
            passwordView.Text = passwordBx.Password;
            passwordView.Visibility = Visibility.Visible;
            passwordBx.Visibility = Visibility.Hidden;

        }
    }
}
