using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroController : VideoController {

    public GameObject audioSource;
    public GameObject buttonContainer;
    AudioSource audioA;
    bool fade = false;

    

    // Use this for initialization
    void Start () {

        //Debug.Log(base.player.clip.length);

        audioA = audioSource.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(player.time);
        //Debug.Log(player.isPlaying);

        if (player.time >= 34.0f)
        {
            panel.SetActive(true);
            buttonContainer.SetActive(false);
            if (player.time >= 37.2f) fade = true;
            if (player.time >= 39.6f)
            {      
                player.time = 39.0f;
                player.Pause();
            }
        }
        else
        {
            fade = false;
            buttonContainer.SetActive(true);
            panel.SetActive(false);
        }

    }

    void FixedUpdate()
    {

        if (fade)
        {
            if (audioA.volume != 0.0f)
            {
                audioA.volume -= 0.01f;
            }
        }
    }

    public void Avanza()
    {
        player.time = 32.0f;
    }

}
