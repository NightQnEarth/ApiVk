using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VkNet;
using VkNet.Model;
using VkNet.Model.Attachments;

namespace ApiVk
{
    public static class ResultGenerator
    {
        public static string RepresentSpecifiedUserInformation(VkApi vkApi, long? userId, Options options) =>
            new StringBuilder("Information about ")
                .Append($"{(vkApi.UserId == userId ? "authorised user" : $"user '{options.TargetUserScreenName}'")}:")
                .Append(Environment.NewLine)
                .Append(Environment.NewLine)
                .Append(options.Friends ? GetFriendsRepresentation(ApiRequest.GetFriends(vkApi, userId)) : string.Empty)
                .Append(options.Videos ? GetVideosRepresentation(ApiRequest.GetVideos(vkApi, userId)) : string.Empty)
                .Append(options.PhotoAlbums
                            ? GetPhotoAlbumsRepresentation(ApiRequest.GetPhotoAlbums(vkApi, userId))
                            : string.Empty)
                .ToString();

        private static string GetEntitiesRepresentation<T>(
            List<T> entities, string entityName, Func<List<T>, IEnumerable<T>> sort,
            Func<T, string> title, Func<T, string> description)
        {
            if (entities.Count == 0) return $"User hasn't {entityName.ToLower()}.{Environment.NewLine}";

            var result = new StringBuilder($"{entityName} total count: {entities.Count}{Environment.NewLine}");
            var maxNameLength = entities.Max(entity => title(entity).Length);

            foreach (var entity in sort(entities))
            {
                var stubLength = maxNameLength - title(entity).Length;

                result.Append(title(entity))
                      .Append(' ', stubLength + 1)
                      .Append(description(entity))
                      .Append(Environment.NewLine);
            }

            return result.Append(Environment.NewLine).ToString();
        }

        private static string GetFriendsRepresentation(List<User> users)
        {
            return GetEntitiesRepresentation(users, "Friends",
                                             usersList => usersList.OrderByDescending(user => user.Online),
                                             user => $"{user.FirstName} {user.LastName}", GetDescription);

            string GetDescription(User user)
            {
                var lastSeenInfo = user.IsDeactivated ? "Profile deactivated" :
                    user.LastSeen?.Time is null       ? "Offline" : $"was online at {user.LastSeen?.Time}";

                return $"[{(user.Online ?? false ? "Online" : lastSeenInfo)}]";
            }
        }

        private static string GetPhotoAlbumsRepresentation(List<PhotoAlbum> photoAlbums) =>
            GetEntitiesRepresentation(photoAlbums, "Photo albums",
                                      albums => albums.OrderByDescending(album => album.Size), album => album.Title,
                                      album => $"[was created at {album.Created}, photos count: {album.Size}]");

        private static string GetVideosRepresentation(List<Video> videos) =>
            GetEntitiesRepresentation(videos, "Videos", videosList => videosList, video => video.Title,
                                      video => $"[duration: {TimeSpan.FromSeconds(video.Duration ?? 0):mm\\:ss}]");
    }
}