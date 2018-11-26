using System;

namespace API_BoilerPlate.BRL.Common
{
    public class ClientSideException : Exception
    {
        //  private string _clientSideStack;

        public ClientSideException()
        {
        }

        //public ClientSideException(string message, string stackTrace) : base(message)
        public ClientSideException(string message) : base(message)
        {
            //this._clientSideStack = stackTrace;
        }

        //public override string StackTrace
        //{
        //    get
        //    {
        //        return this._clientSideStack;
        //    }
        //}
    }
}