using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DigitalRuby.SoundManagerNamespace;

using UnityEngine.UI;

public class ActivityYesNo : MonoBehaviour {

 //Doméstico, Perjudicial o Salvaje

    
    public GameObject options;

    public GameObject animalOptionPrefab;
    public GameObject optionContainer;

    public Color[] myColors; //0 = white, 1 = green, 2 = red

    private ColorBlock oldCB;
    private Transform option;

    private string activityName;
    private readonly int numberOfSubLevels = 5;
    private int score = 0;
    private int errors = 0;
    
    private bool subLevelFinished = false;
    private ArrayList spritesAlreadyInUse;

    private Animal newAnimal ;

    

    private byte order;             // domestico, perjudicial , salvaje
    private byte answer;            // "es util o no"-> "Es doméstico" "Es perjudicial " "Es salvaje"

    private string orderSoundName;  //el nombre del audio que dará la orden en esta actividad

    private readonly string[] LoliVoicesCorrect = { "LoliExcelente", "LoliRespondisteMuybien", "LoliBienSigueAsi", "LoliEsoEstuvoGenial" };
    private readonly string[] LoliVoicesWrong = { "LoliDeNuevo", "LoliIntentaloOtraVez", "LoliNoTeDesanimes", "LoliPuedesHacerloMejor" };

    public LevelMetaData levelData;

    public Text txtShowOrder;
    private string answerr;

    // Use this for initialization
    void Start()
    {
        //AudioManager.Instance.PlayMusic("BGM");
        activityName = GameStateManager.Instance.getCurrentSceneName();

        levelData = new LevelMetaData(SessionManager.Instance.nombre_jugador, "Animales-" + activityName);

        spritesAlreadyInUse = new ArrayList();

        ResetOrder();

        print(AnimalManager.Instance.FilterByUtility(0).Count);

        Spawn();

    }

    public void ResetOrder()
    {

        order = 1;
        byte a = 3;         //Número de opciones en cada clasificación


        answer = System.Convert.ToByte(Random.Range(0, a));

        switch (answer)
        {
            case 0:
                orderSoundName = "A4 - EsAnimalDomestico";
                break;
            case 1:
                orderSoundName = "A4 - EsAnimalPerjudicial";
                break;
            case 2:
                orderSoundName = "A4 - EsAnimalSalvaje";
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
            //if(button.name=="BackButton" || button.name=="OrderButton" || button.name=="YesButton" || button.name=="NoButton"){
            button.interactable = true;
            //}
           
            //print(button);
            
        }
    }

    public void Spawn()
    {
        EnableAllButtons();
        txtShowOrder.text = "Puntaje: " + score+ " / Es este animal " + ((Animal.utility)answer).ToString() + "?";

         
        

        bool modo = System.Convert.ToBoolean(Random.Range(0, 2));

        
        newAnimal = (AnimalManager.Instance.GetRandomAnimalByUtility(answer, modo));
        


         GameObject newAnimalOption = Instantiate(animalOptionPrefab);
            newAnimalOption.GetComponent<AnimalOptionDisplay>().animal = newAnimal;
            

             oldCB = newAnimalOption.GetComponent<Button>().colors;
             oldCB.disabledColor=Color.white;
             

            
            

            print(newAnimal.utilidad);
            print("RESPUESTA: " + (Animal.utility)answer);

            newAnimalOption.GetComponent<Button>().interactable=false;

            newAnimalOption.name = newAnimal.name;


            newAnimalOption.transform.SetParent(optionContainer.transform);

            newAnimalOption.GetComponent<Button>().onClick.AddListener(delegate { EvaluateOnClick(newAnimalOption.GetComponent<Button>()); });

            newAnimalOption.transform.localScale = new Vector3(1f, 1f, 1f);
        
        
        
    }


    public void ResPawn()
    {
        AnimalManager.Instance.alreadyInUse.Clear();
        
         oldCB.disabledColor=Color.white;
        option.GetComponent<Button>().colors = oldCB;

        foreach (Transform child in optionContainer.transform)
        {
            Destroy(child.gameObject);
        }

       

       
        subLevelFinished = false;
        
        Spawn();
        EnableAllButtons();
        
        
    }


    public byte GetOrder()
    {
        return order;
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

   

    public void EvaluateOnClick(Button buttonClicked )
    {
        DisableAllButtons();
        

        
        option = options.transform.GetChild(0);
    
        
        print("CLICK");
        print(answer);
        
        print("Animal de la foto: "+(newAnimal.utilidad).Equals((Animal.utility)answer));
        print("Animal pregunta: "+answer);
        print("Botón presionado: "+buttonClicked.GetComponent<Button>().name);
        print("CLICK");

         
         
        
        bool botonPresionado =buttonClicked.GetComponent<Button>().name.Equals( "YesButton" );

        print("Presionó el botón de que yes "+botonPresionado);


        if (((newAnimal.utilidad).Equals((Animal.utility)answer) && buttonClicked.GetComponent<Button>().name.Equals("YesButton")) || (!(newAnimal.utilidad).Equals((Animal.utility)answer) && buttonClicked.GetComponent<Button>().name.Equals("NoButton")))
        {
            print("CORRECTO");

           oldCB.disabledColor=Color.green;
           
            
            //int[] scores = SessionManager.Instance.getPlayerScore();
            //bool[] levels = SessionManager.Instance.getLevels();

            
                    //scores[0] = scores[0] + 1;
                    //score++;
                    //Debug.Log(score);
                   
                    //SessionManager.Instance.setPlayerScore(scores);
                             

                
                StartCoroutine(Win());
            
         
            
        }
        else
        {
            print("INCORRECTO");
            print(buttonClicked.GetComponent<Button>().name);
            oldCB.disabledColor=Color.red;

         
            StartCoroutine(Wrong());
            

        answerr="";
        }
        answerr="";
        

        option.GetComponent<Button>().colors = oldCB;
        



    }

    

    
    public void ReplayOrder()
    {
        AudioManager.Instance.PlayVoice(orderSoundName);
    }


    IEnumerator Win()
    {
        DisableAllButtons();

        AudioManager.Instance.PlayVoice(RndVoiceGenerator(LoliVoicesCorrect));
        score++;
        
        yield return new WaitForSeconds(3);
        EnableAllButtons();
        oldCB.disabledColor=Color.white;
        option.GetComponent<Button>().colors = oldCB;

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
        DisableAllButtons();
        AudioManager.Instance.PlayVoice(RndVoiceGenerator(LoliVoicesWrong));
        errors++;
        
        yield return new WaitForSeconds(3);
        
        
        oldCB.disabledColor=Color.white;
        option.GetComponent<Button>().colors = oldCB;
        EnableAllButtons();

  

       

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
