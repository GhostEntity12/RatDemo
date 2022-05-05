using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stirring_Minigame : MonoBehaviour
{
  [SerializeField] float rotateSpeed = 0.2f;
  private float rotationAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
      rotationAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
      var rotate = Input.GetAxis("Horizontal");

      if (rotate > 0) {
        transform.Rotate(0, 0, -rotate * rotateSpeed);
        rotationAmount += rotate;
      }

      if ((rotationAmount/360) >= 25f) {
        SceneManager.LoadScene("Testing");
      }

      Debug.Log(rotationAmount/360);
    }
}
