using UnityEditor;
using UnityEngine;

public class Skin : MonoBehaviour
{
    private Animator _animator;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        if(PlayerPrefs.HasKey("skin"))
            _animator.runtimeAnimatorController = Resources.Load<GameObject>(PlayerPrefs.GetString("skin")).GetComponent<Animator>().runtimeAnimatorController;
        else
            _animator.runtimeAnimatorController = Resources.Load<GameObject>("Mario_Prefab").GetComponent<Animator>().runtimeAnimatorController;
    }
}