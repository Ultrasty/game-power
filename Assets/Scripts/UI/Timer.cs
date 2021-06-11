using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
  public int totalTime;
  private int currentTime;
  public Slider slider;
  Scene scene;
  // Start is called before the first frame update
  void Start()
  {
    currentTime = totalTime;
    slider.maxValue = totalTime;
  }

  // Update is called once per frame
  void Update()
  {
    currentTime = (int)(totalTime - Time.time);
    slider.value = currentTime;
    if (currentTime <= 0)
    {
      SceneManager.LoadScene(scene.name);

    }
  }
}
