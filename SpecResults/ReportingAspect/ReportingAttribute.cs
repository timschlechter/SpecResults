using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;

namespace SpecResults.ReportingAspect
{
	internal class ReportingAttribute : ContextAttribute
	{
		public ReportingAttribute()
			: base("Reporting")
		{
		}

		public override void GetPropertiesForNewContext(IConstructionCallMessage ccm)
		{
			ccm.ContextProperties.Add(new ReportingProperty());
		}

		public override bool IsContextOK(Context ctx, IConstructionCallMessage ctorMsg)
		{
			var p = ctx.GetProperty("Reporting") as ReportingProperty;
			if (p == null)
			{
				return false;
			}
			return true;
		}

		public override bool IsNewContextOK(Context newCtx)
		{
			var p = newCtx.GetProperty("Reporting") as ReportingProperty;
			if (p == null)
			{
				return false;
			}
			return true;
		}
	}
}