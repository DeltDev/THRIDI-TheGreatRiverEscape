using System;
using UnityEngine;

[Serializable]
public class PlayerStats
{
    
    [Header("Toxicity")]
    [SerializeField]private float _toxicity;
    [SerializeField]private float minToxicity = 0;
    [SerializeField]private float maxToxicity = 100;
    
    [Header("Hunger")]
    [SerializeField]private float _hunger;
    [SerializeField]private float minHunger = 0;
    [SerializeField]private float maxHunger = 100;
    
    public float Toxicity
    {
        get => _toxicity;
        set => _toxicity = value;
    }
    
    public float Hunger
    {
        get => _hunger;
        set => _hunger = value;
    }
    
    public PlayerStats(float toxicity = 0, float hunger = 100)
    {
        _toxicity = toxicity;
        _hunger = hunger;
    }
    
    public void IncreaseToxicity(float amount)
    {
        _toxicity = Math.Min(_toxicity + amount, maxToxicity);
    }

    public void DecreaseToxicity(float amount)
    {
        _toxicity = Math.Max(_toxicity - amount, minToxicity);
    }

    public void IncreaseHunger(float amount)
    {
        _hunger = Math.Min(_hunger + amount, maxHunger);
    }

    public void DecreaseHunger(float amount)
    {
        _hunger = Math.Max(_hunger - amount, minHunger);
    }
}
