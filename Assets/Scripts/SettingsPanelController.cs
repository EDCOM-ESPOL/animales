using DigitalRuby.SoundManagerNamespace;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelController : MonoBehaviour {

	public GameObject panel;
	public InputField schoolInput, roomInput;
    private int clickCount = 0;


    
    public void Save(){
        AudioManager.Instance.PlaySFX("TinyButtonPush");


        GameStateManager.Instance.SaveGlobalSettings(new GlobalSettings(schoolInput.text.ToUpper(), roomInput.text.ToUpper()));

		panel.SetActive(false);
        clickCount = 0;


    }

	public void Load(){

        clickCount++;

        if (clickCount > 9)
        {
            AudioManager.Instance.PlaySFX("TinyButtonPush");


            if (panel != null)
            {
                panel.SetActive(true);
            }


            schoolInput.text = GameStateManager.Instance.GetGlobalSettings().school;
            roomInput.text = GameStateManager.Instance.GetGlobalSettings().room;
        }


    }

	public void Cancel(){
        AudioManager.Instance.PlaySFX("TinyButtonPush");


        if (panel != null){
			
			panel.SetActive(false);
		}

        clickCount = 0;
	}



}


