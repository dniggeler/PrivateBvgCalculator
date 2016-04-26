using System;
using System.Globalization;
using System.Threading.Tasks;
using BvgCalculatorEngine.Contracts;
using BvgCalculatorEngine.Contracts.Calculators;
using BvgCalculatorEngine.Implementation;
using FluentAssertions;
using Ninject;
using Xunit;
using Xunit.Abstractions;

namespace CalculatorBvg.Tests
{
    public class CalculatorBvgTests
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly StandardKernel _kernel;
        private readonly IBvgCalculator _calculator;

        public CalculatorBvgTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            _kernel = new StandardKernel(new NinjectSettings { LoadExtensions = false });
            _kernel.Load<BvgCalculatorModule>();
            _calculator = _kernel.Get<IBvgCalculator>();

        }

        [Trait("BVG Calculator Tests", "Altersguthaben Ende Jahr")]
        [Theory]
        [InlineData(152000, "1969/03/17", "2016/01/01", 200000.00, 208988.75)]
        [InlineData(66000, "1995/05/13", "2016/01/01", 0.00, 0.00)]
        public async Task AltersguthabenEndeJahrTest(int gemeldeterLohnAsInt, string dateOfBirthAsString, 
            string dateOfEintrittAsString, int einlageAsDouble, double expectedEndaltersguthabenAsDouble)
        {
            // given
            decimal gemeldeterLohn = gemeldeterLohnAsInt;
            decimal einlage = Convert.ToDecimal(einlageAsDouble);
            DateTime dateOfBirth = DateTime.Parse(dateOfBirthAsString, CultureInfo.InvariantCulture);
            DateTime dateOfEintritt = DateTime.Parse(dateOfEintrittAsString, CultureInfo.InvariantCulture);

            decimal expectedEndaltersguthaben = Convert.ToDecimal(expectedEndaltersguthabenAsDouble);

            var input = new BvgCalculationInput
            {
                Lohn = gemeldeterLohn,
                DateOfEintritt = dateOfEintritt,
                DateOfBirth = dateOfBirth,
                Altersguthaben = einlage,
                Geschlecht = Geschlecht.Mann,
            };

            // when
            var plan = new BvgPlan();
            var result = await _calculator.CalculateAsync(plan, input).ConfigureAwait(false);

            // then
            result.AlterguthabenEndeJahr.Should().Be(expectedEndaltersguthaben);

        }

        [Trait("BVG Calculator Tests", "Versicherter Lohn")]
        [Theory]
        [InlineData(152000, 59925)]
        [InlineData(84600, 59925)]
        [InlineData(40000, 15325)]
        [InlineData(25000, 3525)]
        [InlineData(21151, 3525)]
        [InlineData(21150, 0)]
        [InlineData(19000, 0)]
        public async Task VersicherterLohnTest(int gemeldeterLohnAsInt, int expectedVersicherterLohnAsInt)
        {
            // given
            decimal gemeldeterLohn = gemeldeterLohnAsInt;
            decimal expectedVersicherterLohn = expectedVersicherterLohnAsInt;

            var input = new BvgCalculationInput
            {
                Lohn = gemeldeterLohn,
                DateOfEintritt = new DateTime(2016,1,1),
                DateOfBirth = new DateTime(1969,3,17),
                Altersguthaben = 0m,
                Geschlecht = Geschlecht.Mann,
            };

            var calculator = _kernel.Get<IBvgCalculator>();
            var plan = new BvgPlan();

            // when
            var result = await calculator.CalculateAsync(plan, input).ConfigureAwait(false);

            // then
            result.VersicherterLohn.Should().Be(expectedVersicherterLohn);
        }

        [Trait("BVG Calculator Tests", "Altersgutschrift")]
        [Theory]
        [InlineData(152000, 8988.75, "1969/03/17")]
        [InlineData(84600, 8988.75, "1969/03/17")]
        [InlineData(40000, 2298.75, "1969/03/17")]
        [InlineData(25000, 528.75, "1969/03/17")]
        [InlineData(21151, 528.75, "1969/03/17")]
        [InlineData(21150, 0, "1969/03/17")]
        [InlineData(19000, 0, "1969/03/17")]
        // Stufe [55,65]
        [InlineData(40000, 2758.5, "1961/01/01")]
        // Stufe [45,54]
        [InlineData(40000, 2298.75, "1969/03/17")]
        // Stufe [35,44]
        [InlineData(40000, 1532.5, "1979/03/17")]
        // Stufe [25,34]
        [InlineData(40000, 1072.75, "1989/03/17")]
        // Stufe < 25
        [InlineData(40000, 0, "2000/03/17")]
        public async Task AltersgutschriftTest(int gemeldeterLohnAsInt, double expectedAltersgutschriftAsDouble, string dateOfBirthAsString)
        {
            _outputHelper.WriteLine($"Lohn={gemeldeterLohnAsInt}, AGS={expectedAltersgutschriftAsDouble}, DoB={dateOfBirthAsString}");

            // given
            decimal gemeldeterLohn = gemeldeterLohnAsInt;
            decimal expectedAltersgutschrift = Convert.ToDecimal( expectedAltersgutschriftAsDouble );
            DateTime dateOfBirth = DateTime.Parse(dateOfBirthAsString, CultureInfo.InvariantCulture);

            var input = new BvgCalculationInput
            {
                Lohn = gemeldeterLohn,
                DateOfEintritt = new DateTime(2016, 1, 1),
                DateOfBirth = dateOfBirth,
                Altersguthaben = 0m,
                Geschlecht = Geschlecht.Mann,
            };

            var calculator = _kernel.Get<IBvgCalculator>();
            var plan = new BvgPlan();

            // when
            var result = await calculator.CalculateAsync(plan, input).ConfigureAwait(false);

            // then
            result.Altersgutschrift.Should().Be(expectedAltersgutschrift);
        }

    }
}
