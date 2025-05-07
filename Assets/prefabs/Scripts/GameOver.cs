using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public void restartButton()
    {
        SceneManager.LoadScene("level_MENU");
    }
    public void mainmenuButton()
    {
        SceneManager.LoadScene("girisEkran");
    }
}
