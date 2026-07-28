using UnityEditor;
using UnityEngine;
using System.IO;
using System;

public class BuildHelper
{
    [MenuItem("Tools/Build Android APK")]
    public static void BuildAndroid()
    {
        Debug.Log("[BuildHelper] Memulai proses build Android secara programatis...");

        try
        {
            // 1. Dapatkan daftar scene yang aktif di Build Settings
            EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
            if (scenes.Length == 0)
            {
                Debug.LogError("[BuildHelper] Gagal: Tidak ada scene yang terdaftar di Build Settings!");
                return;
            }

            string[] scenePaths = new string[scenes.Length];
            for (int i = 0; i < scenes.Length; i++)
            {
                scenePaths[i] = scenes[i].path;
                Debug.Log($"[BuildHelper] Menambahkan scene: {scenePaths[i]}");
            }

            // 2. Tentukan lokasi output APK di root project agar 100% aman dari masalah hak akses
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "AnatomiARBook.apk");
            Debug.Log($"[BuildHelper] Lokasi output: {outputPath}");

            // 3. Jalankan Build
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = scenePaths;
            buildPlayerOptions.locationPathName = outputPath;
            buildPlayerOptions.target = BuildTarget.Android;
            buildPlayerOptions.options = BuildOptions.None;

            Debug.Log("[BuildHelper] Memulai BuildPipeline.BuildPlayer...");
            var report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            var summary = report.summary;

            if (summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
            {
                Debug.Log($"[BuildHelper] SUKSES! Build berhasil diselesaikan dalam {summary.totalTime.TotalSeconds:F2} detik.");
                Debug.Log($"[BuildHelper] Ukuran file: {summary.totalSize / (1024.0 * 1024.0):F2} MB");
                Debug.Log($"[BuildHelper] File APK telah disimpan di: {outputPath}");
                
                // Coba salin ke Downloads jika memungkinkan
                try
                {
                    string downloadsPath = @"C:\Users\HP\Downloads\AnatomiARBook.apk";
                    File.Copy(outputPath, downloadsPath, true);
                    Debug.Log($"[BuildHelper] Berhasil menyalin APK ke folder Downloads: {downloadsPath}");
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"[BuildHelper] Peringatan: Gagal menyalin ke folder Downloads ({ex.Message}). Namun file APK utama tetap tersimpan aman di root project!");
                }
            }
            else if (summary.result == UnityEditor.Build.Reporting.BuildResult.Failed)
            {
                Debug.LogError("[BuildHelper] GAGAL! Build gagal dilakukan.");
                Debug.LogError($"[BuildHelper] Total Error: {summary.totalErrors}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[BuildHelper] Terjadi Exception saat build: {e.Message}\nStacktrace: {e.StackTrace}");
        }
    }
}
