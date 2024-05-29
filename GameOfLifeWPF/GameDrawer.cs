using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GameOfLifeWPF
{
    public class GameDrawer
    {
        private readonly int _colorChannelBytes;
        private int _cellSize;
        private int _gridThickness;
        private bool _showGridLines;

        public GameDrawer()
        {
            _colorChannelBytes = 4;

            CellSize = 30;
            GridThickness = 3;
            ShowGridLines = true;

            Cells = new bool[0, 0];
            DrawHelper = new DrawHelper()
            {
                ColorChannelBytes = _colorChannelBytes,
                CellSize = CellSize,
                GridThickness = GridThickness,
                Rows = 0,
                Columns = 0
            };
        }

        public bool IsInitialized { get; private set; } = false;
        public bool NeedRedraw { get; private set; } = true;
        public bool[,] Cells { get; private set; }
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public int CurrentPixelWidth { get; private set; }
        public int CurrentPixelHeight { get; private set; }
        public WriteableBitmap? WriteableBitmap { get; private set; }
        public DrawHelper DrawHelper { get; private set; }

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

        public void Init(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Cells = new bool[Rows, Columns];

            DrawHelper = new DrawHelper()
            {
                ColorChannelBytes = _colorChannelBytes,
                CellSize = CellSize,
                GridThickness = GridThickness,
                Rows = Rows,
                Columns = Columns
            };

            CurrentPixelWidth = DrawHelper.CalculatePixelWidth();
            CurrentPixelHeight = DrawHelper.CalculatePixelHeight();
            WriteableBitmap = new WriteableBitmap(CurrentPixelWidth, CurrentPixelHeight, 96, 96, PixelFormats.Bgra32, null);

            NeedRedraw = true;
            IsInitialized = true;
        }

        public WriteableBitmap? Draw(bool[,] cells)
        {
            if (!IsInitialized)
            {
                return WriteableBitmap;
            }

            Cells = cells;
            Init(Rows, Columns);

            if (ShowGridLines)
            {
                DrawGridLines();
            }

            DrawCells();

            NeedRedraw = false;
            return WriteableBitmap;
        }

        public WriteableBitmap? Draw(IEnumerable<ValueTuple<int, int, bool>> changes)
        {
            if (!IsInitialized || NeedRedraw || WriteableBitmap is null)
            {
                return WriteableBitmap;
            }

            Int32Rect cellRect = DrawHelper.GetCellRect();
            int gridOffset = ShowGridLines ? GridThickness : 0;

            foreach (var change in changes)
            {
                Cells[change.Item2, change.Item1] = change.Item3;

                cellRect.X = gridOffset + change.Item1 * (CellSize + gridOffset);
                cellRect.Y = gridOffset + change.Item2 * (CellSize + gridOffset);

                byte[] cell = change.Item3
                        ? DrawHelper.GetWhiteCellPixels()
                        : DrawHelper.GetBlackCellPixels();
                WriteableBitmap.WritePixels(
                    cellRect,
                    cell,
                    DrawHelper.GetCellStride(),
                    0);
            }

            return WriteableBitmap;
        }

        private void DrawCells()
        {
            if (WriteableBitmap is null)
            {
                return;
            }

            Int32Rect cellRect = DrawHelper.GetCellRect();

            int gridOffset = ShowGridLines ? GridThickness : 0;
            int yOffset = gridOffset;

            for (int y = 0; y < Rows; y++)
            {
                int xOffset = gridOffset;

                for (int x = 0; x < Columns; x++)
                {
                    cellRect.Y = yOffset;
                    cellRect.X = xOffset;
                    byte[] cell = Cells[y, x]
                        ? DrawHelper.GetWhiteCellPixels()
                        : DrawHelper.GetBlackCellPixels();
                    WriteableBitmap.WritePixels(
                        cellRect,
                        cell,
                        DrawHelper.GetCellStride(),
                        0);

                    xOffset += CellSize + gridOffset;
                }
                yOffset += CellSize + gridOffset;
            }
        }

        private void DrawGridLines()
        {
            if (WriteableBitmap is null)
            {
                return;
            }

            Int32Rect horizontalLine = DrawHelper.GetHorizontalGridLineRect();
            horizontalLine.X = 0;
            Int32Rect verticalLine = DrawHelper.GetVerticalGridLineRect();
            verticalLine.Y = 0;

            for (int i = 0; i < Columns + 1; i++)
            {
                verticalLine.X = i * (CellSize + GridThickness);
                WriteableBitmap.WritePixels(
                    verticalLine,
                    DrawHelper.GetVerticalGridLinePixels(),
                    DrawHelper.GetVerticalLineStride(),
                    0);
            }

            for (int i = 0; i < Rows + 1; i++)
            {
                horizontalLine.Y = i * (CellSize + GridThickness);
                WriteableBitmap.WritePixels(
                    horizontalLine,
                    DrawHelper.GetHorizontalGridLinePixels(),
                    DrawHelper.GetHorizontalLineStride(),
                    0);
            }
        }
    }
}