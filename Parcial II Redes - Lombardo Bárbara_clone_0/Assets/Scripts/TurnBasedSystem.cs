using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TurnState { START, PLAYERTURN, ENEMYTURN, WON, GAMEOVER }

public class TurnBasedSystem : MonoBehaviour
{

    [SerializeField] GameObject playerPrefab;// Puedo usar los jugadores almacenados en el diccionario del servidor, el local sería el mío y los demás (Recorrer el diccionario)
                                            //serían los enemigos
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] Transform playerBattleStation;
    [SerializeField] Transform enemyBattleStation;

    CharacterHY playerUnit; // En el tutorial es Unit (class)
    CharacterHY enemyUnit;

    TurnState currentState;

    // Start is called before the first frame update
    void Start()
    {

        currentState = TurnState.START;

        StartCoroutine(SetUpGame());
        
    }

    IEnumerator SetUpGame()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<CharacterHY>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<CharacterHY>();

        //gameServer.photonView.RPC("InitializePlayer", clientServer, localPlayer);
        //gameServer.photonView.RPC("RequestGetPlayer", clientServer, localPlayer);

        yield return new WaitForSeconds(2f);

        currentState = TurnState.PLAYERTURN;
        PlayerTurn();

    }

    IEnumerator PlayerAttack()
    {
        //bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        yield return new WaitForSeconds(2f);

        //Check if enemy is dead
        //if (isDead)
        //{
        //    TurnState.WON;// End the battle
        //    EndBattle();
        //}
        //else
        //{
        //    currentState = TurnState.ENEMYTURN;
        //    StartCoroutine(EnemyTurn());
        //}

        //CHange state based on what happened
    }

    IEnumerator EnemyTurn()
    {
        //dialogueText.text = enemyUnit.unitName + "attacks! ";
        yield return new WaitForSeconds(1f);

        //bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        yield return new WaitForSeconds(1f);

        //if (isDead)
        //{
        //    currentState = TurnState.LOST;
        //    EndBattle();
        //}
        //else
        //{
        //    currentState = TurnState.PLAYERTURN;
        //    PlayerTurn();
        //}
    }

    void EndBattle()
    {
        //if(currentState == TurnState.WON)
        //{
        //    dialogueText.text = "You won the battle!";
        //}
        //else if(currentState == TurnState.LOST)
        //{
        //    dialogueText.text = "You were defeated."
        //}
    }

    public void PlayerTurn()
    {
        //dialogueText.text = "Choose an action: " //Tengo que poner una UI que me indique los turnos.
    }
    public void OnAttackButton()
    {
        if (currentState != TurnState.PLAYERTURN) return;

        StartCoroutine(PlayerAttack());

    }

}
