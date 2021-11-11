using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TJGameController : MonoBehaviour//primary game controller to manage various aspects of the game
{
    //data for general variables
    public int wood, stone, gold, food, WoodMax, StoneMax, GoldMax, FoodMax, BaseLimit, BonusGeneration;
    public Text WoodCount, StoneCount, GoldCount, FoodCount, PopulationCounter, BuildingHealth, BuildingInfo, Wave, GameOverText;
    public RaycastHit hit, target;
    public GameObject Buildable, BuildingRim, BuildingInfoFull, ButtonBackground, GameOverUI;
    public Vector3 HitEnd;
    public int PopuationLimit, CurrentPopulation, GatherValue, DMGValue = 0, Health = 0;
    public bool PlacingBuilding, GameOver;
    public float CollectionTimer = 3f;
    private DifficultyManager DifficultyOBJ;
    public int WaveValue;

    //data for barracks
    public GameObject BarracksButtons;

    //data for castle
    public GameObject CaslteButtons, ResearchBuildButton, CastleRime;

    //data for store house
    public int StorehouseCount;
    public GameObject SHouseButtons;

    //data for research center
    public GameObject RCButtons;
    public int PopulationBonus, GenerationBonus;
    public bool ActiveResearchBuild;

    //data for houses
    public GameObject HouseButtons;
    public int HouseCount;

    //data for watch towers
    public GameObject WTButtons;

    //data for deposit points
    public GameObject DepositButtons;
    public int LumberCount, MiningCount, MillCount;

    private int fingerID = -1;
    // Start is called before the first frame update
    void Start()//set the difficulty values based on what was changed in the main menu
    {
        Time.timeScale = 1;
        GameObject gameControllerObject = GameObject.FindWithTag("Difficulty");
        DifficultyOBJ = gameControllerObject.GetComponent<DifficultyManager>();
        if (DifficultyOBJ.DifficultyValue == 1)
        {
            Health = 1;
            DMGValue = 1;
            GatherValue = GatherValue + 2;
        }
        if (DifficultyOBJ.DifficultyValue == 3)
        {
            Health = -1;
            GatherValue = GatherValue - 2;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        UpdateResources();
        UpdatePopulation();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver == true)
        {
            GameFailed();
        }
        if (EventSystem.current.IsPointerOverGameObject(fingerID))
        {
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//fire a ray from mouse to track what the player is pointing at
        Physics.Raycast(ray, out target);
        HitEnd = (hit.point);
        Buildable.transform.position = HitEnd;
         //&& hit.transform.tag != "UI"
        if (Physics.Raycast(ray, out hit) && Input.GetButton("Fire1") && PlacingBuilding == false && hit.transform.tag != "UI")
        {
            if (hit.transform.tag == "Barracks")//if the player clicks the barracks then the game opens up the menu popups for that barracks and sets refrence data to that barracks
            {
                ClearButtons();
                BarracksButtons.SetActive(true);
                ButtonBackground.SetActive(true);
                BarracksMechanics Master = hit.collider.GetComponentInParent<BarracksMechanics>();
                BarracksSpawnPoint Child = hit.collider.GetComponent<BarracksSpawnPoint>();
                Master.ArchBut.SetActive(false);
                Master.HorseBut.SetActive(false);
                Master.TroopUnlockBut.SetActive(true);
                Master.TroopUnlock = 0;
                BuildingInfoFull.SetActive(true);
                if (Master != null)
                {
                    BuildingRim = Child.BuildingRime;
                    BuildingRim.SetActive(true);
                    Master.SpawnPoint = Child.SpawnPoint;
                    Master.ActiveBuilding = Child.Building;
                    if (Child.TroopUnlock == 1)
                    {
                        Master.TroopUnlock = 1;
                        Master.ArchBut.SetActive(true);
                    }
                    if (Child.TroopUnlock == 2)
                    {
                        Master.TroopUnlock = 2;
                        Master.HorseBut.SetActive(true);
                        Master.TroopUnlockBut.SetActive(false);
                    }
                }
            }
            if (hit.transform.tag == "Castle")//if the player clicks the castle then the game opens up the menu popups for that castle sets refrence data to that caslte
            {
                ClearButtons();
                CaslteButtons.SetActive(true);
                ButtonBackground.SetActive(true);
                ResearchBuildButton.SetActive(true);
                BuildingRim = CastleRime;
                BuildingRim.SetActive(true);
                BuildingInfoFull.SetActive(true);
                if (ActiveResearchBuild == true)
                {
                    ResearchBuildButton.SetActive(false);
                }
            }
            if (hit.transform.tag == "StoreHouse")//if the player clicks the StoreHouse then the game opens up the menu popups for that StoreHouse and sets refrence data to that StoreHouse
            {
                ClearButtons();
                SHouseButtons.SetActive(true);
                ButtonBackground.SetActive(true);
                StoreHouseMaster Master = hit.collider.GetComponentInParent<StoreHouseMaster>();
                StoreHouseMechanics Child = hit.collider.GetComponent<StoreHouseMechanics>();
                if (Master != null)
                {
                    BuildingRim = Child.BuildingRime;
                    BuildingRim.SetActive(true);
                    Master.StoreHouseUpgraded = false;
                    Master.ActiveBuilding = Child.Building;
                    BuildingInfoFull.SetActive(true);
                    if (Child.IncreasedStorage == true)
                    {
                        Master.StoreHouseUpgraded = true;
                    }
                    if (Child.IncreasedHealth == true)
                    {
                        Master.HealthIncreased = true;
                    }
                    else
                    {
                        Master.HealthIncreased = false;
                    }
                }
            }
            if (hit.transform.tag == "House")//if the player clicks the House then the game opens up the menu popups for that House and sets refrence data to that House
            {
                ClearButtons();
                HouseButtons.SetActive(true);
                ButtonBackground.SetActive(true);
                HouseMaster Master = hit.collider.GetComponentInParent<HouseMaster>();
                HouseMechanics Child = hit.collider.GetComponent<HouseMechanics>();
                if (Master != null)
                {
                    BuildingRim = Child.BuildingRime;
                    BuildingRim.SetActive(true);
                    Master.HouseUpgraded = false;
                    Master.ActiveBuilding = Child.Building;
                    BuildingInfoFull.SetActive(true);
                    Master.SpawnPoint = Child.SpawnPoint;
                    if (Child.IncreasedStorage == true)
                    {
                        Master.HouseUpgraded = true;
                    }
                    if (Child.IncreasedHealth == true)
                    {
                        Master.HealthIncreased = true;
                    }
                    else
                    {
                        Master.HealthIncreased = false;
                    }
                }
            }
            if (hit.transform.tag == "ResearchCenter")//if the player clicks the ResearchCenter then the game opens up the menu popups for that ResearchCenter and sets refrence data to that ResearchCenter
            {
                ClearButtons();
                RCButtons.SetActive(true);
                ButtonBackground.SetActive(true);
                ResearchMaster Master = hit.collider.GetComponentInParent<ResearchMaster>();
                ResearchMechanics Child = hit.collider.GetComponent<ResearchMechanics>();
                if (Master != null)
                {
                    BuildingRim = Child.BuildingRime;
                    BuildingRim.SetActive(true);
                    Master.ActiveBuilding = Child.Building;
                    BuildingInfoFull.SetActive(true);
                }
            }
            if (hit.transform.tag == "WatchTower")//if the player clicks the WatchTower then the game opens up the menu popups for that WatchTower and sets refrence data to that WatchTower
            {
                ClearButtons();
                WTButtons.SetActive(true);
                ButtonBackground.SetActive(true);
                WatchTowerMaster Master = hit.collider.GetComponentInParent<WatchTowerMaster>();
                WatchTowerMechanics Child = hit.collider.GetComponent<WatchTowerMechanics>();
                if (Master != null)
                {
                    BuildingRim = Child.BuildingRime;
                    BuildingRim.SetActive(true);
                    Master.ActiveBuilding = Child.Building;
                    BuildingInfoFull.SetActive(true);
                }
            }
            if (hit.transform.tag == "Quary" || hit.transform.tag == "LumberYard" || hit.transform.tag == "Mill")//if the player clicks a deposit point then the game opens up the menu popups for that deposit point and sets refrence data to that deposit point
            {
                ClearButtons();
                DepositButtons.SetActive(true);
                ButtonBackground.SetActive(true);
                DepositPointMaster Master = hit.collider.GetComponentInParent<DepositPointMaster>();
                DepositPointMechanics Child = hit.collider.GetComponent<DepositPointMechanics>();
                Master.LumberActive = false;
                Master.MiningActive = false;
                Master.MillActive = false;
                Master.StoreHouseUpgraded = false;
                if (Master != null)
                {
                    BuildingRim = Child.BuildingRime;
                    BuildingRim.SetActive(true);
                    Master.ActiveBuilding = Child.Building;
                    BuildingInfoFull.SetActive(true);
                    if (Child.Wood == true)
                    {
                        Master.LumberActive = true;
                    }
                    if (Child.Stone == true)
                    {
                        Master.MiningActive = true;
                    }
                    if (Child.Food == true)
                    {
                        Master.MillActive = true;
                    }
                    if (Child.IncreasedStorage == true)
                    {
                        Master.StoreHouseUpgraded = true;
                    }
                }
            }
        }
        if (Input.GetButton("Fire2") && PlacingBuilding == false)// if the player uses right click then the menu popups are hidden
        {
            ClearButtons();
        }
    }
    public void ClearButtons()//hides all menu popups when activated
    {
        BarracksButtons.SetActive(false);
        CaslteButtons.SetActive(false);
        SHouseButtons.SetActive(false);
        HouseButtons.SetActive(false);
        RCButtons.SetActive(false);
        WTButtons.SetActive(false);
        BuildingInfoFull.SetActive(false);
        ButtonBackground.SetActive(false);
        DepositButtons.SetActive(false);
        if (BuildingRim != null)
        {
            BuildingRim.SetActive(false);
        }
    }
    public void GameFailed()//when you lose the game ends and tells you the wave counter
    {
        GameOverText.text = "The enemy was able to destroy your castle. You made it to " + WaveValue + "Waves, Perhaps try a lower difficulty";
        Time.timeScale = 0;
        GameOverUI.SetActive(true);
    }
    public void UpdateResources()//update the players resource counters including the resource limit and current amount the player has
    {
        WoodMax = (StorehouseCount * 200) + BaseLimit + (200 * LumberCount);
        StoneMax = (StorehouseCount * 200) + BaseLimit + (200 * MiningCount);
        GoldMax = (StorehouseCount * 200) + BaseLimit + (200 * MiningCount);
        FoodMax = (StorehouseCount * 200) + BaseLimit + (200 * MillCount);
        if (wood > WoodMax)
        {
            wood = WoodMax;
        }
        if (stone > StoneMax)
        {
            stone = StoneMax;
        }
        if (gold > GoldMax)
        {
            gold = GoldMax;
        }
        if (food > FoodMax)
        {
            food = FoodMax;
        }
        WoodCount.text = "" + wood + "/" + WoodMax;
        StoneCount.text = "" + stone + "/" + StoneMax;
        GoldCount.text = "" + gold + "/" + GoldMax;
        FoodCount.text = "" + food + "/" + FoodMax;
    }
    public void UpdatePopulation()//update the population counter including the max population and limit
    {
        PopuationLimit = (HouseCount + 1) * (5 + PopulationBonus) + 5;
        PopulationCounter.text = "Population: " + CurrentPopulation + "/" + PopuationLimit;
    }
    public void UpdateWave()//update wave text with the current wave
    {
        Wave.text = "Wave: " + WaveValue;
    }
}
