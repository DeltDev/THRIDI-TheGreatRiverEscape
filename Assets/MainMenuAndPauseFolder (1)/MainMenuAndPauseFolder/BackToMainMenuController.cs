using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BackToMainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject MainMenuPanel;
    private RectTransform PausePanelPos,MainMenuPanelPos;
    private void Start() {
        PausePanelPos = PausePanel.GetComponent<RectTransform>();
        MainMenuPanelPos = MainMenuPanel.GetComponent<RectTransform>();
    }
    public void NoButton(){
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
        MainMenuPanelPos.LeanMoveLocalY(-1000,1f).setIgnoreTimeScale(true).setEaseInOutBack().setOnComplete(() => MainMenuPanel.SetActive(false));
        PausePanel.SetActive(true);
        PausePanelPos.LeanMoveLocalY(0,1f).setIgnoreTimeScale(true).setDelay(1.5f).setEaseInOutBack();
    }

    public void YesButton(){
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
        MainMenuPanelPos.LeanMoveLocalY(-1000,1f).setIgnoreTimeScale(true).setEaseInOutBack().setOnComplete(OnYesComplete);
    }

    private void OnYesComplete(){
        MainMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
