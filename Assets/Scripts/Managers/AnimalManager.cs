using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager: UnitySingleton<AnimalManager>{

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
        foreach (Animal a in animalList) {
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

}
