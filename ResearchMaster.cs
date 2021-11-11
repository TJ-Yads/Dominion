using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchMaster : MonoBehaviour//mechanics attached to the research facility in game
{
    private TJGameController gamecontroller;
    public int PopulationBonus, GenerationBonus;
    public GameObject ActiveBuilding;
    public int IncPop, IncGen, ToHe, ToDmg, GenTime;
    private ResearchMechanics Child;
    public Text IncPopCost, IncGenCost, ToHeCost, ToDmgCost, GenTimeCost;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gamecontroller = gameControllerObject.GetComponent<TJGameController>();
    }

    // Update is called once per frame
    void Update()//update refrence data
    {
        if (ActiveBuilding != null && gamecontroller.RCButtons.activeInHierarchy == true)
        {
            GameObject TargetedHouse = ActiveBuilding;
            Child = TargetedHouse.GetComponent<ResearchMechanics>();
            gamecontroller.BuildingHealth.text = "Health: " + Child.Health;
            gamecontroller.BuildingInfo.text = "This building allows for various upgrades to your kingdom.";
        }
        gamecontroller.PopulationBonus = PopulationBonus;
        gamecontroller.GenerationBonus = GenerationBonus;
        IncPopCost.text = "G: -" + IncPop;
        IncGenCost.text = "G: -" + IncGen;
        ToHeCost.text = "F: -" + ToHe;
        ToDmgCost.text = "W: -" + ToDmg;
        GenTimeCost.text = "G: -" + GenTime;
    }
    public void IncreasePopulation()//increase population max and increase upgrade cost
    {
        if (gamecontroller.gold > IncPop)
        {
            gamecontroller.gold -= IncPop;
            PopulationBonus += 1;
            IncPop = IncPop + IncPop - 20;
            gamecontroller.UpdateResources();
            gamecontroller.UpdatePopulation();
        }
    }
    public void IncreaseGeneration()//increase gold generation and upgrade cost
    {
        if (gamecontroller.gold > IncGen)
        {
            gamecontroller.gold -= IncGen;
            GenerationBonus += 1;
            IncGen = IncGen + IncGen - 15;
            gamecontroller.UpdateResources();
        }
    }
    public void TroopHealth()//increase troop health and upgrade cost
    {
        if (gamecontroller.food > ToHe)
        {
            gamecontroller.Health += 1;
            gamecontroller.food -= ToHe;
            ToHe = ToHe + ToHe - 5;
            gamecontroller.UpdateResources();
        }
    }
    public void TroopDamage()//increase troop damage and upgrade cost
    {
        if (gamecontroller.wood > ToDmg)
        {
            gamecontroller.DMGValue += 1;
            gamecontroller.wood -= ToDmg;
            ToDmg = ToDmg + ToDmg - 5;
            gamecontroller.UpdateResources();
        }
    }
    public void GenerationTime()//reduce the time to gain gold and increase upgrade cost
    {
        if (gamecontroller.gold > GenTime)
        {
            gamecontroller.gold -= GenTime;
            gamecontroller.CollectionTimer -= .3f;
            GenTime = GenTime + GenTime - 10;
            gamecontroller.UpdateResources();
        }
    }
    public void SellBuilding()//destroy building and gain resources equal to half the cost
    {
        gamecontroller.wood += 25;
        gamecontroller.stone += 25;
        gamecontroller.gold += 25;
        gamecontroller.food += 50;
        gamecontroller.ActiveResearchBuild = false;
        Destroy(ActiveBuilding);
        gamecontroller.ClearButtons();
        gamecontroller.UpdateResources();
    }
}
