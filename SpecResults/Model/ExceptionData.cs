using System;

namespace SpecResults.Model
{
	/// <summary>
	///     Contains the information about an <see cref="Exception" />.
	/// </summary>
	/// <remarks>
	///     Exceptions are not serializable to xml, due to the
	///     IDictionary Data property. That's why this class is used
	/// </remarks>
	public class ExceptionInfo
	{
		public ExceptionInfo()
		{
		}

		public ExceptionInfo(Exception ex)
		{
			ExceptionType = ex.GetType().FullName;
			HelpLink = ex.HelpLink;
			InnerException = ex.InnerException.ToExceptionInfo();
			Message = ex.Message;
			Source = ex.Source;
			StackTrace = ex.StackTrace;
		}

		public string ExceptionType { get; set; }
		public string HelpLink { get; set; }
		public ExceptionInfo InnerException { get; set; }
		public string Message { get; set; }
		public string Source { get; set; }
		public string StackTrace { get; set; }
	}
}