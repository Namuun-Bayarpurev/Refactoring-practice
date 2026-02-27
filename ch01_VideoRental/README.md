# Video Rental Application

ビデオレンタルアプリケーションのリファクタリング練習用プロジェクト（Chapter 01）

## プロジェクト構成

```
ch01_VideoRental/
├── CSharp/              # C#実装
│   ├── VideoRental.sln
│   └── VideoRental/
│       ├── Program.cs
│       └── VideoRental.csproj
│
└── Java/                # Java実装
    ├── VideoRentalApp.java
    └── VideoRental_StartCode.java
```

## 前提条件

### C#版
- .NET 8.0 SDK以降

### Java版
- JDK 8以降

Javaがインストールされていない場合：
- [Oracle JDK](https://www.oracle.com/java/technologies/downloads/)
- または [OpenJDK](https://adoptium.net/)

インストール後、環境変数PATHにJavaのbinディレクトリを追加してください。

## 実行方法

### C#版
```bash
dotnet run --project ch01_VideoRental/CSharp/VideoRental
```

### Java版
```bash
javac ch01_VideoRental/Java/VideoRentalApp.java
```

## 機能

- 映画のレンタル管理
- レンタル料金の計算
- レンタルポイントの計算
- レンタル明細の出力
