using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Dialog : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI textComp;

    [SerializeField] private Image Images;
    [SerializeField] private Sprite[] Sprites;
    [SerializeField] public string[] Lines;
    [SerializeField] private float textSpeed;

    [SerializeField] private StageType NextStage;
    private int index;
    private void Start() {
        textComp.text = string.Empty;
        
        StartDialogue();
    }
    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            if(textComp.text == Lines[index]){
                NextLine();
                Debug.Log("fefe");
            } else {
                StopAllCoroutines();
                textComp.text = Lines[index];
                Debug.Log("test");
            }
        }
    }

    void StartDialogue(){
        index = 0;
        Images.color = Color.white;
        Images.sprite = Sprites[index];
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine(){
        bool tryTest = true;
        foreach(char c in Lines[index].ToCharArray()){
            
            textComp.text +=c;

            if(tryTest){
                AudioManager audioManager = FindObjectOfType<AudioManager>();
                if (audioManager != null)
                {
                    audioManager.PlaySound("DialogueSFX");
                }
                else
                {
                    Debug.LogWarning("AudioManager not found!");
                    tryTest = false;
                }
            }
            
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine(){
        if(index < Lines.Length-1){            
            index++;
            Images.sprite = Sprites[index];
            textComp.text = string.Empty;
            StartCoroutine(TypeLine());
            
        } else {
            if(NextStage == StageType.MainMenu){
                Application.Quit();
                return;
            }
            Debug.Log((int)NextStage);
            SceneManager.LoadScene((int)NextStage);
            
        }
    }
}
