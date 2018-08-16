using System;
using FluentAssertions;
using Xunit;

namespace FunctionalProgramming.Chapter1.FunctionalFeatures
{
    public class FunctionalUsing
    {
        public static Func<T, U> Using<T, U>(IDisposable resource, Func<T, U> func)
            =>
                x =>
                {
                    using (resource)
                    {
                        return func(x);
                    }
                };
    }

    public class UsingTest
    {
        [Fact]
        public void should_dispose_resource()
        {
            var resource = new Disposable();
            resource.Disposed.Should().Be(false);

            var withUsing = FunctionalUsing.Using<int, int>(
                resource, 
                x => x * 2);
            
            withUsing(10);

            resource.Disposed.Should().Be(true);
        }

        [Fact]
        public void should_invoke_the_function()
        {
            bool invoked = false;

            Func<int, int> doubleMe = x =>
            {
                invoked = true;
                return x * 2;
            };
            
            var resource = new Disposable();
            var withResource = FunctionalUsing.Using(resource, doubleMe);

            withResource(10).Should().Be(20);
            invoked.Should().Be(true);
        }
    }

    public class Disposable : IDisposable
    {
        public Disposable()
        {
            Disposed = false;
        }

        public void Dispose()
        {
            Disposed = true;
        }

        public bool Disposed { get; private set; }
    }
}