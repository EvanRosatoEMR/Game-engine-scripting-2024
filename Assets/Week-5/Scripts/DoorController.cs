using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    private bool open = false;
    private int rotateTimer = 90;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            transform.parent.Rotate(0, 1, 0);

            rotateTimer--;

            if (rotateTimer == 0)
            {
                open = false;
            }
        }
    }

    private void OnCollisionStay(Collision other)
    {
        PlayerMove[] thePlayer = FindObjectsByType<PlayerMove>(FindObjectsSortMode.None);
        PlayerMove myPlayer = thePlayer[0];

        if (other.gameObject.name == "Player")
        {

            Debug.Log("openTest");
            var isItTime = myPlayer.GiveKey();

            Debug.Log(isItTime);

            if (isItTime)
            {
                open = true;
                Debug.Log("open");
            }
        }
    }
}
