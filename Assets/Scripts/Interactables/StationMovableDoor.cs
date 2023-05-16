using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationMovableDoor : MonoBehaviour
{
    public float speed;
    public GameObject door;
    public Transform openPos;
    public Transform closedPos;

    public bool isOpen = true;
    public bool isMoving = false;
    private Vector3 dir;

    public void InteractDoor(){
        isOpen = !isOpen;
        isMoving = !isMoving;
        if(isOpen){
            dir = openPos.position - door.transform.position;
        }
        else{
            dir = closedPos.position - door.transform.position;
        }
    }

    private void Update() {
        if(!isMoving) return;

        if(isOpen){
            door.transform.Translate(dir * speed * Time.deltaTime);
            // door.transform.position = Vector3.MoveTowards();
            if(door.transform.position != openPos.position) return;
        }
        else{
            door.transform.Translate(dir * speed * Time.deltaTime);
            print(door.transform.position);
            if(door.transform.position != closedPos.position) return;
        }
        isMoving = false;
        isOpen = !isOpen;
    }
}
