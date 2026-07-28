using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public Sprite organIllustration;
        public string[] options = new string[4];
        public int correctOptionIndex; // 0 = A, 1 = B, 2 = C, 3 = D
    }

    [Header("Panels")]
    [SerializeField] private GameObject panelBeranda;
    [SerializeField] private GameObject panelKuis;
    [SerializeField] private GameObject bottomNavBar;
    [SerializeField] private GameObject panelHasilKuis; // Panel overlay hasil kuis

    [Header("Result UI (Popup)")]
    [SerializeField] private TextMeshProUGUI textResultCongrats;
    [SerializeField] private TextMeshProUGUI textResultScore;
    [SerializeField] private TextMeshProUGUI textResultXP;
    [SerializeField] private Button btnResultFinish;

    [Header("Header UI")]
    [SerializeField] private TextMeshProUGUI textProgress;
    [SerializeField] private Image progressBarFill; // Tipe Image (Horizontal Fill)

    [Header("Question UI")]
    [SerializeField] private TextMeshProUGUI textQuestion;
    [SerializeField] private Image imgIllustration;

    [Header("Option Buttons")]
    [SerializeField] private Button[] optionButtons = new Button[4];
    [SerializeField] private Image[] optionBackgrounds = new Image[4]; // Background kartu tombol
    [SerializeField] private Image[] optionLetterCircles = new Image[4]; // Lingkaran huruf A, B, C, D
    [SerializeField] private TextMeshProUGUI[] optionLetterTexts = new TextMeshProUGUI[4]; // Teks A, B, C, D
    [SerializeField] private TextMeshProUGUI[] optionValueTexts = new TextMeshProUGUI[4]; // Teks nilai opsi

    [Header("Next Button")]
    [SerializeField] private Button btnNext;

    [Header("Question Database")]
    [SerializeField] private Question[] questionDatabase;

    private int currentQuestionIndex = 0;
    private int score = 0;
    private bool isAnswered = false;

    // Warna status tombol
    private Color colorDefaultCard = new Color32(250, 246, 240, 255); // Krem-putih (#FAF6F0)
    private Color colorDefaultCircle = new Color32(226, 226, 226, 255); // Abu-abu bulat (#E2E2E2)
    private Color colorDefaultText = new Color32(30, 53, 69, 255); // Biru gelap (#1E3545)

    private Color colorCorrectCard = new Color32(60, 166, 92, 255); // Hijau kuis (#3CA65C)
    private Color colorCorrectText = Color.white;

    private Color colorWrongCard = new Color32(217, 56, 56, 255); // Merah salah (#D93838)
    private Color colorWrongText = Color.white;

    private void Awake()
    {
        Debug.Log("[QuizManager] Awake dimulai.");
        AutoAssignOptionReferences();
        InitializeDefaultQuestions();
    }

    private void Start()
    {
        if (btnNext != null) btnNext.gameObject.SetActive(false);
        if (panelKuis != null) panelKuis.SetActive(false); // Sembunyikan kuis saat startup
        if (panelHasilKuis != null) panelHasilKuis.SetActive(false); // Sembunyikan panel hasil kuis saat startup
    }

    private void InitializeDefaultQuestions()
    {
        // Hanya populate jika database kosong atau baru berisi soal tes default (size <= 1)
        if (questionDatabase != null && questionDatabase.Length > 1) return;

        // Kita buat database baru dengan 10 soal
        questionDatabase = new Question[10];

        // Soal 1: Ginjal
        questionDatabase[0] = new Question {
            questionText = "Organ apa yang berfungsi menyaring darah dan membentuk urine?",
            organIllustration = Resources.Load<Sprite>("Organs/ginjal"),
            options = new string[] { "Jantung", "Hati", "Ginjal", "Paru Paru" },
            correctOptionIndex = 2
        };

        // Soal 2: Otak
        questionDatabase[1] = new Question {
            questionText = "Organ yang berfungsi sebagai pusat kendali utama seluruh aktivitas tubuh manusia adalah...",
            organIllustration = Resources.Load<Sprite>("Organs/otak"),
            options = new string[] { "Otak", "Jantung", "Lambung", "Hati" },
            correctOptionIndex = 0
        };

        // Soal 3: Jantung
        questionDatabase[2] = new Question {
            questionText = "Organ berotot yang berfungsi memompa darah beroksigen ke seluruh tubuh adalah...",
            organIllustration = Resources.Load<Sprite>("Organs/jantung"),
            options = new string[] { "Paru-Paru", "Lambung", "Ginjal", "Jantung" },
            correctOptionIndex = 3
        };

        // Soal 4: Paru-Paru
        questionDatabase[3] = new Question {
            questionText = "Organ utama pada sistem pernapasan yang menukar oksigen dengan karbon dioksida adalah...",
            organIllustration = Resources.Load<Sprite>("Organs/paru-paru"),
            options = new string[] { "Hati", "Paru-Paru", "Lambung", "Usus" },
            correctOptionIndex = 1
        };

        // Soal 5: Lambung
        questionDatabase[4] = new Question {
            questionText = "Di organ manakah makanan dicerna secara mekanik dan kimiawi menggunakan asam lambung?",
            organIllustration = Resources.Load<Sprite>("Organs/lambung"),
            options = new string[] { "Lambung", "Usus", "Hati", "Ginjal" },
            correctOptionIndex = 0
        };

        // Soal 6: Hati
        questionDatabase[5] = new Question {
            questionText = "Organ yang berperan penting dalam menyaring racun dari darah dan memproduksi empedu adalah...",
            organIllustration = Resources.Load<Sprite>("Organs/hati"),
            options = new string[] { "Ginjal", "Jantung", "Hati", "Lambung" },
            correctOptionIndex = 2
        };

        // Soal 7: Usus Halus
        questionDatabase[6] = new Question {
            questionText = "Bagian organ pencernaan yang berfungsi menyerap nutrisi makanan ke dalam darah adalah...",
            organIllustration = Resources.Load<Sprite>("Organs/usus"),
            options = new string[] { "Lambung", "Usus Halus", "Usus Besar", "Mulut" },
            correctOptionIndex = 1
        };

        // Soal 8: Tulang & Rangka
        questionDatabase[7] = new Question {
            questionText = "Menopang bentuk tubuh, melindungi organ vital, dan tempat sel darah merah diproduksi adalah fungsi dari...",
            organIllustration = Resources.Load<Sprite>("Organs/tulang rangka"),
            options = new string[] { "Kulit", "Otot", "Jantung", "Tulang Rangka" },
            correctOptionIndex = 3
        };

        // Soal 9: Usus Besar
        questionDatabase[8] = new Question {
            questionText = "Menyerap air dari sisa makanan dan membentuk feses terjadi di dalam...",
            organIllustration = Resources.Load<Sprite>("Organs/usus"),
            options = new string[] { "Lambung", "Usus Halus", "Usus Besar", "Hati" },
            correctOptionIndex = 2
        };

        // Soal 10: Sistem Peredaran Darah
        questionDatabase[9] = new Question {
            questionText = "Organ yang bertugas membawa darah kembali dari seluruh tubuh ke jantung adalah...",
            organIllustration = Resources.Load<Sprite>("Organs/vena"),
            options = new string[] { "Pembuluh Nadi (Arteri)", "Pembuluh Balik (Vena)", "Paru-Paru", "Lambung" },
            correctOptionIndex = 1
        };

        Debug.Log("[QuizManager] Berhasil memuat 10 soal kuis biologi default dengan ilustrasi gambar.");
    }

    private void AutoAssignOptionReferences()
    {
        if (panelKuis == null)
        {
            Debug.LogError("[QuizManager] panelKuis bernilai null!");
            return;
        }
        Transform choicesArea = panelKuis.transform.Find("ChoicesArea");
        if (choicesArea == null)
        {
            Debug.LogError("[QuizManager] Tidak menemukan ChoicesArea di bawah panelKuis!");
            return;
        }

        optionButtons = new Button[4];
        optionBackgrounds = new Image[4];
        optionLetterCircles = new Image[4];
        optionLetterTexts = new TextMeshProUGUI[4];
        optionValueTexts = new TextMeshProUGUI[4];

        Debug.Log($"[QuizManager] ChoicesArea ditemukan dengan jumlah anak: {choicesArea.childCount}");

        for (int i = 0; i < 4; i++)
        {
            if (i < choicesArea.childCount)
            {
                Transform buttonTrans = choicesArea.GetChild(i);
                optionButtons[i] = buttonTrans.GetComponent<Button>();
                optionBackgrounds[i] = buttonTrans.GetComponent<Image>();

                Transform circleTrans = buttonTrans.Find("Circle_Letter");
                if (circleTrans != null)
                {
                    optionLetterCircles[i] = circleTrans.GetComponent<Image>();
                    Transform letterTextTrans = circleTrans.Find("Text (TMP)");
                    if (letterTextTrans != null)
                    {
                        optionLetterTexts[i] = letterTextTrans.GetComponent<TextMeshProUGUI>();
                    }
                }

                Transform valueTextTrans = buttonTrans.Find("Text_Value");
                if (valueTextTrans != null)
                {
                    optionValueTexts[i] = valueTextTrans.GetComponent<TextMeshProUGUI>();
                }
                
                Debug.Log($"[QuizManager] Berhasil mendaftarkan tombol indeks {i}: {buttonTrans.name}");
            }
            else
            {
                Debug.LogWarning($"[QuizManager] Indeks {i} di luar jumlah anak ChoicesArea!");
            }
        }
    }

    // Membuka Kuis dari Beranda
    public void OpenQuiz()
    {
        if (panelBeranda != null) panelBeranda.SetActive(false);
        if (panelKuis != null) panelKuis.SetActive(true);
        if (bottomNavBar != null) bottomNavBar.SetActive(false); // Sembunyikan Nav Bar
        if (panelHasilKuis != null) panelHasilKuis.SetActive(false); // Sembunyikan panel hasil

        currentQuestionIndex = 0;
        score = 0;
        LoadQuestion();
    }

    // Menutup Kuis dan kembali ke Beranda
    public void CloseQuiz()
    {
        if (panelKuis != null) panelKuis.SetActive(false);
        if (panelHasilKuis != null) panelHasilKuis.SetActive(false); // Sembunyikan panel hasil
        if (panelBeranda != null) panelBeranda.SetActive(true);
        if (bottomNavBar != null) bottomNavBar.SetActive(true); // Tampilkan kembali Nav Bar
    }

    private void LoadQuestion()
    {
        Debug.Log($"[QuizManager] LoadQuestion dipanggil untuk indeks: {currentQuestionIndex}");
        if (questionDatabase == null || questionDatabase.Length == 0)
        {
            Debug.LogError("[QuizManager] Database soal kosong!");
            return;
        }

        isAnswered = false;
        if (btnNext != null) btnNext.gameObject.SetActive(false);
        ResetButtonStyles();

        Question currentQuestion = questionDatabase[currentQuestionIndex];

        // Update teks pertanyaan & ilustrasi
        textQuestion.text = currentQuestion.questionText;
        if (imgIllustration != null)
        {
            imgIllustration.sprite = currentQuestion.organIllustration;
            imgIllustration.gameObject.SetActive(currentQuestion.organIllustration != null);
        }

        // Update teks opsi
        for (int i = 0; i < 4; i++)
        {
            if (optionValueTexts != null && i < optionValueTexts.Length && optionValueTexts[i] != null)
                optionValueTexts[i].text = currentQuestion.options[i];
            
            if (optionButtons != null && i < optionButtons.Length && optionButtons[i] != null)
                optionButtons[i].interactable = true;
        }

        // Update Progress Header
        int totalQuestions = questionDatabase.Length;
        textProgress.text = $"{currentQuestionIndex + 1}/{totalQuestions}";

        if (progressBarFill != null)
        {
            progressBarFill.fillAmount = (float)(currentQuestionIndex + 1) / totalQuestions;
        }
    }

    // Fungsi yang dipanggil saat siswa menekan salah satu pilihan ganda
    public void OnOptionSelected(int optionIndex)
    {
        Debug.Log($"[QuizManager] OnOptionSelected dipanggil dengan indeks: {optionIndex}");
        if (isAnswered)
        {
            Debug.LogWarning("[QuizManager] Opsi diklik tetapi kuis sudah dijawab.");
            return;
        }
        isAnswered = true;

        // Nonaktifkan semua tombol agar tidak bisa klik ganda
        for (int i = 0; i < 4; i++)
        {
            if (optionButtons != null && i < optionButtons.Length && optionButtons[i] != null)
                optionButtons[i].interactable = false;
        }

        Question currentQuestion = questionDatabase[currentQuestionIndex];
        int correctIndex = currentQuestion.correctOptionIndex;
        Debug.Log($"[QuizManager] Jawaban diklik: {optionIndex}, Jawaban benar: {correctIndex}");

        if (optionIndex == correctIndex)
        {
            // Jawaban BENAR
            score++;
            SetButtonColor(optionIndex, colorCorrectCard, colorCorrectText, colorCorrectText, colorCorrectCard);
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.sfxCorrect);
            }
        }
        else
        {
            // Jawaban SALAH
            SetButtonColor(optionIndex, colorWrongCard, colorWrongText, colorWrongText, colorWrongCard);
            // Highlight jawaban yang BENAR
            SetButtonColor(correctIndex, colorCorrectCard, colorCorrectText, colorCorrectText, colorCorrectCard);
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.sfxWrong);
            }
        }

        // Munculkan tombol selanjutnya
        if (btnNext != null) btnNext.gameObject.SetActive(true);
    }

    // Mengubah warna tombol secara dinamis
    private void SetButtonColor(int index, Color cardColor, Color textColor, Color circleBgColor, Color circleTextColor)
    {
        Debug.Log($"[QuizManager] SetButtonColor untuk indeks {index} dengan warna kartu {cardColor}");
        if (optionBackgrounds != null && index < optionBackgrounds.Length && optionBackgrounds[index] != null)
        {
            optionBackgrounds[index].color = cardColor;
            Debug.Log($"[QuizManager] Berhasil merubah warna kartu indeks {index} menjadi {cardColor}");
        }
        else
        {
            Debug.LogError($"[QuizManager] Gagal merubah warna kartu indeks {index} karena optionBackgrounds atau elemennya null!");
        }
            
        if (optionValueTexts != null && index < optionValueTexts.Length && optionValueTexts[index] != null)
            optionValueTexts[index].color = textColor;
            
        if (optionLetterCircles != null && index < optionLetterCircles.Length && optionLetterCircles[index] != null)
            optionLetterCircles[index].color = circleBgColor;
            
        if (optionLetterTexts != null && index < optionLetterTexts.Length && optionLetterTexts[index] != null)
            optionLetterTexts[index].color = circleTextColor;
    }

    // Mengembalikan gaya tombol ke default semula
    private void ResetButtonStyles()
    {
        for (int i = 0; i < 4; i++)
        {
            SetButtonColor(i, colorDefaultCard, colorDefaultText, colorDefaultCircle, colorDefaultText);
        }
    }

    // Fungsi dipanggil saat tombol "Selanjutnya" diklik
    public void OnNextQuestion()
    {
        Debug.Log("[QuizManager] OnNextQuestion dipanggil.");
        currentQuestionIndex++;
        Debug.Log($"[QuizManager] Pindah ke indeks: {currentQuestionIndex}, Total soal: {questionDatabase.Length}");

        if (currentQuestionIndex < questionDatabase.Length)
        {
            LoadQuestion();
        }
        else
        {
            // Kuis selesai!
            ShowResults();
        }
    }

    private void ShowResults()
    {
        Debug.Log("[QuizManager] ShowResults dipanggil.");
        
        // Simpan data kuis ke PlayerPrefs untuk sistem profil gamifikasi
        int xpEarned = score * 10;
        int currentTotalXP = PlayerPrefs.GetInt("TotalXP", 0);
        PlayerPrefs.SetInt("TotalXP", currentTotalXP + xpEarned);
        
        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
        PlayerPrefs.SetInt("HighScore", Mathf.Max(currentHighScore, score));
        
        // Tambahkan hitungan total kuis dikerjakan
        int currentAttempts = PlayerPrefs.GetInt("TotalQuizAttempts", 0);
        PlayerPrefs.SetInt("TotalQuizAttempts", currentAttempts + 1);
        
        PlayerPrefs.Save();
        
        Debug.Log($"[QuizManager] Berhasil menyimpan data kuis. XP didapat: +{xpEarned}, Total XP baru: {PlayerPrefs.GetInt("TotalXP")}, HighScore baru: {PlayerPrefs.GetInt("HighScore")}/10");
        
        // Cek jika Panel Hasil Kuis kustom di-assign di Inspector
        if (panelHasilKuis != null)
        {
            panelHasilKuis.SetActive(true);
            
            if (textResultCongrats != null)
            {
                if (score >= 8) textResultCongrats.text = "Luar Biasa! 🎉";
                else if (score >= 5) textResultCongrats.text = "Keren Banget! 👍";
                else textResultCongrats.text = "Ayo Belajar Lagi! 💪";
            }
            
            if (textResultScore != null)
            {
                textResultScore.text = $"Skor Kamu: <b>{score}/{questionDatabase.Length}</b>";
            }
            
            if (textResultXP != null)
            {
                textResultXP.text = $"+{score * 10} XP";
            }
            
            if (btnResultFinish != null)
            {
                btnResultFinish.onClick.RemoveAllListeners();
                btnResultFinish.onClick.AddListener(CloseQuiz);
            }
        }
        else
        {
            // Fallback ke sistem teks sederhana lama jika panel hasil tidak dipasang di Inspector
            textQuestion.text = $"<b>Luar Biasa! Kuis Selesai! 🎉</b>\nSkor kamu: <b>{score}/{questionDatabase.Length}</b>\nKamu mendapatkan +{(score * 10)} XP!";
            if (imgIllustration != null) imgIllustration.gameObject.SetActive(false);

            for (int i = 0; i < 4; i++)
            {
                if (optionButtons != null && i < optionButtons.Length && optionButtons[i] != null)
                    optionButtons[i].gameObject.SetActive(false);
            }

            // Ubah tombol "Selanjutnya" menjadi tombol "Selesai" untuk kembali ke Beranda
            if (btnNext != null)
            {
                btnNext.gameObject.SetActive(true);
                btnNext.GetComponentInChildren<TextMeshProUGUI>().text = "Selesai";
                btnNext.onClick.RemoveAllListeners();
                btnNext.onClick.AddListener(CloseQuiz);
            }
        }
    }
}
