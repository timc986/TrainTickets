using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrainTickets
{
    public interface IDataAccess
    {
        HashSet<string> GetStationList();
        Task<string> GetName(string name);
    }

    public class DataAccess : IDataAccess
    {
        public HashSet<string> GetStationList() //in real situation probably will get this from database
        {
            try
            {
                //Use HashSet instead of List or array because of its O(1) speed for lookup in some occasion 
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
            catch (Exception ex) //catch exception if there is any
            {
                throw ex; //in real situation probably can return error message or save it in a log file
            }
        }

        public async Task<string> GetName(string name)
        {
            //usually will Asynchronously call another api to get data

            return name;
        }
    }
}