# User Stories (Tahap 3)

Dokumen ini menerjemahkan GDD dan TECH_SPEC menjadi user stories yang siap diimplementasikan.

## Epic 1 — Player Core Control
### US-001: Movement Third-Person
**Sebagai** pemain, **saya ingin** menggerakkan karakter dengan kontrol third-person, **sehingga** saya bisa menjelajah area game dengan nyaman.

**Acceptance Criteria**
- Karakter dapat bergerak maju/mundur/kiri/kanan.
- Kecepatan gerak konsisten pada framerate berbeda.
- Karakter menghadap arah gerak saat berjalan.

### US-002: Camera Follow
**Sebagai** pemain, **saya ingin** kamera mengikuti karakter, **sehingga** saya bisa melihat lingkungan dengan jelas.

**Acceptance Criteria**
- Kamera mengikuti player tanpa jitter signifikan.
- Rotasi kamera responsif terhadap input look.
- Kamera tidak menembus objek statis utama.

## Epic 2 — Combat Loop
### US-003: Basic Attack
**Sebagai** pemain, **saya ingin** melakukan basic attack, **sehingga** saya bisa mengalahkan musuh.

**Acceptance Criteria**
- Input attack memicu animasi serang.
- Serangan mengenai enemy jika berada dalam hitbox.
- Enemy menerima damage dan menampilkan feedback hit.

### US-004: Dodge dengan Stamina
**Sebagai** pemain, **saya ingin** dodge dengan konsumsi stamina, **sehingga** combat terasa taktis.

**Acceptance Criteria**
- Dodge hanya bisa dipakai jika stamina cukup.
- Dodge mengurangi stamina sesuai nilai konfigurasi.
- Dodge gagal ketika stamina tidak mencukupi.

### US-005: Enemy Melee FSM
**Sebagai** pemain, **saya ingin** musuh yang dapat mengejar dan menyerang, **sehingga** ada tantangan combat.

**Acceptance Criteria**
- Enemy idle saat player jauh.
- Enemy chase saat player masuk radius deteksi.
- Enemy attack saat dalam jarak serang.
- Enemy masuk state dead ketika HP <= 0.

## Epic 3 — Quest Progression
### US-006: Ambil Kill Quest
**Sebagai** pemain, **saya ingin** menerima quest kill dari NPC, **sehingga** saya punya objective jelas.

**Acceptance Criteria**
- Interaksi ke NPC memberi quest aktif.
- Quest tampil pada quest tracker.
- Status quest berubah `NotStarted -> InProgress`.

### US-007: Progress Kill Quest
**Sebagai** pemain, **saya ingin** progress quest bertambah saat musuh valid mati, **sehingga** saya bisa menyelesaikan objective.

**Acceptance Criteria**
- Kematian musuh target menambah counter quest.
- UI tracker update real-time.
- Status quest berubah ke `Completed` saat target tercapai.

### US-008: Claim Reward Quest
**Sebagai** pemain, **saya ingin** claim reward quest, **sehingga** saya mendapatkan EXP/gold.

**Acceptance Criteria**
- Quest completed dapat di-turn-in ke NPC.
- Reward EXP/gold ditambahkan 1x.
- Status quest berubah `Completed -> TurnedIn`.

## Epic 4 — UI dan Feedback
### US-009: HUD Gameplay
**Sebagai** pemain, **saya ingin** melihat HP/Stamina/EXP, **sehingga** saya paham status karakter.

**Acceptance Criteria**
- HP bar, stamina bar, dan EXP tampil saat gameplay.
- Nilai HUD sinkron terhadap state runtime.
- UI tetap sinkron setelah load.

### US-010: Quest Notification
**Sebagai** pemain, **saya ingin** notifikasi ketika quest selesai, **sehingga** saya tahu kapan harus kembali ke NPC.

**Acceptance Criteria**
- Popup tampil saat quest menjadi `Completed`.
- Popup hanya muncul sekali per quest completion.

## Epic 5 — Save/Load
### US-011: Manual Save
**Sebagai** pemain, **saya ingin** menyimpan progres, **sehingga** saya bisa lanjut di sesi berikutnya.

**Acceptance Criteria**
- Save menyimpan posisi player, stats, inventory, dan progress quest.
- Save file terbuat tanpa error.

### US-012: Load Progress
**Sebagai** pemain, **saya ingin** memuat progres yang tersimpan, **sehingga** saya tidak mengulang dari awal.

**Acceptance Criteria**
- Load mengembalikan scene aktif dan state pemain.
- Progress quest dan UI sesuai data save.

---

## Prioritas Tahap 3
- **P0 (harus selesai):** US-001, US-002, US-003, US-005, US-006, US-007, US-009
- **P1 (target):** US-004, US-008, US-010, US-011, US-012

## Estimasi Kasar
- P0: 5–7 hari kerja
- P1: 3–5 hari kerja tambahan
