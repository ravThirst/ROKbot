using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ROKbot
{
    public class MaraudersHuntingStateManager : StateManager
    {
        public MaraudersHuntingStateManager(IntPtr handle) : base(handle) { }
        public MaraudersHuntingStateManager() { }

        public override void InitializeStatesForStateManager(IntPtr handle)
        {
            states.Add(States.Home, new HomeState(this, handle));
            states.Add(States.Search, new MaraudersFindingState(this, handle));
            states.Add(States.Zoom, new ZoomOutState(this, handle));
            states.Add(States.TroopDispatch, new TroopDispatchMarauderState(this, handle));
            states.Add(States.Exception, new ExceptionHandlerState(this, handle));
            states.Add(States.Launch, new LaunchGameState(this, handle));
            states.Add(States.AP, new AddActionPointsState(this, handle));
            currentState = States.Home;
        }

        public override States StateAfterExeption { get => States.Zoom; }
        public override States StateAfterHome => States.Zoom;
    }
}