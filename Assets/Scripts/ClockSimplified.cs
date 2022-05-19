using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ClockSimplified : MonoBehaviour
{
    [SerializeField] float dayLength = 60;
    [SerializeField] TextMeshProUGUI text;
    private bool audioPlaying;
    // Start is called before the first frame update
    void Start()
    {
        audioPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (dayLength >= 0) {
            dayLength -= Time.deltaTime;
            text.text = $"{(int)dayLength}";
        }
        if (dayLength == 0) {
            SceneManager.LoadScene("GameOver");
        }
    }
}
