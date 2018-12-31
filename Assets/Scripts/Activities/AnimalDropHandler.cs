using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnimalDropHandler : MonoBehaviour, IDropHandler {

    public Animal.utility utilidad;
    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Animal draggedAnimal = AnimalDragHandler.itemBeingDragged.GetComponent<AnimalOptionDisplay>().animal;
        
        if (draggedAnimal.utilidad.Equals(utilidad) && !item)
        {
            
            AnimalDragHandler.itemBeingDragged.transform.SetParent(transform);
            Destroy(item.GetComponent<AnimalDragHandler>());

            RectTransform rectTransform = AnimalDragHandler.itemBeingDragged.GetComponent<RectTransform>();
            
            rectTransform.localPosition = new Vector3(0.0f ,0.0f ,0.0f);

            GameObject.Find("ActivityController").GetComponent<ActivityDragAndDrop>().SuccessfulDrop();
            
        }
    }

    
}
