using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Resultados : MonoBehaviour
{
    [SerializeField] TMP_Text puntaje;
    [SerializeField] TMP_Text tiempo;

    private void OnEnable()
    {
        float time = GameManager.GameTime;
        int seconds = Mathf.FloorToInt(time);
        puntaje.text = GameManager.Score.ToString();
        tiempo.text = seconds + "s";
    }
}
