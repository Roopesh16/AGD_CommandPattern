using Command.Input;
using Command.Main;
using Replay;
using UnityEngine.SceneManagement;

namespace Command.UI
{
    public class BattleEndUIController : IUIController
    {
        private BattleEndUIView battleEndView;

        public BattleEndUIController(BattleEndUIView battleEndView)
        {
            this.battleEndView = battleEndView;
            battleEndView.SetController(this);
        }

        public void Show() => battleEndView.EnableView();

        public void Hide() => battleEndView.DisableView();

        public void SetWinner(int winnerId) => battleEndView.SetResultText($"Player {winnerId} Won!");

        public void OnHomeButtonClicked() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        public void OnReplayButtonClicked()
        {
            GameService.Instance.InputService.SetInputState(InputState.INACTIVE);
            GameService.Instance.ReplayService.SetReplayState(ReplayState.ACTIVE);
            GameService.Instance.EventService.OnReplayButtonClicked.InvokeEvent();
        }
    }
}