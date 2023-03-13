using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class TestScript : MonoBehaviour
{
    [SerializeField] 
    private AudioSource audioSource;

    [SerializeField] 
    private string mp3Uri = "https://file-examples.com/storage/fe137d1f80640cf1e98d9f6/2017/11/file_example_MP3_700KB.mp3";

    private void Start()
    {
        StartCoroutine(LoadClip(mp3Uri));
    }

    private IEnumerator LoadClip(string uri)
    {
        AudioType audioType = AudioType.UNKNOWN;
        if (uri.EndsWith("mp3"))
        {
            audioType = AudioType.MPEG;
        }
        else if (uri.EndsWith("wav"))
        {
            audioType = AudioType.WAV;
        }
        
        using var uwr = UnityWebRequestMultimedia.GetAudioClip(uri, audioType);
        yield return uwr.SendWebRequest();

        if (uwr.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(uwr.error);
        }
        else
        {
            Debug.Log("No errors, creating audio clip...");
            var clip = DownloadHandlerAudioClip.GetContent(uwr);
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}