using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : UnitySingleton<AnimalManager>
{

    public Animal[] animalList;
    public List<Animal> alreadyInUse;


    void Start()
    {
        alreadyInUse = new List<Animal>();
    }


    public List<Animal> FilterByType(int t)
    {
        List<Animal> filtered = new List<Animal>();
        foreach (Animal a in animalList)
        {
            if (a.tipo == (Animal.type)t)
            {
                filtered.Add(a);
            }
        }
        return filtered;
    }


    public List<Animal> FilterByUtility(int u)
    {
        List<Animal> filtered = new List<Animal>();
        foreach (Animal a in animalList)
        {
            if (a.utilidad == (Animal.utility)u)
            {
                filtered.Add(a);
            }
        }
        return filtered;
    }


    public List<Animal> FilterBySkinCover(int sc)
    {
        List<Animal> filtered = new List<Animal>();
        foreach (Animal a in animalList)
        {
            if (a.cubiertos == (Animal.skinCover)sc)
            {
                filtered.Add(a);
            }
        }
        return filtered;
    }


    public List<Animal> FilterByMovement(int m)
    {
        List<Animal> filtered = new List<Animal>();
        foreach (Animal a in animalList)
        {
            if (a.movimiento == (Animal.movement)m)
            {
                filtered.Add(a);
            }
        }
        return filtered;
    }


    public Animal GetRandomAnimalByType(int answer, bool mode)
    {
        List<Animal> aux = new List<Animal>();
        Animal animal;

        if (mode)
        {
            aux = FilterByType(answer);
        }
        else
        {
            int a;

            do
            {
                a = Random.Range(0, 2);
                //print("a: " + a);
                //print("answer: " + answer);
            } while (a == answer);

            aux = FilterByType(a);
        }
        animal = aux[Random.Range(0, aux.Count)];
        return animal;
    }


    public Animal GetRandomAnimalByUtility(int answer, bool mode)
    {
        List<Animal> aux = new List<Animal>();
        Animal animal;

        if (mode)
        {
            aux = FilterByUtility(answer);
        }
        else
        {
            int a;
            do
            {
                a = Random.Range(0, 3);
                //print("a: " + a);
                //print("answer: " + answer);
            } while (a == answer);

            aux = FilterByUtility(a);

        }
        animal = aux[Random.Range(0, aux.Count)];
        return animal;
    }


    public Animal GetRandomAnimalBySkinCover(int answer, bool mode)
    {
        List<Animal> aux = new List<Animal>();
        Animal animal;

        if (mode)
        {
            aux = FilterBySkinCover(answer);
        }
        else
        {
            int a;
            do
            {
                a = Random.Range(0, 3);
                //print("a: " + a);
                //print("answer: " + answer);
            } while (a == answer);

            aux = FilterByType(a);
        }
        animal = aux[Random.Range(0, aux.Count)];
        return animal;
    }


    public Animal GetRandomAnimalByMovement(int answer, bool mode)
    {
        List<Animal> aux = new List<Animal>();
        Animal animal;

        if (mode)
        {
            aux = FilterByMovement(answer);
        }
        else
        {
            int a;

            do
            {
                a = Random.Range(0, 6);
                //print("a: " + a);
                //print("answer: " + answer);
            } while (a == answer);

            aux = FilterByType(a);
        }
        //print("AUX.COUNT=" + aux.Count);
        animal = aux[Random.Range(0, aux.Count)];
        return animal;
    }


    public bool VerifyAnimal(Animal animal)
    {

        foreach (Animal item in alreadyInUse)
        {
            if (item == animal)
            {
                return true;
            }
        }

        return false;
    }


    public List<Animal> GetEmptyAnimalListByUtility(int num)
    {
        Animal emptyAnimal;

        List<Animal> emptyAnimalList = new List<Animal>();

        for (int i = 0; i < num; i++)
        {
            emptyAnimal = ScriptableObject.CreateInstance<Animal>();
            emptyAnimal.name = "Empty_Animal";
            emptyAnimal.utilidad = (Animal.utility)i;
            emptyAnimalList.Add(emptyAnimal);
            emptyAnimalList.Add(emptyAnimal);
            emptyAnimalList.Add(emptyAnimal);
        }

        foreach (Animal item in emptyAnimalList)
        {
            print(item.utilidad);
        }

        return emptyAnimalList;

    }


    public List<Animal> GetRandomListByUtility(int num)
    {
        List<Animal> domesticList = FilterByUtility(0);
        List<Animal> harmfulList = FilterByUtility(1);
        List<Animal> wildList = FilterByUtility(2);

        List<Animal> rndList = new List<Animal>();

        List<int> listNumbersDomestic = new List<int>();
        int numberDomestic;

        List<int> listNumbersH = new List<int>();
        int numberH;

        List<int> listNumbersW = new List<int>();
        int numberW;

        for (int i = 0; i < num; i++)
        {

            do
            {
                numberDomestic = Random.Range(0, domesticList.Count);
            } while (listNumbersDomestic.Contains(numberDomestic));
            listNumbersDomestic.Add(numberDomestic);
            rndList.Add(domesticList[numberDomestic]);
        
     

            do
            {
                numberH = Random.Range(0, harmfulList.Count);
            } while (listNumbersH.Contains(numberH));
            listNumbersH.Add(numberH);
            rndList.Add(harmfulList[numberH]);
        
            do
            {
                numberW = Random.Range(0, wildList.Count);
            } while (listNumbersW.Contains(numberW));
            listNumbersW.Add(numberW);
            rndList.Add(wildList[numberW]);

        }



        return Fisher_Yates_CardDeck_Shuffle(rndList);

    }


    public static List<Animal> Fisher_Yates_CardDeck_Shuffle(List<Animal> aList)
    {

        System.Random _random = new System.Random();

        Animal myGO;

        int n = aList.Count;
        for (int i = 0; i < n; i++)
        {
            // NextDouble returns a random number between 0 and 1.
            // ... It is equivalent to Math.random() in Java.
            int r = i + (int)(_random.NextDouble() * (n - i));
            myGO = aList[r];
            aList[r] = aList[i];
            aList[i] = myGO;
        }

        return aList;
    }


}
