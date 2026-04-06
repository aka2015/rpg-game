# Godot Troubleshooting

## Error: "Load failed due to missing dependencies" for `.cs` scripts

Jika Godot menampilkan error dependency untuk file C# seperti:
- `res://scripts/player/PlayerController.cs`
- `res://scripts/world/WorldController.cs`

maka penyebab paling umum adalah project belum dikenali sebagai **Godot .NET/C# project**.

### Solusi
1. Pastikan menggunakan **Godot .NET build** (bukan standard build).
2. Pastikan file `rpg-game.csproj` ada di root repo.
3. Buka ulang project, lalu tunggu proses restore/build C# selesai.
4. Jika masih gagal, jalankan dari terminal di root project:
   - `dotnet restore rpg-game.csproj`
   - `dotnet build rpg-game.csproj`
5. Buka ulang Godot dan reload project.

### Catatan
- Jika engine yang dipakai bukan .NET build, file `.cs` akan dianggap missing dependency walau fisiknya ada.
