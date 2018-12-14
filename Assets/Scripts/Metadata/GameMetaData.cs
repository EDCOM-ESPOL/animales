using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class GameMetaData {

    public string tipo;
    public string id_registro;

    public string nombre_juego;
    public string descripcion_juego;
    public string nombre_capitulo;
    public string descripcion_capitulo;
    public string nombre_historia;

    public string fecha_inicio;         //":"2018/01/31",
    public string fecha_fin;            //":"2018/01/31",

    public string tiempo_juego;         //en segundos
    public string estado;               //completado o abandonado


    public GameMetaData(string id_registro)
    {
        this.id_registro = id_registro;

        nombre_juego = SessionManager.Instance.nombre_juego;
        descripcion_juego = "En mi Entorno Natural, parte de 4 Aventuras Interactivas.";
        nombre_capitulo = "Animales";
        descripcion_capitulo = "En el capítulo 'Animales', los niños aprenderán la diferencia entre un animal doméstico, perjudicial y uno salvaje. Además de varias maneras de clasificar a los animales.";
        nombre_historia = "Historia-Animales";

        fecha_inicio = System.DateTime.Now.ToString("yyyy/MM/dd");

        estado = "abandonado";
    }

}
