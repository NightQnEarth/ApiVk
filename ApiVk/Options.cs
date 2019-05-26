using System.Diagnostics.CodeAnalysis;
using CommandLine;

namespace ApiVk
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class Options
    {
        [Value(0, MetaName = "target_user_screen_name",
            HelpText = "Screen name of target user. By default gets screen name of authorised user." +
                       "Examples of screen name: 'nightqnearth' or 'id191434305'")]
        public string TargetUserScreenName { get; set; }

        [Option("app_id",
            Default = (ulong)6996875,
            HelpText = "APP_ID of your application.")]
        public ulong AppId { get; set; }

        [Option('f', "friends",
            Default = false,
            HelpText = "Print friends list of target user.")]
        public bool Friends { get; set; }

        [Option('v', "videos",
            Default = false,
            HelpText = "Print videos names for target user.")]
        public bool Videos { get; set; }

        [Option('p', "photo_albums",
            Default = false,
            HelpText = "Print photo albums names for target user.")]
        public bool PhotoAlbums { get; set; }
    }
}