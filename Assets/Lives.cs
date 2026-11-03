using UnityEngine;
using TMPro;
public class Lives : MonoBehaviour
{
    [Header("Dynamic")]
    public int lives = 4;
    private TextMeshProUGUI uiText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        uiText.text = "Lives: " + lives.ToString("#,0");
    }

    // public void removeLife()
    // {
    //     lives--;
    // }
}
