using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivityDragAndDrop : ActivityController {

    public GameObject blankContainer;

    public override void ResetOrder()
    {
        order = 1;
        //Utility
        orderString = "Utility";
        //a = 3;
        answer = Random.Range(0, 3);
        answerString = System.Enum.GetName(typeof(Animal.utility), answer);
    }

    public void SpawnBlanks(int num)
    {
        //Animal emptyAnimal = new Animal();

        List<Animal> emptyAnimalList = AnimalManager.Instance.GetEmptyAnimalListByUtility(num);

        //for (int i = 0; i < num; i++)
        //{
        //    emptyAnimalList.Add(ScriptableObject.CreateInstance<Animal>());
        //}

        foreach (Animal animal in emptyAnimalList)
        {
            GameObject newAnimalOption = Instantiate(animalOptionPrefab);
            newAnimalOption.GetComponent<AnimalOptionDisplay>().animal = animal;
            newAnimalOption.name = animal.name;
            newAnimalOption.transform.SetParent(blankContainer.transform);
            newAnimalOption.AddComponent<AnimalDropHandler>();
            newAnimalOption.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }


    public override void Spawn()
    {
        SpawnBlanks(3);

        txtShowOrder.text = "Puntaje: " + score + " / Orden: " + orderString;

        List<Animal> newAnimals = AnimalManager.Instance.GetRandomListByUtility(3);


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
            


            newAnimalOption.transform.SetParent(optionContainer.transform);
            newAnimalOption.AddComponent<AnimalDragHandler>();
            newAnimalOption.AddComponent<CanvasGroup>();

            //newAnimalOption.GetComponent<Button>().onClick.AddListener(delegate { EvaluateOnClick(newAnimalOption.GetComponent<Button>()); });

            newAnimalOption.transform.localScale = new Vector3(1f, 1f, 1f);
        }

    }

 }
