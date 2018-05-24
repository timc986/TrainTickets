using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using Common.Logging;

namespace TrainTickets
{
    public interface IStationFinder
    {
        Task<Suggestions> GetSuggestions(string userInput);
    }

    public class StationFinder : IStationFinder
    {
        private static HashSet<string> _stationList;
        private readonly IList<string> _nextLetterList;
        private readonly IDataAccess _dataAccess;
        private readonly ILog _iLog;

        //constructor
        public StationFinder(IDataAccess dataAccess)
        {
            _nextLetterList = new List<string>();
            _dataAccess = dataAccess;
            _iLog = LogManager.GetLogger("mylogger");
        }

        public async Task<Suggestions> GetSuggestions(string userInput)
        {
            try
            {
                _stationList = _dataAccess.GetStationList(); //get station list

                //Asynchronous for demonstration
                var test = await _dataAccess.GetName(userInput);

                Suggestions result = new Suggestions();

                //check if station list and user input has values
                if (_stationList.Any() && !string.IsNullOrWhiteSpace(userInput))
                {
                    //ensure the user input is in upper case which is same as the station list
                    userInput = userInput.ToUpper();

                    //LINQ: search through the station list to find the stations start with user input letters
                    var searchResult = _stationList.Where(s => s.StartsWith(userInput));

                    //HashSet Constructor
                    result.Stations = new HashSet<string>(searchResult);

                    //get the next letter of each available stations
                    foreach (var station in result.Stations)
                    {
                        var trimString = station.Substring(userInput.Length);

                        //only return next letter if user hasn't input the full station name
                        if(!string.IsNullOrWhiteSpace(trimString))
                        {
                            //skip the space bar if there is any and get the next available letter
                            //(assume there is no space bar on the touch screen keyboard)
                            if (!string.IsNullOrWhiteSpace(trimString[0].ToString()))
                            {
                                _nextLetterList.Add(trimString[0].ToString());
                            }
                            else
                            {
                                _nextLetterList.Add(trimString[1].ToString());
                            }
                        }
                        //else
                        //{
                        //    //just to demonstrate the custom exception
                        //    throw new StationFinderException(trimString);
                        //}                        
                    }

                    //HashSet Constructor
                    result.NextLetters = new HashSet<string>(_nextLetterList);
                }
                return result;
            }
            catch (Exception ex) //catch exception if there is any
            {
                _iLog.Error("Station finder exception", ex);

                throw ex; //in real situation probably can return error message or save it in a log file
            }
        }
    }

}
