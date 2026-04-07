# Godot Troubleshooting

## Error: "Load failed due to missing dependencies" for `.cs` scripts

Jika Godot menampilkan error dependency untuk file C# seperti:
- `res://scripts/player/PlayerController.cs`
- `res://scripts/world/WorldController.cs`

maka penyebab paling umum:
1. Membuka project dengan **Godot non-.NET** build.
2. .NET SDK di mesin belum terpasang/terdeteksi.
3. Konfigurasi `.csproj` tidak cocok dengan versi Godot.

### Solusi langkah demi langkah
1. Gunakan **Godot 4 .NET** (judul app biasanya ada label .NET/Mono).
2. Pastikan .NET SDK terpasang di OS (minimal .NET 6 untuk Godot 4.2/4.3).
3. Pastikan file `rpg-game.csproj` ada di root project dan sesuai:
   - `Godot.NET.Sdk/4.2.2`
   - `TargetFramework net6.0`
4. Jalankan dari terminal di root project:
   - `dotnet --info`
   - `dotnet restore rpg-game.csproj`
   - `dotnet build rpg-game.csproj`
5. Hapus folder `.godot/mono/temp` (jika ada), lalu reopen project.

### Catatan penting
- Jika engine yang dipakai bukan .NET build, file `.cs` akan dianggap missing dependency walau fisiknya ada.
- Jika kamu memakai Godot 4.4+ .NET, boleh upgrade target framework ke `net8.0`.
