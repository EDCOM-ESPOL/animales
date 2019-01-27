using DigitalRuby.SoundManagerNamespace;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour {

    public VideoPlayer player;
    public GameObject panel;
    public GameObject pauseButton;
    public GameObject playButton;

    protected bool isPaused = false;


    // Use this for initialization
    void Start () {
        
        panel.SetActive(false);
        
    }
    

    public void Replay()
    {
        AudioManager.Instance.PlaySFX("TinyButtonPush");
        print("Reloading Scene...");
        GameStateManager.Instance.ReloadCurrentScene();
    }

    public void Stop()
    {
        player.Stop();
    }

    public void StopAndGoToScene(string scene)
    {
        AudioManager.Instance.PlaySFX("TinyButtonPush");
        GameStateManager.Instance.LoadScene(scene);
    }

    public void ShowOptions()
    {
        panel.SetActive(true);
    }

    public void Pause()
    {
        AudioManager.Instance.PlaySFX("TinyButtonPush");
        player.Pause();
        pauseButton.SetActive(false);
        playButton.SetActive(true);
        isPaused = true;
    }

    public void UnPause()
    {
        AudioManager.Instance.PlaySFX("TinyButtonPush");
        player.Play();
        pauseButton.SetActive(true);
        playButton.SetActive(false);
        isPaused = false;
    }



}
