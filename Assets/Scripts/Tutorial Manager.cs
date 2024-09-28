using UnityEngine;
using TMPro;

public class MovementTutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel; // Reference to the entire tutorial panel
    public TextMeshProUGUI tutorialText; // Reference to the TextMeshPro UI element to provide instructions

    private bool movedUp = false;
    private bool movedLeft = false;
    private bool movedDown = false;
    private bool movedRight = false;
    private bool pressedE = false;
    private bool buildingSelected = false;
    private bool buildingPlaced = false;

    public GameObject[] buildingPrefabs; // Array of building prefabs
    private GameObject selectedBuilding;

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
                tutorialText.text = "Awesome! Now press 'E' to select a building.";
            }
        }
        else if (!pressedE)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pressedE = true;
                tutorialText.text = "Use number keys (1, 2, 3, etc.) to select a building.";
            }
        }
        else if (!buildingSelected)
        {
            SelectBuilding();
        }
        else if (!buildingPlaced)
        {
            if (Input.GetMouseButtonDown(0) && selectedBuilding != null)
            {
                PlaceBuilding();
                buildingPlaced = true;
                tutorialText.text = "Great! You have placed the building!";
                Invoke("HideTutorial", 2f); // Hide the tutorial after 2 seconds
            }
        }
    }

    void SelectBuilding()
    {
        // Allow player to select a building using number keys
        for (int i = 0; i < buildingPrefabs.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                if (selectedBuilding != null)
                {
                    Destroy(selectedBuilding); // Destroy previously selected building if any
                }
                selectedBuilding = Instantiate(buildingPrefabs[i]);
                selectedBuilding.SetActive(false); // Hide initially until placement
                buildingSelected = true;
                tutorialText.text = "Good! Now click on the ground to place the building.";
                break;
            }
        }
    }

    void PlaceBuilding()
    {
        // Place the selected building at the mouse position
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            selectedBuilding.transform.position = hit.point;
            selectedBuilding.SetActive(true);
        }
    }

    void HideTutorial()
    {
        // Hide the tutorial panel
        tutorialPanel.SetActive(false);
    }
}
