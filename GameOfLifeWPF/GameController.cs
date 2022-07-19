using System;
using System.Linq;
using System.Timers;

namespace GameOfLifeWPF
{
    public class GameController
    {
        private Timer _timer;

        public GameController(int width, int height)
        {
            Field = new bool[height, width];
            Width = width;
            Height = height;

            _timer = new Timer(IterationDelay);
            _timer.Elapsed += GameStep;
        }

        public int Iteration { get; set; }
        public int IterationDelay { get; set; } = 100;
        public bool[,] Field { get; set; }
        public int Width { get; }
        public int Height { get; }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void GameStep(object? sender, ElapsedEventArgs e)
        {
            var tempField = (bool[,])Field.Clone();
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var neighbours = CalculateNeighbours(x, y);
                    var alive = Field[y, x];
                    tempField[y, x] = (alive && neighbours > 1 && neighbours < 4) || (!alive && neighbours == 3);
                }
            }

            Field = tempField;
            Iteration++;
        }

        private int CalculateNeighbours(int x, int y)
        {
            int heightIndex = Height - 1;
            int widthIndex = Width - 1;
            var top = y > 0 ? y - 1 : heightIndex;
            var bottom = y < heightIndex ? y + 1 : 0;
            var left = x > 0 ? x - 1 : widthIndex;
            var right = x < widthIndex ? x + 1 : 0;

            bool[] neighbours = new bool[]
            {
                 Field[top, left],     Field[top, x],      Field[top, right],
                 Field[y, left],       false,              Field[y, right],
                 Field[bottom, left],  Field[bottom, x],   Field[bottom, right]
            };

            return neighbours.Count(x => x);
        }

        public void RandomiseField()
        {
            var random = new Random();

            for (int y = 0; y < Field.GetLength(0); y++)
            {
                for (int x = 0; x < Field.GetLength(1); x++)
                {
                    Field[y, x] = random.Next(0, 100) >= 90;
                }
            }
        }
    }
}