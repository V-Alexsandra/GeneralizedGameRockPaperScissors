using GeneralizedGameRockPaperScissors;

class Program
{
    public static int FindKeyByValue(Dictionary<string, string> dictionary, string targetValue)
    {
        var pair = dictionary.FirstOrDefault(kvp => kvp.Value == targetValue);

        if (!pair.Equals(default(KeyValuePair<string, string>)))
        {
            return Convert.ToInt32(pair.Key);
        }

        return -1;
    }

    static void Main(string[] args)
    {
        int movesCount = Move.GetMovesCount(args);

        Rules.GenerateRules(movesCount);

        while (true)
        {
            var moves = Move.CreateMovesDictionary(args);
            byte[] key = KeyGeneration.GenerateKey();

            string computerMove = Move.GetComputerMove(moves, movesCount);

            HMACGeneration.GenerateHMAC(key, computerMove);

            Move.PrintMoveMenu(moves);

            string userMove = Move.GetUserMove(moves, movesCount);

            int userMoveKey = FindKeyByValue(moves, userMove);
            int computerMoveKey = FindKeyByValue(moves, computerMove);

            Move.DisplayMoves(moves[userMoveKey.ToString()], computerMove);

            Rules.CheckMove(userMoveKey, computerMoveKey);
            Console.WriteLine("HMAC Key: " + BitConverter.ToString(key).Replace("-", "").ToUpper());

            Console.WriteLine("");

            ConsoleHelper.WriteColoredLine("New game started!", ConsoleColor.Magenta);
        }
    }
}