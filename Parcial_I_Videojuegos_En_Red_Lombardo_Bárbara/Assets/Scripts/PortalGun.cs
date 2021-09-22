using UnityEngine;
using UnityEngine.UI;

public class PortalGun : MonoBehaviour
{

    [SerializeField]
    int points;

    Character targetCharacter;
    Text scoreText;

    private void Start()
    {

        targetCharacter = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Character")
        {
            targetCharacter.Score += points;
            scoreText.text = targetCharacter.Score.ToString();
            Destroy(this.gameObject);
        }

    }
}
