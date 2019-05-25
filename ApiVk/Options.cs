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
            HelpText = "VK-id of target user.")]
        public string TargetUserId { get; set; }

        [Option('f', "friends",
            Default = false,
            HelpText = "Print friends list of target user.")]
        public bool Friends { get; set; }

        [Option('p', "photo_albums",
            Default = false,
            HelpText = "Print photo albums names of target user.")]
        public int PhotoAlbums { get; set; }
    }
}