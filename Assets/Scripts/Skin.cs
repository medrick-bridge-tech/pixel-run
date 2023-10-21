using UnityEditor;
using UnityEngine;

public class Skin : MonoBehaviour
{
    private Animator _animator;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        var skinPrefab = PlayerPrefs.GetString("skin");
        if(skinPrefab!=null)
            _animator.runtimeAnimatorController = Resources.Load<GameObject>(skinPrefab).GetComponent<Animator>().runtimeAnimatorController;
        else
            _animator.runtimeAnimatorController = Resources.Load<GameObject>("Mario_Prefab").GetComponent<Animator>().runtimeAnimatorController;
        Debug.Log(skinPrefab);
    }
}