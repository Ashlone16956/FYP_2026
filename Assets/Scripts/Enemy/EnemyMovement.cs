using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float movementSpeed = 1.5f;
    private Transform targetBase;

    void Start()
    {
      
        targetBase = GameObject.FindGameObjectWithTag("Base").transform; // Uses the tag of 'Base' to locate the object with the same tag, used to move towards the base.


    }

   
    void Update()
    {

        if (targetBase == null) return; //checks if there is a base for the enemy to move towards first.

        Vector2 direction = (targetBase.position - transform.position).normalized; //gives the direction the enemy needs to move in order to reach the base and normalizes so enemies move at the same speed.
        transform.position += (Vector3)direction * movementSpeed * Time.deltaTime; //Moves the enemy towards the base every frame and makes movement framerate independant.

    }
}
