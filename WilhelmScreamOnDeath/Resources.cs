using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace WilhelmScreamOnDeath;

public static class Resources
{
    #region Init
    private static bool isInit = false;
    public static void EnsureInit()
    {
        if (isInit) return;

        MonoBehaviour arbitraryMb = SoundManager.Instance;

        for (int i = 1; i <= WilhelmAudioClipsCount; i++)
        {
            arbitraryMb.StartCoroutine(InitWilhelmAudioClips(i));
        }

        isInit = true;
    }
    #endregion

    public const int WilhelmAudioClipsCount = 6;
    public static List<AudioClip> WilhelmAudioClips = [];
    public static AudioClip RandomWilhelmAudioClip => WilhelmAudioClips[Random.Range(1, WilhelmAudioClipsCount + 1)];

    private static IEnumerator InitWilhelmAudioClips(int index)
    {
        string url = $"https://ia800802.us.archive.org/31/items/Wilhelm-ScreamSFX/Wilhelm%20{index}.wav";

        Plugin.Instance.LogInfo($"Started loading audio clip: {url}");

        using UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.WAV);
        var response = uwr.SendWebRequest();

        yield return response;

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Plugin.Instance.LogError(uwr.error);
            yield break;
        }
        else
        {
            var clip = DownloadHandlerAudioClip.GetContent(uwr);
            WilhelmAudioClips.Add(clip);

            Plugin.Instance.LogInfo($"Finished loading audio clip: {url}");
        }
    }
}