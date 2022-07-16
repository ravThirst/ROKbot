using RThirst.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROKbot
{
    public enum StateManagers
    {
        Gem, Marauders, Event7K
    }

    public enum States
    {
        Home, Zoom, Search, TroopDispatch, TroopCreation, Exception, Launch, AP
    }

    public interface IState
    {
        public Task Action();
    }

    public class State
    {
        public ListBox ListBox { get; private set; }
        public StateManager StateManager { get; set; }
        public IntPtr Handle { get; set; }
        public Rectangle WindowRectangle { get => Window.GetWindowRect(Handle); }

        public Rectangle BottomLeftRectangle { get => new Rectangle(new Point(WindowRectangle.Left, WindowRectangle.Top + 600), new Size(125, 250)); }
        public Rectangle RightRectangle { get => new Rectangle(new Point(WindowRectangle.Left + 890, WindowRectangle.Top + 50), new Size(410, 780)); }
        public Rectangle TroopCreationRectangle { get => new Rectangle(new Point(WindowRectangle.Left + 590, WindowRectangle.Top + 650), new Size(490, 90)); }
        public Rectangle ConfirmRectangle { get => new Rectangle(new Point(WindowRectangle.Left + 430, WindowRectangle.Top + 430), new Size(450, 250)); }
        public Rectangle GatherButtonRectangle { get => new Rectangle(new Point(WindowRectangle.Left + 190, WindowRectangle.Top + 470), new Size(900, 100)); }
        public Rectangle RightSmallRectangle { get => new Rectangle(new Point(WindowRectangle.Left + 1124, WindowRectangle.Top + 204), new Size(180, 333)); }
        public Rectangle MailRectangle { get => new Rectangle(new Point(WindowRectangle.Left + 1180, WindowRectangle.Top + 600), new Size(100, 200)); }
        public Rectangle TroopSetRectangle { get => new Rectangle(new Point(WindowRectangle.Left + 1055, WindowRectangle.Top + 304), new Size(102, 358)); }

        public Point BottomLeftRectangleOffset { get => new Point(0, 600); }
        public Point RightRectangleOffset { get => new Point(890, 50); }
        public Point TroopCreationRectangleOffset { get => new Point(590, 650); }
        public Point ConfirmRectangleOffset { get => new Point(430, 430); }
        public Point GatherButtonRectangleOffset { get => new Point(190, 470); }
        public Point RightSmallRectangleOffset { get => new Point(1124, 204); }
        public Point MailRectangleOffset { get => new Point(1180, 600); }
        public Point TroopSetRectangleOffset { get => new Point(1055, 304); }

        public string? AppDirectory { get { return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location); } }

        public State(StateManager stateManager, IntPtr handle)
        {
            ListBox = FormElements.listBox ?? throw new NullReferenceException();
            StateManager = stateManager;
            Handle = handle;
        }
    }
}
