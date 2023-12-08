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

    public GameObject activateAccountButton;

    void Start() {
        sdk = ThirdwebManager.Instance.SDK;
    }

    public async void OnConnectWallet() {
        Contract CozyTradingPlayer = ContractManager.Instance.GetContract("CozyTradingPlayer");

        var address = await sdk.wallet.GetAddress();
        var data = await CozyTradingPlayer.Read<bool>("isActivated", address);
        if (data == true) {
            SceneManager.LoadSceneAsync("GamePlayScene");
        } else {
            activateAccountButton.SetActive(true);
        }
    }

    public async void OnActivateAccount() {
        try {
            Contract CozyTradingPlayer = ContractManager.Instance.GetContract("CozyTradingPlayer");
            TransactionResult result = await CozyTradingPlayer.Write("activatePlayer");
            if (result.receipt.status == 1)
            {
                SceneManager.LoadSceneAsync("GamePlayScene");
            }
        } catch (Nethereum.Contracts.SmartContractCustomErrorRevertException e) {
            Debug.Log(e);
        }
    }
}
