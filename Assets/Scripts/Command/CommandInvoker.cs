using Command.Main;
using System.Collections.Generic;
using UnityEngine;

namespace Command.Commands
{
    public class CommandInvoker
    {
        private Stack<ICommand> commandRegistry = new();

        public void ProcessCommand(ICommand commandToProcess)
        {
            ExecuteCommand(commandToProcess);
            RegisterCommand(commandToProcess);
        }

        public void ExecuteCommand(ICommand commandToExecute) => commandToExecute.Execute();

        public void RegisterCommand(ICommand commandToRegister) => commandRegistry.Push(commandToRegister);

        public void Undo()
        {
            if (!RegistryEmpty() && CommandBelongsToActivePlayer())
                commandRegistry.Pop().Undo();
        }

        private bool RegistryEmpty() => commandRegistry.Count == 0;

        private bool CommandBelongsToActivePlayer() => (commandRegistry.Peek() as UnitCommand).commandData.ActorPlayerID == GameService.Instance.PlayerService.ActivePlayerID;

        public void SetReplayCommandStack()
        {
            GameService.Instance.ReplayService.SetCommandStack(commandRegistry);
            commandRegistry.Clear();
        }
    }
}