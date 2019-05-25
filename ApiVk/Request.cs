using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace ApiVk
{
    public static class Request
    {
        public static VkApi GetAuthorisedVkApi(ulong appId, string login, string password)
        {
            var vkApi = new VkApi();

            vkApi.Authorize(new ApiAuthParams
            {
                ApplicationId = appId,
                Login = login,
                Password = password,
                Settings = Settings.All
            });

            return vkApi;
        }
    }
}