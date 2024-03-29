using System.Collections.Generic;
using Command.Commands;
using Command.Main;
using UnityEngine;

namespace Replay
{
    public class ReplayService
    {
        private Stack<ICommand> replayCommandStack = new();
        
        public ReplayState ReplayState { get; private set; }

        public ReplayService() => SetReplayState(ReplayState.DEACTIVE);

        public void SetReplayState(ReplayState newState) => ReplayState = newState;

        public void SetCommandStack(Stack<ICommand> commandStack) =>
            replayCommandStack = new Stack<ICommand>(commandStack);

        public void ExecuteNext()
        {
            if (replayCommandStack.Count > 0)
            {
                GameService.Instance.ProcessUnitCommand(replayCommandStack.Pop());
            }
        }
    }
}