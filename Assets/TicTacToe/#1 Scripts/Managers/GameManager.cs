using UnityEngine;
using UnityEngine.SceneManagement;
using static Constants;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject settingsPanelPrefab;

    [SerializeField] private Canvas canvas;

    // 게임의 종류(싱글 플레이 / 멀티 플레이)
    private GameType gameType;

    private SettingsPanelController settingsPanel;

    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        // Todo : 씬이 전환될 때 GameManager가 처리해야 할 작업 구현
        canvas = FindFirstObjectByType<Canvas>();
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
