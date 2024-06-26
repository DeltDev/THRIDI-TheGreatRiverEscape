using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider _toxicitySlider;
    [SerializeField] private Slider _hungerSlider;
    
    [Header("Settings")]
    [SerializeField] private float _updateSpeed = 0.5f;
    
    public void IncreaseToxicity(float value)
    {
        StartCoroutine(IncreaseCoroutine(value, _toxicitySlider));
    }
    
    public void IncreaseHunger(float value)
    {
        StartCoroutine(IncreaseCoroutine(value, _hungerSlider));
    }
    
    private IEnumerator IncreaseCoroutine(float value, Slider s)
    {
        float delta = value / _updateSpeed;
        float timeElapsed = 0;
        while (timeElapsed < _updateSpeed)
        {
            float currentDelta = delta * Time.deltaTime;
            s.value += currentDelta;
            timeElapsed += Time.deltaTime;
            
            if (timeElapsed >= _updateSpeed)
            {
                s.value = value;
            }
        }
        yield return null;
    }
    
    public void DecreaseToxicity(float value)
    {
        StartCoroutine(DecreaseCoroutine(value, _toxicitySlider));
    }
    
    public void DecreaseHunger(float value)
    {
        StartCoroutine(DecreaseCoroutine(value, _hungerSlider));
    }
    
    private IEnumerator DecreaseCoroutine(float value, Slider s)
    {
        float delta = value / _updateSpeed;
        float timeElapsed = 0;
        while (timeElapsed < _updateSpeed)
        {
            float currentDelta = delta * Time.deltaTime;
            s.value -= currentDelta;
            timeElapsed += Time.deltaTime;
            
            if (timeElapsed >= _updateSpeed)
            {
                s.value = value;
            }
        }
        yield return null;
    }
    
    private void Start()
    {
        _toxicitySlider.value = 0;
        _hungerSlider.value = 100;
    }
}
