using System;
using UnityEngine;

[Serializable]
public class GameManager : MonoBehaviour
{

    
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "GameManager";
                    instance = go.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    

    [Header("Player")]
    [SerializeField] public GameObject player;
    [SerializeField] private Collider playerCollider;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerUI playerUI;

    [Header("Settings")] 
    [SerializeField] private float decreaseRate = 1f;

    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
        playerStats = new PlayerStats();
    }
    
    
    private void Update()
    {
        DecreaseHunger(decreaseRate * Time.deltaTime);
    }
    
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
}