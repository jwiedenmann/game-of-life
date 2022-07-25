using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows.Input;
using System.Windows.Media;

namespace GameOfLifeWPF
{
    public class GameOfLifeViewModel : ObservableObject
    {
        private bool _isRunning;
        private int _rows;
        private int _columns;
        private int _cellSize;
        private int _gridThickness;
        private bool _showGridLines;
        private int _iterationDelay;

        public GameOfLifeViewModel()
        {
            LoadedCommand = new RelayCommand(InitGameOfLife);
            RandomizeCommand = new RelayCommand(() => GameController?.RandomizeField(RandomizationDensity));
            ToggleRunningCommand = new RelayCommand(OnToggleRunning);

            GameController = new GameController();
            GameDrawer = new GameDrawer();

            Rows = 20;
            Columns = 20;
            CellSize = 40;
            GridThickness = 3;
            ShowGridLines = true;
            IterationDelay = 100;
            RandomizationDensity = 10;
        }

        public ICommand LoadedCommand { get; set; }
        public ICommand RandomizeCommand { get; set; }
        public ICommand ToggleRunningCommand { get; set; }

        public GameController GameController { get; set; }
        public GameDrawer GameDrawer { get; set; }
        public uint RandomizationDensity { get; set; }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public int Rows
        {
            get => _rows;
            set
            {
                if (value < 1)
                {
                    return;
                }

                SetProperty(ref _rows, value);
                InitGameOfLife();
            }
        }

        public int Columns
        {
            get => _columns;
            set
            {
                if (value < 1)
                {
                    return;
                }

                SetProperty(ref _columns, value);
                InitGameOfLife();
            }
        }

        public int CellSize
        {
            get => _cellSize;
            set
            {
                if (value < 1)
                {
                    return;
                }

                SetProperty(ref _cellSize, value);
                GameDrawer.CellSize = CellSize;
            }
        }

        public int GridThickness
        {
            get => _gridThickness;
            set
            {
                if (value < 0)
                {
                    return;
                }

                SetProperty(ref _gridThickness, value);
                GameDrawer.GridThickness = GridThickness;
            }
        }

        public bool ShowGridLines
        {
            get => _showGridLines;
            set
            {
                SetProperty(ref _showGridLines, value);
                GameDrawer.ShowGridLines = ShowGridLines;
            }
        }

        public int IterationDelay
        {
            get => _iterationDelay;
            set
            {
                if (value < 1)
                {
                    return;
                }

                SetProperty(ref _iterationDelay, value);
                GameController.IterationDelay = IterationDelay;
            }
        }

        private void InitGameOfLife()
        {
            GameController.Init(Rows, Columns);
            GameDrawer.Init(Rows, Columns);
        }

        private void OnToggleRunning()
        {
            IsRunning = !IsRunning;

            if (IsRunning)
            {
                GameController.Start();
            }
            else
            {
                GameController.Stop();
            }
        }

        public ImageSource? RenderImage()
        {
            return GameDrawer.Draw(GameController.Field);
        }
    }
}