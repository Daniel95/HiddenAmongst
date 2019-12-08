using UnityEngine;

public class SpawnCamera : Mirror.NetworkBehaviour
{
    [SerializeField] private GameObject cameraPrefab;
    [SerializeField] private Transform cameraSpawnPoint;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        GameObject cameraGameObject = Instantiate(cameraPrefab, cameraSpawnPoint.position, cameraSpawnPoint.rotation);
        cameraGameObject.transform.SetParent(transform);
        cameraGameObject.SetActive(true);
    }
}
