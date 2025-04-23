# ⚡ MES-Battery-Monitoring

> A C# WinForms-based application for monitoring and managing EV battery status within a Manufacturing Execution System (MES) <br>
> It connects to an Oracle database and provides essential CRUD functionalities, ensuring efficient real-time diagnostics and operations.

---

## ✨ Key Features

| 기능 | 설명 |
|------|------|
| 🔍 **Search** | Search by `BatteryID` (numeric) or `Status` (text) |
| 🔄 **Real-Time View** | Fetch and display battery records from Oracle DB (`BatteryInfo` table) |
| ⚠️ **Defective Filter** | One-click filter to show only defective battery records |
| 📝 **Edit Records** | Modify existing entries via a custom update form with field validation |
| ➕ **Insert Records** | Add new battery records through an intuitive insert form |
| ❌ **Safe Delete** | Confirmation dialog before deleting any record |
| 🔐 **Secure DB Access** | Environment variable-based password management (no hardcoding) |

---

## 🧰 Technologies Used

- 💻 **Language**: C# (.NET Framework with WinForms)
- 🗄️ **Database**: Oracle Database (XE)
- 🔌 **Library**: Oracle.ManagedDataAccess.Client
