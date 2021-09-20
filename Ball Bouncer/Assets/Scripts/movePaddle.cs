using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePaddle : MonoBehaviour
{
    //Speed of the paddle
    public float speed = 3.0f;

    void Update()
    {
        if (Time.timeScale == 0) return; //Don't execute while paused

        //Set the new movement for the paddle
        //(New position of mouse is where mouse is * speed) * (Speed of PC / speed of game)
        //This is for x and z axis
        Vector3 newPos = new Vector3();
        newPos.x = (Input.GetAxis("Mouse X") * speed) * (Time.deltaTime / Time.timeScale);
        newPos.z = (Input.GetAxis("Mouse Y") * speed) * (Time.deltaTime / Time.timeScale);

        //If the the new position compared to last is over 2, get a new position
        if (newPos.magnitude > 2) return;

        //Move paddle to the new position
        transform.Translate(newPos);
        //print(newPos);
    }
}
