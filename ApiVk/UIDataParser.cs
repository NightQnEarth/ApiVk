using System;
using CommandLine;

namespace ApiVk
{
    public static class UIDataParser
    {
        // ReSharper disable once ParameterTypeCanBeEnumerable.Global
        public static Options GetInputData(string[] args)
        {
            var options = new Options();

            Parser.Default.ParseArguments<Options>(args)
                  .WithParsed(inputOptions =>
                  {
                      options.TargetUserId = inputOptions.TargetUserId;
                      options.Friends = inputOptions.Friends;
                      options.PhotoAlbums = inputOptions.PhotoAlbums;
                  })
                  .WithNotParsed(errors => Environment.Exit(0));

            return options;
        }
    }
}