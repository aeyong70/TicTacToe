using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelController : PanelController
{
    [SerializeField] private Button closeButton;

    private void Start()
    {
        closeButton.onClick.AddListener(OnClickCloseButton);
    }
    
    public void OnClickCloseButton()
    {
        // 1. 설정  저장
        // 2. 창 닫기
        Hide();
    }
}
