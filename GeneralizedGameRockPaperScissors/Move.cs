namespace GeneralizedGameRockPaperScissors
{
    public static class Move
    {
        public static int GetMovesCount(string[] args)
        {
            int movesCount = args.Length;
            if (movesCount % 2 == 0 || movesCount <= 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid number of arguments(moves). The number of moves must be odd and more than one.");
                Console.WriteLine("Please restart the program with an odd number of moves.");
                Console.ResetColor();
                Environment.Exit(0);
            }
            return movesCount;
        }

        public static Dictionary<string, string> CreateMovesDictionary(string[] args)
        {
            var moves = new Dictionary<string, string>();
            for (int i = 0; i < args.Length; i++)
            {
                moves.Add((i + 1).ToString(), args[i]);
            }
            moves.Add("0", "exit");
            moves.Add("?", "help");
            return moves;
        }

        public static string GetComputerMove(Dictionary<string, string> moves, int movesCount)
        {
            Random rnd = new Random();
            int value = rnd.Next(1, movesCount);
            return moves[value.ToString()];
        }

        public static void PrintMoveMenu(Dictionary<string, string> moves)
        {
            Console.WriteLine("Available moves:");
            foreach (var move in moves)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{move.Key} - {move.Value}");
                Console.ResetColor();
            }
        }

        public static string GetUserMove(Dictionary<string, string> moves, int movesCount)
        {
            while (true)
            {
                Console.Write("Enter your move: ");
                string userMoveInpKey = Console.ReadLine();

                switch (userMoveInpKey)
                {
                    case "0":
                        Console.WriteLine("Thanks for the game!");
                        Environment.Exit(0);
                        break;

                    case "?":
                        Rules.PrintHelpTable(moves);
                        PrintMoveMenu(moves);
                        break;

                    default:
                        if (int.TryParse(userMoveInpKey, out int selectedMove) && selectedMove >= 1 && selectedMove <= movesCount)
                        {
                            return moves[selectedMove.ToString()];
                        }
                        else
                        {
                            ConsoleHelper.WriteColoredLine("Incorrect input, please try again", ConsoleColor.Red);
                            PrintMoveMenu(moves);
                        }
                        break;
                }
            }
        }

        public static void DisplayMoves(string userMove, string computerMove)
        {
            Console.WriteLine("Your move: " + userMove);
            Console.WriteLine("Computer move: " + computerMove);
        }

    }
}
