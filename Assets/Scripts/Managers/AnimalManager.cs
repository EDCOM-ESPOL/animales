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


    public Animal GetRandomAnimalByType(int type, bool mode)
    {
        List<Animal> aux = new List<Animal>();
        Animal animal;

        if (mode)
        {
            aux = FilterByType(type);
        }
        else
        {
            byte a;

            do
            {
                a = System.Convert.ToByte(Random.Range(0, 2));
            } while (a == type);

            aux = FilterByType(a);            
        }

        animal = aux[Random.Range(0, aux.Count)];
        return animal;
    }


    public Animal GetRandomAnimalByUtility(int utility, bool mode)
    {
        List<Animal> aux = new List<Animal>();
        Animal animal;

        if (mode)
        {
            aux = FilterByUtility(utility);
        }
        else
        {
            byte a;
            do
            {
                a = System.Convert.ToByte(Random.Range(0, 3));
            } while (a == utility);
            
            aux = FilterByUtility(a);
                                    
        }

        animal = aux[Random.Range(0, aux.Count)];
        return animal;
    }


    public Animal GetRandomAnimalBySkinCover(int skinCover, bool mode)
    {
        List<Animal> aux = new List<Animal>();
        Animal animal;

        if (mode)
        {
            aux = FilterByType(skinCover);
        }
        else
        {
            byte a;

            do
            {
                a = System.Convert.ToByte(Random.Range(0, 3));
            } while (a == skinCover);

            aux = FilterByType(a);
        }

        animal = aux[Random.Range(0, aux.Count)];
        return animal;
    }

    public Animal GetRandomAnimalByMovement(int move, bool mode)
    {
        List<Animal> aux = new List<Animal>();
        Animal animal;

        if (mode)
        {
            aux = FilterByType(move);
        }
        else
        {
            byte a;

            do
            {
                a = System.Convert.ToByte(Random.Range(0, 6));
            } while (a == move);

            aux = FilterByType(a);
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
