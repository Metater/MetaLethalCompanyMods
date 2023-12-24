using BepInEx;
using HarmonyLib;

namespace WilhelmScreamOnDeath;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    private readonly Harmony harmony = new(PluginInfo.PLUGIN_GUID);

    public static Plugin Instance { get; private set; } = null!;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            LogError("Awake() was called more than once!");
            return;
        }

        harmony.PatchAll();

        LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
    }

    public void LogInfo(object data) => Logger.LogInfo(data);
    public void LogWarning(object data) => Logger.LogWarning(data);
    public void LogDebug(object data) => Logger.LogDebug(data);
    public void LogFatal(object data) => Logger.LogFatal(data);
    public void LogMessage(object data) => Logger.LogMessage(data);
    public void LogError(object data) => Logger.LogError(data);
}