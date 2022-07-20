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
        private uint _randomizationDensity;

        public GameOfLifeViewModel()
        {
            LoadedCommand = new RelayCommand(OnLoaded);
            RandomizeCommand = new RelayCommand(() => GameController?.RandomizeField(RandomizationDensity));
            ToggleRunningCommand = new RelayCommand(OnToggleRunning);

            Rows = 20;
            Columns = 20;
            CellSize = 20;
            GridThickness = 20;
            ShowGridLines = true;
            IterationDelay = 100;
            RandomizationDensity = 10;

            GameController = new GameController();
            GameDrawer = new GameDrawer();
        }

        public ICommand LoadedCommand { get; set; }
        public ICommand RandomizeCommand { get; set; }
        public ICommand ToggleRunningCommand { get; set; }

        public GameController GameController { get; set; }
        public GameDrawer GameDrawer { get; set; }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public int Rows
        {
            get => _rows;
            set => SetProperty(ref _rows, value);
        }

        public int Columns
        {
            get => _columns;
            set => SetProperty(ref _columns, value);
        }

        public int CellSize
        {
            get => _cellSize;
            set => SetProperty(ref _cellSize, value);
        }

        public int GridThickness
        {
            get => _gridThickness;
            set => SetProperty(ref _gridThickness, value);
        }

        public bool ShowGridLines
        {
            get => _showGridLines;
            set => SetProperty(ref _showGridLines, value);
        }

        public int IterationDelay
        {
            get => _iterationDelay;
            set
            {
                SetProperty(ref _iterationDelay, value);

                if (GameController != null)
                {
                    GameController.IterationDelay = IterationDelay;
                }
            }
        }

        public uint RandomizationDensity
        {
            get => _randomizationDensity;
            set => SetProperty(ref _randomizationDensity, value);
        }

        private void OnLoaded()
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