using System;
using System.Linq;
using System.Timers;

namespace GameOfLifeWPF
{
    public class GameController
    {
        private readonly Timer _timer;
        private int _iterationDelay = 100;

        public GameController()
        {
            Field = new bool[0, 0];
            IterationDelay = 100;

            _timer = new Timer(IterationDelay);
            _timer.Elapsed += (_, _) => GameStep();
        }

        public bool IsInitialized { get; private set; } = false;
        public int Iteration { get; private set; }
        public bool[,] Field { get; private set; }
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public int IterationDelay
        {
            get => _iterationDelay;
            set
            {
                _iterationDelay = value;

                if (_timer != null)
                {
                    _timer.Interval = IterationDelay;
                }
            }
        }

        public void Init(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Field = new bool[Rows, Columns];

            IsInitialized = true;
        }

        public void Start()
        {
            if (IsInitialized)
            {
                _timer.Start();
            }
        }

        public void Stop()
        {
            if (IsInitialized)
            {
                _timer.Stop();
            }
        }

        public void GameStep()
        {
            if (!IsInitialized)
            {
                return;
            }

            var tempField = (bool[,])Field.Clone();
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
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
            int heightIndex = Rows - 1;
            int widthIndex = Columns - 1;
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

        public void RandomizeField(uint density)
        {
            var random = new Random();

            for (int y = 0; y < Field.GetLength(0); y++)
            {
                for (int x = 0; x < Field.GetLength(1); x++)
                {
                    Field[y, x] = random.Next(0, 100) >= (100 - density);
                }
            }
        }
    }
}