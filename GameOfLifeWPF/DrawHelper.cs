using System;
using System.Windows;

namespace GameOfLifeWPF
{
    public class DrawHelper
    {
        private byte[]? _horizontalGridLinePixels;
        private byte[]? _verticalGridLinePixels;
        private byte[]? _whiteCellPixels;
        private byte[]? _blackCellPixels;
        private Int32Rect? _horizontalGridLineRect;
        private Int32Rect? _verticalGridLineRect;
        private Int32Rect? _cellRect;
        private int? _horizontalLineStride;
        private int? _verticalLineStride;
        private int? _cellStride;

        public int ColorChannelBytes { get; init; }
        public int CellSize { get; init; }
        public int GridThickness { get; init; }
        public int Rows { get; init; }
        public int Columns { get; init; }

        public int CalculatePixelWidth() => GridThickness + (CellSize + GridThickness) * Columns;
        public int CalculatePixelHeight() => GridThickness + (CellSize + GridThickness) * Rows;

        public byte[] GetHorizontalGridLinePixels()
        {
            if (_horizontalGridLinePixels is null)
            {
                int totalWidth = GridThickness + (CellSize + GridThickness) * Columns;
                _horizontalGridLinePixels = new byte[totalWidth * GridThickness * ColorChannelBytes];

                for (int i = 0; i < totalWidth; i++)
                {
                    DrawGreyPixel(i, _horizontalGridLinePixels);
                }
            }

            return _horizontalGridLinePixels;
        }

        public byte[] GetVerticalGridLinePixels()
        {
            if (_verticalGridLinePixels is null)
            {
                int totalHeight = GridThickness + (CellSize + GridThickness) * Rows;
                _verticalGridLinePixels = new byte[totalHeight * GridThickness * ColorChannelBytes];

                for (int i = 0; i < totalHeight; i++)
                {
                    DrawGreyPixel(i, _verticalGridLinePixels);
                }
            }

            return _verticalGridLinePixels;
        }

        public byte[] GetWhiteCellPixels()
        {
            if (_whiteCellPixels is null)
            {
                int totalSize = CellSize * CellSize;
                _whiteCellPixels = new byte[totalSize * ColorChannelBytes];

                for (int i = 0; i < totalSize; i++)
                {
                    DrawWhitePixel(i, _whiteCellPixels);
                }
            }

            return _whiteCellPixels;
        }

        public byte[] GetBlackCellPixels()
        {
            if (_blackCellPixels is null)
            {
                int totalSize = CellSize * CellSize;
                _blackCellPixels = new byte[totalSize * ColorChannelBytes];

                for (int i = 0; i < totalSize; i++)
                {
                    DrawBlackPixel(i, _blackCellPixels);
                }
            }

            return _blackCellPixels;
        }

        public Int32Rect GetHorizontalGridLineRect()
        {
            if (_horizontalGridLineRect is null)
            {
                int totalWidth = GridThickness + (CellSize + GridThickness) * Columns;
                _horizontalGridLineRect = new(0, 0, totalWidth, GridThickness);
            }

            return (Int32Rect)_horizontalGridLineRect;
        }

        public Int32Rect GetVerticalGridLineRect()
        {
            if (_verticalGridLineRect is null)
            {
                int totalHeight = GridThickness + (CellSize + GridThickness) * Rows;
                _verticalGridLineRect = new(0, 0, GridThickness, totalHeight);
            }

            return (Int32Rect)_verticalGridLineRect;
        }

        public Int32Rect GetCellRect()
        {
            if (_cellRect is null)
            {
                _cellRect = new(0, 0, CellSize, CellSize);
            }

            return (Int32Rect)_cellRect;
        }

        public int GetHorizontalLineStride()
        {
            if (_horizontalLineStride is null)
            {
                _horizontalLineStride = GridThickness + (CellSize + GridThickness) * Rows;
            }

            return (int)_horizontalLineStride;
        }

        public int GetVerticalLineStride()
        {
            if (_verticalLineStride is null)
            {
                _verticalLineStride = GridThickness;
            }

            return (int)_verticalLineStride;
        }

        public int GetCellStride()
        {
            if (_cellStride is null)
            {
                _cellStride = CellSize;
            }

            return (int)_cellStride;
        }

        private void DrawBlackPixel(int pixel, byte[] pixels)
        {
            int position = pixel * ColorChannelBytes;
            pixels[position + 0] = 0x00;
            pixels[position + 1] = 0x00;
            pixels[position + 2] = 0x00;
            pixels[position + 3] = 0xFF;
        }

        private void DrawWhitePixel(int pixel, byte[] pixels)
        {
            int position = pixel * ColorChannelBytes;
            pixels[position + 0] = 0xFF;
            pixels[position + 1] = 0xFF;
            pixels[position + 2] = 0xFF;
            pixels[position + 3] = 0xFF;
        }

        private void DrawGreyPixel(int pixel, byte[] pixels)
        {
            int position = pixel * ColorChannelBytes;
            pixels[position + 0] = 0x80;
            pixels[position + 1] = 0x80;
            pixels[position + 2] = 0x80;
            pixels[position + 3] = 0xFF;
        }
    }
}