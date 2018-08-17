using System;
using FluentAssertions;
using Xunit;

namespace FunctionalProgramming.Chapter1.Exercises
{
    public class F
    {
        public static TReturn Using<TResource, TReturn>(Func<TResource> disposable, Func<TResource, TReturn> f)
        where TResource : IDisposable
        {
            using (var disp = disposable())
            {
                return f(disp);
            }
        }
    }

    public class DisposableTest
    {
        [Fact]
        public void should_dispose_of_resource_after_use()
        {
            var disposable = new Disposable("some value");

            F.Using(() => disposable, d => "some result");

            disposable.Disposed.Should().Be(true);
        }

        [Fact]
        public void should_invoke_the_function()
        {
            var disposable = new Disposable("some value");

            var result = F.Using(() => disposable, d => $"disposable contains {d.SomeValue}");

            result.Should().Be("disposable contains some value");
        }
    }
    
    public class Disposable : IDisposable
    {
        public string SomeValue { get; }

        public Disposable(string someValue)
        {
            SomeValue = someValue;
            Disposed = false;
        }

        public void Dispose()
        {
            Disposed = true;
        }

        public bool Disposed { get; private set; }
    }

}