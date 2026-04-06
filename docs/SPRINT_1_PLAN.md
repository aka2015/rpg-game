# Sprint 1 Plan (Tahap 2)
## Durasi
- 1 minggu (5 hari kerja efektif)

## Sprint Goal
Menghasilkan vertical slice minimal yang playable: movement + combat dasar + kill quest + HUD.

## Definition of Done (Sprint)
- Fitur berjalan tanpa blocker di flow utama.
- Tidak ada bug kritis (crash / stuck progress).
- Build internal dapat dimainkan 15–20 menit.

## Backlog Sprint 1

### A. Core & Setup
1. Inisialisasi folder project sesuai TECH_SPEC.
2. Buat `Bootstrap` scene dan `GameManager` minimal.
3. Setup input mapping (move/look/attack/dodge/interact).

### B. Player
4. Implement movement third-person.
5. Implement camera follow + rotate.
6. Integrasi `PlayerStats` (HP/Stamina/Attack/Defense/EXP/Level).

### C. Combat
7. Implement serangan dasar player (1 kombo sederhana).
8. Implement `HealthComponent` dan damage calculator.
9. Implement dodge consume stamina.

### D. Enemy
10. Buat enemy melee prototype.
11. Implement FSM sederhana (Idle -> Chase -> Attack -> Dead).
12. Trigger `OnEnemyDied` saat musuh mati.

### E. Quest
13. Implement `QuestManager` dasar.
14. Tambah 1 kill quest (`Kill 3 Goblin`).
15. Sinkronisasi progress quest dari event enemy death.

### F. UI
16. HUD: HP bar + stamina + level/EXP text.
17. Quest tracker: progress kill quest.
18. Popup sederhana untuk quest complete.

### G. Integration & QA
19. Integrasi scene `Village` dan `Forest` (minimal transition trigger).
20. Smoke test end-to-end + bug fixing prioritas tinggi.

## Breakdown Harian
### Day 1
- Task 1–3 selesai.

### Day 2
- Task 4–6 selesai.

### Day 3
- Task 7–12 selesai.

### Day 4
- Task 13–18 selesai.

### Day 5
- Task 19–20 + stabilisasi build.

## Risiko Sprint
- **AI belum stabil:** fallback ke behavior chase-attack tanpa patrol.
- **UI lambat selesai:** tampilkan text-only HUD dulu.
- **Quest bug:** lock scope hanya 1 tipe quest (kill).

## Exit Criteria
Sprint dianggap sukses jika:
- Kill quest bisa diambil dan diselesaikan.
- Combat dasar terasa responsif.
- Tidak ada crash selama 15 menit sesi main.
