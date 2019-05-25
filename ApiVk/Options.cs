using System.Diagnostics.CodeAnalysis;
using CommandLine;

namespace ApiVk
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class Options
    {
        [Value(0, MetaName = "target_user_id",
            Required = true,
            HelpText = "user_id of target user.")]
        public ulong TargetUserId { get; set; }

        [Option('a', "app_id",
            Default = (ulong)6996875,
            HelpText = "APP_ID of your application.")]
        public ulong AppId { get; set; }

        [Option('f', "friends",
            Default = false,
            HelpText = "Print friends list of target user.")]
        public bool Friends { get; set; }

        [Option('p', "photo_albums",
            Default = false,
            HelpText = "Print photo albums names of target user.")]
        public bool PhotoAlbums { get; set; }
    }
}