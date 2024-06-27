using System;
using UnityEngine;

[Serializable]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    [Header("Player")]
    [SerializeField] public GameObject player;
    [SerializeField] private Collider playerCollider;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerUI playerUI;

    [Header("Settings")] 
    [SerializeField] private float decreaseRate = 1f;
    public GameObject boundingBox; 

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        
        playerStats = new PlayerStats();
    }
    
    private void Update()
    {
        DecreaseHunger(decreaseRate * Time.deltaTime);
    }
    
    
    // ------------------- Player Stats ------------------- //
    public void IncreaseToxicity(float value)
    {
        playerStats.IncreaseToxicity(value);
        playerUI.IncreaseToxicity(playerStats.Toxicity);
    }
    
    public void IncreaseHunger(float value)
    {
        playerStats.IncreaseHunger(value);
        playerUI.IncreaseHunger(playerStats.Hunger);
        Debug.Log("Nyam");
    }
    
    public void DecreaseToxicity(float value)
    {
        playerStats.DecreaseToxicity(value);
        playerUI.DecreaseToxicity(playerStats.Toxicity);
    }
    
    public void DecreaseHunger(float value)
    {
        playerStats.DecreaseHunger(value);
        playerUI.DecreaseHunger(playerStats.Hunger);
    }
    
    // ------------------- Player Power ups ------------------- //
    
    [Header("Power Ups")]
    [SerializeField] private bool isDashObtained;
    [SerializeField] private bool isImmuneBleachObtained;
    [SerializeField] private bool isNightVisionObtained;
    
    public void ObtainDash()
    {
        isDashObtained = true;
    }
    
    public bool IsDashObtained()
    {
        return isDashObtained;
    }
    
    public void ObtainImmuneBleach()
    {
        isImmuneBleachObtained = true;
    }
    
    public bool IsImmuneBleachObtained()
    {
        return isImmuneBleachObtained;
    }
    
    public void ObtainNightVision()
    {
        isNightVisionObtained = true;
    }
    
    public bool IsNightVisionObtained()
    {
        return isNightVisionObtained;
    }
    
}