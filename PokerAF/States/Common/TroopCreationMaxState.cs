using ROKbot;
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
    public class TroopCreationMaxState : State, IState
    {
        public TroopCreationMaxState(StateManager stateManager, IntPtr handle) : base(stateManager, handle)
        {

        }

        public async Task Action()
        {
            var point = new Point(640, 400);

            if (Window.TemplateMatching(TroopCreationRectangle, TroopCreationRectangleOffset, AppDirectory + "\\templates\\buttons\\max.png", out point))
            {
                await MouseSim.ClickOnPoint(Handle, point, true);
                await Task.Delay(250);
                if (Window.TemplateMatching(TroopCreationRectangle, TroopCreationRectangleOffset, AppDirectory + "\\templates\\buttons\\march.png", out point))
                {
                    await MouseSim.ClickOnPoint(Handle, point, true);
                    await Task.Delay(1000);
                    StateManager.NextState(States.Zoom);
                    ListBox.Items[0] = "troop created";
                }
                else
                {
                    StateManager.NextState(States.Exception);
                    Debug.WriteLine("No button MARCH was found");
                }
            }
            else
            {
                StateManager.NextState(States.Exception);
                Debug.WriteLine("no button MAX was found");
            }
        }
    }
}