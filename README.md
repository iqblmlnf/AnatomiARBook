# 🧠 AnatomiARBook 🫁
**AnatomiARBook** adalah aplikasi edukasi interaktif berbasis **Augmented Reality (AR)** yang dirancang untuk membantu siswa mempelajari struktur anatomi dan fungsi organ tubuh manusia secara menyenangkan, visual, dan dinamis.

Repositori ini berisi seluruh aset proyek Unity, skrip pemrograman C#, dan konfigurasi antarmuka (UI) untuk aplikasi AnatomiARBook.

---

## 🚀 Fitur Utama
1. **🔍 Pemindaian AR Organ 3D**: Menggunakan teknologi Vuforia untuk memunculkan model 3D organ tubuh manusia secara interaktif (Otak, Jantung, Paru-paru, Lambung, Hati, Ginjal, Usus, dan Tulang Rangka) di atas kartu target.
2. **🔊 Narasi Suara Medis (Bahasa Indonesia)**: Penjelasan suara (voiceover) medis otomatis berbahasa Indonesia yang jernih saat organ berhasil terdeteksi kamera, mencegah suara bertumpuk saat berpindah organ.
3. **🗺️ Peta Organ 2D Interaktif**: Halaman peta anatomi tubuh utuh yang dapat diklik untuk mempelajari fungsi masing-masing organ secara visual.
4. **📊 Dasbor Statistik Riwayat**: Melacak pencapaian belajar siswa meliputi: total kuis diselesaikan, tingkat akurasi jawaban, dan rekor skor kuis tertinggi.
5. **🏆 Gelar Belajar & Lencana**: Gelar peringkat dinamis (mulai dari *Calon Dokter*, *Asisten Laboratorium*, hingga *Ahli Anatomi*) lengkap dengan perubahan warna lencana sesuai total XP yang dikumpulkan.
6. **📝 Kuis Interaktif**: Evaluasi pemahaman dengan kuis pilihan ganda yang dilengkapi efek suara jawaban benar (lonceng) dan salah secara real-time.
7. **👤 Profil Siswa & Sistem Leveling**: Sistem penyimpanan data lokal (`PlayerPrefs`) untuk tingkat level siswa (naik tingkat setiap kelipatan 100 XP) lengkap dengan progress bar visual yang halus.

---

## 🛠️ Teknologi yang Digunakan
* **Game Engine**: Unity 6 (6000.5.1f1)
* **AR SDK**: Vuforia Engine
* **Render Pipeline**: Universal Render Pipeline (URP)
* **UI Framework**: Unity UGUI & TextMesh Pro (TMP)
* **Bahasa Pemrograman**: C#
* **Penyimpanan Data**: Local PlayerPrefs

---

## 💻 Cara Menjalankan Aplikasi

### Versi PC / Laptop (Windows)
1. Unduh hasil build PC di folder penyimpanan Anda.
2. Klik dua kali file **`AnatomiARBook.exe`**.
3. Pastikan laptop memiliki kamera webcam aktif. Masuk ke menu **Scan AR** untuk memindai kartu organ menggunakan webcam Anda.

### Versi Mobile (Android)
1. Unduh file **`AnatomiARBook.apk`** ke handphone Android Anda.
2. Instal file APK tersebut di handphone (aktifkan izin instalasi dari sumber tidak dikenal jika diminta).
3. Jalankan aplikasi, berikan **izin akses kamera**, dan arahkan kamera HP ke kartu organ untuk memunculkan objek 3D!

---
---
*Proyek ini dikembangkan menggunakan standar pemrograman Unity berorientasi objek (OOP) dengan manajemen audio terpusat (Singleton Pattern) serta optimalisasi performa rendering mobile.*
