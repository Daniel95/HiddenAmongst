using UnityEngine;
using Mirror;
using System.Collections.Generic;

public class GameNetWorkManager : NetworkManager
{
    [SerializeField] private GameObject guardPlayerPrefab;
    [SerializeField] private GameObject thiefPlayerPrefab;
    [SerializeField] private GameObject civilianPrefab;
    [SerializeField] private bool prioritizeThiefPlayer;
    [SerializeField] private int civiliansAmountToSpawn = 11;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        PlayerType playerType = GetPlayerTypeToSpawn();

        GameObject player;

        if (playerType == PlayerType.Guard)
        {
            player = Instantiate(guardPlayerPrefab);
        }
        else
        {
            List<Point> points = PointManager.Instance.GetPoints(PointType.WayPoint);

            Debug.Assert(points.Count >= civiliansAmountToSpawn - 1);

            int randomIndex = Random.Range(0, points.Count);
            Point thiefSpawnPoint = points[randomIndex];

            player = Instantiate(thiefPlayerPrefab, thiefSpawnPoint.transform.position, thiefSpawnPoint.transform.rotation);

            points.RemoveAt(randomIndex);

            for (int i = 0; i < civiliansAmountToSpawn; i++)
            {
                randomIndex = Random.Range(0, points.Count);
                Point civilianSpawnPoint = points[randomIndex];

                GameObject civilian = Instantiate(civilianPrefab, civilianSpawnPoint.transform.position, civilianSpawnPoint.transform.rotation);
                NetworkServer.Spawn(civilian);
                civilian.GetComponent<EnemyStateManager>().StartCivilian();

                points.RemoveAt(randomIndex);
            }
        }

        NetworkServer.AddPlayerForConnection(conn, player);
        NetworkServer.Spawn(player);

        if (numPlayers == 2)
        {
            Debug.Log("START");
        }
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        Debug.Log("DISCONNECT");

        base.OnServerDisconnect(conn);
    }

    private PlayerType GetPlayerTypeToSpawn()
    {
        PlayerType playerType = PlayerType.Thief;

        string thiefName = thiefPlayerPrefab.name + "(Clone)";
        string guardName = guardPlayerPrefab.name + "(Clone)";

        bool thiefExists = GameObject.Find(thiefName) != null;
        bool guardExists = GameObject.Find(guardName) != null;

        if (prioritizeThiefPlayer)
        {
            if (!thiefExists)
            {
                playerType = PlayerType.Thief;
            }
            else if (!guardExists)
            {
                playerType = PlayerType.Guard;
            }
            else
            {
                Debug.Assert(true, "Player type Guard and Thief already exists!");
            }
        }
        else
        {
            if (!guardExists)
            {
                playerType = PlayerType.Guard;
            }
            else if (!thiefExists)
            {
                playerType = PlayerType.Thief;
            }
            else
            {
                Debug.Assert(true, "No player spawned!");
            }
        }

        return playerType;
    }
}
