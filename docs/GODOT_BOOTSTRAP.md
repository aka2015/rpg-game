# Godot Bootstrap Guide (Stage 5)

Dokumen ini menetapkan bahwa implementasi proyek menggunakan **Godot 4** sebagai engine utama.

## 1) Keputusan Engine
- Engine utama: **Godot 4.2+**
- Bahasa script:
  - Opsi A: GDScript (lebih cepat untuk prototyping)
  - Opsi B: C# (lebih dekat dengan arsitektur class yang sudah dibuat)

## 2) Struktur Folder Godot (Rekomendasi)
```text
project/
  scenes/
    bootstrap/
    world/
    player/
    enemy/
    ui/
  scripts/
    core/
    combat/
    player/
    enemy/
    quest/
    save/
    ui/
  data/
    items/
    enemies/
    quests/
  assets/
    art/
    audio/
```

## 3) Mapping dari Struktur Lama
- `Assets/_Project/Scripts/Core` -> `scripts/core`
- `Assets/_Project/Scripts/Player` -> `scripts/player`
- `Assets/_Project/Scripts/Enemy` -> `scripts/enemy`
- `Assets/_Project/Scripts/Combat` -> `scripts/combat`
- `Assets/_Project/Scripts/Quest` -> `scripts/quest`
- `Assets/_Project/Scripts/Save` -> `scripts/save`
- `Assets/_Project/Scripts/UI` -> `scripts/ui`

## 4) Status Port yang Sudah Ada
- `project.godot` sudah dibuat dengan `Bootstrap.tscn` sebagai main scene.
- Scene awal sudah dibuat: `Bootstrap`, `World`, `Player`, `EnemyDummy`, dan `Hud`.
- `GameManager` sudah disiapkan sebagai Autoload.
- HUD telah subscribe event `QuestProgressChangedEvent` dari `EventBus`.

## 5) Status Implementasi Lanjutan
1. Input map dasar sudah ditambahkan di `project.godot` (`move_*`, `interact`, `attack`, `quick_save`, `quick_load`).
2. `WorldController` sudah menghubungkan flow `EnemyDiedEvent -> QuestManager.RegisterEnemyDeath`.
3. Quick save/load dasar sudah ditambahkan via input (`F5`/`F6`) ke `user://save_01.json`.
4. Scene `QuestGiver` sudah ditambahkan untuk start quest via aksi `interact`.

## 6) Langkah Berikutnya
1. Combat sudah upgrade ke raycast sederhana ke hitbox enemy; tahap berikutnya tambahkan animasi timing + combo attack.
2. Save/load sekarang sudah restore quest + stat + posisi player; berikutnya rapikan ke sistem save slot dan versioning data.
3. Tambahkan feedback visual/audio untuk enemy hit/death dan quest complete.

## 7) Definition of Done Bootstrap
- Scene bootstrap bisa load world scene.
- Event bus dapat publish/subscribe event dasar.
- 1 loop gameplay berjalan: bunuh enemy -> progress quest naik -> UI update.
