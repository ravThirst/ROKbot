using AForge.Imaging;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;

namespace RThirst.Tools
{
    internal static class Window
    {

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        public static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            else throw new Exception();
        }

        public static void MinimizeWindow(IntPtr handle)
        {
            ShowWindow(handle, 6);
        }

        public static void MaximizeWindow(IntPtr handle)
        {
            ShowWindow(handle, 3);
        }

        public static Bitmap Screenshot(Point point, Size size)
        {
            Bitmap bmpScreenshot = new Bitmap(size.Width, size.Height);
            Graphics g = Graphics.FromImage(bmpScreenshot);
            g.CopyFromScreen(point.X, point.Y, 0, 0, size);

            return bmpScreenshot;
        }

        public static Bitmap Screenshot(Rectangle rectangle)
        {
            Bitmap bmpScreenshot = new Bitmap(rectangle.Width, rectangle.Height);
            Graphics g = Graphics.FromImage(bmpScreenshot);
            g.CopyFromScreen(rectangle.Location.X, rectangle.Location.Y, 0, 0, rectangle.Size);

            return bmpScreenshot;
        }

        public static Bitmap PixelFormating(Bitmap orig)
        {
            Bitmap clone = new Bitmap(orig.Width, orig.Height,
                PixelFormat.Format24bppRgb);

            using (Graphics gr = Graphics.FromImage(clone))
            {
                gr.DrawImage(orig, new Rectangle(0, 0, clone.Width, clone.Height));
            }

            return clone;
        }

        public static Bitmap Grayscale(Bitmap orig)
        {
            Bitmap clone = new Bitmap(orig.Width, orig.Height,
                PixelFormat.Format8bppIndexed);

            using (Graphics gr = Graphics.FromImage(clone))
            {
                gr.DrawImage(orig, new Rectangle(0, 0, clone.Width, clone.Height));
            }

            return clone;
        }

        public static bool TemplateMatching(Rectangle source, string path, out Point point)
        {
            var sourceImage = PixelFormating(Screenshot(source));
            var template = PixelFormating((Bitmap)System.Drawing.Image.FromFile(path));

            string? d = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            sourceImage.Save(d + "\\log\\" + new Random().Next().ToString() + ".png");

            ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0.95f);
            TemplateMatch[] matchings = tm.ProcessImage(sourceImage, template);

            foreach (TemplateMatch m in matchings)
            {
                point = m.Rectangle.Location;
                return true;
            }
            point = new Point();
            return false;
        }

        public static bool TemplateMatching(Rectangle source, Point offset, string path, out Point point)
        {
            var sourceImage = PixelFormating(Screenshot(source));
            var template = PixelFormating((Bitmap)System.Drawing.Image.FromFile(path));

            string? d = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            sourceImage.Save(d + "\\log\\" + new Random().Next().ToString() + ".png");

            ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0.95f);
            TemplateMatch[] matchings = tm.ProcessImage(sourceImage, template);

            foreach (TemplateMatch m in matchings)
            {
                point = m.Rectangle.Location;
                point = new Point(point.X + offset.X, point.Y + offset.Y);
                return true;
            }
            point = new Point();
            return false;
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public static Rectangle GetWindowRect(IntPtr hWnd)
        {
            RECT rect = new RECT();
            if (GetWindowRect(hWnd, ref rect))
            {
                Size size = new Size(rect.Right - rect.Left, rect.Bottom - rect.Top);

                int hWndX = rect.Left;
                int hWndY = rect.Top;
                return new Rectangle(hWndX, hWndY, size.Width, size.Height);
            }
            else throw new Exception();
        }
    }

    public static class ExtensionMethods
    {
        public static Point Add(this Point operand1, Point operand2)
        {
            return new Point(operand1.X + operand2.X, operand1.Y + operand2.Y);
        }
    }
}