using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] private List<GameObject> winners = new List<GameObject>();
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            winners.Add(other.gameObject);
            Destroy(other.gameObject.GetComponent<PlayerMovement>());
            other.GetComponent<Animator>().SetFloat("Speed",0);
        }
    }
}
