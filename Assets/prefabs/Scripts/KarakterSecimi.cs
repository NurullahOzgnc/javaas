using UnityEngine;
using UnityEngine.SceneManagement;

public class KarakterSecimi : MonoBehaviour
{
    public static KarakterTip secilenKarakter;

    public void KarakterSecMeta()
    {
        secilenKarakter = KarakterTip.META;
        SceneManager.LoadScene("Level_MENU");
    }

    public void KarakterSecKristal()
    {
        secilenKarakter = KarakterTip.KRISTAL;
        SceneManager.LoadScene("Level_MENU");
    }
}

public enum KarakterTip
{
    META,
    KRISTAL
}