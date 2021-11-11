using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseMaster : MonoBehaviour//mechanics for the house building which allows for resource gatherers
{
    private TJGameController gamecontroller;
    private HouseMechanics Child;
    public GameObject ActiveBuilding, Farmer, Lumberjack, Miner;
    public int HouseCount;
    public Transform SpawnPoint;
    public bool HouseUpgraded, HealthIncreased;
    public GameObject UpgradeButton, HealthButton;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gamecontroller = gameControllerObject.GetComponent<TJGameController>();
    }

    // Update is called once per frame
    void Update()//update refrence data
    {
        if (ActiveBuilding != null && gamecontroller.HouseButtons.activeInHierarchy == true)
        {
            GameObject TargetedHouse = ActiveBuilding;
            Child = TargetedHouse.GetComponent<HouseMechanics>();
            gamecontroller.BuildingHealth.text = "Health: " + Child.Health;
            gamecontroller.BuildingInfo.text = "This building increases the population of your kingdom by " + Child.TotalPopBonus + ".";
        }

        if (HouseUpgraded == false)
        {
            UpgradeButton.SetActive(true);
        }
        else
            UpgradeButton.SetActive(false);
        if (HealthIncreased == false)
        {
            HealthButton.SetActive(false);
        }
        else
            HealthButton.SetActive(false);
    }
    public void SpawnFarmer()//spawn a farmer unit and update resources and population
    {
        if (gamecontroller.stone > 10 && gamecontroller.wood > 10 && gamecontroller.PopuationLimit > gamecontroller.CurrentPopulation)
        {
            gamecontroller.stone -= 10;
            gamecontroller.wood -= 10;
            Instantiate(Farmer, SpawnPoint);
            gamecontroller.UpdateResources();
        }
    }
    public void SpawnMiner()//spawn a miner unit and update resources and population
    {
        if (gamecontroller.food > 10 && gamecontroller.wood > 10 && gamecontroller.PopuationLimit > gamecontroller.CurrentPopulation)
        {
            gamecontroller.food -= 10;
            gamecontroller.wood -= 10;
            Instantiate(Miner, SpawnPoint);
            gamecontroller.UpdateResources();
        }
    }
    public void SpawnLumberjack()//spawn a lumberjack unit and update resources and population
    {
        if (gamecontroller.food > 10 && gamecontroller.stone > 10 && gamecontroller.PopuationLimit > gamecontroller.CurrentPopulation)
        {
            gamecontroller.food -= 10;
            gamecontroller.stone -= 10;
            Instantiate(Lumberjack, SpawnPoint);
            gamecontroller.UpdateResources();
        }
    }
    public void SellBuilding()//destroy building and reduce population max
    {
        gamecontroller.wood += 10;
        gamecontroller.stone += 10;
        gamecontroller.food += 10;
        HouseCount -= 1;
        if (HouseUpgraded == true)
        {
            HouseCount -= 1;
            HouseUpgraded = false;
        }
        Destroy(ActiveBuilding);
        gamecontroller.ClearButtons();
        gamecontroller.UpdateResources();
    }
    public void UpgradeCapcity()//increase the population max
    {
        if (HouseUpgraded == false && gamecontroller.food > 8 && gamecontroller.wood > 8 && gamecontroller.stone > 8)
        {
            gamecontroller.wood -= 8;
            gamecontroller.stone -= 8;
            gamecontroller.food -= 8;
            HouseUpgraded = true;
            Child.CapacityUp();
            gamecontroller.UpdateResources();
        }
    }
    public void IncreaseHealth()//increase building health
    {
        if (HealthIncreased == false)
        {
            Child.HealthUp();
        }
        HealthIncreased = true;
    }
    public void PopulationUpdater()
    {
        gamecontroller.HouseCount = HouseCount;
        gamecontroller.UpdatePopulation();
    }
}
