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
            } else {
                StopAllCoroutines();
                textComp.text = Lines[index];
            }
        }
    }

    void StartDialogue(){
        index = 0;
        StartCoroutine(TypeLine());
        Images.color = Color.white;
        Images.sprite = Sprites[index];
    }

    IEnumerator TypeLine(){
        foreach(char c in Lines[index].ToCharArray()){
            textComp.text +=c;
            FindObjectOfType<AudioManager>().PlaySound("DialogueSFX");
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine(){
        if(index < Lines.Length-1){
            index++;
            textComp.text = string.Empty;
            StartCoroutine(TypeLine());
            Images.sprite = Sprites[index];
        } else {
            Debug.Log((int)NextStage);
            SceneManager.LoadScene((int)NextStage);
        }
    }
}
