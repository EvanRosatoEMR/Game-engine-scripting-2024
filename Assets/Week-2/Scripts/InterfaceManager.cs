using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interface_manager : MonoBehaviour
{
    public TextMeshProUGUI label;
    public int num;
    public float decimal_1;
    public double decimal_2;

    public void PrintMessage(string msg)
    {
        label.text = msg;
    }

    public void Add(int num)
    {
        label.text = (num + 10).ToString();
    }
}
