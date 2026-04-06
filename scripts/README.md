# Godot Scripts Skeleton

Folder ini adalah port awal skeleton code ke struktur Godot.

## Catatan
- `scripts/core/GameManager.cs` dirancang untuk dijadikan Autoload singleton di Godot.
- Event flow menggunakan `EventBus` C# agar modul gameplay saling lepas (decoupled).
- `save/SaveManager.cs` sudah menggunakan `user://` path sesuai praktik Godot.
