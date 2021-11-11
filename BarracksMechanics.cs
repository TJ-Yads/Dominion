using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarracksMechanics : MonoBehaviour//the barracks building allows the player to spawn various troops for protection
{
    public GameObject Soldier, Archer, Horsemen, ArchBut, HorseBut, TroopUnlockBut, ActiveBuilding;
    public Transform SpawnPoint;
    private TJGameController gamecontroller;
    private BarracksSpawnPoint Child;
    public int TroopUnlock;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gamecontroller = gameControllerObject.GetComponent<TJGameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ActiveBuilding != null && gamecontroller.BarracksButtons.activeInHierarchy == true)//when clicked this building will display its tooltip on the bottom
        {
            GameObject TargetedHouse = ActiveBuilding;
            Child = TargetedHouse.GetComponent<BarracksSpawnPoint>();
            gamecontroller.BuildingHealth.text = "Health: " + Child.Health;
            gamecontroller.BuildingInfo.text = "This building allows you to train and upgrade various troops.";
        }
        if (TroopUnlock == 1)
        {
           ArchBut.SetActive(true);
        }
        if (TroopUnlock == 2)
        {
           ArchBut.SetActive(true);
           HorseBut.SetActive(true);
           TroopUnlockBut.SetActive(false);
        }
        if (TroopUnlock == 0)
        {
           ArchBut.SetActive(false);
           HorseBut.SetActive(false);
        }
    }
    public void UnlockTroops()//unlock new troops to spawn from the building
    {
        if (gamecontroller.gold > 50)
        {
            gamecontroller.gold -= 50;
            gamecontroller.UpdateResources();
            if (Child.TroopUnlock < 2)
            {
                Child.TroopUnlock += 1;
                Child.TroopUnlocked();
            }
        }
    }
    //spawn methods: each on will check resources and then make the purchase if you have enough and deduct resources from game controller
    public void SpawnSoldiers()//spawn a footsolider-- basic solider with melee combat
    {
        if (gamecontroller.food > 15 && gamecontroller.wood > 15 && gamecontroller.PopuationLimit > gamecontroller.CurrentPopulation)
        {
            gamecontroller.food -= 15;
            gamecontroller.wood -= 15;
            Instantiate(Soldier, SpawnPoint);
            gamecontroller.UpdateResources();
        }
    }
    public void SpawnArchers()//spawn an archer-- ranged fighter
    {
        if (gamecontroller.food > 35 && gamecontroller.wood > 35 && gamecontroller.PopuationLimit > gamecontroller.CurrentPopulation)
        {
            gamecontroller.food -= 35;
            gamecontroller.wood -= 35;
            Instantiate(Archer, SpawnPoint);
            gamecontroller.UpdateResources();
        }
    }
    public void SpawnHorsemen()//spawn a horsemen-- direct upgrade of footsoldier
    {
        if (gamecontroller.food > 60 && gamecontroller.wood > 60 && gamecontroller.gold > 30 && gamecontroller.PopuationLimit > gamecontroller.CurrentPopulation)
        {
            gamecontroller.food -= 30;
            gamecontroller.wood -= 30;
            gamecontroller.gold -= 30;
            Instantiate(Horsemen, SpawnPoint);
            gamecontroller.UpdateResources();
        }
    }
    public void SellBuilding()//remove the building and increase the resource counter by half the building cost
    {
        gamecontroller.wood += 12;
        gamecontroller.stone += 18;
        gamecontroller.gold += 12;
        gamecontroller.food += 7;
        Destroy(ActiveBuilding);
        gamecontroller.ClearButtons();
        gamecontroller.UpdateResources();
    }
}
