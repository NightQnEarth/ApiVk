using System;
using System.Text;
using VkNet.Exception;

namespace ApiVk
{
    static class Program
    {
        public static void Main(string[] args)
        {
            var options = UIDataParser.GetInputData(args);
            var (login, password) = UIDataParser.ReadLoginAndPasswordFromConsole();

            Console.OutputEncoding = Encoding.UTF8;

            try
            {
                using (var vkApi = ApiRequest.GetAuthorisedVkApi(options.AppId, login, password))
                {
                    var userId = options.TargetUserScreenName is null
                        ? vkApi.UserId
                        : ApiRequest.GetUserId(vkApi, options.TargetUserScreenName) ??
                          throw new VkApiException(
                              $"Not found specified target_user_screen_name: '{options.TargetUserScreenName}'.");

                    Console.Write(ResultGenerator.RepresentSpecifiedUserInformation(vkApi, userId, options));
                }
            }
            catch (VkApiException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}