# Test TCC Project

โปรเจกต์นี้แยกเป็น 2 ส่วนชัดเจน

- `FrontEnd/TestTCCFrontEnd` เป็น Angular 19 แบบ standalone component
- `BackEnd/TestTCCBackEnd/TestTCCBackEnd` เป็น ASP.NET Core Web API บน .NET 8


### โครงสร้างหลัก

```text
FrontEnd/TestTCCFrontEnd/
├─ src/
│  ├─ app/
│  │  ├─ core/
│  │  │  ├─ guards/
│  │  │  ├─ models/
│  │  │  ├─ repositories/
│  │  │  ├─ services/
│  │  │  └─ use-cases/
│  │  ├─ presentation/
│  │  │  ├─ pages/
│  │  │  └─ shared/
│  │  ├─ app.routes.ts
│  │  └─ app.config.ts
│  ├─ environments/
│  ├─ main.ts
│  ├─ main.server.ts
│  └─ server.ts
├─ angular.json
├─ package.json
└─ proxy.conf.json
```

### Routing ของ Frontend

route หลักอยู่ใน `src/app/app.routes.ts`

- `/` หน้าเมนูหลัก
- `/it01` จัดการข้อมูลบุคคล
- `/it02/login`, `/it02/register`, `/it02/welcome` ระบบ auth
- `/it03` อนุมัติ/ปฏิเสธเอกสาร
- `/it04` ฟอร์มสมัครสมาชิก/บันทึกข้อมูลสมาชิก
- `/it05`, `/it05/ticket`, `/it05/reset` ระบบคิว
- `/it06` จัดการ product code และแสดง barcode
- `/it07` จัดการ serial code และแสดง QR code
- `/it08` ดูโพสต์และเพิ่มคอมเมนต์
- `/it09`, `/it09/add` จัดการคลังข้อสอบ
- `/it10` ทำข้อสอบและส่งคำตอบ


## โครงสร้าง Backend

Backend เป็น ASP.NET Core Web API บน .NET 8 ใช้ EF Core + SQLite + JWT Authentication

### โครงสร้างหลัก

```text
BackEnd/TestTCCBackEnd/TestTCCBackEnd/
├─ Controllers/
├─ Data/
├─ DTOs/
├─ Models/
├─ IServices/
├─ Services/
├─ Properties/
├─ Program.cs
├─ appsettings.json
└─ app.db
```

### Service registration และ middleware

`Program.cs` ทำหน้าที่หลักดังนี้

- ลงทะเบียน `AppDbContext` ให้ใช้ SQLite
- ลงทะเบียน service ของแต่ละโมดูลผ่าน DI
- เปิดใช้ JWT Bearer authentication
- เปิด CORS แบบ allow all
- เปิด Swagger ใน Development
- เรียก `Database.EnsureCreated()` ตอน startup เพื่อสร้างฐานข้อมูลอัตโนมัติถ้ายังไม่มี

### โมเดลข้อมูลสำคัญ

ใน `AppDbContext` มี `DbSet` หลักดังนี้

- `Persons`
- `Users`
- `Documents`
- `Occupations`
- `Members`
- `QueueStates`
- `ProductCodes`
- `SerialCodes`
- `ExamQuestions`
- `Posts`
- `Comments`
- `ExamSessions`
- `ExamSessionAnswers`


### กลุ่ม API หลัก

จาก controller ที่มีอยู่ ปัจจุบัน backend รองรับ endpoint กลุ่มนี้

- `AuthController`
  - `POST /api/Auth/register`
  - `POST /api/Auth/login`
- `PersonsController`
  - `GET /api/persons`
  - `GET /api/persons/{id}`
  - `POST /api/persons`
- `DocumentsController`
  - `GET /api/documents`
  - `POST /api/documents/approve`
  - `POST /api/documents/reject`
- `MembersController`
  - `POST /api/members`
  - `GET /api/members/occupations`
- `QueueController`
  - `GET /api/queue`
  - `POST /api/queue/take`
  - `POST /api/queue/reset`
- `ProductCodesController`
  - `GET /api/productcodes`
  - `POST /api/productcodes`
  - `DELETE /api/productcodes/{id}`
- `SerialCodesController`
  - `GET /api/serialcodes`
  - `POST /api/serialcodes`
  - `DELETE /api/serialcodes/{id}`
- `PostsController`
  - `GET /api/posts/{postId}`
  - `POST /api/posts/{postId}/comments`
- `ExamQuestionsController`
  - `GET /api/examquestions`
  - `POST /api/examquestions`
  - `DELETE /api/examquestions/{id}`
- `ExamController`
  - `GET /api/exam/questions`
  - `POST /api/exam/submit`


## วิธีรันโปรเจกต์

### 1. รัน Backend

ไปที่

```powershell
cd BackEnd\TestTCCBackEnd\TestTCCBackEnd
dotnet run
```

Backend จะรันที่ `http://localhost:5228`

Swagger:

```text
http://localhost:5228/swagger
```

### 2. รัน Frontend

ไปที่

```powershell
cd FrontEnd\TestTCCFrontEnd
npm install
npm start
```

Angular dev server จะใช้ proxy ไปหา backend ตาม `proxy.conf.json`

## เทคโนโลยีที่ใช้

### Frontend

- Angular 19
- Angular Router
- Angular SSR / Hydration
- RxJS
- Bootstrap 5
- `jsbarcode`
- `qrcode`

### Backend

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- JWT Bearer Authentication
- BCrypt
- Swagger / OpenAPI
