﻿namespace js.net.TestAdapters.Coverage
{
  public class CoverageResultsBase
  {
    public int Statements { get; set; }

    public int Executed { get; set; }

    public decimal CoveragePercentage { get { return Statements*100.0m/Executed; } }    
  }
}