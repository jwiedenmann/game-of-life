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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //BitmapSource bitmapSource = BitmapSource.Create(2, 2, 96, 96, PixelFormats.Indexed1, new BitmapPalette(new List<Color> { Colors.Transparent }), new byte[] { 0, 0, 0, 0 }, 1);
            //WriteableBitmap writeableBitmap = new WriteableBitmap(bitmapSource);

            WriteableBitmap writeableBitmap = new(100, 100, 300, 300, PixelFormats.Bgra32, null);

            byte[] pixels = new byte[writeableBitmap.PixelHeight * writeableBitmap.PixelWidth * (writeableBitmap.Format.BitsPerPixel < 8 ? 1 : writeableBitmap.Format.BitsPerPixel / 8)];

            for (int y = 0; y < 40000; y++)
            {
                pixels[y] = 0xFF;
            }

            writeableBitmap.WritePixels(
                new Int32Rect(0, 0, writeableBitmap.PixelWidth, writeableBitmap.PixelHeight),
                pixels,
                writeableBitmap.PixelWidth * writeableBitmap.Format.BitsPerPixel / 8,
                0);

            gameImage.Source = writeableBitmap;

        }
    }
}
