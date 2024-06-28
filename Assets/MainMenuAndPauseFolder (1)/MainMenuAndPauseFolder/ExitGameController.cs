using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitGameController : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject ExitPanel;
    [SerializeField] private GameObject BGOverlay;
    private RectTransform PausePanelPos,ExitPanelPos;
    private CanvasGroup overlay;
    private void Start() {
        PausePanelPos = PausePanel.GetComponent<RectTransform>();
        ExitPanelPos = ExitPanel.GetComponent<RectTransform>();
        overlay = BGOverlay.GetComponent<CanvasGroup>();
    }
    public void NoButton(){
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        ExitPanelPos.LeanMoveLocalY(-1000,1f).setIgnoreTimeScale(true).setEaseInOutBack().setOnComplete(() => ExitPanel.SetActive(false));
        if(SceneManager.GetActiveScene().buildIndex != 0){ //jika ini bukan main menu
            PausePanel.SetActive(true);
            PausePanelPos.LeanMoveLocalY(0,1f).setIgnoreTimeScale(true).setDelay(1.5f).setEaseInOutBack();
        } else {
            LeanTween.alphaCanvas(overlay, 0, 0.5f).setDelay(1f).setOnComplete(() =>BGOverlay.SetActive(false));
        }
    }

    public void YesButton(){
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
        Debug.Log("Keluar");
        Application.Quit();
    }
}
