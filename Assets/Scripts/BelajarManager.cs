using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BelajarManager : MonoBehaviour
{
    [System.Serializable]
    public class OrganSystem
    {
        public string systemName;         // Nama sistem: "Sistem Pencernaan"
        public string scientificName;     // Nama ilmiah: "(Digestive System)"
        [TextArea(3, 10)]
        public string description;        // Penjelasan lengkap sistem
        public string organsList;         // Daftar organ terkait: "Lambung, Hati, Usus"
        public Color themeColor;          // Warna tema sistem organ untuk kartu
    }

    [Header("Organ Systems Database")]
    [SerializeField] private OrganSystem[] organSystems = new OrganSystem[]
    {
        new OrganSystem {
            systemName = "Sistem Pernapasan",
            scientificName = "Sistem Respirasi",
            description = "Sistem pernapasan berfungsi untuk memasukkan oksigen dari udara luar ke dalam tubuh dan mengeluarkan karbon dioksida dari tubuh. Proses pertukaran gas ini terjadi di dalam paru-paru.",
            organsList = "Organ Terkait: Paru-Paru, Tenggorokan, Hidung",
            themeColor = new Color32(52, 152, 219, 255) // Biru
        },
        new OrganSystem {
            systemName = "Sistem Pencernaan",
            scientificName = "Sistem Digestif",
            description = "Sistem pencernaan berfungsi untuk mencerna makanan secara mekanik dan kimiawi, menyerap nutrisi penting untuk energi, serta membuang sisa makanan yang tidak dapat dicerna oleh tubuh.",
            organsList = "Organ Terkait: Lambung, Hati, Usus Halus, Usus Besar",
            themeColor = new Color32(230, 126, 34, 255) // Oranye
        },
        new OrganSystem {
            systemName = "Sistem Peredaran Darah",
            scientificName = "Sistem Kardiovaskular",
            description = "Sistem ini bertugas memompa dan mengedarkan darah yang kaya akan oksigen dan nutrisi ke seluruh sel-sel tubuh, serta membawa darah kotor kembali ke paru-paru untuk dibersihkan.",
            organsList = "Organ Terkait: Jantung, Pembuluh Darah (Arteri & Vena)",
            themeColor = new Color32(231, 76, 60, 255) // Merah
        },
        new OrganSystem {
            systemName = "Sistem Saraf & Rangka",
            scientificName = "Sistem Regulasi & Lokomotor",
            description = "Sistem saraf bertindak sebagai pusat kendali seluruh aktivitas tubuh, sementara sistem rangka melindungi organ vital dalam tubuh dan memberikan penopang fisik agar tubuh dapat bergerak bebas.",
            organsList = "Organ Terkait: Otak, Tulang Rangka, Sumsum Tulang",
            themeColor = new Color32(155, 89, 182, 255) // Ungu
        }
    };

    [Header("Panels")]
    [SerializeField] private GameObject panelDetail; // Pop-up detail sistem organ
    [SerializeField] private GameObject panelPetaOrgan; // Halaman Peta Organ untuk dipanggil
    [SerializeField] private GameObject panelBelajar; // Halaman ini sendiri

    [Header("Detail UI Elements")]
    [SerializeField] private TextMeshProUGUI textDetailTitle;
    [SerializeField] private TextMeshProUGUI textDetailScientific;
    [SerializeField] private TextMeshProUGUI textDetailDesc;
    [SerializeField] private TextMeshProUGUI textDetailOrgans;
    [SerializeField] private Image imgDetailHeaderBar; // Garis hiasan warna tema di pop-up

    private void Start()
    {
        // Tutup pop-up detail secara default di awal
        if (panelDetail != null) panelDetail.SetActive(false);

        // Cari tombol Btn_Study secara otomatis di bawah panelDetail dan ikat event kliknya secara kode!
        if (panelDetail != null)
        {
            UnityEngine.UI.Button btnStudy = panelDetail.transform.Find("Card_Detail/Btn_Study")?.GetComponent<UnityEngine.UI.Button>();
            if (btnStudy != null)
            {
                btnStudy.onClick.RemoveAllListeners();
                btnStudy.onClick.AddListener(OpenPetaOrgan);
                Debug.Log("[BelajarManager] Sukses mendeteksi dan mengikat event klik Btn_Study secara otomatis lewat kode!");
            }
            else
            {
                Debug.LogWarning("[BelajarManager] Tidak dapat menemukan tombol Btn_Study di path 'Card_Detail/Btn_Study'!");
            }
        }
    }

    // Fungsi dipanggil saat salah satu kartu sistem organ diklik (index 0 - 3)
    public void OpenSystemDetail(int index)
    {
        if (index < 0 || index >= organSystems.Length) return;

        OrganSystem system = organSystems[index];

        // Masukkan data ke UI Detail
        if (textDetailTitle != null) textDetailTitle.text = system.systemName;
        if (textDetailScientific != null) textDetailScientific.text = system.scientificName;
        if (textDetailDesc != null) textDetailDesc.text = system.description;
        if (textDetailOrgans != null) textDetailOrgans.text = system.organsList;

        if (imgDetailHeaderBar != null)
        {
            // Berikan warna tema yang cocok untuk bar atas pop-up detail
            imgDetailHeaderBar.color = system.themeColor;
        }

        // Tampilkan pop-up detail
        if (panelDetail != null) panelDetail.SetActive(true);
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayPopup();
        }
    }

    // Menutup pop-up detail
    public void CloseSystemDetail()
    {
        if (panelDetail != null) panelDetail.SetActive(false);
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayClick();
        }
    }

    // Membuka Peta Organ untuk mulai belajar organ-organ dari sistem tersebut
    public void OpenPetaOrgan()
    {
        if (panelBelajar != null) panelBelajar.SetActive(false);
        
        // Cari PetaOrganManager di scene secara otomatis (karena script terpasang di objek root MainMenu, bukan di panel UI)
        PetaOrganManager manager = FindAnyObjectByType<PetaOrganManager>();
        if (manager != null)
        {
            manager.OpenPetaOrgan();
        }
        else if (panelPetaOrgan != null)
        {
            panelPetaOrgan.SetActive(true);
        }
    }
}
