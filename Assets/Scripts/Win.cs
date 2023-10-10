using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Win : MonoBehaviour
{
    [SerializeField] private List<GameObject> winners = new List<GameObject>();
    [SerializeField] private GameObject winUI;
    [SerializeField] private AudioClip audioClip;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            winners.Add(other.gameObject);
            Destroy(other.gameObject.GetComponent<PlayerMovement>());
            other.GetComponent<Animator>().SetFloat("Speed", 0);
            GetComponent<AudioSource>().PlayOneShot(audioClip);
            StartCoroutine(DisplayWinUI());
        }
    }

    private IEnumerator DisplayWinUI()
    {
        yield return new WaitForSeconds(3f);
        winUI.SetActive(true);
    }
}
