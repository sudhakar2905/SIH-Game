using UnityEngine;
using UnityEngine.UI;

public class AssetPlacer : MonoBehaviour
{
    public Transform placementArea;   // Reference to the area where assets will be placed
    public Button[] assetButtons;     // Array of buttons for selecting assets
    public GameObject[] assets;       // Array of prefabs corresponding to each button

    private GameObject selectedAsset = null; // Track the currently selected asset
    private bool isBuildMode = false;        // Track if in build mode

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
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle build mode when pressing 'E'
        if (Input.GetKeyDown(KeyCode.E))
        {
            SetBuildMode(!isBuildMode); // Toggle build mode on and off
        }

        // Place the asset when clicking on the scene in build mode
        if (isBuildMode && Input.GetMouseButtonDown(0) && selectedAsset != null)
        {
            // Raycast to determine the placement position in the scene
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Instantiate the selected asset at the hit position
                Instantiate(selectedAsset, hit.point, Quaternion.Euler(-90, 0, 0), placementArea);
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
        }
        Debug.Log("Build Mode: " + isBuildMode);
    }
}
