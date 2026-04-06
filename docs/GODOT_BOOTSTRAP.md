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

## 5) Langkah Implementasi Berikutnya
1. Tambah input map (`move_left`, `move_right`, `move_forward`, `move_back`) di Project Settings.
2. Lengkapi collision shape player dan enemy agar interaksi fisik valid.
3. Hubungkan flow combat -> enemy death -> quest progress event.
4. Tambahkan tombol quick save/load untuk validasi `SaveManager`.
5. Buat scene test quest giver NPC.

## 6) Definition of Done Bootstrap
- Scene bootstrap bisa load world scene.
- Event bus dapat publish/subscribe event dasar.
- 1 loop gameplay berjalan: bunuh enemy -> progress quest naik -> UI update.
