# Game Design Document (GDD)
## Proyek: RPG 3D Pemula yang Scalable

## 1. Ringkasan Proyek
- **Judul Sementara:** Echoes of Aether
- **Genre:** 3D Action RPG (Single-player)
- **Platform Awal:** PC (Windows)
- **Target Pemain:** Pemain kasual hingga menengah yang suka eksplorasi, combat ringan, dan progres karakter.
- **Tujuan MVP:** Membangun RPG 3D berdurasi 30–60 menit dengan core loop yang solid dan siap dikembangkan.

## 2. Visi Produk
Membuat game RPG 3D yang ramah untuk pengembang pemula: mekaniknya sederhana, struktur proyek rapi, dan sistemnya modular agar mudah ditambah konten (musuh, item, quest, area, skill) tanpa refactor besar.

## 3. Pilar Desain
1. **Mudah Dimainkan:** Kontrol intuitif, objective jelas.
2. **Progres Terasa:** Pemain naik level, loot membaik, karakter makin kuat.
3. **Scalable by Design:** Data dipisahkan dari logic, sistem modular, event-driven.

## 4. Core Gameplay Loop
1. Pemain menerima quest.
2. Pemain menjelajah area.
3. Pemain bertarung dan mengumpulkan loot.
4. Pemain menyelesaikan objective dan dapat reward (EXP, item, gold).
5. Pemain upgrade karakter/equipment.
6. Pemain membuka objective berikutnya.

## 5. Ruang Lingkup MVP (v0.1)
### 5.1 Fitur Wajib
- 1 karakter pemain (third-person).
- 1 area utama: desa + hutan.
- 2 tipe musuh: melee dan ranged.
- Combat dasar: basic attack, dodge, damage, death.
- Stat dasar: HP, Stamina, Attack, Defense.
- Sistem level-up + EXP.
- Inventory sederhana (weapon, potion).
- 3 quest dasar (Kill, Collect, Talk).
- UI dasar: HP/Stamina, quest tracker, inventory sederhana.
- Save/Load lokal.

### 5.2 Di Luar Scope MVP
- Multiplayer.
- Open world besar.
- Skill tree kompleks.
- Crafting mendalam.
- Cutscene sinematik panjang.

## 6. Desain Sistem
### 6.1 Player System
- **Input:** Move, Camera, Attack, Dodge, Interact, Use Item.
- **Komponen utama:**
  - `PlayerController`
  - `PlayerStats`
  - `PlayerCombat`
  - `PlayerInventory`

### 6.2 Combat System
- Serangan dasar dengan cooldown ringan.
- Dodge menggunakan stamina.
- Damage formula sederhana:
  - `finalDamage = max(1, attackerAttack - defenderDefense)`
- Hit reaction + invulnerability frame singkat (opsional pada dodge).

### 6.3 Enemy AI
- **Melee Enemy:** Patrol → Chase → Attack.
- **Ranged Enemy:** Patrol → Keep Distance → Shoot.
- Implementasi awal dapat memakai finite state machine sederhana.

### 6.4 Quest System
- Jenis quest:
  - `KillQuest` (bunuh X musuh)
  - `CollectQuest` (kumpulkan X item)
  - `TalkQuest` (bicara ke NPC)
- Status: `NotStarted`, `InProgress`, `Completed`, `TurnedIn`.

### 6.5 Inventory & Item
- Kategori item MVP:
  - Weapon
  - Consumable (potion)
  - Quest Item
- Data item disimpan sebagai data asset (ScriptableObject/JSON) agar mudah tambah item baru.

### 6.6 Save/Load
- Simpan:
  - Posisi pemain
  - Level/EXP/stat
  - Isi inventory
  - Progress quest
- Format: JSON lokal (versi awal).

### 6.7 UI/UX
- HUD: HP, Stamina, EXP bar.
- Quest tracker kanan layar.
- Popup loot dan level-up.
- Menu pause + save/load + settings sederhana.

## 7. Konten MVP
### 7.1 Dunia
- **Village Hub:** NPC, quest giver, vendor sederhana.
- **Forest Zone:** area combat utama, resource untuk collect quest.

### 7.2 NPC
- Quest Giver
- Vendor sederhana (opsional MVP+)

### 7.3 Musuh
- Goblin Swordsman (melee)
- Goblin Archer (ranged)

### 7.4 Quest Contoh
1. “Bersih-bersih Hutan” → bunuh 5 goblin.
2. “Ramuan Penyembuh” → kumpulkan 3 herb.
3. “Laporan ke Tetua” → bicara ke NPC kepala desa.

## 8. Progression & Balancing Awal
### 8.1 Level Curve Sederhana
- EXP per level: `100 * level` (dapat dituning).

### 8.2 Stat Growth (contoh)
- HP: +10 per level
- Attack: +2 per level
- Defense: +1 per level
- Stamina: +5 per level

### 8.3 Loot Drop Contoh
- Gold: 70%
- Potion kecil: 25%
- Weapon common: 5%

## 9. Arsitektur Teknis (Scalable)
### 9.1 Prinsip
- **Modular:** tiap sistem berdiri sendiri.
- **Data-driven:** balancing cukup ubah data, minim ubah code.
- **Event-driven:** komunikasi antar sistem via event (contoh `OnEnemyDied`).

### 9.2 Struktur Folder (contoh)
```
Assets/
  Scripts/
    Core/
    Player/
    Combat/
    Enemy/
    Quest/
    Inventory/
    UI/
    Save/
  Data/
    Items/
    Enemies/
    Quests/
  Prefabs/
  Scenes/
  Art/
  Audio/
```

## 10. Pipeline Produksi
### 10.1 Tools
- Engine: Godot 4 (utama).
- Version control: Git + GitHub.
- Task management: Trello/Notion.

### 10.2 Workflow Sprint (mingguan)
- Plan (Senin)
- Build (Selasa–Kamis)
- Test + Polish (Jumat)
- Review + Retrospective (akhir minggu)

## 11. Roadmap 8 Minggu
1. **M1–M2:** Player movement, camera, animasi dasar.
2. **M3–M4:** Combat + enemy AI dasar.
3. **M5:** Quest system + UI.
4. **M6:** Inventory + save/load.
5. **M7:** Audio, VFX ringan, bug fixing.
6. **M8:** Playtest, balancing, release MVP internal.

## 12. Risiko dan Mitigasi
- **Scope creep:** batasi fitur pada MVP checklist.
- **Bug sistemik:** lakukan test per modul, bukan tunggu akhir.
- **Asset mismatch:** tetapkan style guide visual sejak awal.
- **Performa:** batasi jumlah AI aktif dan optimasi collider/animator.

## 13. KPI MVP
- Sesi main rata-rata > 20 menit.
- Crash rate < 2% pada playtest internal.
- 80% tester menyelesaikan minimal 2 dari 3 quest.
- Feedback positif pada “combat feel” dan “kejelasan objective”.

## 14. Rencana Pengembangan Lanjutan (Post-MVP)
- Skill tree dan class system.
- Dungeon tambahan.
- Boss fight unik.
- Crafting.
- Sistem equipment rarity penuh.
- New Game+ dan endgame loop.

## 15. Definisi Selesai (Definition of Done)
MVP dinyatakan selesai bila:
- Core loop berjalan utuh tanpa blocker.
- Player dapat start → quest → combat → reward → save/load.
- Tidak ada bug kritikal pada flow utama.
- Build stabil dan dapat dimainkan tester non-dev.
