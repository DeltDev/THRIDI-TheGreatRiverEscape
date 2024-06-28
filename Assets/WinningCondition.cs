using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningCondition : MonoBehaviour
{
   public static int ToxicCondition;
    [SerializeField] private Dialog dialog;
   public void Win(){
        if(ToxicCondition <= 50){
            ChangeDialogueLines("Karena ikan yang dimakan tidak terlalu beracun karena kadar toxicnya dibawah 50%, manusia yang memakannya pun hidup dengan tenang");
        }
        else{
            ChangeDialogueLines("Ikan yang sudah dimakan ternyata beracun karena ");
        }
   }

   private void ChangeDialogueLines(string message){
        dialog.Lines[3] = message;
   }
}
