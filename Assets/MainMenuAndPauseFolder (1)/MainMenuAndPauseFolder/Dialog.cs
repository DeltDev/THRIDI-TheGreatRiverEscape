using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
public class Dialog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComp;
    [SerializeField] private string[] Lines;
    [SerializeField] private float textSpeed;
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
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
