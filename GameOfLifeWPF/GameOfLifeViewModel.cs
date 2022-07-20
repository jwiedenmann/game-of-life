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

        public GameOfLifeViewModel()
        {
            LoadedCommand = new RelayCommand(OnLoaded);
            ToggleRunningCommand = new RelayCommand(OnToggleRunning);

            Rows = 20;
            Columns = 20;
            CellSize = 20;
            GridThickness = 20;
            ShowGridLines = true;


        }

        public ICommand LoadedCommand { get; set; }
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

        private void OnLoaded()
        {
        }

        private void OnToggleRunning()
        {
            IsRunning = !IsRunning;
        }

        public ImageSource RenderImage()
        {
            throw new Exception();
        }
    }
}