using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameOfLife
{
    public class ConsoleDrawer
    {
        private readonly Point _startPoint;
        private readonly int _width;
        private readonly int _height;
        private readonly Dictionary<(bool, bool), char> _characterDictionary;

        public ConsoleDrawer(Point start, int width, int height)
        {
            _startPoint = start;
            _width = width;
            _height = height;
            _characterDictionary = new Dictionary<(bool, bool), char>()
            {
                { (false, false), '\u0020' },
                { (true , false), '\u2580' },
                { (false, true) , '\u2584' },
                { (true , true) , '\u2588' }
            };
        }

        public void Draw(bool[,] pixels)
        {
            if (pixels.GetLength(0) != _height || pixels.GetLength(1) != _width)
            {
                throw new ArgumentException(nameof(pixels));
            }

            Console.SetCursorPosition(_startPoint.X, _startPoint.Y);

            for (int y = 0; y < _height; y += 2)
            {
                var line = string.Empty;

                for (int x = 0; x < _width; x++)
                {
                    _ = _characterDictionary.TryGetValue((pixels[y, x], pixels[y + 1, x]), out char character);
                    line += character;
                }

                Console.WriteLine(line);
            }
        }
    }
}