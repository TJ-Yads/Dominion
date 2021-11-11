using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleMechanics : MonoBehaviour//mechanics for the castle
{
    public GameObject BuildableEmpty, TextPopUp;
    private TJGameController gamecontroller;
    private ResourceCost CostList;
    public int Health;
    public Text BuildingHealth;
    private DifficultyManager DifficultyOBJ;
    // Start is called before the first frame update
    void Start()
    {
        GameObject DiffcultlyOject = GameObject.FindWithTag("Difficulty");
        DifficultyOBJ = DiffcultlyOject.GetComponent<DifficultyManager>();
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gamecontroller = gameControllerObject.GetComponent<TJGameController>();
        CostList = gameControllerObject.GetComponent<ResourceCost>();
        if (DifficultyOBJ.DifficultyValue == 3)
        {
            Health = Health - 5;
        }
    }

    // Update is called once per frame
    void Update()//update refrence data
    {
        BuildingHealth.text = "Castle health: " + Health;
        if (gamecontroller.CaslteButtons.activeInHierarchy == true)
        {
            gamecontroller.BuildingHealth.text = "Health: " + Health;
            gamecontroller.BuildingInfo.text = "This building allows for the construction of other buildings.";
        }
        if (Health <= 0)
        {
            gamecontroller.GameOver = true;
        }
    }
    public void BuildBarracks(GameObject BuildingName)//build a barracks building if the player has the resources-- make troops
    {
        CostList.BarracksCost();//find cost
        if (gamecontroller.wood >= CostList.Wood && gamecontroller.stone >= CostList.Stone && gamecontroller.gold >= CostList.Gold && gamecontroller.food >= CostList.Food)//check cost and resource count
        {
            //reduce player resources
            gamecontroller.wood -= CostList.Wood;
            gamecontroller.stone -= CostList.Stone;
            gamecontroller.gold -= CostList.Gold;
            gamecontroller.food -= CostList.Food;

            BuildableEmpty = BuildingName;//set name of building to barracks
            Instantiate(BuildableEmpty, gamecontroller.Buildable.transform);//create the barracks and attach it to the player mouse
            gamecontroller.ClearButtons();//clear menu popups
            gamecontroller.UpdateResources();// reduce resources
        }
    }
    public void BuildFarm(GameObject BuildingName)//build a farm-- gain food
    {
        CostList.FarmCost();
        if (gamecontroller.wood >= CostList.Wood && gamecontroller.stone >= CostList.Stone && gamecontroller.gold >= CostList.Gold && gamecontroller.food >= CostList.Food)
        {
            gamecontroller.wood -= CostList.Wood;
            gamecontroller.stone -= CostList.Stone;
            gamecontroller.gold -= CostList.Gold;
            gamecontroller.food -= CostList.Food;
            BuildableEmpty = BuildingName;
            Instantiate(BuildableEmpty, gamecontroller.Buildable.transform);
            gamecontroller.ClearButtons();
            gamecontroller.UpdateResources();
        }
    }
    public void BuildMill(GameObject BuildingName)//build a mill--increase food storage
    {
        CostList.Mill();
        if (gamecontroller.wood >= CostList.Wood && gamecontroller.stone >= CostList.Stone && gamecontroller.gold >= CostList.Gold && gamecontroller.food >= CostList.Food)
        {
            gamecontroller.wood -= CostList.Wood;
            gamecontroller.stone -= CostList.Stone;
            gamecontroller.gold -= CostList.Gold;
            gamecontroller.food -= CostList.Food;
            BuildableEmpty = BuildingName;
            Instantiate(BuildableEmpty, gamecontroller.Buildable.transform);
            gamecontroller.ClearButtons();
            gamecontroller.UpdateResources();
        }
    }
    public void BuildRC(GameObject BuildingName)//build a research center-- upgrade various values
    {
        CostList.ResearchCenterCost();
        if (gamecontroller.wood >= CostList.Wood && gamecontroller.stone >= CostList.Stone && gamecontroller.gold >= CostList.Gold && gamecontroller.food >= CostList.Food)
        {
            gamecontroller.wood -= CostList.Wood;
            gamecontroller.stone -= CostList.Stone;
            gamecontroller.gold -= CostList.Gold;
            gamecontroller.food -= CostList.Food;
            BuildableEmpty = BuildingName;
            Instantiate(BuildableEmpty, gamecontroller.Buildable.transform);
            gamecontroller.ClearButtons();
            gamecontroller.UpdateResources();
        }
    }
    public void BuildLY(GameObject BuildingName)// build a lumber yard-- wood storage
    {
        CostList.LumberYardCost();
        if (gamecontroller.wood >= CostList.Wood && gamecontroller.stone >= CostList.Stone && gamecontroller.gold >= CostList.Gold && gamecontroller.food >= CostList.Food)
        {
            gamecontroller.wood -= CostList.Wood;
            gamecontroller.stone -= CostList.Stone;
            gamecontroller.gold -= CostList.Gold;
            gamecontroller.food -= CostList.Food;
            BuildableEmpty = BuildingName;
            Instantiate(BuildableEmpty, gamecontroller.Buildable.transform);
            gamecontroller.ClearButtons();
            gamecontroller.UpdateResources();
        }
    }
    public void BuildMC(GameObject BuildingName)// build a mining camp-- stone storage
    {
        CostList.MiningCampCost();
        if (gamecontroller.wood >= CostList.Wood && gamecontroller.stone >= CostList.Stone && gamecontroller.gold >= CostList.Gold && gamecontroller.food >= CostList.Food)
        {
            gamecontroller.wood -= CostList.Wood;
            gamecontroller.stone -= CostList.Stone;
            gamecontroller.gold -= CostList.Gold;
            gamecontroller.food -= CostList.Food;
            BuildableEmpty = BuildingName;
            Instantiate(BuildableEmpty, gamecontroller.Buildable.transform);
            gamecontroller.ClearButtons();
            gamecontroller.UpdateResources();
        }
    }
    public void BuildWT(GameObject BuildingName)//build a watch tower-- defense building
    {
        CostList.WatchtowerCost();
        if (gamecontroller.wood >= CostList.Wood && gamecontroller.stone >= CostList.Stone && gamecontroller.gold >= CostList.Gold && gamecontroller.food >= CostList.Food)
        {
            gamecontroller.wood -= CostList.Wood;
            gamecontroller.stone -= CostList.Stone;
            gamecontroller.gold -= CostList.Gold;
            gamecontroller.food -= CostList.Food;
            BuildableEmpty = BuildingName;
            Instantiate(BuildableEmpty, gamecontroller.Buildable.transform);
            gamecontroller.ClearButtons();
            gamecontroller.UpdateResources();
        }
    }
    public void BuildHouse(GameObject BuildingName)//build a house-- population and gatherers
    {
        CostList.HouseCost();
        if (gamecontroller.wood >= CostList.Wood && gamecontroller.stone >= CostList.Stone && gamecontroller.gold >= CostList.Gold && gamecontroller.food >= CostList.Food)
        {
            gamecontroller.wood -= CostList.Wood;
            gamecontroller.stone -= CostList.Stone;
            gamecontroller.gold -= CostList.Gold;
            gamecontroller.food -= CostList.Food;
            BuildableEmpty = BuildingName;
            Instantiate(BuildableEmpty, gamecontroller.Buildable.transform);
            gamecontroller.ClearButtons();
            gamecontroller.UpdateResources();
        }
    }
}
