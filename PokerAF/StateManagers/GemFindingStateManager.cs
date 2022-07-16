using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ROKbot
{
    public class GemFindingStateManager : StateManager
    {
        public GemFindingStateManager(IntPtr handle) : base(handle) { }
        public GemFindingStateManager() { }

        public override void InitializeStatesForStateManager(IntPtr handle)
        {
            states.Add(States.Home, new HomeState(this, handle));
            states.Add(States.Search, new GemsFindingState(this, handle));
            states.Add(States.Zoom, new ZoomOutState(this, handle));
            states.Add(States.TroopDispatch, new TroopDispatchGemState(this, handle));
            states.Add(States.TroopCreation, new TroopCreationGemState(this, handle));
            states.Add(States.Exception, new ExceptionHandlerState(this, handle));
            states.Add(States.Launch, new LaunchGameState(this, handle));
            currentState = States.Home;
        }

        public override States StateAfterExeption => States.Home;
        public override States StateAfterHome => States.Zoom;
    }
}
