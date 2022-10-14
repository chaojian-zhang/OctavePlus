using Octave_;
using System.Drawing;
using Console = Colorful.Console;
using System.Windows.Forms;

// Print welcome
Console.WriteLine(
@"
GNU Octave, version 7.2.0
Copyright (C) 1993-2022 The Octave Project Developers.
This is free software; see the source code for copying conditions.
There is ABSOLUTELY NO WARRANTY; not even for MERCHANTABILITY or
FITNESS FOR A PARTICULAR PURPOSE.  For details, type 'warranty'.

Octave was configured for ""x86_64-w64-mingw32"".

Additional information about Octave is available at https://www.octave.org.

Please contribute if you find this software useful.
For more information, visit https://www.octave.org/get-involved.html

Read https://www.octave.org/bugs.html to learn how to submit bug reports.
For information about changes from previous versions, type 'news'.

".TrimStart());

// Read pescudo scripts
string scriptFile = "Scripts.txt";
if (!File.Exists(scriptFile))
{
    Console.Write($"Missing: {scriptFile}");
    return;
}
string[] scripts = File.ReadAllLines(scriptFile);

Point lastCursorPos = new Point(0, 0);
Cursor.Position = lastCursorPos;

int commandIndex = 0;
while (true)
{
    Console.Write($"octave:{commandIndex}> ", Color.Green);
    Console.WriteLine(scripts[(commandIndex * 2) % scripts.Length], Color.White);
    Thread.Sleep(new Random().Next(150, 1500));

    Console.WriteLine(scripts[(commandIndex * 2 + 1) % scripts.Length], Color.White);
    Thread.Sleep(new Random().Next(150, 1500));

    if (Cursor.Position != lastCursorPos)
        return;
    Point newCursorPos = new Point(new Random().Next(1024), new Random().Next(768));
    Cursor.Position = newCursorPos;
    lastCursorPos = newCursorPos;

    commandIndex++;
}