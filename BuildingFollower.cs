using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFollower : MonoBehaviour//used when placing a building-- buildings follow the mouse and change color based on placement
{
    private TJGameController gamecontroller;
    public GameObject BuildingEmpty, Building, BuildingGreen, BuildingRed;
    public Transform ObjSize;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gamecontroller = gameControllerObject.GetComponent<TJGameController>();
        gamecontroller.PlacingBuilding = true;
    }

    // Update is called once per frame
    void Update()//while attempting to place the building it will follow the mouse and search for the floor
    {
        BuildingEmpty.transform.position = gamecontroller.HitEnd;
        if (Input.GetButtonUp("Fire1") && gamecontroller.hit.transform.tag == "Floor")//if the building is on the floor then it will build and deduct resource
        {
            gamecontroller.PlacingBuilding = false;
            Destroy(gameObject);
            Instantiate(Building, gamecontroller.Buildable.transform);
        }
        if (Input.GetButtonUp("Fire2"))//if the player cancels the build then it will be deleted
        {
            gamecontroller.PlacingBuilding = false;
            Destroy(gameObject);
        }
        if (gamecontroller.hit.transform.tag != "Floor")//when the cursor is not on the floor the building cannot be place and the building turns red to notify the player
        {
            BuildingGreen.SetActive(false);
            BuildingRed.SetActive(true);
        }
        else
        {
            BuildingGreen.SetActive(true);
            BuildingRed.SetActive(false);
        }
    }
}
