using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;

namespace RThirst.Tools
{
    public class MouseSim
    {

        [DllImport("user32.dll")]
        internal static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, int cButtons, UIntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern int PostMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

#pragma warning disable 649
        internal struct INPUT
        {
            public uint Type;
            public MOUSEKEYBDHARDWAREINPUT Data;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct MOUSEKEYBDHARDWAREINPUT
        {
            [FieldOffset(0)]
            public MOUSEINPUT Mouse;
        }

        internal struct MOUSEINPUT
        {
            public int X;
            public int Y;
            public int MouseData;
            public uint Flags;
            public uint Time;
            public IntPtr ExtraInfo;
        }

#pragma warning restore 649


        public static async Task ClickOnPoint(IntPtr wndHandle, Point clientPoint, bool IsRandom)
        {
            var oldPos = Cursor.Position;
            Random random = new Random();

            //Window.SetForegroundWindow(wndHandle);

            /// get screen coordinates
            Window.ClientToScreen(wndHandle, ref clientPoint);
            if (IsRandom) clientPoint = new Point(clientPoint.X + random.Next(-1, 2), clientPoint.Y + random.Next(-1, 2));

            /// set cursor on coords, and press mouse
            Cursor.Position = new Point(clientPoint.X - 5, clientPoint.Y - 30);

            var inputMouseDown = new INPUT();
            inputMouseDown.Type = 0; /// input type mouse
            inputMouseDown.Data.Mouse.Flags = 0x0002; /// left button down
            inputMouseDown.Data.Mouse.ExtraInfo = (IntPtr)0;

            var inputs = new INPUT[] { inputMouseDown };
            var t = SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));

            await Task.Delay(new Random().Next(70, 90));

            var inputMouseUp = new INPUT();
            inputMouseUp.Type = 0; /// input type mouse
            inputMouseUp.Data.Mouse.Flags = 0x0004; /// left button up

            inputs = new INPUT[] { inputMouseUp };
            t = SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));

            await Task.Delay(80);
            Cursor.Position = oldPos;
        }

        public async static Task DragNDrop(IntPtr handle, Point start, Point target, float speed)
        {
            Window.ClientToScreen(handle, ref start);
            Window.ClientToScreen(handle, ref target);

            Cursor.Position = start;
            MouseSim.MouseDownScreenC(handle, start);
            await Task.Delay(60);
            var watch = new Stopwatch();
            watch.Start();
            while (watch.Elapsed.TotalMilliseconds * 2.5 / speed < 1000)
            {
                var time = watch.Elapsed.TotalMilliseconds / 1000 * 2.5 / speed;
                var X = (int)Math.Abs((start.X + (target.X - start.X) * time));
                var Y = (int)Math.Abs((start.Y + (target.Y - start.Y) * time));
                Cursor.Position = new Point(X, Y);
                await Task.Delay(4);
            }
            watch.Stop();
            Cursor.Position = target;
            MouseSim.MouseUpScreenC(handle, target);
            await Task.Delay(60);
        }

        public static async Task ClickOnPointWindows(Point clientPoint)
        {
            var oldPos = Cursor.Position;

            /// set cursor on coords, and press mouse
            Cursor.Position = new Point(clientPoint.X, clientPoint.Y);

            var inputMouseDown = new INPUT();
            inputMouseDown.Type = 0; /// input type mouse
            inputMouseDown.Data.Mouse.Flags = 0x0002; /// left button down
            inputMouseDown.Data.Mouse.ExtraInfo = (IntPtr)0;

            var inputs = new INPUT[] { inputMouseDown };
            var t = SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));

            await Task.Delay(new Random().Next(80, 86));

            var inputMouseUp = new INPUT();
            inputMouseUp.Type = 0; /// input type mouse
            inputMouseUp.Data.Mouse.Flags = 0x0004; /// left button up

            inputs = new INPUT[] { inputMouseUp };
            t = SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));

            await Task.Delay(80);
            Cursor.Position = oldPos;
        }

        public static async Task WheelDown(IntPtr wndHandle, Point clientPoint)
        {
            var oldPos = Cursor.Position;

            Window.SetForegroundWindow(wndHandle);

            /// get screen coordinates
            Window.ClientToScreen(wndHandle, ref clientPoint);

            /// set cursor on coords, and press mouse
            Cursor.Position = new Point(clientPoint.X, clientPoint.Y);

            await Task.Delay(30);
            var inputWheelDown = new INPUT();
            inputWheelDown.Type = 0; /// input type mouse
            inputWheelDown.Data.Mouse.Flags = 0x0800; /// left button up
            inputWheelDown.Data.Mouse.MouseData = -120;

            var inputs = new INPUT[] { inputWheelDown };
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));

            await Task.Delay(30);
            /// return mouse 
            Cursor.Position = oldPos;
        }

        public static void MouseDown(IntPtr wndHandle, Point clientPoint)
        {
            Window.SetForegroundWindow(wndHandle);

            /// get screen coordinates
            Window.ClientToScreen(wndHandle, ref clientPoint);

            /// set cursor on coords, and press mouse
            Cursor.Position = new Point(clientPoint.X, clientPoint.Y);

            var inputMouseDown = new INPUT();
            inputMouseDown.Type = 0; /// input type mouse
            inputMouseDown.Data.Mouse.Flags = 0x0002; /// left button down

            var inputs = new INPUT[] { inputMouseDown };
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
        }

        public static void MouseUp(IntPtr wndHandle, Point clientPoint)
        {
            Window.SetForegroundWindow(wndHandle);

            /// get screen coordinates
            Window.ClientToScreen(wndHandle, ref clientPoint);

            /// set cursor on coords, and press mouse
            Cursor.Position = new Point(clientPoint.X, clientPoint.Y);

            var inputMouseUp = new INPUT();
            inputMouseUp.Type = 0; /// input type mouse
            inputMouseUp.Data.Mouse.Flags = 0x0004; /// left button up

            var inputs = new INPUT[] { inputMouseUp };
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
        }

        public static void MouseDownScreenC(IntPtr wndHandle, Point clientPoint)
        {

            Window.SetForegroundWindow(wndHandle);

            /// set cursor on coords, and press mouse
            Cursor.Position = new Point(clientPoint.X, clientPoint.Y);

            var inputMouseDown = new INPUT();
            inputMouseDown.Type = 0; /// input type mouse
            inputMouseDown.Data.Mouse.Flags = 0x0002; /// left button down

            var inputs = new INPUT[] { inputMouseDown };
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
        }

        public static void MouseUpScreenC(IntPtr wndHandle, Point clientPoint)
        {
            Window.SetForegroundWindow(wndHandle);

            /// set cursor on coords, and press mouse
            Cursor.Position = new Point(clientPoint.X, clientPoint.Y);

            var inputMouseUp = new INPUT();
            inputMouseUp.Type = 0; /// input type mouse
            inputMouseUp.Data.Mouse.Flags = 0x0004; /// left button up

            var inputs = new INPUT[] { inputMouseUp };
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
        }

    }
}
