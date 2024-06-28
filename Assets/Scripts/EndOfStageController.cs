using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Threading;
public enum StageType {MainMenu = 0, FirstStage = 1, SecondStage = 2, ThirdStage = 3, FinalStage = 4, FirstDialogue = 5, SecondDialogue = 6, ThirdDialogue = 7, FinalDialogue = 8, DieDialogue = 9, WinDialogue = 10, PreviewDialogue = 11}
public class EndOfStageController : MonoBehaviour
{
    
    [SerializeField] private StageType NextStage;
    //[SerializeField] private float TransitionDuration = 2f;

    [SerializeField] private Image FadeImage;
    
    private  void OnTriggerEnter(Collider other)
    {   
        if(other.CompareTag("Player")){
            StageTransition();
        }   
    }

    private void StageTransition(){
        SceneManager.LoadScene((int)NextStage);
    }

    // private IEnumerator FadeInTransition(){
    //     float timer = 0f;
    //     Color fadeColor = FadeImage.color;
    //     fadeColor.a = 1f;
    //     FadeImage.color = fadeColor;

    //     while(timer < TransitionDuration){
    //         timer += Time.deltaTime;
    //         fadeColor.a = Mathf.Lerp()
    //     }
    // }


}
