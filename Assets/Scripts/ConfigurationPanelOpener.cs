using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurationPanelOpener : MonoBehaviour {

	public GameObject Panel;
	public InputField schoolInput, roomInput;
	
	 public void Save(){
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath+"/schoolRoomInfo.dat");
        
        SchoolRoomData data = new SchoolRoomData();
        data.school = schoolInput.text;
        data.room = roomInput.text;

		SessionManager.Instance.SetSchool(data.school);
		SessionManager.Instance.SetRoom(data.room);

        bf.Serialize(file, data);
        file.Close();
		Panel.SetActive(false);

		
    }

	  public void Load(){

		  if(Panel!=null){
			
			Panel.SetActive(true);
		}
        if(File.Exists(Application.persistentDataPath+"/schoolRoomInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath+"/schoolRoomInfo.dat",FileMode.Open);
            SchoolRoomData data = (SchoolRoomData)bf.Deserialize(file);
			schoolInput.text=data.school;
			roomInput.text=data.room;
            file.Close();

			
		

           
        }
    }

	public void Cancel(){

		  if(Panel!=null){
			
			Panel.SetActive(false);
		}

	}



}


