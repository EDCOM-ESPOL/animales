using DigitalRuby.SoundManagerNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryController : VideoController {

    public Button replayButton;
    public Button skipButton;

    public StoryMetaData storyData;

    private int skipsCount = 0;

    // Use this for initialization
    void Start () {
        storyData = new StoryMetaData(SessionManager.Instance.nombre_jugador, System.Math.Round(player.clip.length).ToString());
        panel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (!isPaused)
        {
            if (player.time >= 197.0f)
            {
                replayButton.interactable = false;
                skipButton.interactable = false;
                if (skipsCount == 0)
                {
                    storyData.estado = "completado";
                }else storyData.estado = "abandonado";
                panel.SetActive(true);
                if (player.time >= 207.0f)
                {
                    SendJSONAndGoToScene("EntornoNaturalHub");
                }
            }
            else
            {
                replayButton.interactable = true;
                skipButton.interactable = true;
                if (Input.GetMouseButtonUp(0))
                {
                    if (panel.activeSelf == false)
                    {
                        panel.SetActive(true);
                    }
                    else
                    {
                        panel.SetActive(false);
                    }
                }
            }
            
        }
        else
        {
            panel.SetActive(true);
        }

    }

    //public void Pause()
    //{
    //    AudioManager.Instance.PlaySFX("TinyButtonPush");
    //    player.Pause();
    //    pauseButton.SetActive(false);
    //    playButton.SetActive(true);
    //    isPaused = true;
    //}

    //public void UnPause()
    //{
    //    AudioManager.Instance.PlaySFX("TinyButtonPush");
    //    player.Play();
    //    pauseButton.SetActive(true);
    //    playButton.SetActive(false);
    //    isPaused = false;
    //}

    public void Avanza()
    {
        player.time = player.time + 15.0f;
        skipsCount++;
    }

    public void SendJSONAndGoToScene(string sceneName)
    {
        storyData.fecha_fin = System.DateTime.Now.ToString("yyyy/MM/dd");
        storyData.tiempo_juego = System.Math.Round(Time.timeSinceLevelLoad).ToString();
        GameStateManager.Instance.AddJsonToList(JsonUtility.ToJson(storyData));
        AudioManager.Instance.PlaySFX("TinyButtonPush");
        base.StopAndGoToScene(sceneName);
    }

}
