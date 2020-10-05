using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkspaceManager.Core.Domain
{
    [Serializable]
    public class Program
    {
        public string Name { get; set; }

        public bool HasWindowProcess { get; set; }
        public string WindowProcessName { get; set; }

        public string Path { get; set; }
        public string Arguments { get; set; }
        public RECT Position { get; set; }
    }
}
