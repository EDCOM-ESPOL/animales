using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

[System.Serializable]
public class SessionManager : UnitySingleton<SessionManager> {

    public string tipo = "jugador";
    public string avatar;
    public string nombre_jugador;
    public string nombre_juego = "En mi Entorno Natural";
    private string V;


    public void SetPlayerInfo(string avatar, string nombre_jugador)
    {
        string school = GameStateManager.Instance.GetGlobalSettings().school;
        string room = GameStateManager.Instance.GetGlobalSettings().room;

        if (school == "" || room == "")
        {
            V = "";
        }
        else
        {
            V = "-";
        }

        this.avatar = avatar;
        this.nombre_jugador = school + V + room + V + nombre_jugador;

        GameStateManager.Instance.AddJsonToList(JsonUtility.ToJson(this));
    }
	
}

[Serializable]
    public class GlobalSettings{
        public string school;
        public string room;
    
        public GlobalSettings()
        {
            school = "";
            room = "";
        }

        public GlobalSettings(string school, string room)
        {
            this.school = school;
            this.room = room;
        }

}
