using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
    ThirdwebSDK sdk;
    Contract playerContract;
    public GameObject activateAccountButton;
    void Start() {
        sdk = ThirdwebManager.Instance.SDK;
        playerContract = sdk.GetContract("0x530E933Bd4e688222456D9d16a43D08712534c93");
    }
    public void playGame() {
        SceneManager.LoadSceneAsync("GamePlayScene");
    }

    public async void OnConnectWallet() {
        var address = await sdk.wallet.GetAddress();
        var data = await playerContract.Read<bool>("isActivated", address);
        if (data == true) {
            SceneManager.LoadSceneAsync("GamePlayScene");
        } else {
            activateAccountButton.SetActive(true);
        }
    }

    public async void OnActivateAccount() {
        TransactionResult result = await playerContract.Write("activatePlayer");
        Debug.Log(result);
    }
}
