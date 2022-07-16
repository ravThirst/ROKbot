using RThirst.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROKbot
{
    public class AddActionPointsState : State, IState
    {
        public AddActionPointsState(StateManager stateManager, IntPtr handle) : base(stateManager, handle)
        {

        }

        public async Task Action()
        {
            ListBox.Items[0] = "adding AP";
            for (int i = 0; i<15; i++)
            {
                if (Window.TemplateMatching(APRectangle, APRectangleOffset, AppDirectory + "\\templates\\buttons\\use.png", out var point1))
                {
                    await MouseSim.ClickOnPoint(Handle, point1, true);
                    await Task.Delay(100);
                }
            }

            if (Window.TemplateMatching(CloseRectangle, CloseRectangleOffset, AppDirectory + "\\templates\\buttons\\close.png", out var point))
            {
                await MouseSim.ClickOnPoint(Handle, point, true);
                await Task.Delay(100);
            }
            else
            {
                StateManager.NextState(States.Exception);
                Debug.WriteLine("button close not found");
            }
            StateManager.NextState(States.Zoom);
            return;
        }

        public Rectangle APRectangle => new Rectangle(new Point(WindowRectangle.Left + 831, WindowRectangle.Top + 247), new Size(240, 400));
        public Point APRectangleOffset => new Point(831, 247);

        public Rectangle CloseRectangle => new Rectangle(new Point(WindowRectangle.Left + 1000, WindowRectangle.Top + 100), new Size(200, 150));
        public Point CloseRectangleOffset => new Point(1000, 100);
    }
}