using UnityEngine;
using Command.Main;
using Command.Commands;
using Command.Player;

namespace Command.Input
{
    public class InputService
    {
        private MouseInputHandler mouseInputHandler;

        private InputState currentState;
        private CommandType selectedCommandType;
        private TargetType targetType;

        public InputService()
        {
            mouseInputHandler = new MouseInputHandler(this);
            SetInputState(InputState.INACTIVE);
            SubscribeToEvents();
        }

        public void SetInputState(InputState inputStateToSet) => currentState = inputStateToSet;

        private void SubscribeToEvents() => GameService.Instance.EventService.OnActionSelected.AddListener(OnActionSelected);

        public void UpdateInputService()
        {
            if(currentState == InputState.SELECTING_TARGET)
                mouseInputHandler.HandleTargetSelection(targetType);
        }

        public void OnActionSelected(CommandType selectedCommandType)
        {
            this.selectedCommandType = selectedCommandType;
            Debug.Log($"The Action Selected is {selectedCommandType}");
            SetInputState(InputState.SELECTING_TARGET);
            SetTargetType(selectedCommandType);
        }

        private void SetTargetType(CommandType selectedCommandType) => targetType = GameService.Instance.ActionService.GetTargetTypeForAction(selectedCommandType);

        public void OnTargetSelected(UnitController targetUnit)
        {
            SetInputState(InputState.EXECUTING_INPUT);
            UnitCommand commandToProcess = CreateUnitCommand(targetUnit);
            GameService.Instance.ProcessUnitCommand(commandToProcess);
        }

        private UnitCommand CreateUnitCommand(UnitController targetUnit)
        {
            switch (selectedCommandType)
            {
                case CommandType.Attack:
                    return new AttackCommand(GameService.Instance.PlayerService.ActiveUnitID,
                                             targetUnit.UnitID,
                                             GameService.Instance.PlayerService.ActivePlayerID,
                                             targetUnit.Owner.PlayerID);
                case CommandType.Heal:
                    return new HealCommand(GameService.Instance.PlayerService.ActiveUnitID,
                                           targetUnit.UnitID,
                                           GameService.Instance.PlayerService.ActivePlayerID,
                                           targetUnit.Owner.PlayerID);
                case CommandType.AttackStance:
                    return new AttackStanceCommand(GameService.Instance.PlayerService.ActiveUnitID,
                                                   targetUnit.UnitID,
                                                   GameService.Instance.PlayerService.ActivePlayerID,
                                                   targetUnit.Owner.PlayerID);
                case CommandType.Cleanse:
                    return new CleanseCommand(GameService.Instance.PlayerService.ActiveUnitID,
                                              targetUnit.UnitID,
                                              GameService.Instance.PlayerService.ActivePlayerID,
                                              targetUnit.Owner.PlayerID);
                case CommandType.Meditate:
                    return new MeditateCommand(GameService.Instance.PlayerService.ActiveUnitID,
                                               targetUnit.UnitID,
                                               GameService.Instance.PlayerService.ActivePlayerID,
                                               targetUnit.Owner.PlayerID);
                default:
                    throw new System.Exception($"No Command found of type: {selectedCommandType}");
            }
        }
    }

    public enum TargetType
    {
        Friendly,
        Enemy,
        Self
    }
}