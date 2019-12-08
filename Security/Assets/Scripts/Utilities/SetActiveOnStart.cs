using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveOnStart : MonoBehaviour
{
    [SerializeField] private bool setActiveOnStart;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }
}
