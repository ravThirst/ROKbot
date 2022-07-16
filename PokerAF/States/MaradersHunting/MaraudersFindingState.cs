using RThirst.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROKbot
{
    public class MaraudersFindingState : State, IState
    {
        Random random = new Random();

        public MaraudersFindingState(StateManager stateManager, IntPtr handle) : base(stateManager, handle)
        {

        }

        public async Task Action()
        {
            ListBox.Items[0] = "finding marauders";
            var point = new Point();
            await Task.Delay(3000);

            async Task Omg()
            {
                DirectoryInfo d = new DirectoryInfo(AppDirectory + "\\templates\\marauders\\onMap\\");
                Stopwatch sw = new Stopwatch();
                sw.Start();

                while (true)
                {

                    if (Path.IsEnd())
                    {
                        StateManager.NextState(States.Home);
                        return;
                    }

                    if (sw.ElapsedMilliseconds > 300000)
                    {
                        StateManager.NextState(States.Exception);
                        Debug.WriteLine("Finding gems was too long");
                        return;
                    }

                    if (Window.TemplateMatching(new Rectangle(WindowRectangle.Location.Add(new Point(268, 338)), new Size(759, 135)), AppDirectory + "\\templates\\buttons\\verify.png", out var point5))
                    {
                        StateManager.NextState(States.Exception);
                        Debug.WriteLine("Verification");
                        return;
                    }

                    foreach (var file in d.GetFiles())
                    {
                        if (Window.TemplateMatching(WindowRectangle, AppDirectory + "\\templates\\marauders\\onMap\\" + file.Name, out point))
                        {
                            if (PointAllowed(point))
                            {
                                await MouseSim.ClickOnPoint(Handle, point, false);
                                ListBox.Items[0] = "marauders found";
                                await Task.Delay(3000);
                                return;
                            }
                        }
                    }

                    await MoveOnPath();
                    await Task.Delay(4000);
                }
            }

            await Omg();

            if (StateManager.GetState() != States.Search)
                return;

            StateManager.NextState(States.TroopDispatch);
        }

        private async Task MoveOnPath()
        {
            var t = Path.GetData();
            ListBox.Items[1] = "way " + t.Item1.ToString();
            ListBox.Items[2] = "cycle " + t.Item2.ToString();
            ListBox.Items[3] = "iteration " + t.Item3.ToString();
            await Move(Path.GetDirection());

        }

        private async Task Move(int direction)
        {
            switch (direction)
            {
                case 0: await Move(new Point(random.Next(90, 170), random.Next(300, 500)), new Point(random.Next(900, 1000), random.Next(300, 500))); break;
                case 1: await Move(new Point(random.Next(900, 1000), random.Next(300, 500)), new Point(random.Next(90, 170), random.Next(300, 500))); break;
                case 2: await Move(new Point(random.Next(500, 700), random.Next(80, 140)), new Point(random.Next(500, 700), random.Next(580, 630))); break;
                case 3: await Move(new Point(random.Next(500, 700), random.Next(580, 630)), new Point(random.Next(500, 700), random.Next(80, 140))); break;
            }
        }

        public async Task Move(Point start, Point target)
        {
            await MouseSim.DragNDrop(Handle, start, target, 3f);
        }

        public Rectangle GemRectangle()
        {
            var r = WindowRectangle;
            return new Rectangle(new Point(r.Left + 530, r.Top + 300), new Size(240, 160));
            
        }

        public bool PointAllowed(Point point)
        {
            if (point.X < 120 && point.Y < 160)
                return false;
            if (point.X < 510 && point.Y < 130)
                return false;
            if (point.X > 1080 && point.Y < 180)
                return false;
            if (point.X < 135 && point.Y > 690)
                return false;
            if (point.X < 655 && point.Y > 745)
                return false;
            if (point.X > 1190 && point.Y > 635 && point.Y < 735)
                return false;
            if (point.X > 1160 && point.Y > 216 && point.Y < 505)
                return false;
            return true;
        }
    }
}