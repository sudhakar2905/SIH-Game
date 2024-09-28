using UnityEngine;
using UnityEngine.UI;

public class AssetPlacer : MonoBehaviour
{
    public Transform placementArea;   // Reference to the area where assets will be placed
    public Button[] assetButtons;     // Array of buttons for selecting assets
    public GameObject[] assets;       // Array of prefabs corresponding to each button
    public GameObject[] buildingPrefabs; // Array of building prefabs for key-based selection

    private GameObject selectedAsset = null; // Track the currently selected asset
    private GameObject selectedBuilding = null; // Track the currently selected building
    private bool isBuildMode = false;        // Track if in build mode
    private bool buildingSelected = false;   // Track if a building has been selected

    void Start()
    {
        // Assign button click events
        for (int i = 0; i < assetButtons.Length; i++)
        {
            int index = i; // Capture index to use in the button click event
            assetButtons[i].onClick.AddListener(() => SelectAsset(index));
        }
    }

    // Function to select an asset based on button click
    void SelectAsset(int index)
    {
        selectedAsset = assets[index]; // Set the selected asset based on the clicked button
        Debug.Log("Selected Asset: " + selectedAsset.name);
        isBuildMode = true; // Enable build mode after selecting an asset
        buildingSelected = false; // Deselect any selected building
    }

    // Function to select a building using number keys
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
                buildingSelected = true; // Mark a building as selected
                isBuildMode = true; // Enable build mode
                selectedAsset = null; // Deselect any selected asset
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle build mode when pressing 'E'
        if (Input.GetKeyDown(KeyCode.E))
        {
            SetBuildMode(!isBuildMode); // Toggle build mode on and off
        }

        // Select a building using number keys
        SelectBuilding();

        // Place the selected asset or building when clicking on the scene in build mode
        if (isBuildMode && Input.GetMouseButtonDown(0))
        {
            if (selectedAsset != null)
            {
                // Raycast to determine the placement position in the scene
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    // Instantiate the selected asset at the hit position
                    Instantiate(selectedAsset, hit.point, Quaternion.Euler(-90, 0, 0), placementArea);
                }
            }
            else if (buildingSelected && selectedBuilding != null)
            {
                // Raycast to determine the placement position in the scene
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    // Set the position and activate the selected building for placement
                    selectedBuilding.transform.position = hit.point;
                    selectedBuilding.SetActive(true);
                    selectedBuilding = null; // Deselect the building after placement
                    buildingSelected = false; // Disable building selection
                }
            }
        }
    }

    // Function to toggle build mode
    public void SetBuildMode(bool enable)
    {
        isBuildMode = enable;
        if (!enable)
        {
            selectedAsset = null; // Deselect the asset when exiting build mode
            if (selectedBuilding != null)
            {
                Destroy(selectedBuilding); // Destroy the preview of selected building when exiting build mode
            }
            buildingSelected = false; // Disable building selection when exiting build mode
        }
        Debug.Log("Build Mode: " + isBuildMode);
    }
}
