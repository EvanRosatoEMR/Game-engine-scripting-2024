using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CharacterEditor
{
    public class CharacterEditor : MonoBehaviour
    {
        [SerializeField] Button nextMaterial;
        [SerializeField] Button nextBodyPart;
        [SerializeField] Button loadGame;

        [SerializeField] Character character;

        int id;
        BodyTypes bodyType = BodyTypes.Head;

        private void Awake()
        {
            nextMaterial.onClick.AddListener(NextMaterial);
            nextBodyPart.onClick.AddListener(NextBodyPart);
            loadGame.onClick.AddListener(LoadGame);
        }

        void NextMaterial()
        {
            id++;

            if (id > 3)
            {
                id = 0;
            }
            //TODO: Make a switch case for each BodyType and save the value of id to the correct PlayerPref
            switch (bodyType)
            {
                case BodyTypes.Arm:
                    PlayerPrefs.SetInt("playerArm", id);
                    break;

                case BodyTypes.Leg:
                    PlayerPrefs.SetInt("playerLeg", id);
                    break;

                case BodyTypes.Head:
                    PlayerPrefs.SetInt("playerHead", id);
                    break;

                case BodyTypes.Body:
                    PlayerPrefs.SetInt("playerBody", id);
                    break;
            }

            //TODO: Tell the character to load to get the updated body piece
            character.Load();
        }

        void NextBodyPart()
        {     
            //TODO: Setup a switch case that will go through the different body types
            //      ie if the current type is Head and we click next then set it to Body

            switch (bodyType)
            {
                case BodyTypes.Arm:
                    bodyType = BodyTypes.Leg;
                    break;

                case BodyTypes.Leg:
                    bodyType = BodyTypes.Head;
                    break;

                case BodyTypes.Head:
                    bodyType = BodyTypes.Body;
                    break;

                case BodyTypes.Body:
                    bodyType = BodyTypes.Arm;
                    break;
            }

            //TODO: Then setup another switch case that will get the current saved value
            //      from the player prefs for the current body type and set it to id

            switch (bodyType)
            {
                case BodyTypes.Arm:
                    id = PlayerPrefs.GetInt("playerArm");
                    break;

                case BodyTypes.Leg:
                    id = PlayerPrefs.GetInt("playerLeg");
                    break;

                case BodyTypes.Head:
                    id = PlayerPrefs.GetInt("playerHead");
                    break;

                case BodyTypes.Body:
                    id = PlayerPrefs.GetInt("playeBody");
                    break;
            }
        }

        void LoadGame()
        {
            SceneManager.LoadScene("Game");
        }
    }
}