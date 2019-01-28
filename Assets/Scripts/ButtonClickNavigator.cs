using DigitalRuby.SoundManagerNamespace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonClickNavigator : MonoBehaviour {

    public string sceneName;
    public bool isURL;
    public bool delayedWithAudio;
    public string audioName;
    Button thisButton;

    // Use this for initialization
    void Start () {
        thisButton = GetComponent<Button>();
        if (isURL)
        {
            thisButton.onClick.AddListener(delegate { loadAboutURL(); });
        }else if (delayedWithAudio)
            {
                thisButton.onClick.AddListener(delegate { LoadSceneDelayedWithAudio(); });
            }
            else thisButton.onClick.AddListener(delegate { loadScene(); });

    }

    private void PlayDefaultButtonSound()
    {
        AudioManager.Instance.PlaySFX("TinyButtonPush");
    }

    private void LoadSceneDelayedWithAudio()
    {
        PlayDefaultButtonSound();

        Button[] buttons = FindObjectsOfType<Button>();
        foreach (Button button in buttons)
        {
            //print(button);
            button.interactable = false;
        }

        //thisButton.interactable = false;
        StartCoroutine(AudioDelay());
    }


    void loadScene()
    {
        PlayDefaultButtonSound();

        //if (GameStateManager.Instance.getCurrentSceneName() == "MainScreen")
        //{
        //    thisButton.interactable = false;
        //    StartCoroutine(PlayLoliStartAudio());
        //}else

        if (sceneName == "Credits")
        {
            GameStateManager.Instance.creditsScreenCaller = GameStateManager.Instance.getCurrentSceneName();
        }
        GameStateManager.Instance.LoadScene(sceneName);


    }

    IEnumerator AudioDelay()
    {
        AudioManager.Instance.PlayVoice(audioName);
        yield return new WaitForSeconds(1.6f);
        GameStateManager.Instance.LoadScene(sceneName);
    }

    void loadAboutURL()
    {
        PlayDefaultButtonSound();
        Application.OpenURL("http://midi.espol.edu.ec");
    }



}
