using System;
using System.Collections.Generic;
using System.Text;

namespace EarablesKIT.Models.Library
{
    class ChecksumException : Exception
    {
        public ChecksumException(string message): base(message)
        {

        }
    }
}
