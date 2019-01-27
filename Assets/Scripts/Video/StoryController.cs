using DigitalRuby.SoundManagerNamespace;
using UnityEngine;
using UnityEngine.UI;

public class StoryController : VideoController {

    public Button backButton;
    public Button replayButton;
    public GameObject videoControlsPanel;

    public StoryMetaData storyData;

    private int skipsCount = 0;
    private readonly float videoTotalLength = 175.0f;
    

    // Use this for initialization
    void Start () {
        storyData = new StoryMetaData(SessionManager.Instance.nombre_jugador, System.Math.Round(player.clip.length).ToString());
        SetAlpha(0.0f);
        SetButtonsInteractable(false);
    }
	
	// Update is called once per frame
	void Update () {

        if (player.time >= (videoTotalLength - 5.0))
        {
            panel.SetActive(false);
            if (skipsCount == 0)
            {
                storyData.estado = "completado";
            }else storyData.estado = "abandonado";

            if (player.time >= videoTotalLength)
            {
                SendJSONAndGoToScene("EntornoNaturalHub");
            }
        }
            

    }


    public void Retroceder()
    {
        AudioManager.Instance.PlaySFX("TinyButtonPush");
        if (player.time <= 15.0f)
        {
            player.time = 0.0;
        }
        else
        {
            player.time = player.time - 15.0f;
        }

        skipsCount--;
        if (skipsCount < 0) {
            skipsCount = 0;
        }

        Debug.Log("SKIPSCOUNT = " + skipsCount);
        
    }


    public void Avanzar()
    {
        AudioManager.Instance.PlaySFX("TinyButtonPush");
        player.time = player.time + 15.0f;
        skipsCount++;

        Debug.Log("SKIPSCOUNT = " + skipsCount);
    }

    public void SendJSONAndGoToScene(string sceneName)
    {
        storyData.fecha_fin = System.DateTime.Now.ToString("yyyy/MM/dd");
        storyData.tiempo_juego = System.Math.Round(Time.timeSinceLevelLoad).ToString();
        GameStateManager.Instance.AddJsonToList(JsonUtility.ToJson(storyData));
        AudioManager.Instance.PlaySFX("TinyButtonPush");
        base.StopAndGoToScene(sceneName);
    }

    public float GetAlpha()
    {
        return panel.GetComponent<CanvasGroup>().alpha;
    }

    public void SetAlpha(float alpha)
    {
        panel.GetComponent<CanvasGroup>().alpha = alpha;
    }

    public void TooglePanel()
    {
        if (Mathf.Approximately(GetAlpha(), 0.0f))
        {
            SetAlpha(1.0f);
            SetButtonsInteractable(true);
        }
        else
        {
            SetAlpha(0.0f);
            SetButtonsInteractable(false);
        }
    }

    private void SetButtonsInteractable(bool v)
    {
        backButton.gameObject.SetActive(v);
        replayButton.gameObject.SetActive(v);
        videoControlsPanel.SetActive(v);
    }
}
