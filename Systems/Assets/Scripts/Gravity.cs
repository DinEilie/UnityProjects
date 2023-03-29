using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public bool enable;
    private const float defaultValue = 9.8f;
    [SerializeField] private float gravity = defaultValue;
    [SerializeField] private float velocity;

    // Update is called once per frame
    void Update()
    {
        if (enable){
            // Gain velocity every second
            velocity += gravity * Time.deltaTime * (-1f);
            if (gameObject.GetComponent<Movement>().IsGrounded() && velocity < 0)
                velocity = -2f;
            
            // Update the coordinates
            Vector3 move = transform.up * velocity;
            gameObject.GetComponent<CharacterController>().Move(move * Time.deltaTime );
        }
    }

    // Get the gravity to other value
    public float Get(){
        return gravity;
    }

    public float GetVelocity(){
        return velocity;
    }

    // Set the gravity to other value
    public void Set(float num){
        gravity = num;
    }

    // Reduce Velocity
    public void AddVelocity(){
        float value = (gravity + 4.5f) * Time.deltaTime;
        velocity += value;
    }

    // Add velocity 
    public void SetVelocity(float height, float speed){
        float movement = Mathf.Sqrt(height * speed * gravity);
        velocity = movement;
    }

    // Reset the gravity value back to default
    public void Reset(){
        gravity = defaultValue;
    }
}
