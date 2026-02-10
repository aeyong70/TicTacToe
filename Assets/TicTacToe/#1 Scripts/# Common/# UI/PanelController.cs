using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class PanelController : MonoBehaviour
{
    [SerializeField] private RectTransform panelTransform;

    public delegate void PanelControllerHideDelegate();

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        Debug.Log("Show Panel");
        gameObject.SetActive(true);

        canvasGroup.alpha = 0;
        panelTransform.localScale = Vector3.zero;

        canvasGroup.DOFade(1, 0.4f).SetEase(Ease.Linear);
        panelTransform.DOScale(1, 0.4f).SetEase(Ease.OutBack);
    }

    public void Hide(PanelControllerHideDelegate onComplete = null)
    {
        Debug.Log("Hide Panel");

        canvasGroup.DOFade(0, 0.4f).SetEase(Ease.Linear);
        panelTransform.DOScale(0, 0.4f).SetEase(Ease.InBack).OnComplete(() =>
        {
            onComplete?.Invoke();
            gameObject.SetActive(false);
        });
    }


}
