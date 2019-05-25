namespace ApiVk
{
    static class Program
    {
        public static void Main(string[] args)
        {
            var options = UIDataParser.GetInputData(args);
            var (login, password) = UIDataParser.ReadLoginAndPasswordFromConsole();

            using (var vkApi = Request.GetAuthorisedVkApi(options.AppId, login, password))
            {
                
            }
        }
    }
}