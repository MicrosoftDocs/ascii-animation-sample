using System.Text;

// Increase FramesPerSecond for faster playback.
const int FramesPerSecond = 20;
const int RowsInCell = 13;
const string animationFrames = @"cells.txt";

StringBuilder _buffer = new();

using (var fileStream = File.OpenText(animationFrames))
{
    while (fileStream is { EndOfStream: false })
    {
        await ShowFramesAsync(fileStream); 
    }
}

Console.Clear();
Console.SetCursorPosition(0, 0);
Console.WriteLine("~~~ The End ~~~");

async Task ShowFramesAsync(StreamReader stream)
{
    _buffer.Clear();

    var FramesToDisplayCell = decimal.Parse(await stream.ReadLineAsync() ?? "0");

    for (var i = 0; i < RowsInCell; i++)
    {
        _buffer.AppendLine(await stream.ReadLineAsync());
    }

    Console.Clear();
    Console.SetCursorPosition(0, 0);
    Console.Write(_buffer.ToString());

    var secondsToDisplayCell = FramesToDisplayCell / FramesPerSecond;
    var ticksToDisplayCell = Convert.ToInt64(secondsToDisplayCell * TimeSpan.TicksPerSecond);
    await Task.Delay(TimeSpan.FromTicks(ticksToDisplayCell));
}
