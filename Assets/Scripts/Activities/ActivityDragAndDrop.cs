﻿using DigitalRuby.SoundManagerNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivityDragAndDrop : ActivityController {

    public GameObject slotContainer;
    private static int successfulDropsCount = 0;
    protected new readonly int numberOfSubLevels = 5;
    protected int currentSubLevel = 0;


    public override void ResetOrder()
    {

        foreach (Transform child in slotContainer.transform)
        {
            child.GetComponent<Image>().enabled = true;
        }

        successfulDropsCount = 0;
        order = 1;
        //Utility
        orderString = "Utility";
        //a = 3;
        answer = Random.Range(0, 3);
        answerString = System.Enum.GetName(typeof(Animal.utility), answer);


        orderSoundName = "A3 - UbicaAnimalesCasillaCorresponda";
        AudioManager.Instance.PlayVoice(orderSoundName);
    }

    //public void SpawnBlanks(int num)
    //{
    //    //Animal emptyAnimal = new Animal();

    //    List<Animal> emptyAnimalList = AnimalManager.Instance.GetEmptyAnimalListByUtility(num);

    //    //for (int i = 0; i < num; i++)
    //    //{
    //    //    emptyAnimalList.Add(ScriptableObject.CreateInstance<Animal>());
    //    //}

    //    foreach (Animal animal in emptyAnimalList)
    //    {
    //        GameObject newAnimalOption = Instantiate(animalOptionPrefab);
    //        newAnimalOption.GetComponent<AnimalOptionDisplay>().animal = animal;
    //        newAnimalOption.name = animal.name;
    //        newAnimalOption.transform.SetParent(slotContainer.transform);
    //        newAnimalOption.AddComponent<AnimalDropHandler>();
    //        newAnimalOption.transform.localScale = new Vector3(1f, 1f, 1f);
    //    }
    //}


    public override void Spawn()
    {
        //SpawnBlanks(3);

        txtShowOrder.text = "Puntaje: " + score + " / Orden: " + orderString;

        List<Animal> newAnimals = AnimalManager.Instance.GetRandomListByUtility(2);

        int i = 0;
        foreach (Animal animal in newAnimals)
        {
            GameObject newAnimalOption = Instantiate(animalOptionPrefab);
            newAnimalOption.GetComponent<AnimalOptionDisplay>().animal = animal;

            newAnimalOption.name = animal.name;

            newAnimalOption.transform.SetParent(optionContainer.transform.GetChild(i));
            i++;
            newAnimalOption.AddComponent<AnimalDragHandler>();
            newAnimalOption.AddComponent<CanvasGroup>();

            
            newAnimalOption.transform.localScale = new Vector3(1f, 1f, 1f);
            newAnimalOption.transform.localPosition = new Vector3(0f, 0f, 0f);
            
            newAnimalOption.GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 0.0f);

        }

    }


    public override void ResPawn()
    {
        //AnimalManager.Instance.alreadyInUse.Clear();

        foreach (Transform child in slotContainer.transform)
        {
            Destroy(child.transform.GetChild(0).gameObject);
            
        }

        successfulDropsCount = 0;
        
        //subLevelFinished = false;
        Spawn();

        EnableAllButtons();
    }



    public void SuccessfulDrop(){
        successfulDropsCount++;
        print(successfulDropsCount);
        AudioManager.Instance.PlayVoice(RndVoiceGenerator(LoliVoicesCorrect));

        //DisableAllButtons();
        

        if (successfulDropsCount > 5)
        {
            currentSubLevel++;
            StartCoroutine(Win());
        }
    }

    public void WrongDrop(Button button)
    {
        AudioManager.Instance.PlayVoice(RndVoiceGenerator(LoliVoicesWrong));
        errors++;
        StartCoroutine(RedButton(button));
        //StartCoroutine(Wrong());   
    }

    IEnumerator RedButton(Button button)
    {
        ColorBlock colorBlock;
        colorBlock = button.colors;
        colorBlock.normalColor = Color.red;
        colorBlock.highlightedColor = Color.red;

        button.colors = colorBlock;

        yield return new WaitForSeconds(2);

        colorBlock = button.colors;
        colorBlock.normalColor = Color.white;
        colorBlock.highlightedColor = Color.white;

        button.colors = colorBlock;
    }


    public override IEnumerator Win()
    {
        DisableAllButtons();

        AudioManager.Instance.PlayVoice(RndVoiceGenerator(LoliVoicesCorrect));
        score += successfulDropsCount;

        yield return new WaitForSeconds(3);

        if (currentSubLevel >= numberOfSubLevels)
        {
            EndLevel("completado");
        }
        else
        {
            ResetOrder();
            ResPawn();
        }

    }

}

