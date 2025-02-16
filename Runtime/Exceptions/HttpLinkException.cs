using System;

namespace GameFramework
{
    public class InvalidUrlException : Exception 
    {
        public InvalidUrlException() : base ("Invalid URL. Call SetUrl()")
        {
        }
    }
}