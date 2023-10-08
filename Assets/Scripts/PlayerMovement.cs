using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;

    private float _distanceFromEarth = 0.5f;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        Run();
        Jump();
    }
    
    private void Run()
    {
        var x = Input.GetAxisRaw("Horizontal");
        transform.position += (Vector3)new Vector2(x * moveSpeed * Time.deltaTime, 0);
    }

    private void Jump()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, -transform.up,_distanceFromEarth,LayerMask.GetMask("Ground"));
        if (Input.GetKeyDown(KeyCode.Space) && hit.collider != null)
        {
            _rigidbody2D.AddForce(transform.up * jumpSpeed, ForceMode2D.Impulse);
        }
    }
}
