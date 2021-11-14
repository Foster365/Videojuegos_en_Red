using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooseTile : MonoBehaviour
{

    Respawner respawner;

    private void Start()
    {
        respawner = GetComponent<Respawner>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);

            StartCoroutine(WaitToEnable());

        }
        else gameObject.SetActive(true);

    }

    IEnumerator WaitToEnable()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Ready to re enable tile");
    }



}
