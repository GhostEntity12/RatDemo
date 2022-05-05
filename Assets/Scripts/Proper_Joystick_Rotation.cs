using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proper_Joystick_Rotation : MonoBehaviour
{
  private int rotationAmount = 0;
    // Start is called before the first frame update
    void Start()
    {
      rotationAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
      var rotateHorizontal = Input.GetAxis("Horizontal");
      var rotateVertical = Input.GetAxis("Vertical");

      Vector3 rotation = (Vector3.up * rotateHorizontal + Vector3.left * rotateVertical);
      if (rotateHorizontal != 0 || rotateVertical != 0) {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, rotation);
      }

      if (rotationAmount >= 5) {

      }
    }
}
