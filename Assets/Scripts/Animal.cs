using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Animal", menuName ="Animal")]
public class Animal : ScriptableObject{

    public string nombre;
    public Sprite sprite;

    public enum utility {Domestic,Harmful,Wild};
    public utility utilidad;

    public enum type {Land, Sea};
    public type tipo;

    public enum skinCover {Fur, Scale, Feather };
    public skinCover cubiertos;

    public enum movement {Climb, Crawl, Fly, Jump, Swim, Walk };
    public movement movimiento;

    
    

}
