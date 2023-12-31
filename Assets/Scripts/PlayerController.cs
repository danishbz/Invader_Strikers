using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    [SerializeField] private float moveSpeed;
    [SerializeField] private float speedIncreasePercentage; // Percentage increase in speed
    [SerializeField] private float maxSpeed; // Maximum speed

    private Animator anim;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveInput = new Vector3(0f, 0f, 0f);
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();
        
        // transform.position += moveInput * moveSpeed * Time.deltaTime;

        Vector3 newPosition = transform.position + moveInput * moveSpeed * Time.deltaTime;
        // Clamp the player's position
        newPosition.x = Mathf.Clamp(newPosition.x, -95f, 95f);
        newPosition.y = Mathf.Clamp(newPosition.y, -55f, 55f);

        transform.position = newPosition;

        if(moveInput != Vector3.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else{
            anim.SetBool("isMoving", false);
        }

        if(moveInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(moveInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    public void IncreaseSpeed()
    {
        float newSpeed = moveSpeed + moveSpeed * speedIncreasePercentage;

        // Cap the speed to the maximum value
        if (newSpeed > maxSpeed)
        {
            newSpeed = maxSpeed;
        }

        // Update the player's move speed
        moveSpeed = newSpeed;
    }
}
