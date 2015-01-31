using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SpecResults
{
  public class Step : ReportItem
  {
    public Step()
    {
      Steps = new List<Step>();
    }

    public List<Step> Steps { get; set; }

    public TableParam Table { get; set; }

    public string MultiLineParameter { get; set; }

    public ExceptionInfo Exception { get; set; }
  }
}