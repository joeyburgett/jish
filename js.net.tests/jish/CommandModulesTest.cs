﻿using System.IO;
using NUnit.Framework;

namespace js.net.tests.jish
{
  [TestFixture] public class CommandModulesTest : AbstractJishTest
  {
    private const string sourceCommandDll = @"..\..\..\js.net.test.module\bin\Debug\js.net.test.module.dll";
    private const string targetCommandDll = @"modules\js.net.test.module.dll";

    public override void SetUp()
    {
      if (!Directory.Exists("modules")) { Directory.CreateDirectory("modules"); }
      File.Copy(sourceCommandDll, targetCommandDll, true);
      base.SetUp();
    }

    public override void TearDown()
    {
      if (File.Exists(targetCommandDll)) File.Delete(targetCommandDll);
      base.TearDown();
    }

    [Test] public void TestExecutingTestModule()
    {
      jish.ExecuteCommand(".testcommand");
      Assert.AreEqual("test command executed", console.GetLastMessage());
    }

    [Test] public void TestTestModuleHasHelp()
    {
      jish.ExecuteCommand(".help");
      Assert.IsTrue(console.GetLastMessage().IndexOf(".testcommand - test command help") >= 0, console.GetLastMessage());
    }
  }
}
