using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    bool avaiable;

    public bool Avaiable { get => avaiable; set => avaiable = value; }

    private void Start()
    {
        avaiable = true;
    }
}
