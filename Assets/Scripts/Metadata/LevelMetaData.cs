using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelMetaData : GameMetaData
{
    public string nombre_nivel;         //":"Seres-Nivel-1",
    public string descripcion_nivel;    //":"Sigue las instrucciones que te da la Loly",
    public string correctas;            //básicamente cuantos subniveles hay
    public string incorrectas;          //
    


	public LevelMetaData(string id_registro, string nombre_nivel) : base(id_registro)
    {
        tipo = "juego";
        this.nombre_nivel = nombre_nivel;
        descripcion_nivel = "Sigue las instrucciones que te da Loly";
        correctas = "0";
        incorrectas = "0";
}

}


