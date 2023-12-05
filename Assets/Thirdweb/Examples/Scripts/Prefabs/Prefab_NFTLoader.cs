using System;
using System.Collections.Generic;
using UnityEngine;

namespace Thirdweb.Examples
{
    public class Prefab_NFTLoader : MonoBehaviour
    {
        public string owner;
        public string contractAddress;

        [Header("UI ELEMENTS (DO NOT EDIT)")]
        public Transform contentParent;
        public Prefab_NFT nftPrefab;
        public GameObject loadingPanel;

        private void Start()
        {
            foreach (Transform child in contentParent)
                Destroy(child.gameObject);

            // FindObjectOfType<Prefab_ConnectWallet>()?.OnConnectedCallback.AddListener(() => LoadNFTs());
            // FindObjectOfType<Prefab_ConnectWallet>()?.OnConnectedCallback.AddListener(() => LoadNFTs());
           
            contractAddress = "0x81696C7A36DFda53B81bE4a84abb1102f6D6e868";
            
            LoadNFTs();
        }

        public async void LoadNFTs()
        {
            loadingPanel.SetActive(true);
            List<NFT> nftsToLoad = new List<NFT>();
            owner = ThirdwebManager.Instance.SDK.wallet.GetAddress().ToString();
            try
            {
                Contract contract = ThirdwebManager.Instance.SDK.GetContract(contractAddress);
                string balance = await contract.Read<string>("balanceOf", owner);

                List<NFT> tempNFTList = await contract.ERC721.GetOwned(owner);

                nftsToLoad.AddRange(tempNFTList);
            }
            catch (Exception e)
            {
                print($"Error Loading OwnedQuery NFTs: {e.Message}");
            }

            // Load all NFTs into the scene

            foreach (NFT nft in nftsToLoad)
            {
                if (!Application.isPlaying)
                    return;

                Prefab_NFT nftPrefabScript = Instantiate(nftPrefab, contentParent);
                nftPrefabScript.LoadNFT(nft);
            }

            loadingPanel.SetActive(false);
        }
    }
}
