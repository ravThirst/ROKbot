using RThirst.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROKbot
{
    public class ZoomOutState : State, IState
    {
        public ZoomOutState(StateManager stateManager, IntPtr handle) : base(stateManager, handle)
        {

        }

        public async Task Action()
        {
            ListBox.Items[0] = "zooming out";

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (Window.TemplateMatching(BottomLeftRectangle, BottomLeftRectangleOffset, AppDirectory + "\\templates\\buttons\\search.png", out var point))
            {
                if (stopwatch.ElapsedMilliseconds > 100000)
                {
                    StateManager.NextState(States.Exception);
                    Debug.WriteLine("zoom not zooming");
                }
                point = new Point(640, 400);
                Window.ClientToScreen(Handle, ref point);
                Cursor.Position = point;
                MouseSim.WheelDown(Handle, new Point(640, 400));
                await Task.Delay(1000);
            }
            stopwatch.Stop();

            if (Window.TemplateMatching(BottomLeftRectangle, BottomLeftRectangleOffset, AppDirectory + "\\templates\\buttons\\onMap.png", out var point1))
            {
                StateManager.NextState(States.Search);
                ListBox.Items[0] = "zoomed";
            }
            else
            {
                StateManager.NextState(States.Exception);
                Debug.WriteLine("zoom not zooming");
            }
        }
    }
}
