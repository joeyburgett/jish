﻿using System.IO;
using NUnit.Framework;

namespace js.net.tests.jish
{
  [TestFixture] public class JavaScriptModulesTest : AbstractJishTest
  {
    private const string TEST_FILE = @"modules\testModule.js";
    
    public override void SetUp()
    {
      const string command = @"
function testFunction(arg1) {
  console.log('arg1: ' + arg1);
  return arg1;
}
";
      if (!Directory.Exists("modules")) { Directory.CreateDirectory("modules"); }
      File.WriteAllText(TEST_FILE, command);
      base.SetUp();
    }

    public void TearDown()
    {
      if (File.Exists(TEST_FILE)) File.Delete(TEST_FILE);
    }

    [Test] public void TestModuleLoaded()
    {
      jish.ExecuteCommand("var x = testFunction(123);");
      Assert.AreEqual("arg1: 123", console.GetLastMessage());
      jish.ExecuteCommand("console.log('x: ' + x);");
      Assert.AreEqual("x: 123", console.GetLastMessage());
    }
  }
}
