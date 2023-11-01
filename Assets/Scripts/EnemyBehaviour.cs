using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    
    private float _xVector;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _xVector = -1f;
    }

    private void Update()
    {
        Walk();
    }

    private void Walk()
    {
        float horizontalMovement = _xVector * moveSpeed * Time.deltaTime;
        transform.Translate(new Vector3(horizontalMovement, 0, 0));

        var hit = Physics2D.Raycast(transform.position, _xVector * transform.right, 1f,
            LayerMask.GetMask("Ground"));
        if (hit.collider != null)
            Flip();
    }
    
    private void Flip()
    {
        _xVector = -_xVector;
        transform.localScale = (Vector3)new Vector2(-_xVector, transform.localScale.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _animator.SetBool("IsAlive",false);
            _rigidbody2D.AddForce(new Vector2(0,jumpForce),ForceMode2D.Impulse);
            moveSpeed = 1f;
            Destroy(_boxCollider2D);
        }
    }
}