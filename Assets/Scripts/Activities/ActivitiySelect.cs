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
        //int a = 0;         //Número de opciones en cada clasificación

        ButtonContainers[order].SetActive(true);

        switch (order)
        {
            case 0:
                //Type
                orderString = "Type";
                //a = 2;
                answer = Random.Range(0, 2);
                answerString = System.Enum.GetName(typeof(Animal.type), answer);
                break;
            case 1:
                //Utility
                orderString = "Utility";
                //a = 3;
                answer = Random.Range(0, 3);
                answerString = System.Enum.GetName(typeof(Animal.utility), answer);
                break;
            case 2:
                //SkinCover
                orderString = "SkinCover";
                //a = 3;
                answer = Random.Range(0, 3);
                answerString = System.Enum.GetName(typeof(Animal.skinCover), answer);
                break;
            case 3:
                //Movement
                orderString = "Movement";
                //a = 6;
                answer = Random.Range(0, 6);
                answerString = System.Enum.GetName(typeof(Animal.movement), answer);
                break;
            default:
                break;
        }

        //answer = Random.Range(0, a);

        //AudioManager.Instance.PlayVoice(orderSoundName);
    }


    public override void Spawn()
    {

        txtShowOrder.text = "Puntaje: " + score + " / Orden: "+ orderString + " / Respuesta: " + answerString;

        Animal animal = null;

        print("Order: " + orderString);
        print("Answer: " + answerString);

        switch (order)
        {
            case 0:
                //Type
                //print("Order: Type");
                //print("Answer: " + (Animal.type)answer);
                animal = AnimalManager.Instance.GetRandomAnimalByType(answer, true);
                break;
            case 1:
                //Utility
                //print("Order: Utility");
                //print("Answer: " + (Animal.utility)answer);
                animal = AnimalManager.Instance.GetRandomAnimalByUtility(answer, true);
                break;
            case 2:
                //SkinCover
                //print("Order: SkinCover");
                //print("Answer: " + (Animal.skinCover)answer);
                animal = AnimalManager.Instance.GetRandomAnimalBySkinCover(answer, true);
                break;
            case 3:
                //Movement
                //print("Order: Movement");
                //print("Answer: " + (Animal.movement)answer);
                animal = AnimalManager.Instance.GetRandomAnimalByMovement(answer, true);
                break;
            default:
                break;
        }
        
        
        GameObject newAnimalOption = Instantiate(animalOptionPrefab);
        newAnimalOption.GetComponent<AnimalOptionDisplay>().animal = animal;

        //ColorBlock oldCB = newAnimalOption.GetComponent<Button>().colors;

        
        //if (animal.utilidad.Equals((Animal.utility)answer))
        //{
        //    print("ANIMAL: recibio color 1 (pressed)");

        //    oldCB.pressedColor = Color.green;
        //}
        //else
        //{
        //    print("ANIMAL: recibio color 2 (pressed)");
        //    oldCB.pressedColor = Color.red;
        //}

        //oldCB.disabledColor = Color.white;

        //newAnimalOption.GetComponent<Button>().colors = oldCB;

        newAnimalOption.name = animal.name;     
        newAnimalOption.transform.SetParent(optionContainer.transform);
        newAnimalOption.GetComponent<Button>().interactable = false;
        //newAnimalOption.GetComponent<Button>().onClick.AddListener(delegate { EvaluateOnClick(newAnimalOption.GetComponent<Button>()); });

        newAnimalOption.transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);

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

        Button animalOption = optionContainer.transform.GetChild(0).GetComponent<Button>();
        ColorBlock oldCB;
        oldCB = animalOption.colors;


        print("CLICK");
        //DisableAllButtons();
        

        if (buttonClicked.name.Equals(answerString))
        {
            print("CORRECTO");

            oldCB.disabledColor = Color.green;
            animalOption.colors = oldCB;

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
                        
            oldCB.disabledColor = Color.red;
            animalOption.colors = oldCB;

            DisableAllButtons();
            StartCoroutine(Wrong());

        }
    }
}
