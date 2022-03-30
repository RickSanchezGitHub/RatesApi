using Moq;
using NUnit.Framework;
using RatesApi.Services;
using RatesApi.Tests.TestCaseSourse;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RatesApi.Services.Interface;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Options;
using RatesApi.Core;

namespace RatesApi.Tests
{
    public class ConvertServiceTests
    {
        private IConverterService _convertService;
        private ConvertTestSourse _convertTestSourse;
        private Mock<IRequiredCurrencies> _currencies;


        [SetUp]
        public void Setup()
        {
            _convertService = new ConverterService(_currencies.Object);
            _convertTestSourse = new ConvertTestSourse();
        }

        [Test]
        public void ConvertToDictionaryFirstSourceTest_ShouldReturnDictionary()
        {
            var testData = _convertTestSourse.GetJsonFirstData();
            var expected = _convertTestSourse.GetFirstOutputData();

            var actual = _convertService.ConvertToDictionaryFirstSource(testData);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ConvertToDictionaryFirstSourceNegativeTest_ShouldREturnExceptionNullReferenceException()
        {
            var testData = _convertTestSourse.GetJsonSecondData();

            Assert.Throws<NullReferenceException>(() => _convertService.ConvertToDictionaryFirstSource(testData));
        }

        [Test]
        public void ConvertToDictionarySecondSource()
        {
            var testData = _convertTestSourse.GetJsonSecondData();
            var expected = _convertTestSourse.GetSecondOutputData();

            var actual = _convertService.ConvertToDictionarySecondSource(testData);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ConvertToDictionarySecondSourceNegativeTest_ShouldReturnExceptionNullReferenceException()
        {
            var testData = _convertTestSourse.GetJsonFirstData();

            Assert.Throws<NullReferenceException>(() => _convertService.ConvertToDictionarySecondSource(testData));
        }
    }
}
