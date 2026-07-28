using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileManager : MonoBehaviour
{
    [Header("Profile UI Elements")]
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textLevel;
    [SerializeField] private TextMeshProUGUI textXP;
    [SerializeField] private TextMeshProUGUI textHighScore;
    [SerializeField] private Image xpProgressBar; // Tipe Image (Horizontal Fill)

    private void OnEnable()
    {
        // Otomatis update UI setiap kali panel profil dibuka/aktif
        UpdateProfileUI();
    }

    private void Start()
    {
        UpdateProfileUI();
    }

    public void UpdateProfileUI()
    {
        int totalXP = PlayerPrefs.GetInt("TotalXP", 0);
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        // Algoritma Leveling: Setiap 100 XP naik 1 Level
        // Contoh: 0-99 XP = Level 1, 100-199 XP = Level 2, dst.
        int level = (totalXP / 100) + 1;

        // Hitung sisa XP dalam level saat ini untuk progress bar
        int currentLevelXP = totalXP % 100;

        // Set teks UI
        if (textName != null)
        {
            // Default nama jika belum disetel
            textName.text = "Siswa Anatomi";
        }

        if (textLevel != null)
        {
            textLevel.text = $"Level {level}";
        }

        if (textXP != null)
        {
            textXP.text = $"{totalXP} XP";
        }

        if (textHighScore != null)
        {
            textHighScore.text = $"Skor Kuis Tertinggi: <b>{highScore}/10</b>";
        }

        // Update progress bar pengisian level (0 s.d 1)
        if (xpProgressBar != null)
        {
            xpProgressBar.fillAmount = (float)currentLevelXP / 100f;
        }

        Debug.Log($"[ProfileManager] UI terupdate. TotalXP: {totalXP}, Level: {level}, Progress Level: {currentLevelXP}/100");
    }

    // Fungsi pembantu untuk mereset seluruh data permainan (bisa dihubungkan ke tombol reset di UI jika ada)
    public void ResetProfileData()
    {
        PlayerPrefs.DeleteKey("TotalXP");
        PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.Save();
        
        UpdateProfileUI();
        Debug.Log("[ProfileManager] Data profil berhasil di-reset ke nol.");
    }
}
