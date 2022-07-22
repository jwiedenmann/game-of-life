using System.Windows;

namespace GameOfLifeWPF
{
    public class DrawHelper
    {
        private byte[]? _horizontalGridLinePixels;
        private byte[]? _verticalGridLinePixels;
        private Int32Rect? _horizontalGridLineRect;
        private Int32Rect? _verticalGridLineRect;
        private int? _horizontalLineStride;
        private int? _verticalLineStride;

        public int ColorChannelBytes { get; init; }
        public int CellSize { get; init; }
        public int GridThickness { get; init; }
        public int Rows { get; init; }
        public int Columns { get; init; }

        public byte[] HorizontalGridLinePixels
        {
            get
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
            set => _horizontalGridLinePixels = value;
        }

        public byte[] VerticalGridLinePixels
        {
            get
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
            set => _verticalGridLinePixels = value;
        }

        public Int32Rect HorizontalGridLineRect
        {
            get
            {
                if (_horizontalGridLineRect is null)
                {
                    int totalWidth = GridThickness + (CellSize + GridThickness) * Columns;
                    _horizontalGridLineRect = new(0, 0, totalWidth, GridThickness);
                }

                return (Int32Rect)_horizontalGridLineRect;
            }
            set => _horizontalGridLineRect = value;
        }

        public Int32Rect VerticalGridLineRect
        {
            get
            {
                if (_verticalGridLineRect is null)
                {
                    int totalHeight = GridThickness + (CellSize + GridThickness) * Rows;
                    _verticalGridLineRect = new(0, 0, totalHeight, GridThickness);
                }

                return (Int32Rect)_verticalGridLineRect;
            }
            set => _verticalGridLineRect = value;
        }


        public int HorizontalLineStride
        {
            get
            {
                if (_horizontalLineStride is null)
                {
                    _horizontalLineStride = GridThickness + (CellSize + GridThickness) * Rows;
                }

                return (int)_horizontalLineStride;
            }
            set => _horizontalLineStride = value;
        }


        public int VerticalLineStride
        {
            get
            {
                if (_verticalLineStride is null)
                {
                    _verticalLineStride = GridThickness;
                }

                return (int)_verticalLineStride;
            }
            set => _verticalLineStride = value;
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