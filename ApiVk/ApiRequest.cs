using System.Collections.Generic;
using System.Linq;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;

namespace ApiVk
{
    public static class ApiRequest
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

        public static long? GetUserId(VkApi vkApi, string screenName) => vkApi.Utils.ResolveScreenName(screenName)?.Id;

        // ReSharper disable once ReturnTypeCanBeEnumerable.Global
        public static List<User> GetFriends(VkApi vkApi, long? userId) =>
            vkApi.Friends.Get(new FriendsGetParams
                 {
                     Order = FriendsOrder.Hints,
                     Fields = ProfileFields.All,
                     UserId = userId ?? vkApi.UserId
                 })
                 .ToList();

        public static List<PhotoAlbum> GetPhotoAlbums(VkApi vkApi, long? userId) =>
            vkApi.Photo.GetAlbums(new PhotoGetAlbumsParams { OwnerId = userId })
                 .ToList();

        public static List<Video> GetVideos(VkApi vkApi, long? userId) =>
            vkApi.Video.Get(new VideoGetParams { OwnerId = userId })
                 .ToList();
    }
}