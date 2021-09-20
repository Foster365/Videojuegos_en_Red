using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] float maxGameTimer;
    [SerializeField] Text textGameCounter;
    GameObject[] winners;

    // Start is called before the first frame update
    void Start()
    {

        textGameCounter.text = maxGameTimer.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        GameTimer();
    }

    void GameTimer()
    {

        maxGameTimer -= Time.deltaTime;
        textGameCounter.text = maxGameTimer.ToString();
        if (maxGameTimer <= 0) EndGame();

    }

    void EndGame()
    {
        winners = GameObject.FindGameObjectsWithTag("Character");

        for (int i = winners.Length -1; i >= 0; i--)
        {
            int winScore = winners[i].GetComponent<Character>().Score;
            if (winScore < winners[i].GetComponent<Character>().Score) winScore = winners[i].GetComponent<Character>().Score;
        }

        //Ver si cuenta automáticamente los puntos de cada uno.
        //Podría llevar a los players a otra sala o en la misma, mostrar los puntos y compararlos, mostrando al ganador.
    }
}

