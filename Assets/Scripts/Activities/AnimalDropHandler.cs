using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnimalDropHandler : MonoBehaviour, IDropHandler {

    public void OnDrop(PointerEventData eventData)
    {
        Animal draggedAnimal = AnimalDragHandler.itemBeingDragged.GetComponent<AnimalOptionDisplay>().animal;
        Animal slotAnimal = gameObject.GetComponent<AnimalOptionDisplay>().animal;
        print("Dragged: " + draggedAnimal.utilidad);
        print("Dropped to: " + slotAnimal.utilidad);

        if (draggedAnimal.utilidad.Equals(slotAnimal.utilidad))
        {
            print("CORRECT");
            gameObject.name = draggedAnimal.name;
            gameObject.transform.GetChild(0).GetComponent<Image>().sprite = draggedAnimal.sprite;
            gameObject.GetComponent<AnimalOptionDisplay>().animal = draggedAnimal;

            GameObject.Destroy(AnimalDragHandler.itemBeingDragged);
            print(GameObject.Find("ActivityController").GetComponent<ActivityDragAndDrop>().optionContainer.transform.GetChild(0));

            //if (GetComponent<ActivityDragAndDrop>().optionContainer.transform.childCount == 0)
            //{
            //    GetComponent<ActivityDragAndDrop>().Win();
            //}

        }
    }

    
}
