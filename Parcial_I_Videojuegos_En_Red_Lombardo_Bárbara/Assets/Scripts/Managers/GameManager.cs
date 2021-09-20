using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] float maxGameTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameTimer();
    }

    void GameTimer()
    {

        float timer = 0;

        timer += Time.deltaTime;
        if (timer >= maxGameTimer) EndGame();

    }

    void EndGame()
    {
        //Ver si cuenta automáticamente los puntos de cada uno.
        //Podría llevar a los players a otra sala o en la misma, mostrar los puntos y compararlos, mostrando al ganador.
    }
}

