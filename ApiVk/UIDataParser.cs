using System;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using CommandLine;

namespace ApiVk
{
    public static class UIDataParser
    {
        private static readonly ConsoleKey[] notCharacterKeys =
        {
            ConsoleKey.PageDown, ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.DownArrow, ConsoleKey.UpArrow,
            ConsoleKey.Backspace, ConsoleKey.Delete, ConsoleKey.End, ConsoleKey.Home, ConsoleKey.PageUp
        };

        // ReSharper disable once ParameterTypeCanBeEnumerable.Global
        public static Options GetInputData(string[] args)
        {
            var options = new Options();

            Parser.Default.ParseArguments<Options>(args)
                  .WithParsed(inputOptions =>
                  {
                      options.TargetUserScreenName = inputOptions.TargetUserScreenName;
                      options.AppId = inputOptions.AppId;
                      options.Friends = inputOptions.Friends;
                      options.Videos = inputOptions.Videos;
                      options.PhotoAlbums = inputOptions.PhotoAlbums;
                  })
                  .WithNotParsed(errors => Environment.Exit(0));

            return options;
        }

        public static (string login, string password) ReadLoginAndPasswordFromConsole()
        {
            Console.WriteLine("Type login:");
            var login = Console.ReadLine();

            Console.WriteLine("Type password:");
            var password = ReadPasswordFromConsole();

            Console.WriteLine();

            return (login, password);
        }

        private static string ReadPasswordFromConsole()
        {
            var password = new StringBuilder();

            while (true)
            {
                ConsoleKeyInfo passedKey = Console.ReadKey(true);

                switch (passedKey.Key)
                {
                    case ConsoleKey.Enter when password.Length == 0:
                        throw new AuthenticationException("Empty password was passed.");
                    case ConsoleKey.Enter:
                        return password.ToString();
                    case ConsoleKey.Backspace when password.Length > 0:
                        password.Remove(password.Length - 1, 1);
                        Console.Write("\b \b");
                        break;
                    default:
                    {
                        if (!notCharacterKeys.Contains(passedKey.Key))
                        {
                            password.Append(passedKey.KeyChar);
                            Console.Write("*");
                        }

                        break;
                    }
                }
            }
        }
    }
}