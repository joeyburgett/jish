﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using js.net.Util;

namespace js.net.jish.Command.InlineCommand
{
  public class ProcessCommand : IInlineCommand
  {
    private readonly JSConsole console;

    public ProcessCommand(JSConsole console)
    {
      Trace.Assert(console != null);

      this.console = console;
    }

    public string GetName()
    {
      return "process";
    }

    public string GetHelpDescription()
    {
      return "Executes the command in a separate Process.";
    }

    public IEnumerable<CommandParam> GetParameters()
    {
      CommandParam a1 = new CommandParam { Name = "command" };
      CommandParam a2 = new CommandParam { Name = "arguments", Null = true};
      return new[] { a1, a2 };
    }

    public string GetNameSpace()
    {
      return "jish";
    }

    public int process(string command, string arguments = null) 
    {
      Trace.Assert(!String.IsNullOrWhiteSpace(command));

      using (var process = new Process
                      {
                        StartInfo =
                          {
                            FileName = command,
                            Arguments = arguments,
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true
                          }
                      })
      {
        process.Start();
        string err = process.StandardError.ReadToEnd();
        string output = process.StandardOutput.ReadToEnd();
        
        if (!String.IsNullOrWhiteSpace(err)) console.error(err);
        if (!String.IsNullOrWhiteSpace(output)) console.log(output);
        
        process.WaitForExit();
        
        return process.ExitCode;
      }
    }
  }
}