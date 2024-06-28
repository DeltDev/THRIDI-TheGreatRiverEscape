using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPause : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private GameObject ExitPanel;
    [SerializeField] private GameObject BGOverlay;
    private CanvasGroup overlay;
    private RectTransform PausePanelPos,MainMenuPanelPos,ExitPanelPos;
    public static bool isGamePaused; //pake ini kalo gamenya lagi di pause
    private void Start() {
        isGamePaused = false;
        overlay = BGOverlay.GetComponent<CanvasGroup>();
        PausePanelPos = PausePanel.GetComponent<RectTransform>();
        MainMenuPanelPos = MainMenuPanel.GetComponent<RectTransform>();
        ExitPanelPos = ExitPanel.GetComponent<RectTransform>();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isGamePaused){
                Continue();
            } else {
                Pause();
            }
        }
    }
    public void Pause(){
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
        PausePanel.SetActive(true);
        BGOverlay.SetActive(true);
        PausePanelPos.localPosition = new Vector3(0, -1000, 0);
        overlay.alpha = 0;
        LeanTween.alphaCanvas(overlay, 1, 0.5f).setIgnoreTimeScale(true);
        PausePanelPos.LeanMoveLocalY(0,1f).setIgnoreTimeScale(true).setDelay(0.5f).setEaseInOutBack();
        Time.timeScale = 0f;
        isGamePaused = true;
        Debug.Log("Paused");
    }

    public void Continue(){
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
        PausePanelPos.LeanMoveLocalY(-1000,1f).setIgnoreTimeScale(true).setEaseInOutBack().setOnComplete(() => PausePanel.SetActive(false));
        if(MainMenuPanel.activeSelf){
            MainMenuPanelPos.LeanMoveLocalY(-1000,1f).setIgnoreTimeScale(true).setEaseInOutBack().setOnComplete(() => MainMenuPanel.SetActive(false));
        }
        if(ExitPanel.activeSelf){
            ExitPanelPos.LeanMoveLocalY(-1000,1f).setIgnoreTimeScale(true).setEaseInOutBack().setOnComplete(() => ExitPanel.SetActive(false));
        }
        
        LeanTween.alphaCanvas(overlay, 0, 0.5f).setDelay(1f).setIgnoreTimeScale(true).setOnComplete(PauseOnComplete);
    }

    private void PauseOnComplete(){
        BGOverlay.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }
}
