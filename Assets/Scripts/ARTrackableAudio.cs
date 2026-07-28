using UnityEngine;

public class ARTrackableAudio : MonoBehaviour
{
    [Header("Voiceover Audio")]
    [SerializeField] private AudioClip voiceoverClip;

    // Panggil fungsi ini dari On Target Found () di DefaultObserverEventHandler
    public void OnTargetFound()
    {
        if (voiceoverClip != null && AudioManager.instance != null)
        {
            Debug.Log($"[ARTrackableAudio] Target ditemukan. Memutar penjelasan: {voiceoverClip.name}");
            AudioManager.instance.PlayVoiceover(voiceoverClip);
        }
    }

    // Panggil fungsi ini dari On Target Lost () di DefaultObserverEventHandler
    public void OnTargetLost()
    {
        if (AudioManager.instance != null)
        {
            Debug.Log("[ARTrackableAudio] Target hilang. Menghentikan penjelasan.");
            AudioManager.instance.StopVoiceover();
        }
    }
}
