using DigitalRuby.SoundManagerNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivityController : MonoBehaviour {

    public GameObject activityOptionPrefab;
    public GameObject options;
    public Sprite[] nonLivingSprites;
    public Sprite[] livingSprites;
    public Color[] myColors; //0 = white, 1 = green, 2 = red

    private string activityName;
    private readonly int numberOfSubLevels = 10;
    private int score = 0;
    private int errors = 0;
    private int activity3Flag = 0;
    private bool subLevelFinished = false;
    private ArrayList spritesAlreadyInUse;

    private bool order;  //false = no vivo ... true = vivo
    private string orderSoundName;  //el nombre del audio que dará la orden en esta actividad

    private readonly string[] LoliVoicesCorrect = { "LoliExcelente", "LoliRespondisteMuybien", "LoliBienSigueAsi", "LoliEsoEstuvoGenial" };
    private readonly string[] LoliVoicesWrong = { "LoliDeNuevo", "LoliIntentaloOtraVez", "LoliNoTeDesanimes", "LoliPuedesHacerloMejor" };

    public LevelMetaData levelData;


    // Use this for initialization
    void Start() {
        //AudioManager.Instance.PlayMusic("BGM");
        activityName = GameStateManager.Instance.getCurrentSceneName();

        levelData = new LevelMetaData(SessionManager.Instance.nombre_jugador, "Seres"+ activityName);

        spritesAlreadyInUse = new ArrayList();

        ResetOrder();

        Spawn();

    }

    public void ResetOrder(){

        bool i = System.Convert.ToBoolean(Random.Range(0, 2));
        order = i;

        switch (activityName)
        {
            case "Activity1":
            case "Activity2":
                if (order)
                {
                    orderSoundName = "LoliSeleccionaVivo";
                }
                else orderSoundName = "LoliSeleccionaNoVivo";

                break;
            case "Activity3":
                if (order)
                {
                    orderSoundName = "LoliTodosVivos";
                }
                else orderSoundName = "LoliTodosNoVivos";

                break;
            case "Activity4":
                if (order)
                {
                    orderSoundName = "LoliEsVivo";
                }
                else orderSoundName = "LoliEsNoVivo";

                break;
            default:
                break;
        }


        AudioManager.Instance.PlayVoice(orderSoundName);
    }

    public void DisableAllButtons()
    {
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (Button button in buttons)
        {
            //print(button);
            button.interactable = false;
        }
    }

    public void EnableAllButtons()
    {
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (Button button in buttons)
        {
            //print(button);
            button.interactable = true;
        }
    }

    public void Spawn()
    {
        bool modo;
        int cols;
        int rndCol;
        bool[] p;

        switch (activityName)
        {
            case "Activity1":
                modo = System.Convert.ToBoolean(Random.Range(0,2));
                SpawnBeing(modo);
                SpawnBeing(!modo);

                break;
            case "Activity2":
                cols = 4;

                p = new bool[cols];

                for (int i = 0; i < cols; i++)
                {
                    p[i] = !order;
                }

                rndCol = Random.Range(0, cols);
                p[rndCol] = order;

                for (int i = 0; i < cols; i++)
                {
                    SpawnBeing(p[i]);
                }

                break;
            case "Activity3":
                cols = 3;
                
                p = new bool[cols];

                for (int i = 0; i < cols; i++)
                {
                    p[i] = order;
                }

                rndCol = Random.Range(0, cols);
                p[rndCol] = !order;
                
                for (int i = 0; i < cols; i++)
                {
                    SpawnBeing(p[i]);
                }

                break;
            case "Activity4":
                modo = System.Convert.ToBoolean(Random.Range(0, 2));
                SpawnBeing(modo);

                break;
            default:
                break;
        }
    }


    public void ResPawn()
    {
        spritesAlreadyInUse.Clear();

        foreach (Transform child in options.transform)
        {
            Destroy(child.gameObject);
        }

        if (activityName == "Activity2")
        {
            ColorBlock oldCB;
            GameObject ans = GameObject.Find("Answer");
            oldCB = ans.GetComponent<Button>().colors;
            oldCB.disabledColor = myColors[0];
            ans.GetComponent<Button>().colors = oldCB;

            ans.transform.GetChild(0).GetComponent<Image>().sprite = null;
        }

        activity3Flag = 0;
        subLevelFinished = false;
        Spawn();

        EnableAllButtons();
    }

    
    public bool GetOrder()
    {
        return order;
    }



    public void SpawnBeing(bool isAlive)
    {
        GameObject newActivityOption = Instantiate(activityOptionPrefab);
        newActivityOption.GetComponent<ActivityOption>().isAlive = isAlive;
        ColorBlock oldCB = newActivityOption.GetComponent<Button>().colors;

        if (activityName != "Activity4")
        {
            if (isAlive == order)
            {
                oldCB.pressedColor = myColors[1];
            }
            else
            {
                oldCB.pressedColor = myColors[2];
            }
            
        }
        oldCB.disabledColor = myColors[0];


        newActivityOption.GetComponent<Button>().colors = oldCB;

        Sprite beingSprite;

        Sprite spriteAux;

        if (isAlive)
        {

            spriteAux = livingSprites[Random.Range(0, livingSprites.Length)];

            while (VerifySprite(spriteAux))
            {
                spriteAux = livingSprites[Random.Range(0, livingSprites.Length)];
            }
            spritesAlreadyInUse.Add(spriteAux.name);
            beingSprite = spriteAux;
            
        }
        else
        {
            spriteAux = nonLivingSprites[Random.Range(0, nonLivingSprites.Length)];

            while (VerifySprite(spriteAux))
            {
                spriteAux = nonLivingSprites[Random.Range(0, nonLivingSprites.Length)];
            }

            spritesAlreadyInUse.Add(spriteAux.name);
            beingSprite = spriteAux;
        
        }

        newActivityOption.name = beingSprite.name;
        newActivityOption.transform.GetChild(0).GetComponent<Image>().sprite = beingSprite;


        newActivityOption.transform.SetParent(options.transform);

        if (activityName == "Activity2")
        {
            newActivityOption.transform.GetChild(0).transform.localPosition = new Vector3(0f, 2f, 0f);
            newActivityOption.transform.GetChild(0).transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }


        if (activityName != "Activity4")
        {
            newActivityOption.GetComponent<Button>().onClick.AddListener(delegate { EvaluateOnClick(newActivityOption.GetComponent<Button>()); });
        }

        newActivityOption.transform.localScale = new Vector3(1f, 1f, 1f);
        
    }

    public bool VerifySprite(Sprite rndSprite)
    {

        foreach (string item in spritesAlreadyInUse)
        {
            if (item == rndSprite.name)
            {
                return true;
            }
        }
        
        return false;
    }

    public string RndVoiceGenerator(string[] voices)
    {
        return voices[Random.Range(0, voices.Length)];
    }

    public void EvaluateOnClick(Button buttonClicked)
    {
        //DisableAllButtons();
        ColorBlock oldCB;

        if (buttonClicked.GetComponent<ActivityOption>().isAlive == this.GetOrder())
        {
            oldCB = buttonClicked.colors;
            oldCB.disabledColor = myColors[1];
            buttonClicked.colors = oldCB;

            int[] scores = SessionManager.Instance.getPlayerScore();
            //bool[] levels = SessionManager.Instance.getLevels();

            switch (activityName)
            {
                case "Activity1":
                    

                    scores[0] = scores[0] + 1;
                    score++;
                    Debug.Log(score);
                    subLevelFinished = true;

                    break;
                case "Activity2":
                    oldCB = GameObject.Find("Answer").GetComponent<Button>().colors;
                    oldCB.disabledColor = myColors[1];

                    GameObject.Find("Answer").GetComponent<Button>().colors = oldCB;

                    GameObject.Find("Answer").transform.GetChild(0).GetComponent<Image>().sprite = buttonClicked.transform.GetChild(0).GetComponent<Image>().sprite;

                    scores[1] = scores[1] + 1;
                    score++;
                    subLevelFinished = true;

                    break;
                case "Activity3":
                    oldCB = buttonClicked.colors;

                    if (buttonClicked.GetComponent<ActivityOption>().selected)
                    {
                        oldCB.normalColor = myColors[0];
                        oldCB.highlightedColor = myColors[0];
                        oldCB.disabledColor = myColors[0];
                        buttonClicked.colors = oldCB;
                        buttonClicked.GetComponent<ActivityOption>().selected = false;
                        activity3Flag = activity3Flag - 1;
                    }
                    else
                    {    
                        oldCB.normalColor = myColors[1];
                        oldCB.highlightedColor = myColors[1];
                        oldCB.disabledColor = myColors[1];
                        buttonClicked.colors = oldCB;
                        buttonClicked.GetComponent<ActivityOption>().selected = true;
                        activity3Flag++;
                    }

                    
                    Debug.Log(activity3Flag);

                    if (activity3Flag >= 2)
                    {
                        scores[2] = scores[2] + 1;
                        score++;
                        subLevelFinished = true;
                    }
                    
                    break;

                default:
                    break;
            }


            if (subLevelFinished)
            {
                SessionManager.Instance.setPlayerScore(scores);
                //SessionManager.Instance.setLevels(levels);
                //AudioManager.Instance.PlaySound("Win");
                DisableAllButtons();
                StartCoroutine(Win());
            }

        }
        else
        {
            oldCB = buttonClicked.colors;
            oldCB.disabledColor = myColors[2];
            buttonClicked.colors = oldCB;

            DisableAllButtons();
            StartCoroutine(Wrong());

            
        }


    }


    public void EvaluateOnClickAct4(bool answer)
    {
        

        int[] scores = SessionManager.Instance.getPlayerScore();
        
        Transform option = options.transform.GetChild(0);
        
        ColorBlock oldCB = option.GetComponent<Button>().colors;
        

        bool activityOption = option.GetComponent<ActivityOption>().isAlive;

        if (((order == answer) & activityOption) | ((order != answer) & !activityOption) )
        {
            scores[3] = scores[3] + 1;
            score++;
            
            SessionManager.Instance.setPlayerScore(scores);

            
            oldCB.disabledColor = myColors[1];
            StartCoroutine(Win());
            
        }
        else
        {
            oldCB.disabledColor = myColors[2];
            StartCoroutine(Wrong());
        }

        option.GetComponent<Button>().colors = oldCB;
        DisableAllButtons();


    }



    public void ReplayOrder()
    {
        AudioManager.Instance.PlayVoice(orderSoundName);
    }


    IEnumerator Win()
    {
        //DisableAllButtons();

        AudioManager.Instance.PlayVoice(RndVoiceGenerator(LoliVoicesCorrect));

        yield return new WaitForSeconds(3);

        if (score >= numberOfSubLevels)
        {
            EndLevel("completado");
        }
        else
        {
            ResetOrder();
            ResPawn();
        }
        
    }

    IEnumerator Wrong()
    {
        AudioManager.Instance.PlayVoice(RndVoiceGenerator(LoliVoicesWrong));
        errors++;
        yield return new WaitForSeconds(3);
        EnableAllButtons();

        ColorBlock oldCB;

        foreach (Button button in options.GetComponentsInChildren<Button>())
        {
            button.GetComponent<ActivityOption>().selected = false;

            oldCB = button.colors;
            oldCB.normalColor = myColors[0];
            oldCB.highlightedColor = myColors[0];
            oldCB.disabledColor = myColors[0];
            button.colors = oldCB;
        }

        if (activityName == "Activity3")
        {
            activity3Flag = 0;
        }

    }

    public void EndLevel(string status)
    {
        if (status == "abandonado")
        {
            AudioManager.Instance.PlaySFX("TinyButtonPush");
        }

        levelData.estado = status;
        levelData.fecha_fin = System.DateTime.Now.ToString("yyyy/MM/dd");
        levelData.tiempo_juego = System.Math.Round(Time.timeSinceLevelLoad).ToString();
        levelData.correctas = score.ToString();
        levelData.incorrectas = errors.ToString();
        GameStateManager.Instance.AddJsonToList(JsonUtility.ToJson(levelData));

        GameStateManager.Instance.LoadScene("ActivityHub");
    }
}
