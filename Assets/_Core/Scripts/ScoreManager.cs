using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
  [SerializeField] private Text text;

  public static int score;

  private void Start()
  {
    score = 0;
  }

  private void Update()
  {
    text.text = "Score: " + score;
  }
}
