using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private NetworkPrefabRef playerPrefab;

    [SerializeField] private Transform[] spawnPoints;

    private Dictionary<PlayerRef, NetworkObject> spawnedPlayers =
        new Dictionary<PlayerRef, NetworkObject>();

    private void OnEnable()
    {
        LobbyEvents.OnPlayerJoined += SpawnPlayer;
        LobbyEvents.OnPlayerLeft += DespawnPlayer;
    }

    private void OnDisable()
    {
        LobbyEvents.OnPlayerJoined -= SpawnPlayer;
        LobbyEvents.OnPlayerLeft -= DespawnPlayer;
    }

    private void SpawnPlayer(PlayerRef player)
    {
        NetworkRunner runner = LobbyNetwork.Instance.GetRunner();

        if (runner == null)
            return;

        // SOLO EL HOST SPAWNEA
        if (!runner.IsServer)
            return;

        Vector3 spawnPosition = GetSpawnPosition(player);

        NetworkObject playerObject = runner.Spawn(
            playerPrefab,
            spawnPosition,
            Quaternion.identity,
            player
        );

        spawnedPlayers.Add(player, playerObject);
    }

    private void DespawnPlayer(PlayerRef player)
    {
        NetworkRunner runner = LobbyNetwork.Instance.GetRunner();

        if (runner == null)
            return;

        if (spawnedPlayers.TryGetValue(player, out NetworkObject playerObject))
        {
            runner.Despawn(playerObject);

            spawnedPlayers.Remove(player);
        }
    }

    private Vector3 GetSpawnPosition(PlayerRef player)
    {
        if (spawnPoints.Length == 0)
            return Vector3.zero;

        int index = player.RawEncoded % spawnPoints.Length;

        return spawnPoints[index].position;
    }

    private void OnDrawGizmos()
    {
        if (spawnPoints == null)
            return;
        Gizmos.color = Color.green;
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (spawnPoint != null)
            {
                Gizmos.DrawSphere(spawnPoint.position, 0.5f);
            }
        }
    }
}
