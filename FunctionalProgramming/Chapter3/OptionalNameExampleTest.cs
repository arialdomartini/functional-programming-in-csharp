using FluentAssertions;
using Xunit;

namespace FunctionalProgramming.Chapter3
{
    public struct Subscriber
    {
        public Option<string> Name { get; set; }
        public string Email { get; set; }
    }

    public static class Subscription
    {
        public static string GreetingsFor(Subscriber subscriber) =>
            $"Dear {subscriber.Name.Match(() => "subscriber", r => r)}, your mail is {subscriber.Email}";
    }
    public class OptionalNameExampleTest
    {
        [Fact]
        public void should_greet_subscribers_who_provided_their_name()
        {
            var subscriber = new Subscriber {Name = "Mario", Email = "tux@tux.com"};

            var result = Subscription.GreetingsFor(subscriber);

            result.Should().Be("Dear Mario, your mail is tux@tux.com");
        }
        
        [Fact]
        public void should_greet_subscribers_who_didn_t_provide_names()
        {
            var subscriber = new Subscriber {Email = "tux@tux.com"};

            var result = Subscription.GreetingsFor(subscriber);

            result.Should().Be("Dear subscriber, your mail is tux@tux.com");
        }
    }
}