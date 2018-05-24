using System.Collections.Generic;
using NUnit.Framework;
using TrainTickets;
using Assert = NUnit.Framework.Assert;
using Moq;

namespace Test
{
    [TestFixture]
    public class StationFinderMockTest
    {
        private IStationFinder _stationFinder;
        private Suggestions _suggestions;

        private Mock<IDataAccess> _dataAccess; 
        
        //Unit test should be testing the logic instead of testing if it can get data from db
        //that should be in integration test instead

        [SetUp]
        public void SetUp()
        {
            //Arrange
            _suggestions = new Suggestions();

            _dataAccess = new Mock<IDataAccess>();
            _stationFinder = new StationFinder(_dataAccess.Object); //Using Mock object
        }

        [Test]
        public void GIVEN_a_valid_request_WHEN_stationFinder_is_called_TEHN_should_call_GetName_once()
        {
            //Arrange
            var result = GetValidResult();

            _dataAccess.Setup(x => x.GetStationList()).Returns(result);
            _dataAccess.Setup(x => x.GetName(It.IsAny<string>())).ReturnsAsync("testgetname");

            //Act
            var response = _stationFinder.GetSuggestions("dart");

            //Assert
            _dataAccess.Verify(x => x.GetName(It.IsAny<string>()),Times.Once);
        }

        [Test]
        public void GIVEN_a_valid_request_WHEN_stationFinder_is_called_TEHN_should_call_GetStationList_once()
        {
            //Arrange
            var result = GetValidResult();

            _dataAccess.Setup(x => x.GetStationList()).Returns(result);
            _dataAccess.Setup(x => x.GetName(It.IsAny<string>())).ReturnsAsync("testgetname");

            //Act
            var response = _stationFinder.GetSuggestions("dart");

            //Assert
            _dataAccess.Verify(x => x.GetStationList(), Times.Once);
        }

        [Test]
        public void GIVEN_Dart_WHEN_stationFinder_is_called_TEHN_should_return_correct_response()
        {
            //Arrange
            var result = GetValidResult();

            _dataAccess.Setup(x => x.GetStationList()).Returns(result);
            _dataAccess.Setup(x => x.GetName(It.IsAny<string>())).ReturnsAsync("testgetname");

            //Act
            var response = _stationFinder.GetSuggestions("Dart");

            _suggestions.NextLetters = new HashSet<string>()
            {
                "F","M"
            };
            _suggestions.Stations = new HashSet<string>()
            {
                "DARTFORD","DARTMOUTH"
            };

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.Result.NextLetters.Count, 2);
            Assert.AreEqual(response.Result.Stations.Count, 2);
            Assert.AreEqual(response.Result.NextLetters, _suggestions.NextLetters);
            Assert.AreEqual(response.Result.Stations, _suggestions.Stations);
        }

