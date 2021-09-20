using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Ball : MonoBehaviour 
{ 
    Rigidbody rigid;
   
    const float upForce = 8f; // So the ball can keep staying up in the air
    const float minTranslation = 0.05f; //So that the ball doesn't stay still
    
    private GameManager manager; //GameManager object
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        manager = GameManager.instance;
        rigid = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //When the ball hits the paddle
        if (collision.gameObject.tag == "Paddle") 
        {
            //Play boing SFX
            audio.Play();

            //Add 1000 points per bounce
            manager.AddPoints(); 
            
            //Object to change position
            Vector3 colPos = collision.transform.position; 
            
            //Randomizes where the ball will land in depending on where the it lands on the paddle
            //Will change for x and z axis
            float diffX = (minTranslation + (colPos.x - transform.position.x)) * Random.Range(1, 10);
            float diffZ = (minTranslation + (colPos.z - transform.position.z)) * Random.Range(1, 10);
            
            //Change position of defined by diffX and diffZ
            rigid.velocity = new Vector3(-diffX, upForce, -diffZ);
        } else
        {
            manager.GameOver(gameObject);
        }
    }


}
