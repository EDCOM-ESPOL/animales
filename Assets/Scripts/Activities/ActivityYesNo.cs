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
    private readonly int numberOfSubLevels = 10;
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

        //byte i = System.Convert.ToByte(Random.Range(0, 4));
        order = 1;
        byte a = 3;         //Número de opciones en cada clasificación

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

        answer = System.Convert.ToByte(Random.Range(0, a));

        //AudioManager.Instance.PlayVoice(orderSoundName);
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
            if(button.name=="BackButton" || button.name=="OrderButton" || button.name=="YesButton" || button.name=="NoButton"){
            button.interactable = true;
            }
           
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

//                oldCB.pressedColor = myColors[2];
  //               oldCB.disabledColor = myColors[0];

        //    if (animal.utilidad.Equals((Animal.utility)answer))
          //  {
            //    print("ANIMAL: recibio color 1 (pressed)");
//
  //              oldCB.pressedColor = myColors[1];
    //        }
      //      else
        //    {
          //      print("ANIMAL: recibio color 2 (pressed)");
//                oldCB.pressedColor = myColors[2];
            //}

//            oldCB.disabledColor = myColors[0];
        
            //newAnimalOption.GetComponent<Button>().colors = oldCB;
            newAnimalOption.GetComponent<Button>().interactable=false;

            newAnimalOption.name = newAnimal.name;
            //newAnimalOption.transform.GetChild(0).GetComponent<Image>().sprite = beingSprite;


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

        //if (activityName == "Activity2")
        //{
        //    ColorBlock oldCB;
        //    GameObject ans = GameObject.Find("Answer");
        //    oldCB = ans.GetComponent<Button>().colors;
        //    oldCB.disabledColor = myColors[0];
        //    ans.GetComponent<Button>().colors = oldCB;

        //    ans.transform.GetChild(0).GetComponent<Image>().sprite = null;
        //}

       
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
        
        
// //Domestic Harmful  Wild
//       switch (answer)
//       {
//           case 0:
//               answerr="Domestic";
//               break;
//           case 1:
//                 answerr="Harmful";
//               break;
//           case 2:
//               answerr="Wild";
//               break;
//       }
        
        //DisableAllButtons();
        
         option = options.transform.GetChild(0);
        
        // ColorBlock oldCB = option.GetComponent<Button>().colors;
        // print(oldCB.pressedColor);
        // oldCB.pressedColor=Color.green;
        // print(oldCB.pressedColor);

        // print(option.GetComponent<Button>().name);
        
        print("CLICK");
        print(answer);
        
        print("Animal de la foto: "+(newAnimal.utilidad).Equals((Animal.utility)answer));
        print("Animal pregunta: "+answer);
       print("Botón presionado: "+buttonClicked.GetComponent<Button>().name);
//       print(buttonClicked.GetComponent<AnimalOptionDisplay>().animal.utilidad);
        print("CLICK");

         
         
       // bool esLoMismo = (newAnimal.utilidad)answer;
        
        bool botonPresionado =buttonClicked.GetComponent<Button>().name.Equals( "YesButton" );

//        print("El tipo de la foto es igual al tipo de la pregunta: "+esLoMismo);
        print("Presionó el botón de que yes "+botonPresionado);

//buttonClicked.GetComponent<AnimalOptionDisplay>().animal.utilidad.Equals((Animal.utility)answer);

        if (((newAnimal.utilidad).Equals((Animal.utility)answer) && buttonClicked.GetComponent<Button>().name.Equals("YesButton")) || (!(newAnimal.utilidad).Equals((Animal.utility)answer) && buttonClicked.GetComponent<Button>().name.Equals("NoButton")))
        {
            print("CORRECTO");
//            oldCB.pressedColor = myColors[1];
            //oldCB.normalColor=Color.blue;
            //oldCB.highlightedColor=Color.blue;
           // oldCB.pressedColor=Color.blue;
           oldCB.disabledColor=Color.green;
           
            
            int[] scores = SessionManager.Instance.getPlayerScore();
            //bool[] levels = SessionManager.Instance.getLevels();

            
                    scores[0] = scores[0] + 1;
                    score++;
                    Debug.Log(score);
                   
                    SessionManager.Instance.setPlayerScore(scores);
                             
//                oldCB.disabledColor = myColors[1];

                
                StartCoroutine(Win());
            
         
            
        }
        else
        {
            print("INCORRECTO");
            print(buttonClicked.GetComponent<Button>().name);
            oldCB.disabledColor=Color.red;
//            oldCB.pressedColor = myColors[2];
            
            //              oldCB.disabledColor = myColors[2];
         
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

        // foreach (Button button in optionContainer.GetComponentsInChildren<Button>())
        // {
        //   //  button.GetComponent<AnimalOptionDisplay>().selected = false;

        //     oldCB = button.colors;
        //     oldCB.normalColor = myColors[0];
        //     oldCB.highlightedColor = myColors[0];
        //     oldCB.disabledColor = myColors[0];
        //     button.colors = oldCB;
        // }

       

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
