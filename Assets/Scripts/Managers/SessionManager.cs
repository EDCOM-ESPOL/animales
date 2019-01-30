using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

[System.Serializable]
public class SessionManager : UnitySingleton<SessionManager> {

     void Start () {
        Load();
    }
    private const string V = "-";
    public string school = "ES001";
    public string room = "R1";
    
    //public string PlayerAvatarName { get; set; }
    //public string PlayerName { get; set; }
    

    public string tipo = "jugador";
    public string avatar;
    public string nombre_jugador;
    public string nombre_juego = "En mi Entorno Natural";


    private int[] playerScore = { 0, 0, 0, 0 };

    //    {

    //  "tipo":"jugador",

    //  "avatar":"prueba",

    //  "nombre_jugador":"ES001-R1-Marco"

    // }


    //private bool[] levels = { true, true, true, true };


    public string GetSchool(){
        return this.school;
    }
     public string GetRoom(){
        return this.room;
    }

    public void SetSchool(string school){
        this.school = school;
    }

     public void SetRoom(string room){
        this.room = room;
    }



    public int[] getPlayerScore()
    {
       return this.playerScore;
    }

    public void setPlayerScore(int[] score)
    {
       this.playerScore = score;
    }

    public void SetPlayerInfo(string avatar, string nombre_jugador)
    {
        this.avatar = avatar;
        this.nombre_jugador = this.school + V + this.room + V + nombre_jugador;

        GameStateManager.Instance.AddJsonToList(JsonUtility.ToJson(this));
    }

   
	  public void Load(){
        if(File.Exists(Application.persistentDataPath+"/schoolRoomInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath+"/schoolRoomInfo.dat",FileMode.Open);
            SchoolRoomData data = (SchoolRoomData)bf.Deserialize(file);
            file.Close();
            school=data.school;
            room=data.room;
          
        }
    }
  

    

    //public bool[] getLevels()
    //{
    //    return this.levels;
    //}

    //public void setLevels(bool[] levels)
    //{
    //    this.levels = levels;
    //}
}

[Serializable]
    class SchoolRoomData{
        public string school;
        public string room;
    }
