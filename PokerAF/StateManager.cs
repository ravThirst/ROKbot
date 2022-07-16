using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ROKbot
{
    public abstract class StateManager
    {
        public Dictionary<States, IState> states = new Dictionary<States, IState>();
        public bool Enabled { get; set; }
        public States currentState;
        public abstract States StateAfterExeption { get; }
        public abstract States StateAfterHome { get; }

        public StateManager()
        {
            LaunchGame();
        }

        private void LaunchGame()
        {
            states.Add(States.Launch, new LaunchGameState(this, IntPtr.Zero));
            currentState = States.Launch;
        }

        public StateManager(IntPtr handle)
        {
            InitializeStatesForStateManager(handle);
        }

        public abstract void InitializeStatesForStateManager(IntPtr handle);

        public async void Start()
        {
            if (!Enabled)
            {
                Enabled = true;
                while (Enabled)
                {
                    await states[currentState].Action();
                    await Task.Delay(4000);
                }
            }
        }

        public void NextState(States state)
        {
            if (states.ContainsKey(state))
                currentState = state;
            else
                throw new Exception();
        }

        public States GetState()
        {
            return currentState;
        }
    }
}
