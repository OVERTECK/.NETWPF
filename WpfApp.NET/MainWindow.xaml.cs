using System.Windows;

namespace Pract_3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MainWindow _context = null!;

        public MainWindow()
        {
            InitializeComponent();

            Input.owner = this;

            var table = new TableWindow();

            frame.Content = table.ShowDialog();

            //frame.Content = Input.getContext();
        }

        public static MainWindow getContext()
        {
            if (_context == null)
            {
                _context = new MainWindow();

                return _context;
            }

            return _context;
        }
    }
}
