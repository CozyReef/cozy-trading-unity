using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using Newtonsoft.Json;

public class ContractManager : MonoBehaviour {
    public static ContractManager Instance { get; private set; }
    public bool isInitialized = false;
    public TextAsset ContractConfig;
    
    private Dictionary<string, string> contractNameToAddress = new Dictionary<string, string>();
    private Dictionary<string, string> contractNameToAbi = new Dictionary<string, string>();
    private Dictionary<string, Contract> contractNameToContract = new Dictionary<string, Contract>();

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }
    }

    void Update() {
        if (isInitialized || !ThirdwebManager.Instance.isInitialized || !ContractConfig) {
            return;
        }
        
        ThirdwebSDK sdk = ThirdwebManager.Instance.SDK;
        string jsonString = ContractConfig.text;
        ContractJson.ContractSettings contractSettings = JsonConvert.DeserializeObject<ContractJson.ContractSettings>(jsonString);
        for (int i = 0; i < contractSettings.contracts.Count; i++) {
            string contractName = contractSettings.contracts[i].name;
            contractNameToAddress.Add(contractName, contractSettings.contracts[i].address);
            // if (contractSettings.environment == "development") {
                contractNameToAbi.Add(contractName, Resources.Load<TextAsset>($"ContractABIs/{contractName}").text);
                contractNameToContract.Add(contractName, sdk.GetContract(contractNameToAddress[contractName], contractNameToAbi[contractName]));
            // } else {
            //     contractNameToContract.Add(contractName, sdk.GetContract(contractNameToAddress[contractName]));
            // }
        }

        Debug.Log("ContractManager initialized");
        isInitialized = true;
    }

    public Contract GetContract(string contractName) {
        return contractNameToContract[contractName];
    }
}
