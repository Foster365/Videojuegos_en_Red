using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPun
{

    [SerializeField] float maxGameTimer;
    [SerializeField] Text textGameCounter;
    GameObject[] players;
    [SerializeField] Text[] textCharactersScores;
    [SerializeField] Instantiator gameInstantiator;
    [SerializeField] int playersNumber;

    float gameTimer;

    // Start is called before the first frame update
    void Start()
    {

        gameTimer = maxGameTimer;
        textGameCounter.text = gameTimer.ToString();
        var character = PhotonNetwork.LocalPlayer;

        CheckScoreValue(gameInstantiator.CharacterPrefab.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        CheckScoreValue(gameInstantiator.CharacterPrefab);
        GameTimer();
    }

    void GameTimer()
    {

        gameTimer -= Time.deltaTime;
        textGameCounter.text = gameTimer.ToString();
        if (gameTimer <= 0)
        {
            Debug.Log("Game End");
            EndGame();
        }

    }
    void CheckScoreValue(GameObject character)
    {
        foreach (var ts in textCharactersScores)
        {
            if (ts.text == null && ts.GetComponent<Score>() != null)
                ts.GetComponent<Score>().SetScore(character.GetComponent<Character>().Score.ToString(), character);

        }
    }

    void EndGame()
    {
        Winner();
        Loser();
    }

    void Winner()
    {

        textGameCounter.text = "0"; //Ojo

        players = GameObject.FindGameObjectsWithTag(CharacterTags.CHARACTER_TAG);
        for (int i = players.Length - 1; i >= 0; i--)
        {

            int winScore = players[i].GetComponent<Character>().Score;
            if (winScore < players[i].GetComponent<Character>().Score)
            {
                winScore = players[i].GetComponent<Character>().Score;
            }

            SetWinner(players[i]);

        }

        //Ver si cuenta automáticamente los puntos de cada uno.
        //Podría llevar a los players a otra sala o en la misma, mostrar los puntos y compararlos, mostrando al ganador.
    }

   void Loser()
    {

        textGameCounter.text = "0"; //Ojo

        players = GameObject.FindGameObjectsWithTag(CharacterTags.CHARACTER_TAG);
        for (int i = players.Length - 1; i >= 0; i--)
        {

            int winScore = players[i].GetComponent<Character>().Score;
            if (winScore > players[i].GetComponent<Character>().Score)
            {
                winScore = players[i].GetComponent<Character>().Score;
            }

            SetLoser(players[i]);

        }

        //Ver si cuenta automáticamente los puntos de cada uno.
        //Podría llevar a los players a otra sala o en la misma, mostrar los puntos y compararlos, mostrando al ganador.
    }

    void SetWinner(GameObject winnerGameObject)
    {

        var photonViewCharacter = winnerGameObject.GetComponent<Character>().GetComponent<PhotonView>();
        var playerClient = photonViewCharacter.Owner;
        photonView.RPC("CheckWinner", RpcTarget.All, players);

    }

    void SetLoser(GameObject winnerGameObject)
    {

        var photonViewCharacter = winnerGameObject.GetComponent<Character>().GetComponent<PhotonView>();
        var playerClient = photonViewCharacter.Owner;
        photonView.RPC("CheckLoser", RpcTarget.All, players);

    }

    [PunRPC]
    public void CheckWinner(Player player, CharacterController character)
    {
        if (player == PhotonNetwork.LocalPlayer)
            character.GetComponent<CharacterAnimations>().WinAnimation();
      
        Application.Quit();
    }

    [PunRPC]
    public void CheckLoser(Player player, CharacterController character)
    {
        if (player == PhotonNetwork.LocalPlayer)
            character.GetComponent<CharacterAnimations>().DefeatAnimation();
        
        Application.Quit();
    }

}
