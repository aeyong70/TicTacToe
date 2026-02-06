using UnityEngine;
using UnityEngine.SceneManagement;

public class MainPanelController : MonoBehaviour
{
    public void OnClikSinglePlayButton()
    {
        // TODO : 싱글 플레이 버튼 클릭 시
        GameManager.Instance.ChangeToGameScene(Constants.GameType.SinglePlay);

    }

    public void OnClickMultiPlayButton()
    {
        //PlayerPrefs.
        // TODO : 멀티 플레이 버튼 클릭 시
        GameManager.Instance.ChangeToGameScene(Constants.GameType.MultiPlay);
    }

    public void OnClickSettingsButton()
    {
        // TODO : 설정 버튼 클릭 시
        GameManager.Instance.OpenSettingsPanel();
    }
}
