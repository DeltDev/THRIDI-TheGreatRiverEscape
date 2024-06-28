using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanelController : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private GameObject ExitPanel;
    
    private RectTransform PausePanelPos,MainMenuPanelPos,ExitPanelPos;
    private void Start() {
        PausePanelPos = PausePanel.GetComponent<RectTransform>();
        MainMenuPanelPos = MainMenuPanel.GetComponent<RectTransform>();
        ExitPanelPos = ExitPanel.GetComponent<RectTransform>();
    }
    public void SpawnMainMenuPanel(){
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
        PausePanelPos.LeanMoveLocalY(-1000,1f).setIgnoreTimeScale(true).setEaseInOutBack().setOnComplete(() => PausePanel.SetActive(false));
        MainMenuPanel.SetActive(true);
        MainMenuPanelPos.localPosition = new Vector3(0, -1000, 0);
        MainMenuPanelPos.LeanMoveLocalY(0,1f).setIgnoreTimeScale(true).setDelay(1.5f).setEaseInOutBack();
    }

    public void SpawnExitPanel(){
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
        PausePanelPos.LeanMoveLocalY(-1000,1f).setIgnoreTimeScale(true).setEaseInOutBack().setOnComplete(() => PausePanel.SetActive(false));
        ExitPanel.SetActive(true);
        ExitPanelPos.localPosition = new Vector3(0, -1000, 0);
        ExitPanelPos.LeanMoveLocalY(0,1f).setIgnoreTimeScale(true).setDelay(1.5f).setEaseInOutBack();
    }
}
