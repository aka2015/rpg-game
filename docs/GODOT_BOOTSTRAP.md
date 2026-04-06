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

## 4) Langkah Implementasi Pertama di Godot
1. Buat scene `Bootstrap.tscn` dan autoload `GameManager`.
2. Implement event bus sederhana (signal atau custom dispatcher).
3. Port `PlayerStats`, `EnemyStats`, dan `DamageCalculator`.
4. Port `QuestRuntime` + `QuestManager` untuk kill-quest.
5. Buat HUD minimal (HP/Stamina/Quest progress).
6. Tambahkan save/load JSON.

## 5) Definition of Done Bootstrap
- Scene bootstrap bisa load world scene.
- Event bus dapat publish/subscribe event dasar.
- 1 loop gameplay berjalan: bunuh enemy -> progress quest naik -> UI update.
