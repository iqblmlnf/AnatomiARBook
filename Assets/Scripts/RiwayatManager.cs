using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RiwayatManager : MonoBehaviour
{
    [Header("UI Text Fields")]
    [SerializeField] private TextMeshProUGUI textTotalAttempts;
    [SerializeField] private TextMeshProUGUI textHighScore;
    [SerializeField] private TextMeshProUGUI textAccuracy;
    [SerializeField] private TextMeshProUGUI textRankName;
    [SerializeField] private TextMeshProUGUI textRankDescription;

    [Header("UI Badges")]
    [SerializeField] private Image imgRankBadge;
    [SerializeField] private Image imgBadgeBackground;

    private void OnEnable()
    {
        // Panggil fungsi pembaruan data setiap kali halaman Riwayat dibuka
        UpdateStatistics();
    }

    public void UpdateStatistics()
    {
        // 1. Ambil data dari PlayerPrefs
        int totalAttempts = PlayerPrefs.GetInt("TotalQuizAttempts", 0);
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        int totalXP = PlayerPrefs.GetInt("TotalXP", 0);

        // 2. Hitung persentase akurasi (Maksimal skor adalah 10)
        int accuracyPercent = highScore * 10;

        // 3. Tentukan Gelar Belajar (Rank) & Penjelasan berdasarkan Total XP
        string rankName = "Calon Dokter";
        string rankDesc = "Yuk kerjakan kuis dan temukan organ tubuh di AR untuk mengumpulkan XP!";
        Color32 rankColor = new Color32(52, 152, 219, 255); // Biru

        if (totalXP >= 300)
        {
            rankName = "Ahli Anatomi";
            rankDesc = "Hebat! Pemahaman anatomi tubuh manusia Anda sudah setingkat ahli medis!";
            rankColor = new Color32(241, 196, 15, 255); // Emas (Gold)
        }
        else if (totalXP >= 100)
        {
            rankName = "Asisten Laboratorium";
            rankDesc = "Keren! Anda mulai menguasai tata letak dan fungsi organ tubuh!";
            rankColor = new Color32(230, 126, 34, 255); // Oranye
        }

        // 4. Update elemen teks di layar
        if (textTotalAttempts != null) textTotalAttempts.text = totalAttempts.ToString();
        if (textHighScore != null) textHighScore.text = $"{highScore}/10 Soal";
        if (textAccuracy != null) textAccuracy.text = $"{accuracyPercent}%";
        if (textRankName != null)
        {
            textRankName.text = rankName;
            textRankName.color = rankColor;
        }
        if (textRankDescription != null) textRankDescription.text = rankDesc;

        // 5. Update visual lencana/badge (jika ada)
        if (imgRankBadge != null)
        {
            imgRankBadge.color = rankColor;
        }
        if (imgBadgeBackground != null)
        {
            Color32 bgTransparant = rankColor;
            bgTransparant.a = 40; // Transparan 15%
            imgBadgeBackground.color = bgTransparant;
        }

        Debug.Log($"[RiwayatManager] Statistik Diperbarui. Attempts: {totalAttempts}, HighScore: {highScore}, Accuracy: {accuracyPercent}%, Rank: {rankName}");
    }
}
