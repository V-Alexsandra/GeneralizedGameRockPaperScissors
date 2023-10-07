namespace GeneralizedGameRockPaperScissors
{
    public static class Rules
    {
        public static Dictionary<int, int[]> lossRules;

        public static void GenerateRules(int movesCount)
        {
            lossRules = new Dictionary<int, int[]>();

            for (int move = 1; move <= movesCount; move++)
            {
                int lossMove = move;
                int[] losingMoves = new int[movesCount / 2];
                for (int i = 0; i < losingMoves.Length; i++)
                {
                    lossMove++;
                    if (lossMove > movesCount)
                    {
                        lossMove = lossMove % movesCount;
                    }
                    losingMoves[i] = lossMove;
                }
                lossRules.Add(move, losingMoves);
            }
        }

        public static void CheckMove(int userMove, int computerMove)
        {
            if (userMove == computerMove)
            {
                ConsoleHelper.WriteColoredLine("It's a draw", ConsoleColor.Cyan);
            }
            else if (lossRules.ContainsKey(userMove))
            {
                int[] losingMoves = lossRules[userMove];
                bool isUserWin = !losingMoves.Contains(computerMove);

                if (isUserWin)
                {
                    ConsoleHelper.WriteColoredLine("You win!", ConsoleColor.Green);
                }
                else
                {
                    ConsoleHelper.WriteColoredLine("You lose", ConsoleColor.DarkRed);
                }
            }
            else Console.WriteLine("Something wrong");
        }

        private static Dictionary<string, string> GetMovesWithoutLastTwo(Dictionary<string, string> moves)
        {
            return moves.Take(moves.Count - 2).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        private static void PrintHorizontalLine(int maxMoveWidth, Dictionary<string, string> movesWithoutLastTwo)
        {
            string horizontalLine = "+";
            for (int j = 0; j < movesWithoutLastTwo.Count + 1; j++)
            {
                for (int i = 0; i < maxMoveWidth; i++)
                {
                    horizontalLine += "-";
                }
                horizontalLine += "+";
            }

            Console.WriteLine(horizontalLine);
        }

        private static void PrintMovesComparisonTable(Dictionary<string, string> movesWithoutLastTwo, int maxMoveWidth)
        {
            foreach (var move1 in movesWithoutLastTwo)
            {
                Console.Write($"| {move1.Value}".PadRight(maxMoveWidth) + " |");

                foreach (var move2 in movesWithoutLastTwo)
                {
                    if (move1.Key == move2.Key)
                    {
                        Console.Write(" Draw".PadRight(maxMoveWidth - 1) + " |");
                    }
                    else
                    {
                        string cellValue = GetMoveComparisonResult(move1, move2);
                        Console.Write($" {cellValue} ".PadRight(maxMoveWidth - 1) + " |");
                    }
                }

                Console.WriteLine();
                PrintHorizontalLine(maxMoveWidth, movesWithoutLastTwo);
            }
        }

        private static string GetMoveComparisonResult(KeyValuePair<string, string> move1, KeyValuePair<string, string> move2)
        {
            int userMoveKey = int.Parse(move1.Key);
            int computerMoveKey = int.Parse(move2.Key);

            if (lossRules.ContainsKey(userMoveKey))
            {
                int[] losingMoves = lossRules[userMoveKey];
                bool isUserWin = !losingMoves.Contains(computerMoveKey);

                return isUserWin ? "Win" : "Lose";
            }

            return "";
        }

        public static void PrintHelpTable(Dictionary<string, string> moves)
        {
            var movesWithoutLastTwo = GetMovesWithoutLastTwo(moves);

            int maxMoveWidth = movesWithoutLastTwo.Values.Max(v => v.Length);
            string header = "| v PC\\User >";

            if (header.Length > maxMoveWidth)
            {
                maxMoveWidth = header.Length;
            }

            PrintHorizontalLine(maxMoveWidth, movesWithoutLastTwo);

            Console.Write(header.PadRight(maxMoveWidth-2) + " |");
            foreach (var move in movesWithoutLastTwo)
            {
                Console.Write($" {move.Value} ".PadRight(maxMoveWidth)+"|");
            }
            Console.WriteLine();

            PrintHorizontalLine(maxMoveWidth, movesWithoutLastTwo);

            PrintMovesComparisonTable(movesWithoutLastTwo, maxMoveWidth);
        }
    }
}

