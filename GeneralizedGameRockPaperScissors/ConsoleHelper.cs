namespace GeneralizedGameRockPaperScissors
{
    internal static class ConsoleHelper
    {
        public static void WriteColoredLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }

}
