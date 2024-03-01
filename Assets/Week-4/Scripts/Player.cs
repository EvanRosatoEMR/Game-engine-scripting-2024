using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassDemo
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("How much damage the player can take")]
        [Range(0f,100f)]
        private float health = 10;

        [SerializeField]
        [TextArea(1,5)]
        private string name;

        public void Damage(int amt)
        {
            health -= amt;
        }

        private Enemy FindNewTarget()
        {
            /*GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("EnemyTag");
            int randomIndex = Random.Range(0, enemyObjects.Length);
            return enemyObjects[randomIndex].GetComponent<Enemy>();*/

            /*GameObject enemyObject = GameObject.Find("Boss");
            return enemyObject.GetComponent<Enemy>();*/

            Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
            int randomIndex = Random.Range(0, enemies.Length);
            return enemies[randomIndex];
        }

        [ContextMenu("Attack")]
        void Attack()
        {
            Enemy target = FindNewTarget();

            Vector3 origin = transform.position;

            transform.DOMove(target.transform.position, 1f)
                     .OnComplete(() =>
                     {
                         target.Damage(10);
                         SoundEffect attack = SoundManager.GetSound(SoundManager.SoundType.Attack);
                         attack.Play(1f);


                         transform.DOMove(origin, 1f);
                     });


           


            
           
            //attack.SetLoop(true);
            
            //mAudioSource.Play();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
        }
    }
}