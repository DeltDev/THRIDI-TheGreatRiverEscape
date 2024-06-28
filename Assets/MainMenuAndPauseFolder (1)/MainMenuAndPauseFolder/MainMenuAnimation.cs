using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform LogoScale,PlayButtonScale,ExitButtonScale;
    [SerializeField] private RectTransform ThridiLogo,GemastikLogo;
    void Start()
    {
        LogoScale.localScale = Vector3.zero;
        PlayButtonScale.localScale = Vector3.zero;
        ExitButtonScale.localScale = Vector3.zero;
        ThridiLogo.localPosition = new Vector3(-800,-1000,0);
        GemastikLogo.localPosition = new Vector3(-550,-1000,0); 
        LogoScale.LeanScale(Vector3.one,1f).setIgnoreTimeScale(true).setEaseInOutBack().setDelay(0.25f);
        PlayButtonScale.LeanScale(Vector3.one,1f).setIgnoreTimeScale(true).setEaseInOutBack().setDelay(1.5f);
        ExitButtonScale.LeanScale(Vector3.one,1f).setIgnoreTimeScale(true).setEaseInOutBack().setDelay(2.75f);
        ThridiLogo.LeanMoveLocalY(-400,1f).setIgnoreTimeScale(true).setDelay(4f).setEaseOutQuart();
        GemastikLogo.LeanMoveLocalY(-400,1f).setIgnoreTimeScale(true).setDelay(4.5f).setEaseOutQuart();
    }
}
