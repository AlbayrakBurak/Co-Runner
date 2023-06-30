using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScript : MonoBehaviour
{
    public Animator _Animator;

    public void thisPassive(){
        _Animator.SetBool("ok",false);
    }
}
