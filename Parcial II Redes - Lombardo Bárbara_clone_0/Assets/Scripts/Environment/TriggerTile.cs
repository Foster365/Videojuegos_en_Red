using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTile : MonoBehaviour
{
    bool isCollision;
    CharacterHY target;

    public bool IsCollision { get => isCollision; set => isCollision = false; }
    public CharacterHY Target { get => target; set => target = value; }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<CharacterHY>() != null)
        {
            isCollision = true;
            target = collision.gameObject.GetComponent<CharacterHY>();
        }

    }

}
