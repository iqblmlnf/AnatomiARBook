using UnityEngine;
using TMPro; // Digunakan untuk Text Mesh Pro agar tulisan tajam di HP

public class ARUIManager : MonoBehaviour
{
    [System.Serializable]
    public class OrganInfo
    {
        public string organId;          // ID unik organ, misal: "Jantung"
        public string displayName;      // Nama yang tampil di UI: "Jantung"
        public string scientificName;   // Nama ilmiah: "(cor)"
        [TextArea(3, 10)]
        public string primaryFunction;  // Penjelasan fungsi utama
    }

    [Header("Organ Database")]
    public OrganInfo[] organsData = new OrganInfo[]
    {
        new OrganInfo { organId = "Jantung", displayName = "Jantung", scientificName = "(cor)", primaryFunction = "Memompa darah kaya oksigen ke seluruh tubuh dan menerima darah kotor kembali ke paru-paru." },
        new OrganInfo { organId = "ParuParu", displayName = "Paru-Paru", scientificName = "(pulmo)", primaryFunction = "Tempat pertukaran gas oksigen (O2) dari udara luar dengan karbon dioksida (CO2) dari darah." },
        new OrganInfo { organId = "Otak", displayName = "Otak", scientificName = "(encephalon)", primaryFunction = "Pusat kendali utama sistem saraf tubuh, mengatur gerakan, pikiran, memori, dan emosi." },
        new OrganInfo { organId = "Hati", displayName = "Hati", scientificName = "(hepar)", primaryFunction = "Menyaring darah dari racun, memproduksi empedu untuk pencernaan, dan menyimpan cadangan energi." },
        new OrganInfo { organId = "Lambung", displayName = "Lambung", scientificName = "(gaster)", primaryFunction = "Mencerna makanan secara mekanik dengan otot lambung dan kimiawi menggunakan asam lambung dan enzim." },
        new OrganInfo { organId = "Ginjal", displayName = "Ginjal", scientificName = "(ren)", primaryFunction = "Menyaring zat sisa metabolisme dan cairan berlebih dari darah untuk dikeluarkan dalam bentuk urin." },
        new OrganInfo { organId = "Usus", displayName = "Usus", scientificName = "(intestine)", primaryFunction = "Usus halus menyerap nutrisi makanan, sedangkan usus besar menyerap air dan membentuk sisa makanan." },
        new OrganInfo { organId = "TulangRangka", displayName = "Tulang Rangka", scientificName = "(skeleton)", primaryFunction = "Menopang bentuk tubuh, melindungi organ dalam yang vital, dan tempat sel darah diproduksi." }
    };

    [Header("UI Panels")]
    public GameObject scanningPanel; // Panel "Marker tidak terdeteksi..."
    public GameObject infoPanel;     // Panel informasi organ di bagian bawah

    [Header("UI Elements (Text)")]
    public TMP_Text organNameText;
    public TMP_Text scientificNameText;
    public TMP_Text functionText;

    private string currentActiveOrganId = "";

    private void Start()
    {
        // Kondisi awal: tampilkan panel scan, sembunyikan panel info organ
        ShowScanningPanel();
    }

    // Fungsi yang dipanggil saat marker terdeteksi oleh Vuforia
    public void OnOrganDetected(string organId)
    {
        currentActiveOrganId = organId;

        // Cari data organ berdasarkan ID
        OrganInfo info = GetOrganInfo(organId);
        if (info != null)
        {
            // Masukkan data teks ke komponen UI
            organNameText.text = info.displayName;
            scientificNameText.text = info.scientificName;
            functionText.text = info.primaryFunction;

            // Aktifkan panel info, matikan panel mencari marker
            scanningPanel.SetActive(false);
            infoPanel.SetActive(true);
        }
    }

    // Fungsi yang dipanggil saat marker hilang dari kamera
    public void OnOrganLost(string organId)
    {
        // Sembunyikan panel info HANYA JIKA organ yang hilang adalah organ yang saat ini aktif
        if (currentActiveOrganId == organId)
        {
            currentActiveOrganId = "";
            ShowScanningPanel();
        }
    }

    private void ShowScanningPanel()
    {
        scanningPanel.SetActive(true);
        infoPanel.SetActive(false);
    }

    private OrganInfo GetOrganInfo(string organId)
    {
        foreach (var organ in organsData)
        {
            if (organ.organId.Equals(organId, System.StringComparison.OrdinalIgnoreCase))
            {
                return organ;
            }
        }
        return null;
    }
}