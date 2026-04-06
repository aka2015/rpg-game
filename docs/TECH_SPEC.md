# Technical Specification (Tahap 2)
## Proyek: 3D RPG MVP - Vertical Slice Foundation

## 1. Tujuan Dokumen
Dokumen ini mendefinisikan spesifikasi teknis tahap 2 (pre-production teknis + vertical slice) agar implementasi konsisten, modular, dan mudah dikembangkan setelah MVP awal stabil.

## 2. Scope Tahap 2
### In Scope
- Arsitektur sistem inti untuk vertical slice.
- Kontrak data untuk Player, Enemy, Item, Quest.
- Event flow antar sistem.
- Scene architecture dan dependency bootstrap.
- Performance baseline dan QA checklist awal.

### Out of Scope
- Multiplayer/networking.
- Open world streaming kompleks.
- Skill tree lanjutan.
- Crafting system penuh.

## 3. Target Engine dan Standar
- **Engine target:** Unity 2022/2023 LTS (direkomendasikan).
- **Bahasa:** C#.
- **Paradigma:** Modular component + data-driven + event-driven.
- **Version control:** Git, branch feature kecil per modul.

## 4. Arsitektur Folder (Implementasi)
```text
Assets/
  _Project/
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
      Balance/
    Prefabs/
      Player/
      Enemies/
      UI/
      World/
    Scenes/
      Bootstrap.unity
      Village.unity
      Forest.unity
```

## 5. Arsitektur Scene
### 5.1 Bootstrap Scene
Tanggung jawab:
- Inisialisasi service singleton (event bus, save service, game state).
- Load scene gameplay (`Village` untuk test awal).

### 5.2 Village Scene
Tanggung jawab:
- Hub NPC + quest giver.
- Spawn awal player.
- Trigger ke area forest.

### 5.3 Forest Scene
Tanggung jawab:
- Combat arena terbatas.
- Spawn enemy untuk quest kill.
- Drop loot sederhana.

## 6. Sistem Inti dan Kontrak Antar Modul

### 6.1 Core
Komponen:
- `GameManager`
- `EventBus`
- `ServiceLocator` (opsional jika tidak gunakan DI framework)

Event minimum:
- `OnEnemyDied(enemyId, position)`
- `OnPlayerDamaged(currentHp)`
- `OnQuestProgressChanged(questId, progress)`
- `OnQuestCompleted(questId)`
- `OnItemAdded(itemId, amount)`
- `OnPlayerLevelUp(level)`

### 6.2 Player Module
Komponen:
- `PlayerController` (movement + rotation)
- `PlayerStats` (HP, stamina, attack, defense, level, exp)
- `PlayerCombat` (attack timing, damage sender)
- `PlayerInteractor` (interaksi NPC/item)

Contract:
- Tidak mengakses UI secara langsung.
- Semua update UI dipublish via event.

### 6.3 Enemy Module
Komponen:
- `EnemyBrain` (FSM state: Idle/Patrol/Chase/Attack/Dead)
- `EnemyStats`
- `EnemyCombat`
- `EnemyDropTable`

Contract:
- Saat mati wajib emit `OnEnemyDied`.
- Drop item melalui inventory/loot service, bukan instantiate liar di banyak tempat.

### 6.4 Combat Module
Komponen:
- `DamageCalculator`
- `HitboxController`
- `HealthComponent`

Damage formula awal:
```text
finalDamage = max(1, attackerAttack - defenderDefense)
```

Aturan:
- Friendly fire OFF.
- Invulnerability frame dodge configurable via data.

### 6.5 Quest Module
Komponen:
- `QuestManager`
- `QuestRuntime`
- `QuestConditionEvaluator`

Quest type awal:
- KillQuest
- CollectQuest
- TalkQuest

Status:
- NotStarted
- InProgress
- Completed
- TurnedIn

### 6.6 Inventory Module
Komponen:
- `InventoryManager`
- `InventorySlot`
- `ItemDatabase`

Aturan:
- Stackable item (consumable) support.
- Weapon equip satu slot (MVP).

### 6.7 UI Module
Komponen:
- `HUDController`
- `QuestTrackerView`
- `InventoryView`
- `PopupNotifier`

Aturan:
- UI subscribe event dari EventBus.
- UI tidak menjadi source of truth untuk data gameplay.

### 6.8 Save Module
Komponen:
- `SaveManager`
- `SaveData` DTO

Minimal data disimpan:
- Player position + stats + exp/level
- Inventory
- Quest progress
- Active scene name

## 7. Data Schema (Sederhana)

### 7.1 ItemData
```json
{
  "id": "potion_small",
  "name": "Small Potion",
  "type": "Consumable",
  "maxStack": 20,
  "healAmount": 25,
  "rarity": "Common"
}
```

### 7.2 EnemyData
```json
{
  "id": "goblin_melee",
  "maxHp": 50,
  "attack": 8,
  "defense": 2,
  "expReward": 15,
  "moveSpeed": 3.5
}
```

### 7.3 QuestData
```json
{
  "id": "quest_kill_001",
  "title": "Bersih-bersih Hutan",
  "type": "Kill",
  "targetId": "goblin_melee",
  "requiredCount": 3,
  "expReward": 100,
  "goldReward": 50
}
```

## 8. Coding Conventions
- Satu class public per file.
- Namespace berdasarkan modul (`Project.Player`, `Project.Quest`, dst).
- Hindari hardcoded string; pakai constants atau ID data.
- Hindari coupling silang antarmodul; komunikasi via event/service interface.

## 9. Vertical Slice Definition
Vertical slice dianggap lulus bila:
1. Player bisa bergerak, attack, dodge.
2. Player bisa mengalahkan 1 tipe enemy.
3. Kill quest 3 enemy dapat selesai.
4. EXP bertambah dan level up terpicu minimal sekali.
5. UI menampilkan HP + quest progress real-time.
6. Save lalu load mengembalikan state utama.

## 10. Performance Baseline (Awal)
- Target FPS dev build: >= 60 FPS di scene forest kecil.
- Active enemy bersamaan: maks 10 (MVP awal).
- Hindari alokasi garbage di loop update kritis.

## 11. QA Checklist Teknis
- Null reference check saat ganti scene.
- Tidak ada event listener leak setelah unload scene.
- Save/load tidak merusak progress quest.
- Enemy death selalu trigger event + reward tepat sekali.
- UI tetap sinkron setelah load game.

## 12. Risiko Teknis + Mitigasi
- **Event chaos:** dokumentasi event contract + naming ketat.
- **Data inkonsisten:** validasi data saat startup.
- **Regresi cepat:** smoke test manual per build harian.

