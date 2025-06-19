# WebAPI 專案

這是一個基於 .NET 5 的 Web API 專案，提供用戶管理功能。

## 功能特色

- 用戶CRUD操作（建立、讀取、更新、刪除）
- RESTful API設計
- Swagger文檔支援
- 錯誤處理和日誌記錄
- 資料驗證

## 專案結構

```
WebAPI/
├── Controllers/          # API控制器
│   ├── UsersController.cs
│   └── WeatherForecastController.cs
├── Models/              # 資料模型
│   └── User.cs
├── Services/            # 業務邏輯服務
│   ├── IUserService.cs
│   └── UserService.cs
├── Program.cs           # 應用程式入口點
├── Startup.cs           # 應用程式配置
└── WebAPI.csproj        # 專案檔案
```

## API端點

### 用戶管理 API

| 方法 | 端點 | 描述 |
|------|------|------|
| GET | `/api/users` | 取得所有用戶 |
| GET | `/api/users/{id}` | 根據ID取得特定用戶 |
| POST | `/api/users` | 建立新用戶 |
| PUT | `/api/users/{id}` | 更新用戶資訊 |
| DELETE | `/api/users/{id}` | 刪除用戶 |

### 用戶模型

```json
{
  "id": 1,
  "name": "張三",
  "email": "zhang.san@example.com",
  "phone": "0912345678",
  "createdAt": "2024-01-01T00:00:00Z",
  "isActive": true
}
```

## 執行專案

1. 確保已安裝 .NET 5 SDK
2. 在專案根目錄執行：
   ```bash
   dotnet restore
   dotnet run
   ```
3. 開啟瀏覽器訪問 `https://localhost:5001` 或 `http://localhost:5000`
4. Swagger UI 會自動顯示在根路徑，可以互動式測試API

## 範例請求

### 建立用戶
```bash
curl -X POST "https://localhost:5001/api/users" \
     -H "Content-Type: application/json" \
     -d '{
       "name": "新用戶",
       "email": "new.user@example.com",
       "phone": "0987654321"
     }'
```

### 取得所有用戶
```bash
curl -X GET "https://localhost:5001/api/users"
```

### 取得特定用戶
```bash
curl -X GET "https://localhost:5001/api/users/1"
```

### 更新用戶
```bash
curl -X PUT "https://localhost:5001/api/users/1" \
     -H "Content-Type: application/json" \
     -d '{
       "name": "更新後的用戶",
       "email": "updated.user@example.com",
       "phone": "0987654321"
     }'
```

### 刪除用戶
```bash
curl -X DELETE "https://localhost:5001/api/users/1"
```

## 開發說明

- 目前使用記憶體中的資料儲存，重啟應用程式後資料會重置
- 所有API都包含適當的錯誤處理和日誌記錄
- 使用資料註解進行模型驗證
- 支援非同步操作以提高效能 