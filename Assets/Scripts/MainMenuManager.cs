using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
    ThirdwebSDK sdk;
    public TextAsset CozyTradingPlayerABI;
    private Contract CozyTradingPlayer;
    public string CozyTradingPlayerAddress;

    public GameObject activateAccountButton;

    void Start() {
        
        sdk = ThirdwebManager.Instance.SDK;
        
        CozyTradingPlayer = sdk.GetContract(CozyTradingPlayerAddress, CozyTradingPlayerABI.text);
    }
    public void playGame() {
        SceneManager.LoadSceneAsync("GamePlayScene");
    }

    public async void OnConnectWallet() {
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
            // , new TransactionRequest() { value = "5000000" }
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
