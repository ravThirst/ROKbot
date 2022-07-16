using RThirst.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROKbot
{
    public class TroopDispatchMarauderState : State, IState
    {
        public TroopDispatchMarauderState(StateManager stateManager, IntPtr handle) : base(stateManager, handle)
        {

        }

        public async Task Action()
        {
            var point = new Point(1225, 290);
            await MouseSim.ClickOnPoint(Handle, point, true);
            await Task.Delay(60);
            await MouseSim.ClickOnPoint(Handle, point, true);

            if (Window.TemplateMatching(MarauderRectangle, MarauderRectangleOffset, AppDirectory + "\\templates\\marauders\\close.png", out point))
            {
                await MouseSim.DragNDrop(Handle, new Point(1225, 290) , new Point(point.X+10, point.Y - 50), 1.4f);
                await Task.Delay(1000);
                if (Window.TemplateMatching(APRectangle, APRectangleOffset, AppDirectory + "\\templates\\buttons\\use.png", out var point2))
                {
                    StateManager.NextState(States.AP);
                    return;
                }
                await CollectLetters();
                await Task.Delay(10000);
                while (!Window.TemplateMatching(RightSmallRectangle, RightSmallRectangleOffset, AppDirectory + "\\templates\\condition\\troopWaiting.png", out var point1))
                {
                    ListBox.Items[0] = "awaiting for fight";
                    await Task.Delay(2000);
                    if (!Window.TemplateMatching(BottomLeftRectangle, BottomLeftRectangleOffset, AppDirectory + "\\templates\\buttons\\search.png", out var point7))
                    {
                        StateManager.NextState(States.Exception);
                        return;
                    }
                }
                await MouseSim.ClickOnPoint(Handle, new Point(640,400), true);
                await Task.Delay(500);
                Debug.WriteLine("kicked his ass");
            }
            else
            {
                Debug.WriteLine("marauders not found");
            }
            if (Window.TemplateMatching(BottomLeftRectangle, BottomLeftRectangleOffset, AppDirectory + "\\templates\\buttons\\search.png", out var point3))
            {
                await MouseSim.ClickOnPoint(Handle, point3, true);
                await Task.Delay(1000);
                await MouseSim.ClickOnPoint(Handle, new Point(640, 300), true);
            }
            StateManager.NextState(States.Zoom);
            return;
        }

        async Task CollectLetters()
        {
            if (Window.TemplateMatching(MailRectangle,MailRectangleOffset, AppDirectory + "\\templates\\buttons\\mail.png", out var point3))
            {
                await MouseSim.ClickOnPoint(Handle, point3, true);
                await Task.Delay(500);
                await MouseSim.ClickOnPoint(Handle, new Point(132, 777), true);
                await Task.Delay(500);
                if (Window.TemplateMatching(ConfirmRectangle, ConfirmRectangleOffset, AppDirectory + "\\templates\\buttons\\confirm.png", out var point4))
                {
                    await MouseSim.ClickOnPoint(Handle, point4, true);
                    await Task.Delay(500);
                }
                await MouseSim.ClickOnPoint(Handle, new Point(575, 75), true);
                await Task.Delay(500);
                await MouseSim.ClickOnPoint(Handle, new Point(132, 777), true);
                await Task.Delay(1000);
                if (Window.TemplateMatching(ConfirmRectangle, ConfirmRectangleOffset, AppDirectory + "\\templates\\buttons\\confirm.png", out var point5))
                {
                    await MouseSim.ClickOnPoint(Handle, point5, true);
                    await Task.Delay(500);
                }
                await MouseSim.ClickOnPoint(Handle, new Point(1240, 72), true);
                await Task.Delay(500);
            }
        }

        public Rectangle MarauderRectangle => new Rectangle(new Point(WindowRectangle.Left + 240, WindowRectangle.Top + 150), new Size(740, 520));
        public Point MarauderRectangleOffset => new Point(240, 150);

        public Rectangle APRectangle => new Rectangle(new Point(WindowRectangle.Left + 831, WindowRectangle.Top + 247), new Size(240, 400));
        public Point APRectangleOffset => new Point(831, 247);
    }
}