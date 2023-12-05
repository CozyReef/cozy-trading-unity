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
    string contractABI = @"[
    {
        ""type"": ""constructor"",
        ""name"": """",
        ""inputs"": [],
        ""outputs"": [],
        ""stateMutability"": ""nonpayable""
    },
    {
        ""type"": ""error"",
        ""name"": ""AlreadyActivated"",
        ""inputs"": [],
        ""outputs"": []
    },
    {
        ""type"": ""error"",
        ""name"": ""NotGameController"",
        ""inputs"": [],
        ""outputs"": []
    },
    {
        ""type"": ""error"",
        ""name"": ""OwnableInvalidOwner"",
        ""inputs"": [
            {
                ""type"": ""address"",
                ""name"": ""owner"",
                ""internalType"": ""address""
            }
        ],
        ""outputs"": []
    },
    {
        ""type"": ""error"",
        ""name"": ""OwnableUnauthorizedAccount"",
        ""inputs"": [
            {
                ""type"": ""address"",
                ""name"": ""account"",
                ""internalType"": ""address""
            }
        ],
        ""outputs"": []
    },
    {
        ""type"": ""error"",
        ""name"": ""PriceNotMet"",
        ""inputs"": [],
        ""outputs"": []
    },
    {
        ""type"": ""event"",
        ""name"": ""OwnershipTransferred"",
        ""inputs"": [
            {
                ""type"": ""address"",
                ""name"": ""previousOwner"",
                ""indexed"": true,
                ""internalType"": ""address""
            },
            {
                ""type"": ""address"",
                ""name"": ""newOwner"",
                ""indexed"": true,
                ""internalType"": ""address""
            }
        ],
        ""outputs"": [],
        ""anonymous"": false
    },
    {
        ""type"": ""function"",
        ""name"": ""activatePlayer"",
        ""inputs"": [],
        ""outputs"": [],
        ""stateMutability"": ""payable""
    },
    {
        ""type"": ""function"",
        ""name"": ""activated"",
        ""inputs"": [
            {
                ""type"": ""address"",
                ""name"": """",
                ""internalType"": ""address""
            }
        ],
        ""outputs"": [
            {
                ""type"": ""bool"",
                ""name"": """",
                ""internalType"": ""bool""
            }
        ],
        ""stateMutability"": ""view""
    },
    {
        ""type"": ""function"",
        ""name"": ""activationFee"",
        ""inputs"": [],
        ""outputs"": [
            {
                ""type"": ""uint256"",
                ""name"": """",
                ""internalType"": ""uint256""
            }
        ],
        ""stateMutability"": ""view""
    },
    {
        ""type"": ""function"",
        ""name"": ""gameController"",
        ""inputs"": [],
        ""outputs"": [
            {
                ""type"": ""address"",
                ""name"": """",
                ""internalType"": ""contract CozyTradingCampsite""
            }
        ],
        ""stateMutability"": ""view""
    },
    {
        ""type"": ""function"",
        ""name"": ""getPlayerCargoResources"",
        ""inputs"": [
            {
                ""type"": ""address"",
                ""name"": ""_playerAddress"",
                ""internalType"": ""address""
            },
            {
                ""type"": ""uint256"",
                ""name"": ""_resourceType"",
                ""internalType"": ""uint256""
            }
        ],
        ""outputs"": [
            {
                ""type"": ""uint256"",
                ""name"": ""amount"",
                ""internalType"": ""uint256""
            }
        ],
        ""stateMutability"": ""view""
    },
    {
        ""type"": ""function"",
        ""name"": ""getPlayerSkill"",
        ""inputs"": [
            {
                ""type"": ""address"",
                ""name"": ""_playerAddress"",
                ""internalType"": ""address""
            },
            {
                ""type"": ""uint256"",
                ""name"": ""_skillType"",
                ""internalType"": ""uint256""
            }
        ],
        ""outputs"": [
            {
                ""type"": ""uint256"",
                ""name"": ""level"",
                ""internalType"": ""uint256""
            },
            {
                ""type"": ""uint256"",
                ""name"": ""xp"",
                ""internalType"": ""uint256""
            }
        ],
        ""stateMutability"": ""view""
    },
    {
        ""type"": ""function"",
        ""name"": ""increasePlayerSkillLevel"",
        ""inputs"": [
            {
                ""type"": ""address"",
                ""name"": ""_playerAddress"",
                ""internalType"": ""address""
            },
            {
                ""type"": ""uint256"",
                ""name"": ""_skillType"",
                ""internalType"": ""uint256""
            }
        ],
        ""outputs"": [],
        ""stateMutability"": ""nonpayable""
    },
    {
        ""type"": ""function"",
        ""name"": ""increasePlayerSkillXp"",
        ""inputs"": [
            {
                ""type"": ""address"",
                ""name"": ""_playerAddress"",
                ""internalType"": ""address""
            },
            {
                ""type"": ""uint256"",
                ""name"": ""_skillType"",
                ""internalType"": ""uint256""
            },
            {
                ""type"": ""uint256"",
                ""name"": ""_xp"",
                ""internalType"": ""uint256""
            }
        ],
        ""outputs"": [],
        ""stateMutability"": ""nonpayable""
    },
    {
        ""type"": ""function"",
        ""name"": ""isActivated"",
        ""inputs"": [
            {
                ""type"": ""address"",
                ""name"": ""_playerAddress"",
                ""internalType"": ""address""
            }
        ],
        ""outputs"": [
            {
                ""type"": ""bool"",
                ""name"": """",
                ""internalType"": ""bool""
            }
        ],
        ""stateMutability"": ""view""
    },
    {
        ""type"": ""function"",
        ""name"": ""loadResourceToCargo"",
        ""inputs"": [
            {
                ""type"": ""address"",
                ""name"": ""_playerAddress"",
                ""internalType"": ""address""
            },
            {
                ""type"": ""uint8"",
                ""name"": ""_resource"",
                ""internalType"": ""uint8""
            }
        ],
        ""outputs"": [],
        ""stateMutability"": ""nonpayable""
    },
    {
        ""type"": ""function"",
        ""name"": ""owner"",
        ""inputs"": [],
        ""outputs"": [
            {
                ""type"": ""address"",
                ""name"": """",
                ""internalType"": ""address""
            }
        ],
        ""stateMutability"": ""view""
    },
    {
        ""type"": ""function"",
        ""name"": ""player"",
        ""inputs"": [
            {
                ""type"": ""address"",
                ""name"": """",
                ""internalType"": ""address""
            }
        ],
        ""outputs"": [
            {
                ""type"": ""uint256"",
                ""name"": ""pebbles"",
                ""internalType"": ""uint256""
            }
        ],
        ""stateMutability"": ""view""
    },
    {
        ""type"": ""function"",
        ""name"": ""renounceOwnership"",
        ""inputs"": [],
        ""outputs"": [],
        ""stateMutability"": ""},
  {
    ""type"": ""function"",
    ""name"": ""setActivationFee"",
    ""inputs"": [
      {
            ""type"": ""uint256"",
        ""name"": ""_activationFee"",
        ""internalType"": ""uint256""
      }
    ],
   ""outputs"": [],
    ""stateMutability"": ""nonpayable""
  },
  {
    ""type"": ""function"",
    ""name"": ""setGameController"",
   ""inputs"": [
      {
      ""type"": ""address"",
        ""name"": ""_gameController"",
        ""internalType"": ""address"",
      }
    ],
   ""outputs"": [],
    ""stateMutability"": ""nonpayable""
  },
  {
    ""type"": ""function"",
    ""name"": ""transferOwnership"",
    ""inputs"": [
      {
        ""type"": ""address""
        ""name"": ""newOwner"",
        ""internalType"": ""address""
      }
    ],
  ""outputs"": [],
    ""stateMutability"": ""nonpayable""
  }
]";

        playerContract = sdk.GetContract("0x09635F643e140090A9A8Dcd712eD6285858ceBef", contractABI);
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
        var fee = await playerContract.Read<int>("activationFee");
        TransactionResult result = await playerContract.Write("activatePlayer", new TransactionRequest() { value = fee.ToString() });
        Debug.Log(result);
    }
}
