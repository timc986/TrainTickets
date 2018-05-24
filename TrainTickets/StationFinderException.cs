using System;

namespace TrainTickets
{
    public class StationFinderException: Exception
    {
        public StationFinderException(string message) 
            : base($"Invalid Station: {message}")
        { 

        }
    }
}
