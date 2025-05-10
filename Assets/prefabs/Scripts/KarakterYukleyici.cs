using UnityEngine;

public class KarakterYukleyici : MonoBehaviour
{
    public GameObject karakterMeta;
    public GameObject karakterKristal;
    public Transform spawnPoint;

    void Start()
    {
        // Hepsini önce kapat
        karakterMeta.SetActive(false);
        karakterKristal.SetActive(false);

        GameObject aktifKarakter = null;

        // Seçilen karakteri aç ve referansý al
        switch (KarakterSecimi.secilenKarakter)
        {
            case KarakterTip.META:
                aktifKarakter = karakterMeta;
                break;

            case KarakterTip.KRISTAL:
                aktifKarakter = karakterKristal;
                break;

            default:
                Debug.LogWarning("Hiçbir karakter seçilmedi.");
                return;
        }

        // Aktif et ve spawn noktasýna ýþýnla
        aktifKarakter.SetActive(true);
        aktifKarakter.transform.position = spawnPoint.position;
    }
}