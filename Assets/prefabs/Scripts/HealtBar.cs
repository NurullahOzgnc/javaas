using UnityEngine;
using UnityEngine.UI;

public class Healtbar : MonoBehaviour
{
    public Slider Slider;
    public Color Low = Color.red;
    public Color High = Color.green;

    void Start()
    {

        SetHealth(Slider.maxValue, Slider.maxValue);
    }

    public void SetHealth(float health, float maxHealth)
    {
        Slider.value = health;
        Slider.maxValue = maxHealth;

        float fillValue = Slider.normalizedValue;


        Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, fillValue);
    }
}