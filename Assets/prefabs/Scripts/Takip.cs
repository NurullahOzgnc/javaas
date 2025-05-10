using UnityEngine;
using Cinemachine;

public class Takip : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        // Cinemachine Virtual Camera'y� bul
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (virtualCamera == null)
        {
            Debug.LogError("Cinemachine Virtual Camera bulunamad�.");
            return;
        }


    }
    private void Update()
    {
        // Player tagli GameObject'i bul
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Player'�n Transform'unu Virtual Camera'ya ata
            virtualCamera.Follow = player.transform;
        }
        else
        {
            Debug.LogError("Player tag'li GameObject bulunamad�.");
        }
    }
}