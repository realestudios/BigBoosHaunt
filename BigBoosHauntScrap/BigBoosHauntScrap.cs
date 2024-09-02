using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Steamworks.Ugc;
using System.IO;
using System.Reflection;
using Unity.Netcode;
using UnityEngine;
using LethalLib;
using LethalLib.Modules;

namespace BigBoosHaunt
{

    [BepInPlugin(GUID, NAME, VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        const string GUID = "BigBoosHaunt";
        const string NAME = "BigBoosHaunt";
        const string VERSION = "2.2.0";

        public static Plugin instance;

        void Awake()
        {
            instance = this;

            string assetDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "bigbooshauntscrap");
            AssetBundle bundle = AssetBundle.LoadFromFile(assetDir);

            // Star scrap

            Item BBHStarItem = bundle.LoadAsset<Item>("Assets/LethalCompany/Mods/BigBoosHaunt/Scrap/Star/BBHStarItem.asset");
            LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(BBHStarItem.spawnPrefab);
            LethalLib.Modules.Utilities.FixMixerGroups(BBHStarItem.spawnPrefab);
            Items.RegisterScrap(BBHStarItem, 20, Levels.LevelTypes.None);

            // Coin scrap

            Item BBHCoinItem = bundle.LoadAsset<Item>("Assets/LethalCompany/Mods/BigBoosHaunt/Scrap/Coins/BBHCoinItem.asset");
            LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(BBHCoinItem.spawnPrefab);
            LethalLib.Modules.Utilities.FixMixerGroups(BBHCoinItem.spawnPrefab);
            Items.RegisterScrap(BBHCoinItem, 20, Levels.LevelTypes.None);

            // Red Coin Scrap

            Item BBHRedCoinItem = bundle.LoadAsset<Item>("Assets/LethalCompany/Mods/BigBoosHaunt/Scrap/Coins/BBHRedCoinItem.asset");
            LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(BBHRedCoinItem.spawnPrefab);
            LethalLib.Modules.Utilities.FixMixerGroups(BBHRedCoinItem.spawnPrefab);
            Items.RegisterScrap(BBHRedCoinItem, 20, Levels.LevelTypes.None);

            // Blue Coin Scrap

            Item BBHBlueCoinItem = bundle.LoadAsset<Item>("Assets/LethalCompany/Mods/BigBoosHaunt/Scrap/Coins/BBHBlueCoinItem.asset");
            LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(BBHBlueCoinItem.spawnPrefab);
            LethalLib.Modules.Utilities.FixMixerGroups(BBHBlueCoinItem.spawnPrefab);
            Items.RegisterScrap(BBHBlueCoinItem, 20, Levels.LevelTypes.None);

            // Koopa Shell Scrap

            Item BBHShellItem = bundle.LoadAsset<Item>("Assets/LethalCompany/Mods/BigBoosHaunt/Scrap/Shell/BBHShellItem.asset");
            LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(BBHShellItem.spawnPrefab);
            LethalLib.Modules.Utilities.FixMixerGroups(BBHShellItem.spawnPrefab);
            Items.RegisterScrap(BBHShellItem, 20, Levels.LevelTypes.None);

            // ? Block Scrap

            Item BBHQBlockItem = bundle.LoadAsset<Item>("Assets/LethalCompany/Mods/BigBoosHaunt/Scrap/Q_Block/BBHQBlockItem.asset");
            LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(BBHQBlockItem.spawnPrefab);
            LethalLib.Modules.Utilities.FixMixerGroups(BBHQBlockItem.spawnPrefab);
            Items.RegisterScrap(BBHQBlockItem, 20, Levels.LevelTypes.None);

            // Bob-Omb Scrap

            Item BBHBobOmbItem = bundle.LoadAsset<Item>("Assets/LethalCompany/Mods/BigBoosHaunt/Scrap/bob-omb/BBHBobOmbItem.asset");
            LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(BBHBobOmbItem.spawnPrefab);
            LethalLib.Modules.Utilities.FixMixerGroups(BBHBobOmbItem.spawnPrefab);
            Items.RegisterScrap(BBHBobOmbItem, 20, Levels.LevelTypes.None);

            // Cap Scrap

            Item BBHCapItem = bundle.LoadAsset<Item>("Assets/LethalCompany/Mods/BigBoosHaunt/Scrap/Cap/BBHCapItem.asset");
            LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(BBHCapItem.spawnPrefab);
            LethalLib.Modules.Utilities.FixMixerGroups(BBHCapItem.spawnPrefab);
            Items.RegisterScrap(BBHCapItem, 20, Levels.LevelTypes.None);

            // 1Up Scrap

            Item BBHOneUpItem = bundle.LoadAsset<Item>("Assets/LethalCompany/Mods/BigBoosHaunt/Scrap/1Up/BBHOneUpItem.asset");
            LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(BBHOneUpItem.spawnPrefab);
            LethalLib.Modules.Utilities.FixMixerGroups(BBHOneUpItem.spawnPrefab);
            Items.RegisterScrap(BBHOneUpItem, 20, Levels.LevelTypes.None);

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), GUID);
            Logger.LogInfo("Loaded BigBoosHaunt Scrap");
        }
    }

    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;
        [SerializeField] private float speed = 2.0f;

        private Vector3 currentTarget;

        private void Start()
        {
            // Initialize the current target to be the endpoint, so the platform starts at the end.
            currentTarget = endPoint.position;
        }

        private void Update()
        {
            // Move the platform towards the current target position.
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

            // Check if the platform has reached the current target position.
            if (Vector3.Distance(transform.position, currentTarget) < 0.1f)
            {
                // If the platform is at the endpoint, wait for two seconds, then switch the current target to the start point.
                // If it's at the start point, wait for two seconds, then switch the current target to the endpoint.
                currentTarget = (currentTarget == endPoint.position) ? startPoint.position : endPoint.position;

            }
        }
    }

}