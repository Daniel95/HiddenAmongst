using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPlayer : Mirror.NetworkBehaviour
{
    [SerializeField] private int cameraIndex;
    [SerializeField] private int bullets = 1;

    private GameObject[] cameras;
    private GameObject activeCamera;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        cameras = GetCameras();

        SwitchCamera(cameraIndex);
    }

    private void DisableAllGuardCameras()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            if(cameras[i] != null)
            {
                cameras[i].SetActive(false);
            }
        }
    }

    private GameObject[] GetCameras()
    {
        GameObject cameraParent = GameObject.FindWithTag(Tags.GUARD_CAMERA_PARENT);

        Debug.Assert(cameraParent != null, "No gameobject with tag: " + Tags.GUARD_CAMERA_PARENT + " found!");

        List<GameObject> tempCameras = new List<GameObject>();

        foreach (Transform child in cameraParent.transform)
        {
            tempCameras.Add(child.gameObject);
        }

        return tempCameras.ToArray();
    }

    private void SwitchCamera(int cameraIndex)
    {
        if(activeCamera != null)
        {
            activeCamera.SetActive(false);
        }

        activeCamera = cameras[cameraIndex];
        activeCamera.SetActive(true);
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isLocalPlayer) { return; }

        if (Input.GetKeyDown(KeyCode.A))
        {
            cameraIndex--;
            if(cameraIndex < 0)
            {
                cameraIndex = cameras.Length - 1;
            }

            SwitchCamera(cameraIndex);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            cameraIndex++;
            if (cameraIndex >= cameras.Length)
            {
                cameraIndex = 0;
            }

            SwitchCamera(cameraIndex);
        }

        if (Input.GetMouseButton(0))
        { 
            RaycastHit hit;
            Ray ray = activeCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;

                if(objectHit.CompareTag(Tags.SUSPECT)
                    && bullets > 0)
                {
                    bullets--;
                    Destroy(objectHit.gameObject);
                }
            }

        }
    }
}
