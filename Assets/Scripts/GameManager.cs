using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;

public class Skill
{
    public int Level { get; set; }
    public int XP { get; set; }
}

public class GameManager : MonoBehaviour
{
    ThirdwebSDK sdk;
    string playerAddress;

    [Header("Contracts")]
    public TextAsset CozyTradingPlayerABI;
    private Contract CozyTradingPlayer;
    public string CozyTradingPlayerAddress;

    public TextAsset CozyTradingCampsiteABI;
    private Contract CozyTradingCampsite;
    public string CozyTradingCampsiteAddress;

    public TextAsset CozyTradingResourcesABI;
    private Contract CozyTradingResources;
    public string CozyTradingResourcesAddress;

    [Header("Resources")]
    public int totalResources;
    public int totalPlayerResources;
    public int playerCargoSize;
    // TODO: write solidity function to get how many different resources we have
    public int typesOfResources = 3; // 0 - copper, 1 - tin, 2 - iron (index is the type)
    int[] playerResources = new int[3];
   
    [Header("Pebbles")]
    public int playerPebbles;

    [Header("Skills")]
    // TODO: write solidity function to get how many different skills we have
    public int typesOfSkills = 4;
    Skill[] playerSkills = new Skill[4]; // 0 - mining, 1 - smithing, 2 - woodcutting, 3 - crafting (index is the type)


    // Start is called before the first frame update
    async void Start()
    {
        sdk = ThirdwebManager.Instance.SDK;
        Debug.Log(sdk);
        CozyTradingPlayer = sdk.GetContract(CozyTradingPlayerAddress, CozyTradingPlayerABI.text);
        CozyTradingCampsite = sdk.GetContract(CozyTradingCampsiteAddress, CozyTradingCampsiteABI.text);
        CozyTradingResources = sdk.GetContract(CozyTradingResourcesAddress, CozyTradingResourcesABI.text);
        playerAddress = await sdk.wallet.GetAddress();
        totalResources = await CozyTradingResources.Read<int>("getTotalNumberOfResources");
        InitializeOres();
        GetPlayerResources();
        GetPlayerSkillInfo();
        GetPlayerPebbles();
        GetCargoSize();
        //For testing
        GetResourceInfo(1);
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerResources();
        GetPlayerSkillInfo();
        GetPlayerPebbles();
        GetCargoSize();
    }

    async void InitializeOres()
    {
        for (int i = 0; i < totalResources; i++)
        {
            GameObject Ore = GameObject.FindGameObjectsWithTag("Ore")[i];
            Ore oreScript = Ore.GetComponent<Ore>();
            int oreID = oreScript.resourceId;
            bool isCollected = await CozyTradingCampsite.Read<bool>("isResourceCollectedByPlayer", oreID, playerAddress);
            if (isCollected)
            {
                oreScript.SetCollected();
            }
            
        }
    }

    public async void Collect(int resourceId)
    {
        try
        {
            //TODO: (solidity) allow collect if cargo space is enough
            List<int> resourceInfo = await CozyTradingResources.Read<List<int>>("getResourceInfo", resourceId);
            int requiredLevel = resourceInfo[1];
            int skillType = resourceInfo[2];
            Debug.Log($"requiredLevel {requiredLevel}");
            if (requiredLevel > playerSkills[skillType].Level)
            {
                Debug.Log($"Required lvl {requiredLevel}, player lvl {playerSkills[skillType].Level}");
                return;
            }
            if (totalPlayerResources + 1 > playerCargoSize)
            {
                Debug.Log($"Cargo is full. Cargo size {playerCargoSize}");
                return;
            }
            Debug.Log($"Collecting...");
            TransactionResult result = await CozyTradingCampsite.Write("collect", resourceId);
            if (result.receipt.status == 1)
            {
                Debug.Log($"Collected {resourceId}");
            }
        }
        catch (Nethereum.Contracts.SmartContractCustomErrorRevertException e)
        {
            Debug.Log(e);

        }
    }

    //TODO: sell ores for pebbles ( copper - 1 peb, tin - 2 peb, iron - 5 peb)
    //public async void SellOres()
    //{

    //}

    //TODO: with the backpack cargo size will increase from 10 to 20. Backpack will cost 10 peb
    //public async void BuyBackpack()
    //{

    //}


    // -------------- Getters --------------

    public async void GetPlayerResources()
    {
        try
        {
            for (int i = 0; i < typesOfResources; i++)
            {
                int amountOfResource = await CozyTradingPlayer.Read<int>("getPlayerCargoResources", playerAddress, i);
                playerResources[i] = amountOfResource;
                totalPlayerResources = await CozyTradingPlayer.Read<int>("getPlayerCargoLoad", playerAddress);
            }
            Debug.Log($"In the cargo: Copper: {playerResources[0]}, Tin: {playerResources[1]}, Iron: {playerResources[2]}");
            Debug.Log($"Total in Cargo: {totalPlayerResources}");

        }
        catch (Nethereum.Contracts.SmartContractCustomErrorRevertException e)
        {
            Debug.Log(e);

        }

    }

    public async void GetPlayerSkillInfo()
    {
        try
        {
            for (int i = 0; i < typesOfSkills; i++)
            {
                List<int> skillInfo = await CozyTradingPlayer.Read<List<int>>("getPlayerSkill", playerAddress, i);
                playerSkills[i] = new Skill();
                playerSkills[i].Level = skillInfo[0];
                playerSkills[i].XP = skillInfo[1];

            }
            Debug.Log($"Mining: {playerSkills[0].Level} lvl, {playerSkills[0].XP} xp");
            Debug.Log($"Smithing: {playerSkills[1].Level} lvl, {playerSkills[1].XP} xp");
            Debug.Log($"Woodcutting: {playerSkills[2].Level} lvl, {playerSkills[2].XP} xp");
            Debug.Log($"Crafting: {playerSkills[3].Level} lvl, {playerSkills[3].XP} xp");

        }
        catch (Nethereum.Contracts.SmartContractCustomErrorRevertException e)
        {
            Debug.Log(e);

        }

    }

    public async void GetPlayerPebbles()
    {
        try
        {
            playerPebbles = await CozyTradingPlayer.Read<int>("getPlayerPebbles", playerAddress);
            Debug.Log($"Player pebbles: {playerPebbles}");
        }
        catch (Nethereum.Contracts.SmartContractCustomErrorRevertException e)
        {
            Debug.Log(e);

        }
    }


    public async void GetCargoSize()
    {
        try
        {
            playerCargoSize = await CozyTradingPlayer.Read<int>("getPlayerCargoSize", playerAddress);
            Debug.Log($"Player cargo size: {playerCargoSize}");
        }
        catch (Nethereum.Contracts.SmartContractCustomErrorRevertException e)
        {
            Debug.Log(e);

        }
    }

    // NOTE: use this function to show the user required skill type, required level, how many xp user gets
    //       also to check if the user can collect the resource (e.g. IRON requires mining lvl 10)
    //       Q: how to return multiple values from async function?
    public async void GetResourceInfo(int resourceId)
    {
        List<int> resourceInfo = await CozyTradingResources.Read<List<int>>("getResourceInfo", resourceId);
        int resourceType = resourceInfo[0];
        int requiredLevel = resourceInfo[1];
        int skillType = resourceInfo[2];
        int xp = resourceInfo[3];
        Debug.Log($"Resource {resourceId} Info: type {resourceType}, requiredLevel {requiredLevel}, skill {skillType}, xp {xp}");
        
    }

    }
