using System.Diagnostics;
using AForge.Imaging;
using System.Drawing.Imaging;
using System;
using System.IO;
using MimeKit;
using MailKit.Net.Smtp;
using RThirst.Tools;

namespace ROKbot
{
    public partial class Form1  : Form
    {
        private List<IntPtr> desks = new List<IntPtr>();
        private StateManager SM;
        private HotKey HK;

        public Form1()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            CleanUpLogDirectory();
            HK = new HotKey();
            HK.AddHotKeyRegisterer(button2_Click, HotKeyMods.Alt, ConsoleKey.Z);
            HK.AddHotKeyRegisterer(button1_Click, HotKeyMods.Alt, ConsoleKey.S);
            HK.AddHotKeyRegisterer(TEST, HotKeyMods.Alt, ConsoleKey.Q);
            HK.AddHotKeyRegisterer(Screenshot, HotKeyMods.None, ConsoleKey.F9);
            AddWindow(this, new EventArgs());
            Window.MoveWindow(Handle, 0, 0, this.Size.Width, this.Size.Height, false);
            comboBox1.DataSource = Enum.GetValues(typeof(StateManagers));
            FormElements.checkBox = checkBox1;
            FormElements.listBox = listBox1;
        }

        private void CleanUpLogDirectory()
        {
            string? d = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            d += "\\log\\";
            if (Directory.Exists(d))
                DirectoryTools.EmptyFolder(d);
            else
                Directory.CreateDirectory(d);
        }

        private void TEST(object sender, EventArgs e)
        {
            MouseSim.WheelDown(Window.GetForegroundWindow(), new Point(300,400));
        }

        private void Screenshot(object sender, EventArgs e)
        {
            var s = Window.Screenshot(new Point(), new Size(1920,1080));
            string? d = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            s.Save(d + "\\log\\" + new Random().Next().ToString() + ".png");
            GC.Collect();
        }

        private void AddWindow(object sender, EventArgs e)
        {
            var window = Window.FindWindowByCaption(IntPtr.Zero, "Rise of Kingdoms");
            listBox1.Items.Add("Hello");
            listBox1.Items.Add("");
            listBox1.Items.Add("");
            listBox1.Items.Add("");
            if (window != IntPtr.Zero && desks.All(x => x != window) && desks.Count < 1)
            {
                desks.Add(window);
                checkedListBox1.Items.Add("Rise of Kingdoms");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Enum.TryParse<StateManagers>(comboBox1.SelectedValue.ToString(), out var manager);

            switch (manager)
            {
                case StateManagers.Gem:
                    {
                        if (desks.Count != 0)
                            SM = new GemFindingStateManager(desks[0]);
                        else
                            SM = new GemFindingStateManager();
                        break;
                    }
                case StateManagers.Marauders:
                    {
                        if (desks.Count != 0)
                            SM = new MaraudersHuntingStateManager(desks[0]);
                        else
                            SM = new MaraudersHuntingStateManager();
                        break;
                    }
                case StateManagers.Event7K:
                    {
                        if (desks.Count != 0)
                            SM = new RssFinding7KStateManager(desks[0]);
                        else
                            SM = new RssFinding7KStateManager();
                        break;
                    }
            }
            button1.Enabled = false;
            SM.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Application.MessageLoop)
            {
                // WinForms app
                Application.Exit();
            }
            else
            {
                // Console app
                Environment.Exit(1);
            }
        }
    }
}