using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNdrop : MonoBehaviour {

  [SerializeField]
    private Transform objectPlace;
    private Vector2 initialPosition;

    private Vector2 mousePosition;
    private float deltaX, deltaY;
    public static bool locked;
	// Use this for initialization
	void Start () {
		initialPosition = transform.position;
	}

    private void OnMouseDown(){
        if(!locked){
            deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;

        }
    }

    

    private  void OnMouseDrag()
    {

        if(!locked){
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x - deltaX, mousePosition.y - deltaY);
            
        }

        
    }

    private void OnMouseUp()
    {
         if(Mathf.Abs(transform.position.x - objectPlace.position.x) <= 0.5f && 
                    Mathf.Abs(transform.position.y - objectPlace.position.y) <= 0.5f){
                    transform.position = new Vector2(objectPlace.position.x, objectPlace.position.y);
                    locked = true;
                    print("Dejó de tocar");
                    }
                    else{
                        transform.position = new Vector2 (initialPosition.x, initialPosition.y);
                    }
    }
	
	// Update is called once per frame
// 	void Update () {
        
// 		 if(Input.touchCount > 0 && !locked){
//         Touch touch = Input.GetTouch(0);
//         Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

//         switch(touch.phase)
//         {
//             case TouchPhase.Began:
//                 if(GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos)){
//                     deltaX = touchPos.x - transform.position.x;
//                     deltaY = touchPos.y - transform.position.y;
//                     print("está cogiendo x: "+ deltaX + " está cogiendo y: "+deltaY);
//                 }
//                 break;
//             case TouchPhase.Moved:
//                 if(GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos)){
//                     transform.position = new Vector2(touchPos.x - deltaX, touchPos.y - deltaY);
//                     print(transform.position);
//                     }
//                     break;

//                       case TouchPhase.Ended:
//                 if(Mathf.Abs(transform.position.x - objectPlace.position.x) <= 0.5f && 
//                     Mathf.Abs(transform.position.y - objectPlace.position.y) <= 0.5f){
//                     transform.position = new Vector2(objectPlace.position.x, objectPlace.position.y);
//                     locked = true;
//                     print("Dejó de tocar");
//                     }
//                     else{
//                         transform.position = new Vector2 (initialPosition.x, initialPosition.y);
//                     }
//                     break;
//     }


// 	}
// }
}