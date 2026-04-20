using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    public int PlayerHealth;
    public TextMeshProUGUI UIText;

    void Update()
    {
        UIText.text = $"{PlayerHealth} HP";
    }
}
