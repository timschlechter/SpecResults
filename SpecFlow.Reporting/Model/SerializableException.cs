using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SpecFlow.Reporting
{
    /// <summary>
    /// Wraps the information about an <see cref="Exception"/>.
    /// </summary>
    public class SerializableException : Exception
    {
        private string _stackTrace;

        public SerializableException(Exception ex)
            : base(ex.Message, ex.InnerException.ToSerializable())
        {
            _stackTrace = ex.StackTrace;
            Type = ex.GetType().FullName;
        }

        public override string StackTrace { get { return _stackTrace; } } 

        public string Type { get; set; }
    }
}
