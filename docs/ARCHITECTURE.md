# بنية الكود وقرارات القالب

> يصف هذا الملف الهيكل الأساسي في فرع `develop`. قرارات المنتج نفسها في فرع plan، والمواصفات المعتمدة في فرع docs.

## 1. الطبقات

```text
            ┌──────────────────────────┐
            │       CrowStation.Api    │  HTTP, ProblemDetails, OpenAPI, health, config
            └────────────┬─────────────┘
                         │
            ┌────────────▼─────────────┐
            │ CrowStation.Infrastructure│  EF Core/PostgreSQL, Redis, S3, health checks
            └────────────┬─────────────┘
                         │ implements interfaces of
            ┌────────────▼─────────────┐
            │  CrowStation.Application │  use cases, IQuery/ICommand, IUnitOfWork
            └────────────┬─────────────┘
                         │
            ┌────────────▼─────────────┐
            │    CrowStation.Domain    │  entities, aggregates, domain events, Result/Error
            └──────────────────────────┘
```

الاعتماد يتجه إلى الداخل فقط. تفحص `CrowStation.Architecture.Tests` هذه القاعدة في كل بناء:

- Domain لا يعتمد على أي طبقة، ولا على EF Core أو ASP.NET أو Redis أو AWS.
- Application لا يعتمد على Infrastructure أو Api أو أي إطار بنية تحتية.
- Infrastructure لا يعتمد على Api.
- كل handler في Application من نوع `sealed` وغير عام.

## 2. مسار طلب نموذجي

`GET /api/v1/diagnostics/time` ← `ApiV1` يستدعي `IQueryHandler<GetServerTimeQuery, ServerTimeResponse>` ← الـ handler في Application يعيد `Result<ServerTimeResponse>` ← `ToHttpResult()` يحوّله إلى `200` أو إلى ProblemDetails بالحالة المناسبة (400/403/404/409).

هذا مثال فقط لتوضيح المسار؛ يُستبدل بالميزات الحقيقية بعد اعتماد المواصفات.

## 3. نقاط التشغيل

| المسار | الغرض |
| --- | --- |
| `/health/live` | العملية تعمل (لا يفحص أي اعتماد) |
| `/health/ready` | قاعدة البيانات والكاش والتخزين متاحة؛ JSON بحالة كل فحص |
| `/openapi/v1.json` و`/scalar` | وثيقة OpenAPI وواجهتها التفاعلية (في Development فقط) |
| `/api/health` (الويب) | صحة خادم Next.js |

## 4. قرارات القالب

| القرار | السبب |
| --- | --- |
| بلا MediatR | أصبحت مكتبة تجارية؛ الواجهات البسيطة `IQueryHandler`/`ICommandHandler` مع تسجيل تلقائي تكفي ولا تضيف اعتماداً. |
| `Result`/`Error` بدل الاستثناءات | الأخطاء المتوقعة جزء من العقد وتتحول إلى HTTP بشكل موحّد. |
| Central Package Management + ملفات قفل | إصدار واحد لكل حزمة في `Directory.Packages.props`، وCI يستعيد بـ `--locked-mode` فلا تتغير الحزم دون قصد. |
| المحللات بمستوى Recommended والتحذير = خطأ | المشاكل تُكتشف عند البناء لا في المراجعة. |
| Microsoft.Testing.Platform + xUnit v3 | المشغّل الافتراضي في .NET 10؛ التغطية وتقارير TRX مدمجة. |
| Testcontainers | اختبارات التكامل تعمل على PostgreSQL وRedis وRustFS حقيقية، لا على بدائل في الذاكرة. |
| RustFS بدل MinIO محلياً | MinIO توقفت عن نشر صور Docker الرسمية. الكود يستخدم S3 القياسي عبر `AWSSDK.S3`، فتبديل المزوّد إعداد فقط. |
| صورة API من نوع chiseled | بلا shell ولا مدير حزم، وتعمل بمستخدم غير root؛ سطح هجوم أصغر. |
| Next.js `output: "standalone"` و`API_BASE_URL` وقت التشغيل | صورة واحدة صغيرة تعمل في كل البيئات دون إعادة بناء. |
| لا جداول ولا migrations بعد | نموذج البيانات لم يُعتمد (راجع فرع plan). `AppDbContext` جاهز لإضافة الكيانات. |
| Actions مثبتة بـ SHA | حماية من تغيّر وسم (tag) لمكتبة خارجية؛ Dependabot يحدّثها أسبوعياً. |

## 5. ما لم يُحسم بعد

- مزوّد التخزين والاستضافة في الإنتاج، وعزل بيانات المدرسين (H-Q23).
- المصادقة والصلاحيات (ستُضاف كطبقة في Api وInfrastructure بعد اعتماد مصفوفة الصلاحيات).
- تطبيق الموبايل (Flutter) ودعم اللغة الإنجليزية في الويب.
