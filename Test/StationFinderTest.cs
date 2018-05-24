using System.Collections.Generic;
using NUnit.Framework;
using TrainTickets;
using Assert = NUnit.Framework.Assert;

namespace Test
{
    [TestFixture]
    public class StationFinderTest //runs faster without Unity, commented out just to show the IOC
    {
        private IStationFinder _stationFinder;
        private IDataAccess _dataAccess;
        private Suggestions _suggestions;
        //private IUnityContainer _unityContainer;

        [SetUp]
        public void SetUp()
        {
            //Arrange

            //_unityContainer = new UnityContainer();
            //_unityContainer.RegisterType<IDataAccess, DataAccess>(new ContainerControlledLifetimeManager());
            //_stationFinder = _unityContainer.Resolve<StationFinder>();

            _dataAccess = new DataAccess();
            _stationFinder = new StationFinder(_dataAccess); //can be replaced by resolving unity
            _suggestions = new Suggestions();
        }

        [Test]
        public void TestStationFinderdart()
        {
            //Act
            var response = _stationFinder.GetSuggestions("dart");

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
            Assert.AreEqual(response.Result.NextLetters,_suggestions.NextLetters);
            Assert.AreEqual(response.Result.Stations, _suggestions.Stations);
        }

        [Test]
        public void TestStationFinderDart()
        {
            var response = _stationFinder.GetSuggestions("Dart");

            _suggestions.NextLetters = new HashSet<string>()
            {
                "F","M"
            };
            _suggestions.Stations = new HashSet<string>()
            {
                "DARTFORD","DARTMOUTH"
            };

            Assert.IsNotNull(response);
            Assert.AreEqual(response.Result.NextLetters.Count, 2);
            Assert.AreEqual(response.Result.Stations.Count, 2);
            Assert.AreEqual(response.Result.NextLetters, _suggestions.NextLetters);
            Assert.AreEqual(response.Result.Stations, _suggestions.Stations);
        }

        [Test]
        public void TestStationFinderDartford()
        {
            Suggestions response = _stationFinder.GetSuggestions("Dartford").Result;

            _suggestions.NextLetters = new HashSet<string>(){};
            _suggestions.Stations = new HashSet<string>()
            {
                "DARTFORD"
            };

            Assert.IsNotNull(response);
            Assert.AreEqual(response.NextLetters.Count, 0);
            Assert.AreEqual(response.Stations.Count, 1);
            Assert.AreEqual(response.NextLetters, _suggestions.NextLetters);
            Assert.AreEqual(response.Stations, _suggestions.Stations);
        }

        [Test]
        public void TestStationFinderL()
        {
            Suggestions response = _stationFinder.GetSuggestions("L").Result;

            _suggestions.NextLetters = new HashSet<string>()
            {
                "I"
            };
            _suggestions.Stations = new HashSet<string>()
            {
                "LIVERPOOL LIME STREET"
            };

            Assert.IsNotNull(response);
            Assert.AreEqual(response.NextLetters.Count, 1);
            Assert.AreEqual(response.Stations.Count, 1);
            Assert.AreEqual(response.NextLetters, _suggestions.NextLetters);
            Assert.AreEqual(response.Stations, _suggestions.Stations);
        }

        [Test]
        public void TestStationFinderLastLetter()
        {
            Suggestions response = _stationFinder.GetSuggestions("LIVERPOOL LIME STREE").Result;

            _suggestions.NextLetters = new HashSet<string>()
            {
                "T"
            };
            _suggestions.Stations = new HashSet<string>()
            {
                "LIVERPOOL LIME STREET"
            };

            Assert.IsNotNull(response);
            Assert.AreEqual(response.NextLetters.Count, 1);
            Assert.AreEqual(response.Stations.Count, 1);
            Assert.AreEqual(response.NextLetters, _suggestions.NextLetters);
            Assert.AreEqual(response.Stations, _suggestions.Stations);
        }

        [Test]
        public void TestStationFinderLiverpool()
        {
            Suggestions response = _stationFinder.GetSuggestions("Liverpool").Result;

            _suggestions.NextLetters = new HashSet<string>()
            {
                "L"
            };
            _suggestions.Stations = new HashSet<string>()
            {
                "LIVERPOOL LIME STREET"
            };

            Assert.IsNotNull(response);
            Assert.AreEqual(response.NextLetters.Count, 1);
            Assert.AreEqual(response.Stations.Count, 1);
            Assert.AreEqual(response.NextLetters, _suggestions.NextLetters);
            Assert.AreEqual(response.Stations, _suggestions.Stations);
        }

        [Test]
        public void TestStationFinderNotExist()
        {
            Suggestions response = _stationFinder.GetSuggestions("S").Result;

            _suggestions.NextLetters = new HashSet<string>() {};
            _suggestions.Stations = new HashSet<string>() { };

            Assert.IsNotNull(response);
            Assert.AreEqual(response.NextLetters.Count, 0);
            Assert.AreEqual(response.Stations.Count, 0);
            Assert.AreEqual(response.NextLetters, _suggestions.NextLetters);
            Assert.AreEqual(response.Stations, _suggestions.Stations);
        }

        [Test]
        public void TestStationFinderNotExist2()
        {
            Suggestions response = _stationFinder.GetSuggestions("abcabc").Result;

            _suggestions.NextLetters = new HashSet<string>() { };
            _suggestions.Stations = new HashSet<string>() { };

            Assert.IsNotNull(response);
            Assert.AreEqual(response.NextLetters.Count, 0);
            Assert.AreEqual(response.Stations.Count, 0);
            Assert.AreEqual(response.NextLetters, _suggestions.NextLetters);
            Assert.AreEqual(response.Stations, _suggestions.Stations);
        }

        [Test]
        public void TestStationFinderEmptyInput()
        {
            Suggestions response = _stationFinder.GetSuggestions("").Result;

            Assert.IsNull(response.NextLetters);
            Assert.IsNull(response.Stations);
        }

        [Test]
        public void TestStationFinderNullInput()
        {
            Suggestions response = _stationFinder.GetSuggestions(null).Result;

            Assert.IsNull(response.NextLetters);
            Assert.IsNull(response.Stations);
        }
    }






}
