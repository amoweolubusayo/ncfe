using System.Collections.Generic;
using Ncfe.CodeTest.Models;

namespace Ncfe.CodeTest.Interfaces
{
    public interface IFailoverRepository
    {
        List<FailoverEntry> GetFailOverEntries();
    }
}