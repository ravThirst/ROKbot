using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RThirst.Tools;

namespace PokerAF
{
    internal class WindowMover
    {
        Form form;
        static Rectangle ScreenSize;

        public WindowMover(Form form)
        {
            this.form = form;
            ScreenSize = Screen.FromControl(form).Bounds;
        }

        public bool Move(IntPtr win, int index)
        {
            var point = GetPoint(index);
            if (Window.MoveWindow(win, point.X, point.Y, 1200, 600, true))
                return true;
            return false;
        }

        public static Point GetPoint(int index)
        {
            switch (ScreenSize.Right)
            {
                case 1920:                                                            //540, 420
                    {
                        switch (index)
                        {
                            case 0: return new Point(40, 40);
                        }
                        break;
                    }
                case 1366:
                    {
                        switch (index)
                        {
                            case 0: return new Point(40, 40);
                        }
                        break;
                    }
            }
            throw new ArgumentException();
        }

        public static Rectangle GetRectangle(int index = 0)
        {
            return new Rectangle(GetPoint(index),new Size(1200,600));
        }

        public int GetMaxWindows()
        {
            switch (ScreenSize.Right)
            {
                case 1920:  return 1;                                                            //540, 420
                case 1366:  return 1;
            }
            throw new ArgumentException();
        }

    }
}
