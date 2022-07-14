using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GameOfLifeWPF
{
    public class GameDrawer
    {
        private readonly WriteableBitmap _writeableBitmap;
        private readonly Int32Rect _sourceRect;
        private readonly int _colorChannelBytes;
        private readonly byte[] _pixels;
        private bool _showGridLines;

        public GameDrawer(int width, int height)
        {
            _writeableBitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgra32, null);
            _sourceRect = new(0, 0, _writeableBitmap.PixelWidth, _writeableBitmap.PixelHeight);
            _colorChannelBytes = 4;
            _pixels = new byte[_writeableBitmap.PixelHeight * _writeableBitmap.PixelWidth * _colorChannelBytes];
        }

        public bool NeedRedraw { get; private set; } = true;
        private int _cellSize;

        public int CellSize
        {
            get { return _cellSize; }
            set
            { 
                _cellSize = value; 
                NeedRedraw = true;
            }
        }

        public bool ShowGridLines
        {
            get { return _showGridLines; }
            set
            {
                _showGridLines = value;
                NeedRedraw = true;
            }
        }

        public WriteableBitmap Draw()
        {
            Array.Clear(_pixels, 0, _pixels.Length);

            for (int y = 0; y < 20000; y++)
            {
                _pixels[y * _colorChannelBytes] = 0xFF;
                _pixels[y * _colorChannelBytes + 1] = 0x00;
                _pixels[y * _colorChannelBytes + 2] = 0xFF;
                _pixels[y * _colorChannelBytes + 3] = 0xFF;
            }

            if (ShowGridLines) DrawGridLines();


            _writeableBitmap.WritePixels(_sourceRect, _pixels, _writeableBitmap.PixelWidth * 4, 0);

            NeedRedraw = false;
            return _writeableBitmap;
        }

        private void DrawGridLines()
        {
            
        }
    }
}