using System.Collections.Generic;
using Ncfe.CodeTest.Interfaces;
using Ncfe.CodeTest.Models;

namespace Ncfe.CodeTest.DataAccess
{
    public class FailoverRepository : IFailoverRepository
    {
        public List<FailoverEntry> GetFailOverEntries()
        {
            // Return all failover entries from the database
            return new List<FailoverEntry>();
        }
    }
}