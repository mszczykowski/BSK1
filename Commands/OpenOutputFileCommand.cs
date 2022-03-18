using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSK1.Services;

namespace BSK1.Commands
{
    internal class OpenOutputFileCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            Process.Start(@"notepad.exe", FileService.outputFilePath);
        }
    }
}
