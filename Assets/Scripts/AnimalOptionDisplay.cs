using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalOptionDisplay : MonoBehaviour {

    public Animal animal;

    public Image image;

    public bool selected;

    // Use this for initialization
    void Start () {
        image.sprite = animal.sprite;
	}
	
}
