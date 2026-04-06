# Stage 4 Execution Board

Dokumen ini adalah papan eksekusi praktis untuk memindahkan seluruh dokumen desain ke aktivitas implementasi harian.

## 1) Milestone Stage 4
- **M4.1**: Core loop playable end-to-end (movement, combat, kill quest, HUD).
- **M4.2**: Save/Load stabil.
- **M4.3**: Balancing pass 1 + internal playtest.

## 2) Workstream dan Owner Template
| Workstream | Fokus | Dependency | Owner | Status |
|---|---|---|---|---|
| Core | bootstrap, game state, event bus | none | TBD | Todo |
| Player | movement, camera, stats | Core | TBD | Todo |
| Combat | hitbox, damage, stamina dodge | Player, Enemy | TBD | Todo |
| Enemy AI | FSM melee/ranged basic | Core | TBD | Todo |
| Quest | accept/progress/turn-in | Enemy, UI | TBD | Todo |
| UI | HUD, tracker, notification | Player, Quest | TBD | Todo |
| Save/Load | serialize runtime state | Core, Quest, Inventory | TBD | Todo |
| QA | smoke test, regression checklist | all | TBD | Todo |

## 3) Definition of Ready (DoR)
Task baru boleh dikerjakan jika:
- Requirement jelas (user story + acceptance criteria tersedia).
- Dependency teridentifikasi.
- Output task terukur (contoh: "progress quest update real-time").
- Ada owner dan estimasi.

## 4) Definition of Done (DoD)
Task dianggap selesai jika:
- Acceptance criteria task lulus.
- Tidak ada error kritis pada flow terkait.
- Checklist QA relevan dicentang.
- Dokumentasi perubahan singkat ditulis (1–3 poin).

## 5) Prioritas Eksekusi (P0/P1)
### P0 (Wajib)
1. Player movement + camera.
2. Basic attack + enemy death.
3. Kill quest end-to-end.
4. HUD sync real-time.
5. Save/Load minimal state.

### P1 (Setelah P0 stabil)
1. Ranged enemy baseline.
2. Inventory interaction sederhana.
3. Audio/VFX feedback pass.
4. Balancing damage/EXP pass 1.

## 6) Cadence Harian
- **Daily 15 menit**: status blocker + target harian.
- **Midday sync 10 menit**: cek dependency silang.
- **EOD update**: update board + risk note singkat.

## 7) Risk Register (Live)
| Risk | Impact | Mitigation | Owner | Status |
|---|---|---|---|---|
| Combat feel kurang responsif | tinggi | kurangi input lag, tune animation timing | TBD | Open |
| Quest progress tidak konsisten | tinggi | event contract ketat + logging | TBD | Open |
| Save mismatch UI | sedang | reload UI dari source data tunggal | TBD | Open |
| Scope creep | tinggi | lock P0 sebelum P1 | PM | Open |

## 8) Exit Gate Stage 4
Stage 4 lulus jika:
- Semua item P0 selesai dan lolos acceptance test.
- Tidak ada defect Critical terbuka.
- Internal playtest 3 orang selesai dengan feedback tercatat.
