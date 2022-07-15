using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GameOfLifeWPF
{
    public class GameDrawer
    {
        private readonly WriteableBitmap _writeableBitmap;
        private readonly int _colorChannelBytes;
        private int _cellSize;
        private int _gridThickness;
        private bool _showGridLines;

        public GameDrawer(int width, int height)
        {
            _writeableBitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgra32, null);
            _colorChannelBytes = 4;

            CellSize = 50;
            GridThickness = 3;
            ShowGridLines = true;

            HorizontalCells = _writeableBitmap.PixelWidth / (CellSize + GridThickness);
            VerticalCells = _writeableBitmap.PixelHeight / (CellSize + GridThickness);

            Cells = new bool[VerticalCells, HorizontalCells];
        }

        public bool NeedRedraw { get; private set; } = true;
        public bool[,] Cells { get; private set; }
        public int HorizontalCells { get; set; }
        public int VerticalCells { get; set; }

        public int CellSize
        {
            get { return _cellSize; }
            set
            {
                _cellSize = value;
                NeedRedraw = true;
            }
        }

        public int GridThickness
        {
            get { return _gridThickness; }
            set
            {
                _gridThickness = value;
                NeedRedraw |= true;
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
            if (!NeedRedraw)
            {
                return _writeableBitmap;
            }

            ClearImage();

            if (ShowGridLines) DrawGridLines();
            DrawCells();

            NeedRedraw = false;
            return _writeableBitmap;
        }

        private void ClearImage()
        {
            Int32Rect sourceRect = new(0, 0, _writeableBitmap.PixelWidth, _writeableBitmap.PixelHeight);
            byte[] pixels = new byte[_writeableBitmap.PixelHeight * _writeableBitmap.PixelWidth * _colorChannelBytes];

            Array.Clear(pixels, 0, pixels.Length);
            _writeableBitmap.WritePixels(sourceRect, pixels, _writeableBitmap.PixelWidth * _colorChannelBytes, 0);
        }

        private void DrawCells()
        {
            Int32Rect cellRect = new(0, 0, CellSize, CellSize);
            int cellStride = CellSize * _colorChannelBytes;
            byte[] whiteCellPixels = new byte[CellSize * CellSize * _colorChannelBytes];
            byte[] blackCellPixels = new byte[CellSize * CellSize * _colorChannelBytes];

            for (int i = 0; i < CellSize * CellSize; i++)
            {
                DrawWhitePixel(i, whiteCellPixels);
                DrawBlackPixel(i, blackCellPixels);
            }

            int gridOffset = ShowGridLines ? GridThickness : 0;
            int yOffset = gridOffset;

            for (int y = 0; y < VerticalCells; y++)
            {
                int xOffset = gridOffset;

                for (int x = 0; x < HorizontalCells; x++)
                {
                    cellRect.Y = yOffset;
                    cellRect.X = xOffset;
                    byte[] cell = Cells[y, x] ? whiteCellPixels : blackCellPixels;
                    _writeableBitmap.WritePixels(cellRect, cell, cellStride, 0);

                    xOffset += CellSize + gridOffset;
                }
                yOffset += CellSize + gridOffset;
            }
        }

        private void DrawGridLines()
        {
            Int32Rect horizontalLine = new(0, 0, _writeableBitmap.PixelWidth, GridThickness);
            Int32Rect verticalLine = new(0, 0, GridThickness, _writeableBitmap.PixelHeight);
            byte[] horizontalPixels = new byte[_writeableBitmap.PixelWidth * GridThickness * _colorChannelBytes];
            byte[] verticalPixels = new byte[_writeableBitmap.PixelHeight * GridThickness * _colorChannelBytes];
            int horizontalStride = _writeableBitmap.PixelWidth * 4;
            int verticalStride = GridThickness * 4;

            for (int i = 0; i < _writeableBitmap.PixelWidth * GridThickness; i++)
            {
                DrawGreyPixel(i, horizontalPixels);
            }

            for (int i = 0; i < _writeableBitmap.PixelHeight * GridThickness; i++)
            {
                DrawGreyPixel(i, verticalPixels);
            }

            for (int i = 0; i < HorizontalCells; i++)
            {
                verticalLine.X = i * (CellSize + GridThickness);
                _writeableBitmap.WritePixels(verticalLine, verticalPixels, verticalStride, 0);
            }

            for (int i = 0; i < VerticalCells; i++)
            {
                horizontalLine.Y = i * (CellSize + GridThickness);
                _writeableBitmap.WritePixels(horizontalLine, horizontalPixels, horizontalStride, 0);
            }
        }

        private void DrawBlackPixel(int pixel, byte[] pixels)
        {
            int position = pixel * _colorChannelBytes;
            pixels[position + 0] = 0x00;
            pixels[position + 1] = 0x00;
            pixels[position + 2] = 0x00;
            pixels[position + 3] = 0xFF;
        }

        private void DrawWhitePixel(int pixel, byte[] pixels)
        {
            int position = pixel * _colorChannelBytes;
            pixels[position + 0] = 0xFF;
            pixels[position + 1] = 0xFF;
            pixels[position + 2] = 0xFF;
            pixels[position + 3] = 0xFF;
        }

        private void DrawGreyPixel(int pixel, byte[] pixels)
        {
            int position = pixel * _colorChannelBytes;
            pixels[position + 0] = 0x80;
            pixels[position + 1] = 0x80;
            pixels[position + 2] = 0x80;
            pixels[position + 3] = 0xFF;
        }
    }
}