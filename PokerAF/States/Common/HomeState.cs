using RThirst.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROKbot
{
    public class HomeState : State, IState
    {
        bool collectRSS;

        public HomeState(StateManager stateManager, IntPtr handle) : base(stateManager, handle)
        {
            collectRSS = FormElements.checkBox.Checked;
        }

        public async Task Action()
        {
            Window.SetForegroundWindow(Handle);
            await Task.Delay(200);
            if (Window.TemplateMatching(BottomLeftRectangle, BottomLeftRectangleOffset, AppDirectory + "\\templates\\buttons\\home.png", out var point))
            {
                await CollectRSSInCity();
                await MouseSim.ClickOnPoint(Handle, point, true);
                StateManager.NextState(StateManager.StateAfterHome);
                ListBox.Items[0] = "going on map";
                await Task.Delay(500);
            }
            else if (Window.TemplateMatching(BottomLeftRectangle, BottomLeftRectangleOffset, AppDirectory + "\\templates\\buttons\\onMap.png", out var point1))
            {
                await MouseSim.ClickOnPoint(Handle, point1, true);
                ListBox.Items[0] = "going home";
                await Task.Delay(500);
            }
            else
            {
                StateManager.NextState(States.Exception);
                Debug.WriteLine("No button was found in home state");
            }
        }

        async Task CollectRSSInCity()
        {
            if (collectRSS)
            {
                var point = new Point();
                var rssRect = new Rectangle(new Point(WindowRectangle.X, WindowRectangle.Y + 150), WindowRectangle.Size);
                var rssOffset = new Point(0, 150);
                await MouseSim.WheelDown(Handle, new Point(640, 400));
                await Task.Delay(1000);
                await MouseSim.WheelDown(Handle, new Point(640, 400));
                await Task.Delay(500);
                if (Window.TemplateMatching(rssRect, rssOffset, AppDirectory + "\\templates\\rss\\food.png", out point))
                {
                    await MouseSim.ClickOnPoint(Handle, point, true);
                    await Task.Delay(500);
                }
                if (Window.TemplateMatching(rssRect, rssOffset, AppDirectory + "\\templates\\rss\\wood.png", out point))
                {
                    await MouseSim.ClickOnPoint(Handle, point, true);
                    await Task.Delay(500);
                }
                if (Window.TemplateMatching(rssRect, rssOffset, AppDirectory + "\\templates\\rss\\stone.png", out point))
                {
                    await MouseSim.ClickOnPoint(Handle, point, true);
                    await Task.Delay(500);
                }
                if (Window.TemplateMatching(rssRect, rssOffset, AppDirectory + "\\templates\\rss\\gold.png", out point))
                {
                    await MouseSim.ClickOnPoint(Handle, point, true);
                    await Task.Delay(500);
                }
            }
        }
    }
}
