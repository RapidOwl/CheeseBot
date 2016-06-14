using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using CheeseBot.Core;
using Tweetinvi;
using Tweetinvi.Core.Events.EventArguments;
using Tweetinvi.Core.Interfaces.DTO;
using Tweetinvi.Core.Parameters;
using Stream = System.IO.Stream;

namespace CheeseBot.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var twitterService = new TwitterService();

            EventHandler<TweetReceivedEventArgs> tweetMatched = (sender, tweetArgs) =>
            {
                //System.Console.WriteLine(tweetArgs.Tweet.Text);

                var tweetToReplyTo = tweetArgs.Tweet;

                var assembly = Assembly.GetExecutingAssembly();
                var stream = assembly.GetManifestResourceStream("CheeseBot.Console.Images.Applewood.jpg");
                var imageBytes = ReadFully(stream);

                var media = Upload.UploadImage(imageBytes);

                var tweet = new PublishTweetParameters(
                    $"@{tweetToReplyTo.CreatedBy.ScreenName} Applewood smoked cheddar goes great with everything!",
                    new PublishTweetOptionalParameters
                    {
                        InReplyToTweetId = tweetToReplyTo.Id,
                        Medias = new List<IMedia> { media }
                    });

                Tweet.PublishTweet(tweet);
            };

            try
            {
                twitterService.InitialiseMentionStream(tweetMatched);
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            
            System.Console.ReadLine();

            twitterService.ShutDownStream();
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
