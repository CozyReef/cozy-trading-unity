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
        Debug.Log(CozyTradingPlayerABI.text);
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
            TransactionResult result = await CozyTradingPlayer.Write("activatePlayer", new TransactionRequest() { value = "5000000" });
        } catch (Nethereum.Contracts.SmartContractCustomErrorRevertException e) {
            Debug.Log(e);
        }
    }
}
