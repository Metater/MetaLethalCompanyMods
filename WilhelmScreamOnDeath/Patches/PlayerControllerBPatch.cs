using GameNetcodeStuff;
using HarmonyLib;

namespace WilhelmScreamOnDeath.Patches;

[HarmonyPatch(typeof(PlayerControllerB))]
public class PlayerControllerBPatch
{
    [HarmonyPatch("Start")]
    [HarmonyPostfix]
    private static void Start()
    {
        Resources.EnsureInit();
    }

    [HarmonyPatch(nameof(PlayerControllerB.SpawnDeadBody))]
    [HarmonyPostfix]
    private static void SpawnDeadBody(PlayerControllerB __instance)
    {
        __instance.deadBody.bodyAudio.PlayOneShot(Resources.RandomWilhelmAudioClip);
    }
}