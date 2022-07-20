using System.Windows.Controls;
using System.Windows.Media;

namespace GameOfLifeWPF
{
    /// <summary>
    /// Interaction logic for GameOfLifeView.xaml
    /// </summary>
    public partial class GameOfLifeView : UserControl
    {
        private readonly GameOfLifeViewModel _gameOfLifeViewModel;

        public GameOfLifeView()
        {
            InitializeComponent();

            _gameOfLifeViewModel = new();
            DataContext = _gameOfLifeViewModel;

        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            CompositionTarget.Rendering += CompositionTarget_Rendering; ;
        }

        private void CompositionTarget_Rendering(object? sender, System.EventArgs e)
        {
            gameImage.Source = _gameOfLifeViewModel.RenderImage();
        }
    }
}