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
    public class TroopCreationGemState : State, IState
    {
        public TroopCreationGemState(StateManager stateManager, IntPtr handle) : base(stateManager, handle)
        {

        }

        public async Task Action()
        {
            var point = new Point(640, 400);

            if (Window.TemplateMatching(TroopCreationRectangle, TroopCreationRectangleOffset, AppDirectory + "\\templates\\buttons\\max.png", out point))
            {
                while(!Window.TemplateMatching(TroopSetRectangle, TroopSetRectangleOffset, AppDirectory + "\\templates\\condition\\yellowTroopSet.png", out point))
                {
                    if (Window.TemplateMatching(TroopSetRectangle, TroopSetRectangleOffset, AppDirectory + "\\templates\\buttons\\swap.png", out point))
                        await MouseSim.ClickOnPoint(Handle, point, true);
                    await Task.Delay(500);
                }
                await MouseSim.ClickOnPoint(Handle, new Point(1110, 400), true);
                await Task.Delay(500);
                await MouseSim.ClickOnPoint(Handle, new Point(1110, 460), true);
                await Task.Delay(500);
                await MouseSim.ClickOnPoint(Handle, new Point(1110, 510), true);
                await Task.Delay(500);
                await MouseSim.ClickOnPoint(Handle, new Point(1110, 570), true);
                await Task.Delay(500);
                await MouseSim.ClickOnPoint(Handle, new Point(1110, 630), true);
                await Task.Delay(500);

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