using UnityEngine;

public class KarakterYukleyici : MonoBehaviour
{
    public GameObject karakterMeta;
    public GameObject karakterKristal;
    public Transform spawnPoint;

    void Start()
    {
        // Hepsini �nce kapat
        karakterMeta.SetActive(false);
        karakterKristal.SetActive(false);

        GameObject aktifKarakter = null;

        // Se�ilen karakteri a� ve referans� al
        switch (KarakterSecimi.secilenKarakter)
        {
            case KarakterTip.META:
                aktifKarakter = karakterMeta;
                break;

            case KarakterTip.KRISTAL:
                aktifKarakter = karakterKristal;
                break;

            default:
                Debug.LogWarning("Hi�bir karakter se�ilmedi.");
                return;
        }

        // Aktif et ve spawn noktas�na ���nla
        aktifKarakter.SetActive(true);
        aktifKarakter.transform.position = spawnPoint.position;
    }
}