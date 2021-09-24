using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class Score : MonoBehaviourPun
{
    //[SerializeField] TextMeshPro textScore;
    [SerializeField] Text textCharacterScore;
    GameObject character;

    public void SetScore(string score, GameObject characterScore)
    {
        textCharacterScore.text = score;
        character = characterScore;
        photonView.RPC("UpdateCharacterScore", RpcTarget.OthersBuffered, score);
    }

    public void Update()
    {

    }

    [PunRPC]
    public void UpdateCharacterScore(string scoreText)
    {
        textCharacterScore.text = scoreText;
    }
}
