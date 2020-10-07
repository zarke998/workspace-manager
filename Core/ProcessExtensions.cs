using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkspaceManager.Core
{
    public static class ProcessExtensions
    {
        public static Process GetParentProcess(this Process process)
        {
            var query = string.Format("SELECT ParentProcessId FROM Win32_Process WHERE ProcessId = {0}", process.Id);
            var search = new ManagementObjectSearcher("root\\CIMV2", query);
            var results = search.Get().GetEnumerator();
            results.MoveNext();
            var queryObj = results.Current;
            var parentId = (uint)queryObj["ParentProcessId"];
            Process parent;
            try
            {
                 parent = Process.GetProcessById((int)parentId);
            }
            catch (ArgumentException e) // Parent process does not exist with parentID
            {
                return null;
            }

            return parent;
        }
        public static IEnumerable<Process> GetChildProcesses(this Process process)
        {
            ICollection<Process> childs = new List<Process>();

            var query = string.Format("SELECT ProcessId FROM Win32_Process WHERE ParentProcessId = {0}", process.Id);
            var search = new ManagementObjectSearcher("root\\CIMV2", query);
            var results = search.Get().GetEnumerator();

            while (results.MoveNext())
            {
                var queryObj = results.Current;
                var childId = (uint)queryObj["ProcessId"];
                try
                {
                    var child = Process.GetProcessById((int)childId);
                    childs.Add(child);
                }
                catch (ArgumentException e) { }
            }

            return childs;
        }

        public static void WaitForMainWindowHandle(this Process process, int retryAttempts, int waitBeetweenAttemptsMs = 500)
        {
            if (process.MainWindowHandle != IntPtr.Zero)
                return;

            for (int i = 0; i < retryAttempts; i++)
            {
                Thread.Sleep(waitBeetweenAttemptsMs);

                if (process.MainWindowHandle != IntPtr.Zero)
                    return;
            }
        }
    }
}
