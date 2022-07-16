using RThirst.Tools;

namespace ROKbot
{
    public class ExceptionHandlerState : State, IState
    {
        public ExceptionHandlerState(StateManager stateManager, IntPtr handle) : base(stateManager, handle)
        {

        }

        bool IsVerificated;

        public async Task Action()
        {
            await Task.Delay(2000);
            if (Window.TemplateMatching(new Rectangle(WindowRectangle.Location.Add(new Point(268, 338)), new Size(759, 135)), AppDirectory + "\\templates\\buttons\\verify.png", out var point5))
            {
                IsVerificated = true;
                EMail.SendEmail();
                while (Window.TemplateMatching(new Rectangle(WindowRectangle.Location.Add(new Point(268, 338)), new Size(759, 135)), AppDirectory + "\\templates\\buttons\\verify.png", out var point6))
                {
                    await Task.Delay(30000);
                }
            }
            if (Window.TemplateMatching(new Rectangle(WindowRectangle.Location, new Size(210, 162)), AppDirectory + "\\templates\\env\\windowError.png", out var point4))
            {
                Window.MoveWindow(Handle, WindowRectangle.X + 1, WindowRectangle.Y + 1, WindowRectangle.Width, WindowRectangle.Height, false);
                await Task.Delay(500);
            }
            if (Window.TemplateMatching(RightRectangle, RightRectangleOffset, AppDirectory + "\\templates\\buttons\\newTroop.png", out var point3))
            {
                await MouseSim.ClickOnPoint(Handle, new Point(640, 400), true);
                await Task.Delay(500);
            }
            if (Window.TemplateMatching(BottomLeftRectangle, BottomLeftRectangleOffset, AppDirectory + "\\templates\\buttons\\home.png", out var point))
            {
                await MouseSim.ClickOnPoint(Handle, point, true);
                StateManager.NextState(States.Home);
                Path.ZeroPath();
                ListBox.Items[0] = "going on map";

                await Task.Delay(500);
            }
            else if (Window.TemplateMatching(BottomLeftRectangle, BottomLeftRectangleOffset, AppDirectory + "\\templates\\buttons\\onMap.png", out var point1))
            {
                StateManager.NextState(StateManager.StateAfterExeption);
                if (StateManager.StateAfterExeption == States.Home)
                    Path.ZeroPath();
                ListBox.Items[0] = "exeption handled";
                await Task.Delay(500);
                return;
            }
            else if (Window.TemplateMatching(WindowRectangle, AppDirectory + "\\templates\\buttons\\confirm.png", out var point2))
            {
                await MouseSim.ClickOnPoint(Handle, point2, true);
                ListBox.Items[0] = "restarting app";
                Path.ZeroPath();
                StateManager.NextState(States.Launch);
                if (IsVerificated)
                {
                    await Task.Delay(60000);
                    IsVerificated = false;
                }
                else
                    await Task.Delay(20000);

                if (Window.FindWindowByCaption(IntPtr.Zero, "Rise of Kingdoms") != IntPtr.Zero)
                    StateManager.NextState(States.Home);
            }
            else
            {
                await MouseSim.ClickOnPoint(Handle, new Point(640, 400), true);
                ListBox.Items[0] = "unknown state";
                await Task.Delay(60000);
            }
        }
    }
}
