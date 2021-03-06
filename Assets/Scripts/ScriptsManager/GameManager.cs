using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string nameNextScene;
    [SerializeField] private SOSettingsUser soAnimationSelected;
    [SerializeField] private Animator animatorPlayer;
    
    

    public void FunctionChangeAnimation(int idAnimation)
    {
        animatorPlayer.SetInteger("DanceType", idAnimation);
        soAnimationSelected.animSelection = idAnimation;
    }

    public void ButtonAnimationConfirmation()
    {
        StartCoroutine(NextSceneLoad());
    }

    IEnumerator NextSceneLoad()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(nameNextScene);
    }
}
