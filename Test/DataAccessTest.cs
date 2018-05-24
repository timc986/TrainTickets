using System.Collections.Generic;
using NUnit.Framework;
using TrainTickets;

namespace Test
{
    [TestFixture]
    public class DataAccessTest
    {
        private IDataAccess _dataAccess;
        private HashSet<string> _stationList;

        [SetUp]
        public void SetUp()
        {
            //Arrange
            _dataAccess = new DataAccess();
            _stationList = new HashSet<string>()
            {
                "LIVERPOOL LIME STREET",
                "BIRMINGHAM NEW STREET",
                "KINGSTON",
                "DARTFORD",
                "DARTMOUTH"
            };
        }

        [Test]
        public void TestDataAccess()
        {
            //Act
            var response = _dataAccess.GetStationList();

            //Assert
            Assert.AreEqual(response, _stationList);
        }
    }
}