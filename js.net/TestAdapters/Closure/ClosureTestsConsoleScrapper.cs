﻿using System;
using System.Diagnostics;
using js.net.Engine;

namespace js.net.TestAdapters.Closure
{
  // TODO: We should not need a console scrapper to get results, we should
  // just plug into the test runner.
  public class ClosureTestsConsoleScrapper : JSConsole
  {
    private readonly TestResults results;
    private readonly bool silent;
    public ClosureTestsConsoleScrapper(string testFixtureName, bool silent, IEngine engine) : base(engine)
    {
      Trace.Assert(!String.IsNullOrWhiteSpace(testFixtureName));      

      results = new TestResults(testFixtureName);
      this.silent = silent;
    }

    public TestResults GetResults()
    {
      return results;
    }

    public override string log(object message, bool newline)
    {
      Trace.Assert(message != null);

      if (!silent) base.log(message, newline);

      string msg = message as string;
      if (!String.IsNullOrEmpty(msg)) ScrapeResultInformationFromMessage(msg);
      return msg;
    }

    private void ScrapeResultInformationFromMessage(string message)
    {            
      Trace.Assert(message != null);

      if (message.IndexOf(" : PASSED") < 0 && message.IndexOf(" : FAILED") < 0) { return; }
      string testName = GetTestNameFromPassOrFailLogLine(message);
      if (String.IsNullOrWhiteSpace(testName)) { return; }

      if (message.IndexOf(" : PASSED") >= 0)
      {
        results.AddPassedTest(testName);
      } else if (message.IndexOf(" : FAILED") >= 0)
      {
        results.AddFailedTest(testName);
      }
    }

    private string GetTestNameFromPassOrFailLogLine(string message)
    {
      Trace.Assert(message != null);

      string testName = message.Substring(message.IndexOf(": ") + 2).Trim();
      return testName.Substring(0, testName.IndexOf(": "));
    }
  }
}