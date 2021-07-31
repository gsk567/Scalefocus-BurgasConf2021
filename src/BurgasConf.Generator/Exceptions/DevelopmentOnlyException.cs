using System;

namespace BurgasConf.Generator.Exceptions
{
    public class DevelopmentOnlyException : Exception
    {
        public DevelopmentOnlyException(string message)
            : base(message)
        {
        }
    }
}