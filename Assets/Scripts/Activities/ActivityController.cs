using DigitalRuby.SoundManagerNamespace;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public abstract class ActivityController : MonoBehaviour {

    public GameObject animalOptionPrefab;
    public GameObject optionContainer;
    public Text txtShowOrder;
    public Color[] myColors; //0 = white, 1 = green, 2 = red
    public LevelMetaData levelData;

    protected string activityName;
    protected readonly int numberOfSubLevels = 10;
    protected int score = 0;
    protected int errors = 0;
    protected int activity3Flag = 0;
    protected bool subLevelFinished = false;
    protected ArrayList spritesAlreadyInUse;

    protected int order;                //0
    protected string orderString;       //Type
    protected int answer;               //0
    protected string answerString;      //Land
    protected string orderSoundName;    //el nombre del audio que dará la orden en esta actividad

    protected readonly string[] LoliVoicesCorrect = { "LoliExcelente", "LoliRespondisteMuybien", "LoliBienSigueAsi", "LoliEsoEstuvoGenial" };
    protected readonly string[] LoliVoicesWrong = { "LoliDeNuevo", "LoliIntentaloOtraVez", "LoliNoTeDesanimes", "LoliPuedesHacerloMejor" };



    // Use this for initialization
    public virtual void Start() {
        //AudioManager.Instance.PlayMusic("BGM");
        activityName = GameStateManager.Instance.getCurrentSceneName();

        levelData = new LevelMetaData(SessionManager.Instance.nombre_jugador, "Animales-" + activityName);

        spritesAlreadyInUse = new ArrayList();

        ResetOrder();

        //print(AnimalManager.Instance.FilterByUtility(0).Count);

        Spawn();

    }

    public virtual void ResetOrder(){

        //byte i = System.Convert.ToByte(Random.Range(0, 4));
        //order = 1;
        //byte a = 3;         //Número de opciones en cada clasificación

        //switch (order)
        //{
        //    case 0:
        //        //Type
        //        a = 2;


        //        break;
        //    case 1:
        //        //Utility
        //        a = 3;
        //        break;
        //    case 2:
        //        //SkinCover
        //        a = 3;
        //        break;
        //    case 3:
        //        //Movement
        //        a = 6;
        //        break;

        //    default:
        //        break;
        //}

        //answer = System.Convert.ToByte(Random.Range(0, a));

        //AudioManager.Instance.PlayVoice(orderSoundName);
    }

    public virtual void DisableAllButtons()
    {
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (Button button in buttons)
        {
            //print(button);
            button.interactable = false;
        }
    }

    public virtual void EnableAllButtons()
    {
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (Button button in buttons)
        {
            //print(button);
            button.interactable = true;
        }
    }

    public virtual void Spawn()
    {
        //bool modo;
        //int cols;
        //int rndCol;
        //bool[] p;

        //switch (activityName)
        //{
        //    case "Activity1":
        //        modo = System.Convert.ToBoolean(Random.Range(0,2));
        //        SpawnBeing(modo);
        //        SpawnBeing(!modo);

        //        break;
        //    case "Activity2":
        //        cols = 4;

        //        p = new bool[cols];

        //        for (int i = 0; i < cols; i++)
        //        {
        //            p[i] = !order;
        //        }

        //        rndCol = Random.Range(0, cols);
        //        p[rndCol] = order;

        //        for (int i = 0; i < cols; i++)
        //        {
        //            SpawnBeing(p[i]);
        //        }

        //        break;
        //    case "Activity3":
        //        cols = 3;
                
        //        p = new bool[cols];

        //        for (int i = 0; i < cols; i++)
        //        {
        //            p[i] = order;
        //        }

        //        rndCol = Random.Range(0, cols);
        //        p[rndCol] = !order;
                
        //        for (int i = 0; i < cols; i++)
        //        {
        //            SpawnBeing(p[i]);
        //        }

        //        break;
        //    case "Activity4":
        //        modo = System.Convert.ToBoolean(Random.Range(0, 2));
        //        SpawnBeing(modo);

        //        break;
        //    default:
        //        break;
        //}
    }


    public virtual void ResPawn()
    {
        //spritesAlreadyInUse.Clear();

        //foreach (Transform child in optionContainer.transform)
        //{
        //    Destroy(child.gameObject);
        //}

        //if (activityName == "Activity2")
        //{
        //    ColorBlock oldCB;
        //    GameObject ans = GameObject.Find("Answer");
        //    oldCB = ans.GetComponent<Button>().colors;
        //    oldCB.disabledColor = myColors[0];
        //    ans.GetComponent<Button>().colors = oldCB;

        //    ans.transform.GetChild(0).GetComponent<Image>().sprite = null;
        //}

        //activity3Flag = 0;
        //subLevelFinished = false;
        //Spawn();

        //EnableAllButtons();
    }

    
    public virtual int GetOrder()
    {
        return order;
    }

    
    public virtual string RndVoiceGenerator(string[] voices)
    {
        return voices[Random.Range(0, voices.Length)];
    }

    public virtual void EvaluateOnClick(Button buttonClicked)
    {
        //DisableAllButtons();
        //ColorBlock oldCB;

        //if (buttonClicked.GetComponent<ActivityOption>().isAlive == this.GetOrder())
        //{
        //    oldCB = buttonClicked.colors;
        //    oldCB.disabledColor = myColors[1];
        //    buttonClicked.colors = oldCB;

        //    int[] scores = SessionManager.Instance.getPlayerScore();
        //    //bool[] levels = SessionManager.Instance.getLevels();

        //    switch (activityName)
        //    {
        //        case "Activity1":
                    

        //            scores[0] = scores[0] + 1;
        //            score++;
        //            Debug.Log(score);
        //            subLevelFinished = true;

        //            break;
        //        case "Activity2":
        //            oldCB = GameObject.Find("Answer").GetComponent<Button>().colors;
        //            oldCB.disabledColor = myColors[1];

        //            GameObject.Find("Answer").GetComponent<Button>().colors = oldCB;

        //            GameObject.Find("Answer").transform.GetChild(0).GetComponent<Image>().sprite = buttonClicked.transform.GetChild(0).GetComponent<Image>().sprite;

        //            scores[1] = scores[1] + 1;
        //            score++;
        //            subLevelFinished = true;

        //            break;
        //        case "Activity3":
        //            oldCB = buttonClicked.colors;

        //            if (buttonClicked.GetComponent<ActivityOption>().selected)
        //            {
        //                oldCB.normalColor = myColors[0];
        //                oldCB.highlightedColor = myColors[0];
        //                oldCB.disabledColor = myColors[0];
        //                buttonClicked.colors = oldCB;
        //                buttonClicked.GetComponent<ActivityOption>().selected = false;
        //                activity3Flag = activity3Flag - 1;
        //            }
        //            else
        //            {    
        //                oldCB.normalColor = myColors[1];
        //                oldCB.highlightedColor = myColors[1];
        //                oldCB.disabledColor = myColors[1];
        //                buttonClicked.colors = oldCB;
        //                buttonClicked.GetComponent<ActivityOption>().selected = true;
        //                activity3Flag++;
        //            }

                    
        //            Debug.Log(activity3Flag);

        //            if (activity3Flag >= 2)
        //            {
        //                scores[2] = scores[2] + 1;
        //                score++;
        //                subLevelFinished = true;
        //            }
                    
        //            break;

        //        default:
        //            break;
        //    }


        //    if (subLevelFinished)
        //    {
        //        SessionManager.Instance.setPlayerScore(scores);
        //        //SessionManager.Instance.setLevels(levels);
        //        //AudioManager.Instance.PlaySound("Win");
        //        DisableAllButtons();
        //        StartCoroutine(Win());
        //    }

        //}
        //else
        //{
        //    oldCB = buttonClicked.colors;
        //    oldCB.disabledColor = myColors[2];
        //    buttonClicked.colors = oldCB;

        //    DisableAllButtons();
        //    StartCoroutine(Wrong());

            
        //}


    }


    //public virtual void EvaluateOnClickAct4(bool answer)
    //{
        

    //    int[] scores = SessionManager.Instance.getPlayerScore();
        
    //    Transform option = options.transform.GetChild(0);
        
    //    ColorBlock oldCB = option.GetComponent<Button>().colors;
        

    //    bool activityOption = option.GetComponent<ActivityOption>().isAlive;

    //    if (((order == answer) & activityOption) | ((order != answer) & !activityOption) )
    //    {
    //        scores[3] = scores[3] + 1;
    //        score++;
            
    //        SessionManager.Instance.setPlayerScore(scores);

            
    //        oldCB.disabledColor = myColors[1];
    //        StartCoroutine(Win());
            
    //    }
    //    else
    //    {
    //        oldCB.disabledColor = myColors[2];
    //        StartCoroutine(Wrong());
    //    }

    //    option.GetComponent<Button>().colors = oldCB;
    //    DisableAllButtons();


    //}



    public virtual void ReplayOrder()
    {
        AudioManager.Instance.PlayVoice(orderSoundName);
    }


    public virtual IEnumerator Win()
    {
        //DisableAllButtons();

        AudioManager.Instance.PlayVoice(RndVoiceGenerator(LoliVoicesCorrect));
        score++;

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

    public virtual IEnumerator Wrong()
    {
        AudioManager.Instance.PlayVoice(RndVoiceGenerator(LoliVoicesWrong));
        errors++;
        yield return new WaitForSeconds(3);
        EnableAllButtons();

        ColorBlock oldCB;

        foreach (Button button in optionContainer.GetComponentsInChildren<Button>())
        {
            //button.GetComponent<AnimalOptionDisplay>().selected = false;

            oldCB = button.colors;
            oldCB.normalColor = Color.white;
            oldCB.highlightedColor = Color.white;
            oldCB.disabledColor = Color.white;
            button.colors = oldCB;
        }

        //if (activityName == "Activity3")
        //{
        //    activity3Flag = 0;
        //}

    }

    public virtual void EndLevel(string status)
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