        [Test]
        public void TestStationFinderDartford()
        {
            //Arrange
            var result = GetValidResult();

            _dataAccess.Setup(x => x.GetStationList()).Returns(result);
            _dataAccess.Setup(x => x.GetName(It.IsAny<string>())).ReturnsAsync("testgetname");

            //Act
            Suggestions response = _stationFinder.GetSuggestions("Dartford").Result;

            _suggestions.NextLetters = new HashSet<string>(){};
            _suggestions.Stations = new HashSet<string>()
            {
                "DARTFORD"
            };

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.NextLetters.Count, 0);
            Assert.AreEqual(response.Stations.Count, 1);
            Assert.AreEqual(response.NextLetters, _suggestions.NextLetters);
            Assert.AreEqual(response.Stations, _suggestions.Stations);
        }

        [Test]
        public void TestStationFinderL()
        {
            //Arrange
            var result = GetValidResult();

            _dataAccess.Setup(x => x.GetStationList()).Returns(result);
            _dataAccess.Setup(x => x.GetName(It.IsAny<string>())).ReturnsAsync("testgetname");

            //Act
            Suggestions response = _stationFinder.GetSuggestions("L").Result;

            _suggestions.NextLetters = new HashSet<string>()
            {
                "I"
            };
            _suggestions.Stations = new HashSet<string>()
            {
                "LIVERPOOL LIME STREET"
            };

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.NextLetters.Count, 1);
            Assert.AreEqual(response.Stations.Count, 1);
            Assert.AreEqual(response.NextLetters, _suggestions.NextLetters);
            Assert.AreEqual(response.Stations, _suggestions.Stations);
        }

        [Test]
        public void TestStationFinderLastLetter()
        {
            //Arrange
            var result = GetValidResult();

            _dataAccess.Setup(x => x.GetStationList()).Returns(result);
            _dataAccess.Setup(x => x.GetName(It.IsAny<string>())).ReturnsAsync("testgetname");

            //Act
            Suggestions response = _stationFinder.GetSuggestions("LIVERPOOL LIME STREE").Result;

            _suggestions.NextLetters = new HashSet<string>()
            {
                "T"
            };
            _suggestions.Stations = new HashSet<string>()
            {
                "LIVERPOOL LIME STREET"
            };

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.NextLetters.Count, 1);
            Assert.AreEqual(response.Stations.Count, 1);
            Assert.AreEqual(response.NextLetters, _suggestions.NextLetters);
            Assert.AreEqual(response.Stations, _suggestions.Stations);
        }

        [Test]
        public void TestStationFinderLiverpool()
        {
            //Arrange
            var result = GetValidResult();

            _dataAccess.Setup(x => x.GetStationList()).Returns(result);
            _dataAccess.Setup(x => x.GetName(It.IsAny<string>())).ReturnsAsync("testgetname");

            //Act
            Suggestions response = _stationFinder.GetSuggestions("Liverpool").Result;

            _suggestions.NextLetters = new HashSet<string>()
            {
                "L"
            };
            _suggestions.Stations = new HashSet<string>()
            {
                "LIVERPOOL LIME STREET"
            };

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.NextLetters.Count, 1);
            Assert.AreEqual(response.Stations.Count, 1);
            Assert.AreEqual(response.NextLetters, _suggestions.NextLetters);
            Assert.AreEqual(response.Stations, _suggestions.Stations);
        }

        [Test]
        public void TestStationFinderNotExist()
        {
            //Arrange
            var result = GetValidResult();

            _dataAccess.Setup(x => x.GetStationList()).Returns(result);
            _dataAccess.Setup(x => x.GetName(It.IsAny<string>())).ReturnsAsync("testgetname");

            //Act
            Suggestions response = _stationFinder.GetSuggestions("S").Result;

            _suggestions.NextLetters = new HashSet<string>() {};
            _suggestions.Stations = new HashSet<string>() { };

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.NextLetters.Count, 0);
            Assert.AreEqual(response.Stations.Count, 0);
            Assert.AreEqual(response.NextLetters, _suggestions.NextLetters);
            Assert.AreEqual(response.Stations, _suggestions.Stations);
        }

        [Test]
        public void TestStationFinderNotExist2()
        {
            //Arrange
            var result = GetValidResult();

            _dataAccess.Setup(x => x.GetStationList()).Returns(result);
            _dataAccess.Setup(x => x.GetName(It.IsAny<string>())).ReturnsAsync("testgetname");

            //Act
            Suggestions response = _stationFinder.GetSuggestions("abcabc").Result;

            _suggestions.NextLetters = new HashSet<string>();
            _suggestions.Stations = new HashSet<string>();

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.NextLetters.Count, 0);
            Assert.AreEqual(response.Stations.Count, 0);
            Assert.AreEqual(response.NextLetters, _suggestions.NextLetters);
            Assert.AreEqual(response.Stations, _suggestions.Stations);
        }

        [Test]
        public void GIVEN_Empty_WHEN_stationFinder_is_called_TEHN_should_return_null()
        {
            //Arrange
            var result = GetValidResult();

            _dataAccess.Setup(x => x.GetStationList()).Returns(result);
            _dataAccess.Setup(x => x.GetName(It.IsAny<string>())).ReturnsAsync("testgetname");

            //Act
            Suggestions response = _stationFinder.GetSuggestions("").Result;

            //Assert
            Assert.IsNull(response.NextLetters);
            Assert.IsNull(response.Stations);
        }

        [Test]
        public void GIVEN_Null_WHEN_stationFinder_is_called_TEHN_should_return_null()
        {
            //Arrange
            var result = GetValidResult();

            _dataAccess.Setup(x => x.GetStationList()).Returns(result);
            _dataAccess.Setup(x => x.GetName(It.IsAny<string>())).ReturnsAsync("testgetname");

            //Act
            Suggestions response = _stationFinder.GetSuggestions(null).Result;

            //Assert
            Assert.IsNull(response.NextLetters);
            Assert.IsNull(response.Stations);
        }

        [Test]
        public void GIVEN_Whitespace_WHEN_stationFinder_is_called_TEHN_should_return_null()
        {
            //Arrange
            var result = GetValidResult();

            _dataAccess.Setup(x => x.GetStationList()).Returns(result);
            _dataAccess.Setup(x => x.GetName(It.IsAny<string>())).ReturnsAsync("testgetname");

            //Act
            Suggestions response = _stationFinder.GetSuggestions(" ").Result;

            //Assert
            Assert.IsNull(response.NextLetters);
            Assert.IsNull(response.Stations);
        }

        private HashSet<string> GetValidResult()
        {
            HashSet<string> result = new HashSet<string>()
            {
                "LIVERPOOL LIME STREET",
                "BIRMINGHAM NEW STREET",
                "KINGSTON",
                "DARTFORD",
                "DARTMOUTH"
            };

            return result;
        }
    }






}
