using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    //stats
    [SerializeField] float movementSpeed;
    public Color playerColor;
    public int playerScore;
    public bool isSitting = false;
    Rigidbody2D playerRigidbody2D;
    // Update is called once per frame
    private void Awake()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        //movement method
        MoveThePlayer();
    }

    private void MoveThePlayer()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticallInput = Input.GetAxisRaw("Vertical");
        playerRigidbody2D.velocity = new Vector2(horizontalInput * movementSpeed, verticallInput * movementSpeed);
    }
}
