using System;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace GameOfLife
{
    public class GameController
    {
        private const int _delay = 50;
        public event Action<bool[,]> Draw;

        public GameController(int width, int height)
        {
            Field = new bool[height, width];
            Width = width;
            Height = height;
        }

        public int Iteration { get; set; }
        public bool[,] Field { get; set; }
        public int Width { get; }
        public int Height { get; }

        public void Play()
        {
            RandomiseField();
            Draw?.Invoke(Field);
            var sw = new Stopwatch();

            while (true)
            {
                sw.Restart();
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
                Console.Title = $"Iteration: {Iteration}";
                Draw?.Invoke(Field);
                sw.Stop();

                if ((int)sw.ElapsedMilliseconds < _delay)
                {
                    Thread.Sleep(_delay - (int)sw.ElapsedMilliseconds);
                }
            }
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

        private void RandomiseField()
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