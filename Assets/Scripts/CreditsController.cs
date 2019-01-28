using DigitalRuby.SoundManagerNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour {

    public float textSpeed;
    public GameObject container;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        container.transform.localPosition = new Vector3(container.transform.localPosition.x, container.transform.localPosition.y + textSpeed, 0f);

        if (container.transform.localPosition.y >= 3200f)
        {
            container.transform.localPosition = new Vector3(container.transform.localPosition.x, -1100f, 0f);
        }
    }

    public void ExitApp()
    {
        AudioManager.Instance.PlaySFX("TinyButtonPush");
        Application.Quit();
    }

    public void GoBack()
    {
        AudioManager.Instance.PlaySFX("TinyButtonPush");
        GameStateManager.Instance.LoadScene(GameStateManager.Instance.creditsScreenCaller);
    }
}
