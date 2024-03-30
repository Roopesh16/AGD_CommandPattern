using System.Collections.Generic;
using System.Threading.Tasks;
using Command.Commands;
using Command.Main;
using UnityEngine;

namespace Replay
{
    public class ReplayService
    {
        private const int waitTime = 1000;
        private Stack<ICommand> replayCommandStack;
        
        public ReplayState ReplayState { get; private set; }

        public ReplayService() => SetReplayState(ReplayState.DEACTIVE);

        public void SetReplayState(ReplayState newState) => ReplayState = newState;

        public void SetCommandStack(Stack<ICommand> commandStack) =>
            replayCommandStack = new Stack<ICommand>(commandStack);

        public void ExecuteNext()
        {
            if (replayCommandStack.Count > 0)
            {
                WaitReplay();
            }
        }

        private async void WaitReplay()
        {
            await Task.Delay(waitTime);
            GameService.Instance.ProcessUnitCommand(replayCommandStack.Pop());
        }
    }
}