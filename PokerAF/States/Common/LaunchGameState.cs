using RThirst.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROKbot
{
    public class LaunchGameState : State, IState
    {
        public LaunchGameState(StateManager stateManager, IntPtr handle) : base(stateManager, handle)
        {

        }

        public async Task Action()
        {
            if (Window.FindWindowByCaption(IntPtr.Zero, "Rise of Kingdoms") == IntPtr.Zero)
            {
                ListBox.Items[0] = "Launching app";
                var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                var windowName = path.Split('\\').Last();
                if (Window.FindWindowByCaption(IntPtr.Zero, windowName) == IntPtr.Zero)
                {
                    Process.Start("explorer.exe", path);
                    await Task.Delay(4000);
                }
                else
                    Window.SetForegroundWindow(Window.FindWindowByCaption(IntPtr.Zero, windowName));
                var rect = Window.GetWindowRect(Window.FindWindowByCaption(IntPtr.Zero, windowName));
                if (Window.TemplateMatching(rect, AppDirectory + "\\templates\\env\\icon.png", out var point))
                {
                    await MouseSim.ClickOnPointWindows(point.Add(rect.Location));
                    await MouseSim.ClickOnPointWindows(point.Add(rect.Location));
                    await Task.Delay(10000);

                    var scr = Screen.FromControl(ListBox).Bounds;
                    rect = new Rectangle(new Point(scr.Width / 3, 0), new Size(scr.Width / 3, scr.Height));
                    while (!Window.TemplateMatching(rect, AppDirectory + "\\templates\\env\\play.png", out point))
                    {
                        await Task.Delay(3000);
                    }
                    if (Window.TemplateMatching(rect, AppDirectory + "\\templates\\env\\play.png", out point))
                    {
                        await MouseSim.ClickOnPointWindows(point.Add(rect.Location));

                        while (Window.FindWindowByCaption(IntPtr.Zero, "Rise of Kingdoms") == IntPtr.Zero)
                        {
                            await Task.Delay(3000);
                        }

                        Handle = Window.FindWindowByCaption(IntPtr.Zero, "Rise of Kingdoms");

                        while (!Window.TemplateMatching(BottomLeftRectangle, BottomLeftRectangleOffset, AppDirectory + "\\templates\\buttons\\home.png", out point) &&
                            !Window.TemplateMatching(BottomLeftRectangle, BottomLeftRectangleOffset, AppDirectory + "\\templates\\buttons\\onMap.png", out point))
                        {
                            await Task.Delay(3000);
                        }
                        await Task.Delay(3000);
                        StateManager.Enabled = false;
                        StateManager = new GemFindingStateManager(Handle);
                        StateManager.Start();
                    }
                }
            }
            else
            {
                Handle = Window.FindWindowByCaption(IntPtr.Zero, "Rise of Kingdoms");
                Window.SetForegroundWindow(Handle);

                while (!Window.TemplateMatching(BottomLeftRectangle, BottomLeftRectangleOffset, AppDirectory + "\\templates\\buttons\\home.png", out var point))
                {
                    await Task.Delay(3000);
                }

                StateManager.Enabled = false;
                StateManager = new GemFindingStateManager(Handle);
                StateManager.Start();
            }
        }
    }
}
