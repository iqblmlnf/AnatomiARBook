using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource voiceoverSource;

    [Header("Background Music (BGM)")]
    public AudioClip bgmMenu;

    [Header("Sound Effects (SFX)")]
    public AudioClip sfxClick;
    public AudioClip sfxCorrect;
    public AudioClip sfxWrong;
    public AudioClip sfxPopup;

    [Header("Volume Control")]
    [Range(0f, 1f)] [SerializeField] private float bgmVolume = 0.25f; // Volume default BGM: 25%
    [Range(0f, 1f)] [SerializeField] private float sfxVolume = 0.8f;  // Volume default SFX: 80%
    [Range(0f, 1f)] [SerializeField] private float voiceoverVolume = 0.9f; // Volume default Voiceover: 90%

    private void Awake()
    {
        // Singleton pattern: Pastikan hanya ada satu AudioManager di seluruh game
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // BGM tetap menyala mulus saat berpindah scene!
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Setup AudioSources jika belum dipasang di Inspector
        if (bgmSource == null)
        {
            bgmSource = gameObject.AddComponent<AudioSource>();
            bgmSource.loop = true;
            bgmSource.playOnAwake = false;
        }
        bgmSource.volume = bgmVolume;

        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.loop = false;
            sfxSource.playOnAwake = false;
        }
        sfxSource.volume = sfxVolume;

        if (voiceoverSource == null)
        {
            voiceoverSource = gameObject.AddComponent<AudioSource>();
            voiceoverSource.loop = false;
            voiceoverSource.playOnAwake = false;
        }
        voiceoverSource.volume = voiceoverVolume;
    }

    private void OnValidate()
    {
        // OnValidate dipanggil secara real-time di Editor saat Anda menggeser slider volume di Inspector!
        if (bgmSource != null) bgmSource.volume = bgmVolume;
        if (sfxSource != null) sfxSource.volume = sfxVolume;
        if (voiceoverSource != null) voiceoverSource.volume = voiceoverVolume;
    }

    // Fungsi khusus memutar Suara Penjelasan (Voiceover) agar tidak tumpang tindih
    public void PlayVoiceover(AudioClip clip)
    {
        if (clip == null || voiceoverSource == null) return;

        // Hentikan voiceover yang sedang berjalan sebelum memutar yang baru
        StopVoiceover();

        voiceoverSource.clip = clip;
        voiceoverSource.volume = voiceoverVolume;
        voiceoverSource.Play();
    }

    // Fungsi untuk menghentikan Suara Penjelasan (Voiceover)
    public void StopVoiceover()
    {
        if (voiceoverSource != null && voiceoverSource.isPlaying)
        {
            voiceoverSource.Stop();
        }
    }

    private void Start()
    {
        // Jalankan lagu latar belakang utama jika ada
        if (bgmMenu != null)
        {
            PlayBGM(bgmMenu);
        }
    }

    // Fungsi untuk memutar Lagu Latar Belakang (BGM)
    public void PlayBGM(AudioClip clip)
    {
        if (clip == null || bgmSource == null) return;

        // Jangan putar ulang jika lagu yang sama sedang berjalan
        if (bgmSource.clip == clip && bgmSource.isPlaying) return;

        bgmSource.clip = clip;
        bgmSource.volume = bgmVolume;
        bgmSource.Play();
    }

    // Fungsi umum untuk memutar Efek Suara (SFX) sekali bunyi
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxSource == null) return;
        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    // Fungsi bantuan khusus untuk dipanggil langsung dari On Click () tombol di Unity Inspector
    public void PlayClick()
    {
        if (sfxClick != null)
        {
            PlaySFX(sfxClick);
        }
    }

    // Fungsi bantuan khusus untuk suara pop-up
    public void PlayPopup()
    {
        if (sfxPopup != null)
        {
            PlaySFX(sfxPopup);
        }
    }
}
