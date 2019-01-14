using DigitalRuby.SoundManagerNamespace;
using System.Collections.Generic;
using UnityEngine;


public class ActivityDragAndDrop : ActivityController {

    public GameObject slotContainer;
    private static int successfulDropsCount = 0;

  
    public override void ResetOrder()
    {
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

        List<Animal> newAnimals = AnimalManager.Instance.GetRandomListByUtility(3);

        int i = 0;
        foreach (Animal animal in newAnimals)
        {
            GameObject newAnimalOption = Instantiate(animalOptionPrefab);
            newAnimalOption.GetComponent<AnimalOptionDisplay>().animal = animal;

            //ColorBlock oldCB = newAnimalOption.GetComponent<Button>().colors;

            //print(animal.utilidad);
            //print("RESPUESTA: " + (Animal.utility)answer);


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
        if (successfulDropsCount > 8)
        {
            DisableAllButtons();
            StartCoroutine(Win());
        }
    }

}

