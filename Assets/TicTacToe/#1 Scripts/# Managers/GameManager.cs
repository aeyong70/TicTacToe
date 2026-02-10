using UnityEngine;
using UnityEngine.SceneManagement;
using static Constants;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject settingsPanelPrefab;
    [SerializeField] private GameObject confirmPanelPrefab;

    [SerializeField] private Canvas canvas;

    // 게임 화면의 UI 컨트롤러
    private GamePanelController gamePanelController;

    // 게임의 종류(싱글 플레이 / 멀티 플레이)
    private GameType gameType;

    // Game Logic 
    private GameLogic gameLogic;

    private SettingsPanelController settingsPanel;

    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        // Todo : 씬이 전환될 때 GameManager가 처리해야 할 작업 구현
        canvas = FindFirstObjectByType<Canvas>();

        if(scene.name == SCENE_GAME)
        {
            // 게임 씬
            var blockController = FindFirstObjectByType<BlockController>();
            if (blockController != null)
            {
                blockController.InitBlocks();
            }

            // GamePanelController 참조 가져오기
            gamePanelController = FindFirstObjectByType<GamePanelController>();

            // GameLogic 생성
            gameLogic = new GameLogic(gameType, blockController);
        }
    }

    public void SetGameTurn(Constants.PlayerType playerTurnType)
    {
        gamePanelController.SetPlayerTurnPanel(playerTurnType);
    }

    // Settings Panel 열기
    public void OpenSettingsPanel()
    {
        if(settingsPanel == null)
        {
            var obj = Instantiate(settingsPanelPrefab, canvas.transform);
            settingsPanel = obj.GetComponent<SettingsPanelController>();
        }

        settingsPanel.Show();
    }

    // Confirm Panel 열기
    public void OpenConfirmPanel(string message, ConfirmPanelController.OnConfirmButtonClicked onConfirmButtonClicked)
    {
        var confirmPanelObject = Instantiate(confirmPanelPrefab, canvas.transform);
        confirmPanelObject.GetComponent<ConfirmPanelController>().Show(message,onConfirmButtonClicked);
    }

    // 씬 전환 ( Main -> Game )
    public void ChangeToGameScene(GameType gameType)
    {
        this.gameType = gameType;
        SceneManager.LoadScene("Game");
    }

    // 씬 전환 ( Game -> Main )
    public void ChangeToMainScene()
    {
        SceneManager.LoadScene("Main");
    }
}
