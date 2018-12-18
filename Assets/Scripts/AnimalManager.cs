using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager: UnitySingleton<AnimalManager>{

    public Animal[] animalList;
    public List<Animal> alreadyInUse;


    // Use this for initialization
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
                print(a);
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
                print(a);
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
                print(a);
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
                print(a);
                filtered.Add(a);
            }
        }

        return filtered;
    }

    
	
    public Animal GetRandomAnimalByUtility(int utility, bool mode)
    {
        List<Animal> aux = new List<Animal>();
        Animal animal;

        if (mode)
        {
            aux = FilterByUtility(utility);
            //animal = aux[Random.Range(0, aux.Count)];

        }
        else
        {
            byte a = System.Convert.ToByte(Random.Range(0, 3));
            while (a == utility)
            {
                a = System.Convert.ToByte(Random.Range(0, 3));
            }

            aux = FilterByUtility(a);
                                    
        }

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
