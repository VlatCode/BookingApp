using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelBookingSystem.Shared.CustomExceptions
{
    public class InvalidEntryException : Exception
    {
        public InvalidEntryException(string message) : base(message)
        {

        }
    }
}
