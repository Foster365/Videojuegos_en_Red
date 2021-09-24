using UnityEngine;
using UnityEngine.UI;

public class PortalGun : MonoBehaviour
{

    [SerializeField]
    int points;

    Character targetCharacter;
    Text scoreText;

    private void Awake()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {

        //targetCharacter = GameObject.FindGameObjectWithTag("Character").gameObject.GetComponent<Character>();

        if (collision.gameObject.tag == "Character")
        {
            collision.gameObject.GetComponent<Character>().Score += points;
            //scoreText.text = targetCharacter.Score.ToString();
            Debug.Log("a");
            Destroy(gameObject);
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
