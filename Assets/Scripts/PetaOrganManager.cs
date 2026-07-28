using UnityEngine;
using TMPro;

public class PetaOrganManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject panelBeranda;
    [SerializeField] private GameObject panelPetaOrgan;
    [SerializeField] private GameObject bottomNavBar;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI infoCardText;

    private void Start()
    {
        // Set teks default saat pertama kali dibuka
        ResetInfoText();
        if (panelPetaOrgan != null) panelPetaOrgan.SetActive(false); // Sembunyikan Peta Organ saat startup
    }

    // Fungsi untuk membuka Peta Organ dari Beranda
    public void OpenPetaOrgan()
    {
        Debug.Log($"[PetaOrganManager] OpenPetaOrgan() dipanggil. bottomNavBar null? {bottomNavBar == null}");
        
        // Diagnostik: Temukan semua GameObject bernama "BottomNavBar" di scene aktif
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        int count = 0;
        foreach (GameObject go in allObjects)
        {
            if (go.name == "BottomNavBar" && go.scene.name == "Scene_MainMenu")
            {
                count++;
                string path = go.name;
                Transform t = go.transform;
                while (t.parent != null)
                {
                    path = t.parent.name + "/" + path;
                    t = t.parent;
                }
                Debug.Log($"[Diagnostic] Menemukan objek '{go.name}' ke-{count} di path: '{path}', ActiveSelf: {go.activeSelf}");
            }
        }

        if (panelBeranda != null) panelBeranda.SetActive(false);
        if (panelPetaOrgan != null) panelPetaOrgan.SetActive(true);
        if (bottomNavBar != null) 
        {
            bottomNavBar.SetActive(false); // Sembunyikan Nav Bar
            Debug.Log($"[PetaOrganManager] bottomNavBar.SetActive(false) dipanggil. Objek: '{bottomNavBar.name}', ActiveSelf setelah SetActive(false): {bottomNavBar.activeSelf}");
        }
        else
        {
            Debug.LogWarning("[PetaOrganManager] Peringatan! Variabel bottomNavBar masih kosong (null) di Inspector!");
        }
        ResetInfoText();
    }

    // Fungsi untuk menutup Peta Organ dan kembali ke Beranda
    public void ClosePetaOrgan()
    {
        if (panelPetaOrgan != null) panelPetaOrgan.SetActive(false);
        if (panelBeranda != null) panelBeranda.SetActive(true);
        if (bottomNavBar != null) bottomNavBar.SetActive(true); // Tampilkan kembali Nav Bar
    }

    // Fungsi yang dipanggil saat salah satu dari 8 tombol organ diklik
    public void OnOrganClicked(string organName)
    {
        Debug.Log($"[PetaOrganManager] OnOrganClicked dipanggil dengan parameter organName: '{organName}'");
        if (infoCardText == null)
        {
            Debug.LogError("[PetaOrganManager] Gagal! Variabel 'infoCardText' bernilai null di Inspector!");
            return;
        }

        switch (organName.ToLower())
        {
            case "otak":
                infoCardText.text = "<b>Otak:</b> Pusat kendali utama tubuh manusia yang mengatur pikiran, gerakan, memori, dan emosi.";
                break;
            case "jantung":
                infoCardText.text = "<b>Jantung:</b> Organ berotot yang memompa darah beroksigen ke seluruh tubuh melalui pembuluh darah.";
                break;
            case "paru-paru":
                infoCardText.text = "<b>Paru-paru:</b> Organ utama sistem pernapasan yang menukar oksigen dari udara dengan karbon dioksida.";
                break;
            case "lambung":
                infoCardText.text = "<b>Lambung:</b> Organ pencernaan yang memecah makanan secara kimiawi menggunakan asam lambung.";
                break;
            case "hati":
                infoCardText.text = "<b>Hati:</b> Organ yang menyaring racun dari darah, memproduksi cairan empedu, dan menyimpan energi.";
                break;
            case "ginjal":
                infoCardText.text = "<b>Ginjal:</b> Organ penyaring yang membuang limbah cair dari darah dan memproduksinya sebagai urine.";
                break;
            case "usus":
                infoCardText.text = "<b>Usus:</b> Saluran pencernaan yang menyerap nutrisi makanan (usus halus) dan air (usus besar).";
                break;
            case "tulang":
                infoCardText.text = "<b>Tulang:</b> Rangka keras yang menopang tubuh, melindungi organ dalam, dan membantu kita bergerak.";
                break;
            default:
                ResetInfoText();
                break;
        }
    }

    // Mengembalikan teks card ke petunjuk awal
    private void ResetInfoText()
    {
        if (infoCardText != null)
        {
            infoCardText.text = "Pilih organ untuk mulai belajar!";
        }
    }
}
