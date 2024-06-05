# Spectre Console App

## Display Unicode on Windows

如果在 Windows 上使用 Spectre.Console 時，需要顯示 Unicode，需要在 Windows Terminal 中配置字體以及設定 `Profile`。

- 使用 `PowerShell` 的話，可以在 `$PROFILE` 中加入以下設定：

```powershell
[console]::InputEncoding = [console]::OutputEncoding = [System.Text.UTF8Encoding]::new()
```

Ref: [Doc](https://spectreconsole.net/best-practices#configuring-the-windows-terminal-for-unicode-and-emoji-support)