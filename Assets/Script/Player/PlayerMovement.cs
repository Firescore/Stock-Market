using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float gravity = -9.8f;
    public float groundDistance = 0.4f;

    public Transform groundCheck;

    public LayerMask layer;

    private CharacterController controller;
    private Animator anime;
    private StairCreator sTC;
    private float yAxis, zAxis;

    private Vector3 velocity;
    private bool isGrounded = false;
    // Start is called before the first frame update
    void Start()
    {
        controller = transform.GetComponent<CharacterController>();
        anime = transform.GetChild(0).GetComponent<Animator>();
        sTC = GameObject.FindGameObjectWithTag("StairCreator").GetComponent<StairCreator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, layer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }


        move();
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    void move()
    {
        anime.SetBool("up", true);
        controller.Move(transform.forward * speed * Time.deltaTime);
    }
}
