using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveOnAwake : MonoBehaviour
{
    [SerializeField] private bool setActiveOnStart;

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.SetActive(false);
    }
}
