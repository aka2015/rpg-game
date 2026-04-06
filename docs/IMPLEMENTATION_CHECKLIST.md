# Implementation Checklist (Tahap 2)

Gunakan checklist ini sebagai kontrol harian implementasi vertical slice.

## 1) Project Setup
- [ ] Folder structure sesuai `TECH_SPEC.md` dibuat.
- [ ] Scene `Bootstrap`, `Village`, `Forest` dibuat.
- [ ] Input actions terkonfigurasi dan terdokumentasi.

## 2) Core Systems
- [ ] `GameManager` tersedia dan dapat mengelola state dasar.
- [ ] Event bus aktif untuk event gameplay utama.
- [ ] Logging util tersedia untuk debug cepat.

## 3) Player
- [ ] Movement third-person halus.
- [ ] Kamera mengikuti player dengan kontrol stabil.
- [ ] Player attack bisa mengenai enemy (hitbox valid).
- [ ] Dodge mengonsumsi stamina.
- [ ] HP dan stamina dapat berubah sesuai aksi/damage.

## 4) Enemy
- [ ] Enemy melee dapat mendeteksi player.
- [ ] Enemy chase dan attack berjalan konsisten.
- [ ] Enemy death memicu reward dan event sekali saja.

## 5) Quest
- [ ] Quest kill 3 enemy dapat diterima.
- [ ] Progress bertambah setiap enemy valid mati.
- [ ] Quest selesai memicu reward dan notifikasi.

## 6) UI
- [ ] HP/Stamina tampil dan update real-time.
- [ ] Quest tracker tampil dan update real-time.
- [ ] Popup quest complete tampil sekali.

## 7) Save/Load
- [ ] Save state player berhasil.
- [ ] Save progress quest berhasil.
- [ ] Load mengembalikan state penting tanpa mismatch UI.

## 8) Performance
- [ ] FPS dev build stabil pada scene forest kecil.
- [ ] Tidak ada spike berat saat combat 3–5 enemy.

## 9) QA & Stability
- [ ] Tidak ada null reference saat pindah scene.
- [ ] Tidak ada event listener leak.
- [ ] Smoke test 15 menit lulus tanpa crash.

## 10) Release Internal (Sprint 1)
- [ ] Build internal dibuat.
- [ ] Catatan known issues ditulis.
- [ ] Rencana Sprint 2 disiapkan berdasarkan hasil test.
