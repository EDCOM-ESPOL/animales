using DigitalRuby.SoundManagerNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivitiySelect : ActivityController {

    public GameObject[] ButtonContainers;
    
    public override void ResetOrder()
    {
        foreach (GameObject container in ButtonContainers)
        {
            container.SetActive(false);
        }

        order = Random.Range(0, 4);
        //order = 1;
        int a = 0;         //Número de opciones en cada clasificación

        ButtonContainers[order].SetActive(true);

        switch (order)
        {
            case 0:
                //Type
                a = 2;
                break;
            case 1:
                //Utility
                a = 3;
                break;
            case 2:
                //SkinCover
                a = 3;
                break;
            case 3:
                //Movement
                a = 6;
                break;
            default:
                break;
        }

        answer = Random.Range(0, a);

        //AudioManager.Instance.PlayVoice(orderSoundName);
    }


    public override void Spawn()
    {

        txtShowOrder.text = "Puntaje: " + score + " / Orden: "+ order + " / Respuesta: " + answer;

        Animal animal = null;


        //bool modo = System.Convert.ToBoolean(Random.Range(0, 2));


        switch (order)
        {
            case 0:
                //Type
                animal = AnimalManager.Instance.GetRandomAnimalByType(answer, true);
                break;
            case 1:
                //Utility
                animal = AnimalManager.Instance.GetRandomAnimalByUtility(answer, true);
                break;
            case 2:
                //SkinCover
                animal = AnimalManager.Instance.GetRandomAnimalBySkinCover(answer, true);
                break;
            case 3:
                //Movement
                animal = AnimalManager.Instance.GetRandomAnimalByMovement(answer, true);
                break;
            default:
                break;
        }

        //animal = AnimalManager.Instance.GetRandomAnimalByUtility(answer, true);


        
        GameObject newAnimalOption = Instantiate(animalOptionPrefab);
        newAnimalOption.GetComponent<AnimalOptionDisplay>().animal = animal;

        ColorBlock oldCB = newAnimalOption.GetComponent<Button>().colors;

        print(animal.utilidad);
        print("RESPUESTA: " + (Animal.utility)answer);


        if (animal.utilidad.Equals((Animal.utility)answer))
        {
            print("ANIMAL: recibio color 1 (pressed)");

            oldCB.pressedColor = Color.green;
        }
        else
        {
            print("ANIMAL: recibio color 2 (pressed)");
            oldCB.pressedColor = Color.red;
        }

        oldCB.disabledColor = Color.white;

        newAnimalOption.GetComponent<Button>().colors = oldCB;

        newAnimalOption.name = animal.name;
        //newAnimalOption.transform.GetChild(0).GetComponent<Image>().sprite = beingSprite;


        
        newAnimalOption.transform.SetParent(optionContainer.transform);

        newAnimalOption.GetComponent<Button>().interactable = false;
        //newAnimalOption.GetComponent<Button>().onClick.AddListener(delegate { EvaluateOnClick(newAnimalOption.GetComponent<Button>()); });

        newAnimalOption.transform.localScale = new Vector3(1f, 1f, 1f);
        


    }

    public override void ResPawn()
    {
        //AnimalManager.Instance.alreadyInUse.Clear();

        foreach (Transform child in optionContainer.transform)
        {
            Destroy(child.gameObject);
        }


        //activity3Flag = 0;
        //subLevelFinished = false;
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

            score++;
            Debug.Log(score);
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
