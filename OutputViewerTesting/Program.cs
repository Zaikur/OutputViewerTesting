using System;
using System.IO;
using System.Threading;

class Program
{
    private static string iniFilePath = "ScAbbuchen.ini";

    static void Main()
    {
        Console.WriteLine("Press Ctrl+C to exit.");

        // Create a timer that triggers the UpdateIniFile method every 5 seconds
        Timer timer = new Timer(UpdateIniFile, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

        // Keep the application running until Ctrl+C is pressed
        ManualResetEvent resetEvent = new ManualResetEvent(false);
        Console.CancelKeyPress += (sender, eventArgs) => resetEvent.Set();
        resetEvent.WaitOne();
    }

    static void UpdateIniFile(object state)
    {
        try
        {
            // Read all lines from the ini file
            string[] lines = File.ReadAllLines(iniFilePath);

            // Find the index of the "BtNr", "QuY", "QuZ", "Laenge", and "Ausstosser" lines
            int btNrIndex = Array.FindIndex(lines, line => line.StartsWith("BtNr="));
            int quYIndex = Array.FindIndex(lines, line => line.StartsWith("QuY="));
            int quZIndex = Array.FindIndex(lines, line => line.StartsWith("QuZ="));
            int laengeIndex = Array.FindIndex(lines, line => line.StartsWith("Laenge="));
            int ausstosserIndex = Array.FindIndex(lines, line => line.StartsWith("Ausstosser="));

            // Assign random values to the specified fields
            lines[btNrIndex] = $"BtNr={GetRandomNumber(1, 100)}";
            lines[quYIndex] = $"QuY={GetRandomNumber(50, 300)}";
            lines[quZIndex] = $"QuZ={GetRandomNumber(50, 300)}";
            lines[laengeIndex] = $"Laenge={GetRandomNumber(100, 10000)}";
            lines[ausstosserIndex] = $"Ausstosser={GetRandomNumber(1, 4)}";

            // Write the modified lines back to the ini file
            File.WriteAllLines(iniFilePath, lines);

            Console.WriteLine("Values updated successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static int GetRandomNumber(int minValue, int maxValue)
    {
        // Generate a random number between minValue and maxValue
        Random random = new Random();
        return random.Next(minValue, maxValue + 1);
    }
}
