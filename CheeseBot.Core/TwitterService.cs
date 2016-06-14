using System;
using Tweetinvi;
using Tweetinvi.Core.Events.EventArguments;
using Tweetinvi.Core.Interfaces.Streaminvi;

namespace CheeseBot.Core
{
    public class TwitterService
    {
        //private IFilteredStream _stream;
        private IUserStream _userStream;

        public TwitterService()
        {
            Auth.SetUserCredentials(
                "8VbZaUz8aQsFnNWokBHz4O4xL",
                "jtyiFMaBKwOmEjmOlSIVeESwzr2tK7tSiTy5lrU5iQ0OtMUoAP",
                "736081535732846594-jIKv2rJrxNB2d6LLG5By6PJ2Fq4MapK",
                "50d7YXzcga5MU3wRgD2fOXIfcwusBvEEAkcwZBLLZJoZs");
        }

        public void InitialiseMentionStream(EventHandler<TweetReceivedEventArgs> whenMentioned)
        {
            var _userStream = Stream.CreateUserStream();
            _userStream.TweetCreatedByAnyoneButMe += whenMentioned;
            _userStream.StartStream();
        }

        public void ShutDownStream()
        {
            _userStream.StopStream();
        }
    }
}
