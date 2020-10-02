using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {

        class Game
        {
            private enum Move
            {
                Cross,
                Nolik,
            }
            public static void Start()
            {
                int inpute;
                const int size = 3;
                string[,] ticTacToe = new string[size, size];
                Console.Write("Start Game\n");
                do
                {
                    Console.WriteLine("Выберите кто первый ходит");
                    Console.Write("1.Крестик\n2.Нолик\n[Enter]");
                    inpute = int.Parse(Console.ReadLine());
                } while (inpute < 1 || inpute > 2);

                Console.WriteLine();
                (bool win, Move move) temp; //Принимает значения из функции победы
                temp.win = false;
                //Будем определять кто ходит по чётным и не чётным числам 
                for (int i = inpute; i < inpute + 9; i++)
                {
                    if (i % 2 == 0)
                    {
                        Console.WriteLine("Ходит нолик");
                        EnterMove(ticTacToe, size, Move.Nolik, ref i);
                    }
                    else
                    {
                        Console.WriteLine("Ходит крестик");
                        EnterMove(ticTacToe, size, Move.Cross, ref i);
                    }
                    Console.WriteLine();
                    //Функция WinGame проверяет есть ли победитель и возвращает 2 типа (bool, Move)
                    temp = WinGame(ticTacToe, size);
                    if (temp.win)
                    {
                        switch (temp.move)
                        {
                            case Move.Cross:
                                Console.WriteLine("Победил крестик");
                                break;
                            case Move.Nolik:
                                Console.WriteLine("Победил нолик");
                                break;
                        }
                        break;
                    }
                    //Выводит матрицу в консоль
                    GetAllGame(ticTacToe, size);
                }
                if (!temp.win) 
                {
                    Console.WriteLine("\nПоля законьчились, ничья!");
                }
            }

            //Функция победы
            private static (bool, Move) WinGame(string[,] mass, int size)
            {
                //Проверяем поля на Х
                if (ProtectedWinGame(mass, size, "X"))
                {
                    return (true, Move.Cross);
                }
                // Проверяем поля на 0
                else if(ProtectedWinGame(mass, size, "0"))
                {
                    return (true, Move.Nolik);
                }
                else // Возвращаем false и Move.Nolik(Он возвращается просто так без дальнейших действий т.к победителя нет но вернуть нужно 2 значения bool и Move
                {
                    return (false, Move.Nolik);
                }
            }
            //Возвращает true если введённая строка есть в массиве по диагонале, горизонтале, вертикале и false если нет
            private static bool ProtectedWinGame(string[,] mass, int size, string Coordinate)
            {
                //Проверим 2 диагонали на одинаковые строки 
                (int, int) win = (win.Item1 = 0, win.Item2 = 0);
                for (int i = 0, j = 2; i < size; i++, j--)
                {
                    if (mass[i, i] == Coordinate)
                    {
                        win.Item1++;
                    }
                    if (mass[i, j] == Coordinate)
                    {
                        win.Item2++;
                    }
                }
                if (win.Item1 == 3 || win.Item2 == 3)
                {
                    return true;
                }

                win.Item1 = 0;
                win.Item2 = 0;
                //Проверяем горизонтально 
                for (int i = 0; i < size; i++)
                {
                    win.Item1 = 0;
                    for (int j = 0; j < size; j++)
                    {
                        if (mass[i, j] == Coordinate)
                        {
                            win.Item1++;
                        }
                    }
                    if (win.Item1 == 3)
                    {
                        break;
                    }
                }
                if (win.Item1 == 3)
                {
                    return true;
                }
                //Проверяем вертикально
                for (int i = 0; i < size; i++)
                {
                    win.Item1 = 0;
                    for (int j = 0; j < size; j++)
                    {
                        if (mass[j, i] == Coordinate)
                        {
                            win.Item1++;
                        }
                    }
                    if (win.Item1 == 3)
                    {
                        break;
                    }
                }
                if (win.Item1 == 3)
                {
                    return true;
                }
                return false;
            }

            //Функция шага 
            private static void EnterMove(string[,] mass, int size, Move move, ref int i)
            {
                (int x, int y) Coordinate;
                Console.WriteLine("Введите координаты шага X и Y от 1 до 3");
                do
                {
                    Console.Write("X = ");
                    Coordinate.x = int.Parse(Console.ReadLine());
                } while (Coordinate.x < 1 || Coordinate.x > 3);
                Coordinate.x--;
                do
                {
                    Console.Write("Y = ");
                    Coordinate.y = int.Parse(Console.ReadLine());
                } while (Coordinate.y < 1 || Coordinate.y > 3);
                Coordinate.y--;
                if (mass[Coordinate.x, Coordinate.y] == null)
                {
                    switch (move)
                    {
                        case Move.Cross:
                            mass[Coordinate.x, Coordinate.y] = "X";
                            break;
                        case Move.Nolik:
                            mass[Coordinate.x, Coordinate.y] = "0";
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    //Так как ход не засчитался отнимаем 1 от i что бы игрок мог сходить заного
                    i--;
                    Console.WriteLine("[Erorr]Нельзя ходить на уже заполненое поле!");
                }
            }
            //Вывод нашей матрицы в консоль
            private static void GetAllGame(string[,] mass, int size)
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (j == 0)
                            Console.Write($"\t{mass[i, j]}\t");
                        else
                            Console.Write($"|\t{mass[i, j]}\t");
                    }
                    Console.WriteLine();
                }
            }

        }

        static void Main(string[] args)
        {
            Game.Start();
            

        }
    }
}