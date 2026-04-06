# Acceptance Test Plan (Tahap 3)

Dokumen ini mendefinisikan skenario uji penerimaan untuk vertical slice RPG MVP.

## 1. Lingkungan Uji
- Build: Development build terbaru dari branch implementasi.
- Platform: PC Windows.
- Tester: 2 internal dev + 1 tester non-dev.
- Durasi sesi: 15–20 menit per tester.

## 2. Test Data
- Scene awal: `Village`.
- Enemy target: `goblin_melee`.
- Quest target: `Kill 3 Goblin`.

## 3. Test Scenarios

### AT-001 — Player Movement
**Langkah:**
1. Start game.
2. Gerakkan player ke 4 arah selama 10 detik.

**Expected Result:**
- Movement responsif tanpa freeze/stutter besar.
- Arah hadap karakter sesuai arah gerak.

### AT-002 — Camera Follow
**Langkah:**
1. Putar kamera 360° saat player berjalan.
2. Dekati obstacle statis.

**Expected Result:**
- Kamera tetap mengikuti player.
- Kamera tidak clipping parah menembus objek utama.

### AT-003 — Basic Combat
**Langkah:**
1. Dekati enemy melee.
2. Lakukan basic attack sampai enemy mati.

**Expected Result:**
- Input attack memicu animasi serang.
- Enemy menerima damage dan mati saat HP habis.
- Event kematian enemy tercatat satu kali.

### AT-004 — Dodge + Stamina
**Langkah:**
1. Gunakan dodge berulang.
2. Coba dodge saat stamina nol.

**Expected Result:**
- Stamina berkurang saat dodge.
- Dodge tidak bisa saat stamina tidak cukup.

### AT-005 — Kill Quest Flow
**Langkah:**
1. Ambil quest kill dari NPC.
2. Bunuh 3 goblin.
3. Kembali ke NPC untuk turn-in.

**Expected Result:**
- Tracker quest menampilkan progress 0/3 -> 3/3.
- Status menjadi completed saat target tercapai.
- Reward EXP/gold diterima tepat satu kali.

### AT-006 — HUD Sync
**Langkah:**
1. Terima damage dari enemy.
2. Lakukan dodge dan attack.

**Expected Result:**
- HP/Stamina/EXP di HUD selalu sinkron real-time.

### AT-007 — Save/Load
**Langkah:**
1. Progreskan quest minimal 1 kill.
2. Save game.
3. Tutup dan load ulang save.

**Expected Result:**
- Posisi player, stats, dan progress quest kembali benar.
- UI tidak mismatch dengan state save.

## 4. Defect Severity
- **Critical:** Crash, save corrupt, progress quest hilang.
- **Major:** Objective tidak bisa selesai, reward dobel.
- **Minor:** UI misalignment, typo, feedback visual kecil.

## 5. Exit Criteria UAT
Uji penerimaan dinyatakan lulus jika:
- Semua test P0 pass.
- Tidak ada defect Critical terbuka.
- Defect Major <= 2 dengan workaround jelas.
