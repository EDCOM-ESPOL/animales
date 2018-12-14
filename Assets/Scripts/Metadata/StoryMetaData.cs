using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoryMetaData : GameMetaData
{

    public string descripcion_historia;     //"bla bla"
    public string duracion;                 //"180"


    public StoryMetaData(string id_registro, string duracion) : base (id_registro)
    {
        tipo = "historia";
        descripcion_historia = "Oscar le explica a los niños la diferencia entre animales domésticos, perjudiciales y salvajes";
        this.duracion = duracion;
    }
}
