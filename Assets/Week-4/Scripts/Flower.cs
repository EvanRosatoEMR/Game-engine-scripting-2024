using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    private float nectarTimeMax = 100;
    private float nectarTimer = 0;
    private bool nectar = false;

    [SerializeField] Color colNect = Color.yellow;
    [SerializeField] Color colNoNect = Color.gray;

    public bool GotNectar()
    {
        return nectar;
    }

    public bool TakeNectar()
    {
        if (nectar)
        {
            nectar = false;
            nectarTimer = 0;
            gameObject.GetComponent<SpriteRenderer>().color = colNoNect;

            return true;
        } else
        {
            return false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (nectarTimer < nectarTimeMax)
        {
            nectarTimer += 1;
        } else
        {
            nectar = true;

            gameObject.GetComponent<SpriteRenderer>().color = colNect;
        }
    }
}
