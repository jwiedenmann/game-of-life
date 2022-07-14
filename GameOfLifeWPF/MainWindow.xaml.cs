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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameOfLifeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public GameDrawer? GameDrawer { get; set; }

        private void Render(object? sender, EventArgs e)
        {
            gameImage.Source = GameDrawer?.Draw();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GameDrawer = new GameDrawer((int)borderCanvas.ActualWidth, (int)borderCanvas.ActualHeight);
            CompositionTarget.Rendering += Render;
        }
    }
}