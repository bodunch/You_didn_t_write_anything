using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace You_didn_t_write_anything
{
    internal class Program
    {
        static int countTemptSenterNumber = 0;

        static void Main(string[] args)
        {
            MainMenu();
        }

        static void MainMenu()
        {
            Console.WriteLine("__  __               ___    __    _  __               _ __                       __  __   _          ");
            Console.WriteLine("\\ \\/ /__  __ __  ___/ (_)__/ /__ ( )/ /_  _    ______(_) /____   ___ ____  __ __/ /_/ /  (_)__  ___ _");
            Console.WriteLine(" \\  / _ \\/ // / / _  / / _  / _ \\ V/ __/ | |/|/ / __/ / __/ -_) / _ `/ _ \\/ // / __/ _ \\/ / _ \\/ _ `/   _   _   _ ");
            Console.WriteLine(" /_/\\___/\\_,_/  \\_,_/_/\\_,_/_//_/  \\__/  |__,__/_/ /_/\\__/\\__/  \\_,_/_//_/\\_, /\\__/_//_/_/_//_/\\_, /   (_) (_) (_)");
            Console.WriteLine("                                                                         /___/                /___/  ");
            Console.WriteLine(" ");
            Console.WriteLine("key shortcuts:  ESCAPE - clear all");
            Console.WriteLine("                SHIFT + ESCAPE - back to main menu");
            Console.WriteLine("                SHIFT + BACKSPACE - go out");
            Console.WriteLine(" ");
            Console.WriteLine("first, set the number of words after which erasing will begin (for example 8) :");
            int targetCountToClear = TryToTakeNum();
            Console.WriteLine(" ");
            Console.WriteLine("to start or reject enter: T/F");
            string choice = Console.ReadLine();
            if (choice == "T")
            {
                try
                {
                    StartProgram(targetCountToClear);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else if (choice == "F")
            {
                Console.Clear();
                return;
            }
            else
            {
                if(countTemptSenterNumber >= 5)
                {
                    Console.WriteLine("you're a clinical idiot, throw your computer out the window!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    Environment.Exit(0);
                }
                Console.Clear();
                Console.WriteLine("you`re idiot!");
                return;
            }
        }

        static int TryToTakeNum()
        {
            string targetCountToCleatStr = Console.ReadLine();
            int targetCountToClear = 0;
            if (int.TryParse(targetCountToCleatStr, out targetCountToClear))
            {
                targetCountToClear = int.Parse(targetCountToCleatStr);
                return targetCountToClear;
            }
            else
            {
                countTemptSenterNumber++;
                if (countTemptSenterNumber < 5)
                {
                    Console.WriteLine("enter a number damn");
                    TryToTakeNum();
                }
                else
                {
                    Console.WriteLine(" ");
                    Console.WriteLine("shit, that'll be 10, enough.");
                    return targetCountToClear = 10;
                }
            }
            return targetCountToClear;
        }

        static void StartProgram(int num)
        {
            Console.Clear();

            StringBuilder buffer = new StringBuilder();

            int spacesCount = 0;
            int startTop = Console.CursorTop;
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(intercept: true);

                    if (key.Key == ConsoleKey.Escape)
                    {
                        buffer = new StringBuilder();
                        Console.Clear();

                        if(key.Modifiers.HasFlag(ConsoleModifiers.Shift))
                        {
                            buffer = new StringBuilder();
                            Console.Clear();
                            MainMenu();
                        }
                    }

                    if (key.Key == ConsoleKey.Backspace && buffer.Length > 0)
                    {
                        buffer.Remove(buffer.Length - 1, 1);
                        Console.Write("\b \b");

                        spacesCount = FindAllSpaces(buffer);

                        if (key.Modifiers.HasFlag(ConsoleModifiers.Shift))
                        {
                            buffer = new StringBuilder();
                            Console.Clear();
                            Environment.Exit(0);
                            break;
                        }
                    }
                    else if (!char.IsControl(key.KeyChar))
                    {
                        buffer.Append(key.KeyChar);
                        Console.Write(key.KeyChar);
                    }

                    if (key.Key == ConsoleKey.Spacebar)
                        spacesCount++;

                    if (spacesCount >= num)
                    {
                        int firstSpace = buffer.ToString().IndexOf(' ');
                        if (firstSpace != -1)
                        {
                            buffer.Remove(0, firstSpace + 1);
                        }

                        Console.Clear();
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write(new string(' ', Console.BufferWidth));
                        Console.SetCursorPosition(0, Console.CursorTop);

                        Console.Write(buffer.ToString());

                        spacesCount = FindAllSpaces(buffer);
                    }
                }
            }
        }

        static int FindAllSpaces(StringBuilder buffer)
        {
            return buffer.ToString().Count(c => c == ' ');

            //string str = buffer.ToString();
            //int count = 0;
            //for(int i = 0; i < str.Length; i++)
            //{
            //    if (str[i] == ' ')
            //    {
            //        count++;
            //    }
            //}
            //return count;
        }
    }
}
