using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHY : MonoBehaviour
{
    float speed;
    bool isSpawn;
    [SerializeField] string prefabName;

    Rigidbody playerRigidbody;
    CharacterAnimations characterAnim;

    //Waypoints
    [SerializeField] List<GameObject> waypoints;
    float distance;
    int nextWaypoint = 0;
    int indexModifier = 1;

    public List<GameObject> Waypoints { get => waypoints; set => waypoints = value; }
    public float Distance { get => distance; set => distance = value; }
    public int NextWaypoint { get => nextWaypoint; set => nextWaypoint = value; }
    public int IndexModifier { get => indexModifier; set => indexModifier = value; }
    public bool IsSpawn { get => isSpawn; set => isSpawn = value; }

    private void Start()
    {

        isSpawn = false;

        waypoints = GetWaypoints();

    }

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 dir)
    {
        dir = dir.normalized;
        var ySpeed = playerRigidbody.velocity.y;
        playerRigidbody.velocity = new Vector3(dir.x * speed, ySpeed);

    }

    List<GameObject> GetWaypoints()
    {

        List<GameObject> spawnPointsPositions = new List<GameObject>();

        RaycastHit[] colliders = Physics.RaycastAll(transform.position, transform.forward, 6, 1 << LayerMask.NameToLayer("NodeWaypoint")); //BUG ALERT: Puede que con el
        //transform forward dos personajes se muevan mal (Por dirección de los game objects en la escena)

        foreach (var sp in colliders)
        {
            spawnPointsPositions.Add(sp.collider.gameObject);
        }

        return spawnPointsPositions;

    }

}
