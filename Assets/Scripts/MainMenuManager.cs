using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject panelBeranda;
    public GameObject panelBelajar;
    public GameObject panelRiwayat;
    public GameObject panelProfil;

    private void Start()
    {
        // Saat aplikasi dibuka pertama kali, tampilkan Beranda secara default
        ShowBeranda();
    }

    public void ShowBeranda()
    {
        panelBeranda.SetActive(true);
        panelBelajar.SetActive(false);
        panelRiwayat.SetActive(false);
        panelProfil.SetActive(false);

        if (Time.timeSinceLevelLoad > 0.1f && AudioManager.instance != null)
        {
            AudioManager.instance.PlayClick();
        }
    }

    public void ShowBelajar()
    {
        panelBeranda.SetActive(false);
        panelBelajar.SetActive(true);
        panelRiwayat.SetActive(false);
        panelProfil.SetActive(false);

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayClick();
        }
    }

    public void ShowRiwayat()
    {
        panelBeranda.SetActive(false);
        panelBelajar.SetActive(false);
        panelRiwayat.SetActive(true);
        panelProfil.SetActive(false);

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayClick();
        }
    }

    public void ShowProfil()
    {
        panelBeranda.SetActive(false);
        panelBelajar.SetActive(false);
        panelRiwayat.SetActive(false);
        panelProfil.SetActive(true);

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayClick();
        }
    }
}