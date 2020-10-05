using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkspaceManager.Core.Domain;

namespace WorkspaceManager.Core
{
    public static class WindowManager
    {
        private static readonly int _currentProcessId;

        static WindowManager()
        {
            _currentProcessId = Process.GetCurrentProcess().Id;
        }
        private delegate bool EnumDelegate(IntPtr hWnd, int lParam);

        #region DLLIMPORTS
        [DllImport("dwmapi.dll")]
        private static extern int DwmGetWindowAttribute(IntPtr hwnd, DwmWindowAttribute dwAttribute, out bool pvAttribute, int cbAttribute);

        [DllImport("user32.dll", EntryPoint = "EnumDesktopWindows", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        private static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumDelegate callback, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "GetWindowText", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder wndTitle, int titleMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        private static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
        #endregion

        private static IEnumerable<Process> GetVisibleProcesses()
        {
            ICollection<Process> visibleProcesses = new List<Process>();

            var processes = Process.GetProcesses();
            foreach (var process in processes)
            {
                if (process.MainWindowHandle != IntPtr.Zero && IsWindowUserVisible(process.MainWindowHandle))
                {
                    visibleProcesses.Add(process);
                }
            }
            return visibleProcesses;
        }
        private static IEnumerable<IntPtr> GetVisibleWindowses()
        {
            var windowses = new List<IntPtr>();

            EnumDelegate filter = delegate (IntPtr hWnd, int lParam)
            {
                StringBuilder winTitle = new StringBuilder(255);
                int textLength = GetWindowText(hWnd, winTitle, winTitle.Capacity + 1);
                var title = winTitle.ToString();

                if (IsWindowUserVisible(hWnd) && String.IsNullOrEmpty(title) == false)
                {
                    windowses.Add(hWnd);
                }
                return true;
            };            

            if (EnumDesktopWindows(IntPtr.Zero, filter, IntPtr.Zero))
            {
                return windowses;
            }

            return null;
        }
        private static bool IsWindowUserVisible(IntPtr hWnd)
        {
            bool isCloacked = false;
            DwmGetWindowAttribute(hWnd, DwmWindowAttribute.Cloaked, out isCloacked, Marshal.SizeOf(typeof(bool)));

            if (IsWindowVisible(hWnd) && !isCloacked)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Gets windowses that are visible to the user, and their processes and child process 
        /// </summary>        
        /// <returns>
        /// Return a tuple with a (Item1)Process that has a window visible to the user, (Item2)IntPtr a window that is visible, and a child (Item3)Process, if exists, 
        /// that controls the window that is visible.
        /// </returns>
        public static IEnumerable<Tuple<Process, IntPtr, Process>> GetVisibleWindowsProcessses()
        {
            var visibleProcesses = GetVisibleProcesses();

            var visibleWindowses = GetVisibleWindowses();

            var processWindowsPairs = new List<Tuple<Process, IntPtr, Process>>();
            foreach(var window in visibleWindowses)
            {
                GetWindowThreadProcessId(window, out uint processId);
                var process = visibleProcesses.FirstOrDefault(p => p.Id == (int)processId);

                if (process == null || process.ProcessName == "explorer" || process.Id == _currentProcessId)
                    continue;

                var parentProcess = process.GetParentProcess();

                if(parentProcess.ProcessName == "explorer")
                {
                    processWindowsPairs.Add(new Tuple<Process, IntPtr, Process>(process, window, null));
                }
                else
                {
                    processWindowsPairs.Add(new Tuple<Process, IntPtr, Process>(parentProcess, window, process));
                }
            }

            return processWindowsPairs;
        }

        public static RECT GetWindowPosition(IntPtr window)
        {
            GetWindowRect(window, out RECT position);
            return position;
        }

        public static void SetWindowPosition(IntPtr window, RECT position, string processName)
        {
            if (window == IntPtr.Zero)
                return;

            var x = position.Left;
            var y = position.Top;

            var width = position.Right - x;
            var height = position.Bottom - y;

            for(int i = 0; i < 10; i++) // Try 10 times until position is set
            {
                SetWindowPos(window, 0, x, y, width, height, (int)WindowSizePosFlags.SWP_SHOWWINDOW);

                GetWindowRect(window, out RECT appliedRect);

                if(position.Left == appliedRect.Left || position.Right == appliedRect.Right || position.Top == appliedRect.Top || position.Bottom == appliedRect.Bottom)
                {
                    i = 10;
                }

                Thread.Sleep(1000);
            }
            
        }
        public static void SetWindowPosition(string windowProcessName, RECT position)
        {
            var visibleProcesses = GetVisibleProcesses();
            var process = visibleProcesses.Single(p => p.ProcessName == windowProcessName);

            SetWindowPosition(process.MainWindowHandle, position, "");
        }
    }
}
