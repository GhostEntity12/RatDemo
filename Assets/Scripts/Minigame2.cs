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
  ProgressBar pb;
  float progress;
  [SerializeField] float duration;
    // Start is called before the first frame update
    void Start()
    {
        pb = Instantiate(GameManager.Instance.progressBarPrefab, GameManager.Instance.c.transform);
    }

    // Update is called once per frame
    void Update()
    {
      if (active) {
        pb.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2);

        var rotateHorizontal = Input.GetAxis("Horizontal");
        var rotateVertical = Input.GetAxis("Vertical");

        Vector3 rotation = (Vector3.up * rotateHorizontal + Vector3.left * rotateVertical);
        if (rotateHorizontal != 0 || rotateVertical != 0) {
          minigameImage.transform.rotation = Quaternion.LookRotation(Vector3.forward, rotation);
          progress += Time.deltaTime;
          pb.AddProgress(Time.deltaTime / duration);
          if (progress >= duration) {
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
    void OnMinigameStart() {
      minigameCanvas.gameObject.SetActive(true);
      active = true;
    }
}
