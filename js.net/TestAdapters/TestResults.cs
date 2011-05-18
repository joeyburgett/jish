﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace js.net.TestAdapters
{
  public class TestResults
  {
    public string TestName { get; private set; }
    public IEnumerable<string> Passed { get { return all.Where(kvp => kvp.Value).Select(kvp => kvp.Key); } }
    public IEnumerable<string> Failed { get { return all.Where(kvp => !kvp.Value).Select(kvp => kvp.Key); } }
    private readonly IList<KeyValuePair<string, bool>> all;

    public TestResults(string testName)
    {
      TestName = testName;
      all = new List<KeyValuePair<string, bool>>();
    }

    public void AddPassedTest(string testName)
    {
      Trace.Assert(!String.IsNullOrWhiteSpace(testName));

      all.Add(new KeyValuePair<string, bool>(testName, true));
    }

    public void AddFailedTest(string testName)
    {
      Trace.Assert(!String.IsNullOrWhiteSpace(testName));

      all.Add(new KeyValuePair<string, bool>(testName, false));
    }

    public override string ToString()
    {
      return new TestResultsStringFormatter(TestName, all).FormatTestResults();
    }
  }
}