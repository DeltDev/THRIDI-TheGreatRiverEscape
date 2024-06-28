using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject BGOverlay;
    [SerializeField] private GameObject ExitPanel;
    private RectTransform ExitPanelPos;
    private CanvasGroup overlay;
    private void Start() {
        overlay = BGOverlay.GetComponent<CanvasGroup>();
        ExitPanelPos = ExitPanel.GetComponent<RectTransform>();
    }
    public void PlayGame(){
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame(){
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
        ExitPanel.SetActive(true);
        BGOverlay.SetActive(true);
        ExitPanelPos.localPosition = new Vector3(0, -1000, 0);
        overlay.alpha = 0;
        LeanTween.alphaCanvas(overlay, 1, 0.5f).setIgnoreTimeScale(true);
        ExitPanelPos.LeanMoveLocalY(0,1f).setIgnoreTimeScale(true).setDelay(0.5f).setEaseInOutBack();
    }
}
