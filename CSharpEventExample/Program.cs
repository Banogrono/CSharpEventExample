#nullable enable
using System;

namespace CSharpEventExample
{
    internal class Program
    {
        private static int MAX_SIZE = 20;
        
        static void Main()
        {
            Point p = new Point(MAX_SIZE/2, MAX_SIZE/2);
            
            p.OnPositionChanged += POnOnPositionChanged; // <- when this event is invoked, run following method.
            
            DrawPosition(p);

            while (true)
            {
                var key = Console.ReadKey();
                Console.WriteLine(key.Key);
                if (key.Key == ConsoleKey.UpArrow)
                {
                    p.Y--;
                }
                if (key.Key == ConsoleKey.DownArrow)
                {
                    p.Y++;
                }
                if (key.Key == ConsoleKey.LeftArrow)
                {
                    p.X--;
                }
                if (key.Key== ConsoleKey.RightArrow)
                {
                    p.X++;
                } 
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private static void POnOnPositionChanged(object? sender, Point e) // <- this method is called whenever the event is invoked 
        {
            Console.Clear();
            DrawPosition(e);
        }
        
        // utility method for drawing the point on console screen 
        private static void DrawPosition(Point p)
        {
            for (int y = 0; y < MAX_SIZE; y++)
            {
                for (int x = 0; x < MAX_SIZE * 2; x++)
                {
                    if (y == 1 && x == MAX_SIZE * 2 - 1)
                    {
                        Console.Write("| Position X: " + p.X);
                        continue;
                    }
                    
                    if (y == 2 && x == MAX_SIZE * 2 - 1)
                    {
                        Console.Write("| Position Y: " + p.Y);
                        continue;
                    }
                    
                    if (x == 0 || x == MAX_SIZE * 2 - 1)
                    {
                        Console.Write("|");
                        continue;
                    }
                    if (y == 0 || y == MAX_SIZE-1 )
                    {
                        Console.Write("-");
                        continue;
                    }
                    
                    
                    if (p.X == x && p.Y == y)
                    {
                        Console.Write(p.Signature);
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }

        // EVENT DEMONSTRATION CLASS 
        private class Point
        {
            public readonly string Signature;
            
            private int _x;
            private int _y;

            public int X
            {
                get => _x;
                set
                {
                    _x = value;
                    OnPositionChanged?.Invoke(this, this); // <- this is where I invoke event, I pass 'this" twice, 'cuz my event is returning object of this class essentially. 
                }
            }

            public int Y
            {
                get => _y;
                set
                {
                    _y = value;
                    OnPositionChanged?.Invoke(this, this); // <- again, if value is changed, then invoke event.
                }
            }
            
            public Point( int x, int y, string signature = "#")
            {
                Signature = signature;
                _x = x;
                _y = y;
            }
            
            public event EventHandler<Point>? OnPositionChanged; // <- this is my event
        }
    }
}