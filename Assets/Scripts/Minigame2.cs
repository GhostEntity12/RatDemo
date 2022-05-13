using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame2 : MonoBehaviour
{
  [SerializeField] Canvas minigameCanvas;
  [SerializeField] Image minigameImage;
  [SerializeField] bool active = false;
  bool completed = false;
  [SerializeField] ProgressBar pb;
  [SerializeField] float duration;

    // Update is called once per frame
    void Update()
    {
      if (active) {

        var rotateHorizontal = Input.GetAxis("Horizontal");
        var rotateVertical = Input.GetAxis("Vertical");

        Vector3 rotation = (Vector3.up * rotateHorizontal + Vector3.left * rotateVertical);
        if (rotateHorizontal != 0 || rotateVertical != 0) {
          minigameImage.transform.rotation = Quaternion.LookRotation(Vector3.forward, rotation);
          pb.AddProgress(Time.deltaTime / duration);
          if (pb.Complete) {
            Debug.Log("finished");
            pb.gameObject.SetActive(false);
            active = false;
            completed = true;
            minigameCanvas.gameObject.SetActive(false);
          }
        }
      }
    }

    [ContextMenu("Enable")]
    public void OnMinigameStart() {
      minigameCanvas.gameObject.SetActive(true);
      active = true;
    }
}
