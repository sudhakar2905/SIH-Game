using UnityEngine;
using UnityEngine.UI;

public class MovementTutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel; // Reference to the entire tutorial panel
    public Text tutorialText;        // Reference to the text UI element to provide instructions

    private bool movedUp = false;
    private bool movedLeft = false;
    private bool movedDown = false;
    private bool movedRight = false;

    void Start()
    {
        // Show the tutorial panel at the start
        tutorialPanel.SetActive(true);
        tutorialText.text = "Press 'W' to move Up";
    }

    void Update()
    {
        // Check if player has moved in each direction and update the tutorial accordingly
        if (!movedUp)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                movedUp = true;
                tutorialText.text = "Good! Now press 'A' to move Left";
            }
        }
        else if (!movedLeft)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                movedLeft = true;
                tutorialText.text = "Great! Now press 'S' to move Down";
            }
        }
        else if (!movedDown)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                movedDown = true;
                tutorialText.text = "Nice! Now press 'D' to move Right";
            }
        }
        else if (!movedRight)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                movedRight = true;
                tutorialText.text = "Awesome! You have learned how to move!";
                Invoke("HideTutorial", 2f); // Hide the tutorial after 2 seconds
            }
        }
    }

    void HideTutorial()
    {
        // Hide the tutorial panel
        tutorialPanel.SetActive(false);
    }
}
