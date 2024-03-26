using System.Collections.Generic;

namespace Commands
{
    public class CommandInvoker
    {
        private Stack<ICommand> commandRegistry = new();
        
        /// <summary>
        /// Process a command, which involves both executing it and registering it.
        /// </summary>
        /// <param name="commandToProcess">The command to be processed.</param>
        public void ProcessCommand(ICommand commandToProcess)
        {
            RegisterCommand(commandToProcess);
            ExecuteCommand(commandToProcess);
        }
        
        /// <summary>
        /// Execute a command, invoking its associated action.
        /// </summary>
        /// <param name="commandToExecute">The command to be executed.</param>
        public void ExecuteCommand(ICommand commandToExecute) => commandToExecute.Execute();
        
        /// <summary>
        /// Register a command by adding it to the command registry stack.
        /// </summary>
        /// <param name="commandToRegister">The command to be registered.</param>
        public void RegisterCommand(ICommand commandToRegister) => commandRegistry.Push(commandToRegister);
    }
}