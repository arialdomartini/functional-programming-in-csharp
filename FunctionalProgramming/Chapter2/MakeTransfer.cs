using System;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace FunctionalProgramming.Chapter2
{
    public abstract class Command {}
    
    public class MakeTransfer
    {
        public Guid DebitedAccount { get; set; }
        public string Beneficiary { get; set; }
        public string IBAN { get; set; }
        public string Bic { get; set; }

        public decimal Amount { get; set; }
        public DateTimeOffset Date { get; set; }
    }

    public interface IValidator<in T>
    {
        bool IsValid(T t);
    }
    
    public sealed class BicFormatValidator : IValidator<MakeTransfer>
    {

        static readonly Regex regex = new Regex("^[A-Z]{6}[A-Z1-9]{5}$");

        public bool IsValid(MakeTransfer cmd)
            => regex.IsMatch(cmd.Bic);
    }
    
    
    public class DateNotPastValidator : IValidator<MakeTransfer>
    {
        private readonly IDateTimeService _dateTimeService;

        public DateNotPastValidator(IDateTimeService dateTimeService)
        {
            _dateTimeService = dateTimeService;
        }

        public bool IsValid(MakeTransfer cmd)
            => _dateTimeService.Today <= cmd.Date.Date;
    }

    public class FunctionalDateNotPastValidatorTest
    {
        private readonly FunctionalDateValidator _sut = new FunctionalDateValidator();

        [Fact]
        public void future_dates_should_be_ok()
        {
            var today = new DateTimeOffset(2020, 11, 20, 10, 20, 0, TimeSpan.Zero);
            var tomorrow = today.AddDays(1);

            var result = _sut.IsValid(new MakeTransfer{ Date = tomorrow }, () => today);
            
            result.Should().Be(true);
        }
        
        [Fact]
        public void past_dates_should_not_be_ok()
        {
            var today = new DateTimeOffset(2020, 11, 20, 10, 20, 0, TimeSpan.Zero);
            var yesterday = today.AddDays(-1);

            var result = _sut.IsValid(new MakeTransfer{ Date = yesterday }, () => today);
            
            result.Should().Be(false);
        }
        
    }

    internal class FunctionalDateValidator : IFunctionalDateValidator
    {
        public bool IsValid(MakeTransfer makeTransfer, Func<DateTimeOffset> today) => 
            today() <= makeTransfer.Date;
    }

    internal interface IFunctionalDateValidator
    {
        bool IsValid(MakeTransfer makeTransfer, Func<DateTimeOffset> today);
    }

    public class DateNotPastValidatorTest
    {
        private readonly DateNotPastValidator _sut;
        private readonly IDateTimeService _dateTimeService;

        public DateNotPastValidatorTest()
        {
            _dateTimeService = Substitute.For<IDateTimeService>();
            _sut = new DateNotPastValidator(_dateTimeService);

        }

        [Fact]
        public void future_dates_should_be_ok()
        {
            var today = new DateTimeOffset(2020, 11, 20, 10, 20, 0, TimeSpan.Zero);
            _dateTimeService.Today.Returns(today);

            var tomorrow = today.AddDays(1);

            var result = _sut.IsValid(new MakeTransfer
            {
                Date = tomorrow
            });
            
            result.Should().Be(true);
        }

        [Fact]
        public void past_dates_should_not_be_ok()
        {
            var today = new DateTimeOffset(2020, 11, 20, 10, 20, 0, TimeSpan.Zero);
            _dateTimeService.Today.Returns(today);

            var yesterday = today.AddDays(-1);

            var result = _sut.IsValid(new MakeTransfer
            {
                Date = yesterday
            });
            
            result.Should().Be(false);
        }
    }

    public interface IDateTimeService
    {
        DateTimeOffset Today { get; }
    }
    
}