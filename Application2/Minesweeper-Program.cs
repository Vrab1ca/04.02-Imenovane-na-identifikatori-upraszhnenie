using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minesweeper
{
    public class Minesweeper
    {
        public class ForTheList
        {
            private string name;

            private int points;

            public string Player
            {
                get
                {
                    return name;
                }

                set
                {
                    name = value;
                }
            }

            public int Howmuch
            {
                get
                {
                    return points;
                }

                set
                {
                    points = value;
                }
            }

            public ForTheList()
            {
            }

            public ForTheList(string Name, int Points)
            {
                this.name = Name;
                this.points = Points;
            }
        }

        private static void Main(string[] аргументи)
        {
            string command = string.Empty;
            char[,] field = create_playing_field();
            char[,] Bomb = placebomb();
            int counter = 0;
            bool explosion = false;
            List<ForTheList> champions = new List<ForTheList>(6);
            int red = 0;
            int column = 0;
            bool flag = true;
            const int maks = 35;
            bool flag2 = false;

            do
            {
                if (flag)
                {
                    Console.WriteLine(
                        "Let's play “Minesweeper”. Try your luck at finding the fields without Minesweeper. " +
                         " Command 'top' shows the ranking, 'restart' starts a new game, 'exit' exits and bye!");
                    dumpp(field);
                    flag = false;
                }

                Console.Write("Give red и column : ");
                command = Console.ReadLine().Trim();
                if (command.Length >= 3)
                {
                    if (int.TryParse(command[0].ToString(), out red) && int.TryParse(command[2].ToString(), out column)
                        && red <= field.GetLength(0) && column <= field.GetLength(1))
                    {
                        command = "turn";
                    }
                }

                switch (command)
                {
                    case "top":
                        ranking(champions);
                        break;
                    case "restart":
                        field = create_playing_field();
                        Bomb = placebomb();
                        dumpp(field);
                        explosion = false;
                        flag = false;
                        break;
                    case "exit":
                        Console.WriteLine("Bye, bye, bye!");
                        break;
                    case "turn":
                        if (Bomb[red, column] != '*')
                        {
                            if (Bomb[red, column] == '-')
                            {
                                tisinahod(field, Bomb, red, column);
                                counter++;
                            }

                            if (maks == counter)
                            {
                                flag2 = true;
                            }
                            else
                            {
                                dumpp(field);
                            }
                        }
                        else
                        {
                            explosion = true;
                        }

                        break;
                    default:
                        Console.WriteLine("\nError! invalid command\n");
                        break;
                }

                if (explosion)
                {
                    dumpp(Bomb);
                    Console.Write("\nHrrrrrrrr! The hero died with {0} points. " + "Give yourself a nickname: ", counter);
                    string nickname = Console.ReadLine();
                    ForTheList t = new ForTheList(nickname, counter);
                    if (champions.Count < 5)
                    {
                        champions.Add(t);
                    }
                    else
                    {
                        for (int i = 0; i < champions.Count; i++)
                        {
                            if (champions[i].Howmuch < t.Howmuch)
                            {
                                champions.Insert(i, t);
                                champions.RemoveAt(champions.Count - 1);
                                break;
                            }
                        }
                    }

                    champions.Sort((ForTheList r1, ForTheList r2) => r2.Player.CompareTo(r1.Player));
                    champions.Sort((ForTheList r1, ForTheList r2) => r2.Howmuch.CompareTo(r1.Howmuch));
                    ranking(champions);

                    field = create_playing_field();
                    Bomb = placebomb();
                    counter = 0;
                    explosion = false;
                    flag = true;
                }

                if (flag2)
                {
                    Console.WriteLine("\nBRAVOOOOO! Opened 35 cells without a drop of blood.");
                    dumpp(Bomb);
                    Console.WriteLine("Give me your name, brother: ");
                    string imeee = Console.ReadLine();
                    ForTheList to4kii = new ForTheList(imeee, counter);
                    champions.Add(to4kii);
                    ranking(champions);
                    field = create_playing_field();
                    Bomb = placebomb();
                    counter = 0;
                    flag2 = false;
                    flag = true;
                }
            }
            while (command != "exit");
            Console.WriteLine("Made in Bulgaria - Uauahahahahaha!");
            Console.WriteLine("Leeettssss'ggoooooo");
            Console.Read();
        }
        private static void ranking(List<ForTheList> to4kii)
        {
            Console.WriteLine("\nPoints:");
            if (to4kii.Count > 0)
            {
                for (int i = 0; i < to4kii.Count; i++)
                {
                    Console.WriteLine("{0}. {1} --> {2} box", i + 1, to4kii[i].Player, to4kii[i].Howmuch);
                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("empty ranking\n");
            }
        }

        private static void tisinahod(char[,] POLE, char[,] BOMBI, int RED, int column)
        {
            char HowmuchBombi = Howmuch(BOMBI, RED, column);
            BOMBI[RED, column] = HowmuchBombi;
            POLE[RED, column] = HowmuchBombi;
        }

        private static void dumpp(char[,] board)
        {
            int R = board.GetLength(0);
            int K = board.GetLength(1);
            Console.WriteLine("\n    0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("   ---------------------");
            for (int i = 0; i < R; i++)
            {
                Console.Write("{0} | ", i);
                for (int j = 0; j < K; j++)
                {
                    Console.Write(string.Format("{0} ", board[i, j]));
                }

                Console.Write("|");
                Console.WriteLine();
            }

            Console.WriteLine("   ---------------------\n");
        }

        private static char[,] create_playing_field()
        {
            int boardRows = 5;
            int boardColumns = 10;
            char[,] board = new char[boardRows, boardColumns];
            for (int i = 0; i < boardRows; i++)
            {
                for (int j = 0; j < boardColumns; j++)
                {
                    board[i, j] = '?';
                }
            }

            return board;
        }

        private static char[,] placebomb()
        {
            int lines = 5;
            int columns = 10;
            char[,] playing_field = new char[lines, columns];

            for (int i = 0; i < lines; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    playing_field[i, j] = '-';
                }
            }

            List<int> r3 = new List<int>();
            while (r3.Count < 15)
            {
                Random random = new Random();
                int AS = random.Next(50);
                if (!r3.Contains(AS))
                {
                    r3.Add(AS);
                }
            }

            foreach (int i2 in r3)
            {
                int Col = i2 / columns;
                int red = i2 % columns;
                if (red == 0 && i2 != 0)
                {
                    Col--;
                    red = columns;
                }
                else
                {
                    red++;
                }

                playing_field[Col, red - 1] = '*';
            }

            return playing_field;
        }

        private static char Howmuch(char[,] PL, int BN, int R)
        {
            int numbers = 0;
            int reds = PL.GetLength(0);
            int Cols = PL.GetLength(1);

            if (BN - 1 >= 0)
            {
                if (PL[BN - 1, R] == '*')
                {
                    numbers++;
                }
            }

            if (BN + 1 < reds)
            {
                if (PL[BN + 1, R] == '*')
                {
                    numbers++;
                }
            }

            if (R - 1 >= 0)
            {
                if (PL[BN, R - 1] == '*')
                {
                    numbers++;
                }
            }

            if (R + 1 < Cols)
            {
                if (PL[BN, R + 1] == '*')
                {
                    numbers++;
                }
            }

            if ((BN - 1 >= 0) && (R - 1 >= 0))
            {
                if (PL[BN - 1, R - 1] == '*')
                {
                    numbers++;
                }
            }

            if ((BN - 1 >= 0) && (R + 1 < Cols))
            {
                if (PL[BN - 1, R + 1] == '*')
                {
                    numbers++;
                }
            }

            if ((BN + 1 < reds) && (R - 1 >= 0))
            {
                if (PL[BN + 1, R - 1] == '*')
                {
                    numbers++;
                }
            }

            if ((BN + 1 < reds) && (R + 1 < Cols))
            {
                if (PL[BN + 1, R + 1] == '*')
                {
                    numbers++;
                }
            }

            return char.Parse(numbers.ToString());
        }
    }
}