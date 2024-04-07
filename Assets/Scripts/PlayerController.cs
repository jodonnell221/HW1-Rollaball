using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public bool onGround;
    public Vector3 jumpmovement;
    public float force = 3.0f;
    public bool canDouble;
    
    // Start is called before the first frame update
    void Start()
    {
        winTextObject.SetActive(false);
        rb = GetComponent<Rigidbody>();
        count = 0;
        jumpmovement = new Vector3(0.0f, 2.0f, 0.0f);
        SetCountText();
    }

    void OnCollisionStay()
    {
        onGround = true;
    }
    void OnCollisionExit()
    {
        onGround = false;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
        rb.AddForce(movement);
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (onGround)
            {

                rb.AddForce(jumpmovement * force, ForceMode.Impulse);
                onGround = false;
                canDouble = true;

            }
            else
            {
                if (canDouble)
                {
                    canDouble = false;
                    onGround = false;
                    rb.AddForce(jumpmovement * force, ForceMode.Impulse);

                }
            }

        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 3)
        {
            winTextObject.SetActive(true);
        }
    }
}
