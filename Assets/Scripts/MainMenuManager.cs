using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using UnityEngine.SceneManagement;
using TMPro;
using Newtonsoft.Json;

public class MainMenuManager : MonoBehaviour {
    ThirdwebSDK sdk;

    public TextAsset ContractConfig;
    private Dictionary<string, string> contractNameToAddress = new Dictionary<string, string>();
    private Dictionary<string, string> contractNameToAbi = new Dictionary<string, string>();
    private Contract CozyTradingPlayer;

    public GameObject activateAccountButton;

    async void Start() {
        sdk = ThirdwebManager.Instance.SDK;
    }

    public async void OnConnectWallet() {
        // TODO: move this into it's own singleton that have access to all contracts
        string jsonString = ContractConfig.text;
        ContractJson.Contracts contracts = JsonConvert.DeserializeObject<ContractJson.Contracts>(jsonString);
        for (int i = 0; i < contracts.contracts.Count; i++) {
            contractNameToAddress.Add(contracts.contracts[i].name, contracts.contracts[i].address);
            contractNameToAbi.Add(contracts.contracts[i].name, Resources.Load<TextAsset>($"ContractABIs/{contracts.contracts[i].name}").text);
        }
        Debug.Log("Address: " + contractNameToAddress["CozyTradingPlayer"]);
        CozyTradingPlayer = sdk.GetContract(contractNameToAddress["CozyTradingPlayer"], contractNameToAbi["CozyTradingPlayer"]);

        var address = await sdk.wallet.GetAddress();
        var data = await CozyTradingPlayer.Read<bool>("isActivated", address);
        Debug.Log(data);
        Debug.Log(address);
        if (data == true) {
            SceneManager.LoadSceneAsync("GamePlayScene");
        } else {
            activateAccountButton.SetActive(true);
        }
    }

    public async void OnActivateAccount() {
        try {
            TransactionResult result = await CozyTradingPlayer.Write("activatePlayer", new TransactionRequest() { gasPrice = "1000000000" });
            if (result.receipt.status == 1)
            {
            SceneManager.LoadSceneAsync("GamePlayScene");
            }
            
        } catch (Nethereum.Contracts.SmartContractCustomErrorRevertException e) {
            Debug.Log(e);
        }
    }
}
