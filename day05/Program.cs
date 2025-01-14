﻿// See https://aka.ms/new-console-template for more information

var path = Path.Combine(Environment.CurrentDirectory, "data");
var lines = File.ReadAllLines(path).Select(x => new Line(x)).ToList();


Console.WriteLine($"{lines.Count()} lines");

int size = 1000;
int[,] map = new int[size, size];

lines.ForEach(x => x.Draw(map));

// draw map
int i = 0;
for (var y = 0; y < size; y++)
{
    Console.Write(i + " ");
    for (var x = 0; x < size; x++)
    {
        Console.Write(map[y, x] == 0 ? "." : map[y, x]);
    }
    i++;

    Console.Write(Environment.NewLine);
}

var points = 0;
for (var y = 0; y < size; y++)
{
    points += Enumerable.Range(0, map.GetLength(1))
                .Select(x => map[y, x])
                .Where(x => x >= 2).Count();
}

Console.WriteLine($"Answer: {points}");

class Line
{
    public Line(string data)
    {
        //Console.WriteLine($"line parse {data}");

        var parts = data.Split(" -> ");
        Begin = new Point(parts[0]);
        End = new Point(parts[1]);

        this.data = data;
    }

    private string data;
    public Point Begin { get; set; }
    public Point End { get; set; }

    internal void Draw(int[,] map)
    {
        if (Begin.X == End.X || Begin.Y == End.Y)
        {
            // horizontal or vertical
            var horizontal = Begin.Y == End.Y;
            var delta = Math.Abs(End.X - Begin.X + End.Y - Begin.Y) + 1;
            var x = Math.Min(Begin.X, End.X);
            var y = Math.Min(Begin.Y, End.Y);

            for (var i = 0; i < delta; i++)
            {
                if (horizontal)
                    map[y, x + i]++;
                else
                    map[y + i, x]++;
            }
        }
        else
        {
            //diagonal

            if ((Begin.X > End.X && Begin.Y < End.Y) || (End.X < Begin.X && End.Y < Begin.Y))
            {
                // reverse for easy drawing
                var temp = Begin;
                Begin = End;
                End = temp;
            }

            var down = Begin.Y < End.Y;
            var i = 0;

            while (true)
            {
                if (down)
                {
                    map[Begin.Y + i, Begin.X + i]++;
                }
                else
                {
                    map[Begin.Y - i, Begin.X + i]++;
                }

                if (Begin.X + i == End.X)
                    break;

                i++;
            }

        }
    }

    public override string ToString()
    {
        return this.data;
    }
}

class Point
{
    public Point(string data)
    {
        //Console.WriteLine($"Point parse {data}");

        var parts = data.Split(",");
        X = int.Parse(parts[0]);
        Y = int.Parse(parts[1]);
    }
    public int X { get; set; }
    public int Y { get; set; }
}