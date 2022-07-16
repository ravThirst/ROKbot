using RThirst.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROKbot
{
    public class TroopDispatchGemState : State, IState
    {
        public TroopDispatchGemState( StateManager stateManager, IntPtr handle) : base(stateManager, handle)
        {

        }

        public async Task Action()
        {
            var point = new Point(640, 400);
            await MouseSim.ClickOnPoint(Handle, point, true);
            await Task.Delay(2000);

            if (Window.TemplateMatching(GatherButtonRectangle, GatherButtonRectangleOffset, AppDirectory + "\\templates\\buttons\\gather.png", out point))
            {
                await MouseSim.ClickOnPoint(Handle, point, true);
                await Task.Delay(2000);
            }
            else
            {
                StateManager.NextState(States.Exception);
                Debug.WriteLine("no button GATHER was found");
                return;
            }

            if (Window.TemplateMatching(RightRectangle, RightRectangleOffset, AppDirectory + "\\templates\\buttons\\newTroop.png", out point))
            {
                await MouseSim.ClickOnPoint(Handle, point, true);
                StateManager.NextState(States.TroopCreation);
                ListBox.Items[0] = "new troop creating";
                await Task.Delay(1000);
            }
            else if (Window.TemplateMatching(RightRectangle, RightRectangleOffset, AppDirectory + "\\templates\\condition\\troopReturning.png", out point))
            {
                await MouseSim.ClickOnPoint(Handle, point, true);
                ListBox.Items[0] = "choosing troop";
                await Task.Delay(1000);
                if (Window.TemplateMatching(RightRectangle, RightRectangleOffset, AppDirectory + "\\templates\\buttons\\marchExistingTroop.png", out point))
                {
                    await MouseSim.ClickOnPoint(Handle, point, true);
                    StateManager.NextState(States.Zoom);
                    ListBox.Items[0] = "march sent";
                    await Task.Delay(1000);
                }
            }
            else
            {
                await MouseSim.ClickOnPoint(Handle, new Point(640, 400), true);
                StateManager.NextState(States.Zoom);
                ListBox.Items[0] = "no free troops, 3 min sleep";
                await Task.Delay(180000);
            }
        }
    }
}