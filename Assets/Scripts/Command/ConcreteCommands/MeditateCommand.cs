using Command.Main;

namespace Command.Commands
{
    public class MeditateCommand : UnitCommand
    {
        private bool willHitTarget;

        public MeditateCommand(CommandData commandData)
        {
            this.commandData = commandData;
            willHitTarget = WillHitTarget();
        }

        public override void Execute() => GameService.Instance.ActionService.GetActionByType(CommandType.Meditate).PerformAction(actorUnit, targetUnit, willHitTarget);
        
        public override void Undo()
        {
                targetUnit.TakeDamage(actorUnit.CurrentPower);
                actorUnit.Owner.ResetCurrentActiveUnit();
        }
        
        public override bool WillHitTarget() => true;
    }
}
