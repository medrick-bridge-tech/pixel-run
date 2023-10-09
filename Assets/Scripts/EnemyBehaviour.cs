using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    
    private float _xVector;

    private void Start()
    {
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

        var hit = Physics2D.Raycast(transform.position, _xVector * transform.right, 0.5f,
            LayerMask.GetMask("Ground"));
        if (hit.collider != null)
        {
            Flip();
        }
    }


    private void Flip()
    {
        _xVector = -_xVector; // Change the direction by negating _xVector
        transform.localScale = (Vector3)new Vector2(-_xVector, transform.localScale.y);
    }
}