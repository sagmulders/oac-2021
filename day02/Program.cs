﻿// See https://aka.ms/new-console-template for more information

var path = Path.Combine(Environment.CurrentDirectory, "data");
var commands = File.ReadAllLines(path).Select(x => Command.Parse(x));

int pos = 0, depth = 0, aim = 0;

foreach (var command in commands)
{
    switch (command.Cmd)
    {
        case "up":
            {
                aim -= command.Value;
                break;
            }
        case "down":
            {
                aim += command.Value;
                break;
            }
        case "forward":
            {
                pos += command.Value;
                depth += aim * command.Value;
                break;
            }
        default:
            break;
    }
}

Console.WriteLine($"Final pos {pos} and depth {depth}, result {pos * depth}");


class Command
{
    public string Cmd { get; set; }
    public int Value { get; set; }

    public static Command Parse(string value)
    {
        var p = value.Split(' ');
        return new Command() { Cmd = p[0], Value = int.Parse(p[1]) };
    }
}
