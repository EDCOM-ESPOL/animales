using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager: UnitySingleton<AnimalManager>{

    public Animal[] animalList;


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

    // Use this for initialization
    void Start () {
		
	}
	
    public Animal getRandomAnimalByUtility(int u)
    {
        List<Animal> aux = FilterByUtility(u);
        
        Animal anim = aux[Random.Range(0, aux.Count)];
        print(anim.name);
        return anim;

        //0,1,2,3,4 Lenght = / Count = 
    }
	
}
