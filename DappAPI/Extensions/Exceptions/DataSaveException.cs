using System;

namespace DappAPI.Extensions.Exceptions
{
    public class DataSaveException : Exception
    {
        public DataSaveException(string message): base(message)
        {

        }
    }
}
