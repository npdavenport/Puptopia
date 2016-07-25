using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Puptopia.Domain.Exceptions
{
    [Serializable]
    public class CustomerUnderAgeException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public CustomerUnderAgeException()
        {
        }

        public CustomerUnderAgeException(string message) : base(message)
        {
        }

        public CustomerUnderAgeException(string message, Exception inner) : base(message, inner)
        {
        }

        protected CustomerUnderAgeException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
