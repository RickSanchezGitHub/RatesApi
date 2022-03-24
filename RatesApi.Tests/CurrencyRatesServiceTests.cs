using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RatesApi.Core;
using RatesApi.Services;
using RatesApi.Services.Interface;
using RatesApi.Tests.TestCaseSourse;
using RatesApi.Tests.TestSourse;
using System.Threading.Tasks;

namespace RatesApi.Tests
{
    public class CurrencyRatesServiceTests
    {
        private CurrencyRatesService _currencyRatesService;
        private IOptions<Settings> _options;
        private Mock<IConverterService> _converterService;
        private Mock<IBaseClient> _baseClientMock;
        private BaseClientTestSourse _baseClientTestSourse;
        private CurrencyRatesServiceTestSourse _currencyRatesServiceTestSourse;


        [SetUp]
        public void Setup()
        {
            _options = Options.Create(new Settings());
            _converterService = new Mock<IConverterService>();
            _baseClientMock = new Mock<IBaseClient>();
            _baseClientTestSourse = new BaseClientTestSourse();
            _currencyRatesServiceTestSourse = new CurrencyRatesServiceTestSourse();
            _currencyRatesService = new CurrencyRatesService(_options, _converterService.Object, _baseClientMock.Object);

        }

        [Test]
        public void GetDataFromFirstSourceTest_ShouldReturnCurrencies()
        {
            var expected = _currencyRatesServiceTestSourse.GetFirstOutputData();
            var client = _baseClientTestSourse.GetFirstSourseBaseClient();
            _baseClientMock.Setup(c => c.GetResponseSourse(It.IsAny<string>())).Returns(Task.FromResult(client));
            _converterService.Setup(c => c.ConvertToDictionaryFirstSource(It.IsAny<JObject>())).Returns(expected);

            var actual = _currencyRatesService.GetDataFromFirstSource();

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Keys, actual.Result.Keys);
            Assert.AreEqual(expected.Values, actual.Result.Values);
            Assert.Pass();
            _baseClientMock.Verify(b => b.GetResponseSourse(It.IsAny<string>()), Times.Once());
            _converterService.Verify(c => c.ConvertToDictionaryFirstSource(client), Times.Once());
        }

        [Test]
        public void GetDataFromSecondSourceTest_ShouldReturnCurrencies()
        {
            var expected = _currencyRatesServiceTestSourse.GetSecondOutputData();
            var client = _baseClientTestSourse.GetSecondSourseBaseClient();
            _baseClientMock.Setup(c => c.GetResponseSourse(It.IsAny<string>())).Returns(Task.FromResult(client));
            _converterService.Setup(c => c.ConvertToDictionarySecondSource(It.IsAny<JObject>())).Returns(expected);

            var actual = _currencyRatesService.GetDataFromSecondSource();

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Keys, actual.Result.Keys);
            Assert.AreEqual(expected.Values, actual.Result.Values);
            Assert.Pass();
            _baseClientMock.Verify(b => b.GetResponseSourse(It.IsAny<string>()), Times.Once());
            _converterService.Verify(c => c.ConvertToDictionarySecondSource(client), Times.Once());
        }
    }
}