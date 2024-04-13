using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace week5
{
    public class GameManager : MonoBehaviour
    {

        public UnityEvent GameOverEvent;

        [ContextMenu("Test")]
        void GameOver()
        {
            GameOverEvent.Invoke();
        }

    }

}