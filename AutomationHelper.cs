using System.Drawing;
using System.Runtime.InteropServices;

namespace Octave_
{
    [Obsolete("Not useful; Use System.Windows.Forms instead", true)]
    public static class AutomationHelper
    {
        #region Windows API
        /// <summary>
        /// Struct representing a point, for Windows API.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }
        /// <summary>
        /// Retrieves the cursor's position, in screen coordinates.
        /// </summary>
        /// <see>See MSDN documentation for further information.</see>
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);
        /// <summary>
        /// Not working
        /// </summary>
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(in int x, in int y);
        [StructLayout(LayoutKind.Sequential)]
        struct MOUSEINPUT
        {
            int dx;
            int dy;
            int mouseData;
            public int dwFlags;
            int time;
            IntPtr dwExtraInfo;
        }
        struct INPUT
        {
            public uint dwType;
            public MOUSEINPUT mi;
        }
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint cInputs, INPUT input, int size);
        #endregion

        #region Helpers
        public static Point GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);
            // NOTE: If you need error handling
            // bool success = GetCursorPos(out lpPoint);
            // if (!success)

            return lpPoint;
        }
        public static void SetCursorPosition(Point position)
        {
            var input = new INPUT()
            {
                dwType = 0, // Mouse input
                mi = new MOUSEINPUT() { dwFlags = 0x2 }
            };

            if (SendInput(1, input, Marshal.SizeOf(input)) == 0)
            {
                throw new Exception();
            }
        }
        #endregion

        #region Examples
        public static void ExampleClick()
        {
            DoClickMouse(0x2); // Left mouse button down
            DoClickMouse(0x4); // Left mouse button up

            void DoClickMouse(int mouseButton)
            {
                var input = new INPUT()
                {
                    dwType = 0, // Mouse input
                    mi = new MOUSEINPUT() { dwFlags = mouseButton }
                };

                if (SendInput(1, input, Marshal.SizeOf(input)) == 0)
                {
                    throw new Exception();
                }
            }
        }
        public static void ExampleGet()
        {
            Point p = GetCursorPosition();
            Console.WriteLine($"X: {p.X}, Y: {p.Y}");
        }
        #endregion
    }
}
