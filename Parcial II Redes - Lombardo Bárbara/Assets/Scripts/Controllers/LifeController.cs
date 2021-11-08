using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    public void Respawn()
    {
        //TODO lógica de respawn:
        // Considerar puntos de respawn (Transform) para que el player se reinstancie ahí.
        // La instancia debe ir con PhotonNetwork.Instantiate (Tiene que actualizarse para todos los jugadores por igual)
        Debug.Log("Respawn, estás como loquita");
    }
}
