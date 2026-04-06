# Sprint 2 Plan

## Sprint Goal
Meningkatkan kualitas vertical slice dari "playable" menjadi "stable & testable" dengan fokus stabilitas, save/load, dan balancing pass 1.

## Durasi
- 1 minggu (5 hari kerja)

## Backlog Sprint 2

### A. Stabilitas Sistem
1. Tambah logging terstruktur untuk event penting (enemy death, quest progress, save/load).
2. Tambah guard untuk null reference pada transisi scene.
3. Validasi event subscription/unsubscription agar tidak leak.

### B. Save/Load Hardening
4. Pastikan save memuat: player stats, inventory, quest progress, scene.
5. Tambah validasi data save sebelum apply ke runtime.
6. Tangani fallback bila save file korup.

### C. Gameplay Polish
7. Tuning combat timing (attack windup/recovery).
8. Tuning stamina cost dodge.
9. Tuning EXP reward dan level pacing awal.

### D. UI & Feedback
10. Perbaiki update sinkron HUD setelah load.
11. Tambah feedback quest complete yang tidak spam.
12. Rapikan keterbacaan tracker objective.

### E. QA
13. Jalankan regression checklist dari Stage 3.
14. Jalankan acceptance test P0 ulang.
15. Catat defect + triage (Critical/Major/Minor).

## Breakdown Harian
### Day 1
- Task 1–3 (stabilitas event/scene)

### Day 2
- Task 4–6 (save/load hardening)

### Day 3
- Task 7–9 (gameplay balancing)

### Day 4
- Task 10–12 (UI polish)

### Day 5
- Task 13–15 (QA regression + triage)

## Definition of Done Sprint 2
- Semua task P0 sprint lulus uji manual.
- Tidak ada defect Critical.
- Defect Major maksimal 2 dengan workaround.
- Build internal siap untuk playtest berikutnya.
