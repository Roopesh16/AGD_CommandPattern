using Command.Player;

namespace Commands
{
    public abstract class IUnitCommand : ICommand
    {
        public int ActorUnitID;
        public int TargetUnitID;
        public int ActorPlayerID;
        public int TargetPlayerID;
        
        protected UnitController actorUnit;
        protected UnitController targetUnit;
        
        /// <summary>
        /// Abstract method to execute the unit command. Must be implemented by concrete subclasses.
        /// </summary>
        public abstract void Execute();
        
        /// <summary>
        /// Abstract method to determine whether the command will successfully hit its target.
        /// Must be implemented by concrete subclasses.
        /// </summary>
        public abstract bool WillHitTarget();
    }
}