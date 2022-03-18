using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK1.Commands
{
    internal class OpenOutputFileCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            Process.Start(@"notepad.exe", @"C:\TEST_FILE.txt");
        }
    }
}
