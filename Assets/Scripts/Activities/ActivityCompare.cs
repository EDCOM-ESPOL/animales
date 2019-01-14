using DigitalRuby.SoundManagerNamespace;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivityCompare : ActivityController {

        
    public override void ResetOrder()
    {
        order = 1;
        int a = 3;

        answer = Random.Range(0, a);

        switch (answer)
        {
            case 0:
                orderSoundName = "A1 - CualAnimalUtil";
                break;
            case 1:
                orderSoundName = "A1 - CualAnimalPerjudicial";
                break;
            case 2:
                orderSoundName = "A1 - CualAnimalViveSelva";
                break;
            default:
                break;
        }

        orderString = "Utilidad";
        answerString = System.Enum.GetName(typeof(Animal.utility), answer);
        AudioManager.Instance.PlayVoice(orderSoundName);

    }
       

    public override void Spawn()
    {
        //txtShowOrder.text = "Puntaje: " + score+ " / Orden: Utilidad" + " / Respuesta: " + ((Animal.utility)answer).ToString();
        txtShowOrder.text = "Puntaje: " + score + " / Orden: " + orderString + " / Respuesta: " + answerString;

        List<Animal> newAnimals = new List<Animal>();
        

        bool modo = System.Convert.ToBoolean(Random.Range(0, 2));

        
        newAnimals.Add(AnimalManager.Instance.GetRandomAnimalByUtility(answer, modo));
        newAnimals.Add(AnimalManager.Instance.GetRandomAnimalByUtility(answer, !modo));


        foreach (Animal animal in newAnimals)
        {
            GameObject newAnimalOption = Instantiate(animalOptionPrefab);
            newAnimalOption.GetComponent<AnimalOptionDisplay>().animal = animal;

            ColorBlock oldCB = newAnimalOption.GetComponent<Button>().colors;

            print(animal.utilidad);
            print("RESPUESTA: " + (Animal.utility)answer);


            if (animal.utilidad.Equals((Animal.utility)answer))
            {
                print("ANIMAL: recibio color 1 (pressed)");

                oldCB.pressedColor = myColors[1];
            }
            else
            {
                print("ANIMAL: recibio color 2 (pressed)");
                oldCB.pressedColor = myColors[2];
            }

            oldCB.disabledColor = myColors[0];

            newAnimalOption.GetComponent<Button>().colors = oldCB;

            newAnimalOption.name = animal.name;
            //newAnimalOption.transform.GetChild(0).GetComponent<Image>().sprite = beingSprite;


            newAnimalOption.transform.SetParent(optionContainer.transform);

            newAnimalOption.GetComponent<Button>().onClick.AddListener(delegate { EvaluateOnClick(newAnimalOption.GetComponent<Button>()); });

            newAnimalOption.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        
        
    }


    public override void ResPawn()
    {
        AnimalManager.Instance.alreadyInUse.Clear();

        foreach (Transform child in optionContainer.transform)
        {
            Destroy(child.gameObject);
        }

        
        activity3Flag = 0;
        subLevelFinished = false;
        Spawn();

        EnableAllButtons();
    }



    public override void EvaluateOnClick(Button buttonClicked)
    {
        print("CLICK");
        //DisableAllButtons();
        ColorBlock oldCB;

        if (buttonClicked.GetComponent<AnimalOptionDisplay>().animal.utilidad.Equals((Animal.utility)answer))
        {
            print("CORRECTO");

            oldCB = buttonClicked.colors;
            oldCB.disabledColor = myColors[1];
            buttonClicked.colors = oldCB;
                        
            //score++;
            //Debug.Log(score);
            subLevelFinished = true;
                        

            //AudioManager.Instance.PlaySound("Win");
            DisableAllButtons();
            StartCoroutine(Win());
            

        }
        else
        {
            print("INCORRECTO");

            oldCB = buttonClicked.colors;
            oldCB.disabledColor = myColors[2];
            buttonClicked.colors = oldCB;

            DisableAllButtons();
            StartCoroutine(Wrong());

        }
    }

    

    
}
