using RThirst.Tools;
using System.ComponentModel.DataAnnotations;

namespace ROKbot
{
    public class RssFinding7KState : State, IState
    {
        Random random = new Random();
        [Range(0, 1)]
        int currentTarget;

        public RssFinding7KState(StateManager stateManager, IntPtr handle) : base(stateManager, handle)
        {

        }

        public async Task Action()
        {
            var point = new Point(640,640);
            //if (Window.TemplateMatching(BottomLeftRectangle, BottomLeftRectangleOffset, AppDirectory + "\\templates\\buttons\\onMap.png", out point))
            //{
            //    await MouseSim.ClickOnPoint(Handle, point, true);
            //    ListBox.Items[0] = "going home";
            //    await Task.Delay(1000);
            //}
            //if (Window.TemplateMatching(BottomLeftRectangle, BottomLeftRectangleOffset, AppDirectory + "\\templates\\buttons\\home.png", out point))
            //{
            //    await MouseSim.ClickOnPoint(Handle, point, true);
            //    ListBox.Items[0] = "going on map";
            //    await Task.Delay(1000);
            //}
            if (Window.TemplateMatching(BottomLeftRectangle, BottomLeftRectangleOffset, AppDirectory + "\\templates\\buttons\\search.png", out point))
            {
                await MouseSim.ClickOnPoint(Handle, point, true);
                ListBox.Items[0] = "search";
                await Task.Delay(1000);
                await Search();
                StateManager.NextState(States.TroopDispatch);
            }
        }

        async Task Search()
        {
            switch (currentTarget)
            {
                case 0:
                    {
                        await MouseSim.ClickOnPoint(Handle, new Point(460, 750), true);
                        await Task.Delay(500);
                        await MouseSim.ClickOnPoint(Handle, new Point(460, 600), true);
                        await Task.Delay(500);
                        currentTarget = 1;
                        break;
                    }
                case 1:
                    {
                        await MouseSim.ClickOnPoint(Handle, new Point(650, 750), true);
                        await Task.Delay(500);
                        await MouseSim.ClickOnPoint(Handle, new Point(650, 600), true);
                        await Task.Delay(500);
                        currentTarget = 0;
                        break;
                    }
            }
        }
    }
}