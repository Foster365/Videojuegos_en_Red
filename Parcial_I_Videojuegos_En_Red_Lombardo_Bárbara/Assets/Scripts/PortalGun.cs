using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PortalGun : MonoBehaviourPun
{

    [SerializeField]
    int points;

    Character targetCharacter;
    Text scoreText;

    private void OnCollisionEnter(Collision collision)
    {

        //targetCharacter = GameObject.FindGameObjectWithTag("Character").gameObject.GetComponent<Character>();

        if (collision.gameObject.tag == "Character" && photonView.IsMine)
        {
            collision.gameObject.GetComponent<Character>().Score += points;
            //scoreText.text = targetCharacter.Score.ToString();
            Debug.Log("a");
            PhotonNetwork.Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetCharacter)
            targetCharacter.transform.parent = transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == targetCharacter)
            targetCharacter.transform.parent = null;
    }
}
