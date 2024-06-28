using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Threading;

public class EndOfStageController : MonoBehaviour
{
    private enum StageType {FirstStage, SecondStage, ThirdStage, FinalStage}
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
