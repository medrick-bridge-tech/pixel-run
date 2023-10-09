using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;

    private float _distanceFromEarth = 0.5f;
    private float _xVector;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private CinemachineVirtualCamera _cvm;
    
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _cvm = GetComponentInChildren<CinemachineVirtualCamera>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Run();
        Jump();
        HandleAnimation();
    }

    private void Run()
    {
        if (_animator.GetBool("IsAlive"))
        {
            _xVector = Input.GetAxisRaw("Horizontal");
            transform.position += (Vector3)new Vector2(_xVector * moveSpeed * Time.deltaTime, 0);
            Flip();
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CheckDistanceFromEarth())
        {
            _rigidbody2D.AddForce(transform.up * jumpSpeed, ForceMode2D.Impulse);
        }
    }

    private void Flip()
    {
        if (_xVector != 0)
        {
            transform.localScale = (Vector3)new Vector2(_xVector, 1);
        }
    }

    private void HandleAnimation()
    {
        if (_xVector != 0 && !_animator.GetBool("IsJump"))
        {
            _animator.SetFloat("Speed", 1);
        }
        else
        {
            _animator.SetFloat("Speed", 0);
        }

        if (!CheckDistanceFromEarth())
        {
            _animator.SetBool("IsJump", true);
        }
        else
        {
            _animator.SetBool("IsJump", false);
        }

    }

    private bool CheckDistanceFromEarth()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, -transform.up, _distanceFromEarth, LayerMask.GetMask("Ground"));
        if (hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("do");
            _animator.SetBool("IsAlive",false);
            Destroy(GetComponent<BoxCollider2D>());
            _rigidbody2D.AddForce(new Vector2(0,jumpSpeed),ForceMode2D.Impulse);
            GameObject crow = new GameObject();
            crow.transform.position = transform.position;
            _cvm.Follow = crow.transform;
        }
    }
}
