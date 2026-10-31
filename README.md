# Crow Station

منصة تعليمية للكورسات والتطبيق العملي والمسارات والإرشاد. المشروع في مرحلة التحليل والتوثيق، ويحتوي فرع `develop` على الهيكل الأساسي للكود (Backend + Frontend + البنية التحتية + CI/CD) دون ميزات المنتج بعد؛ لا توجد نسخة منتج منشورة.

## الفروع الدائمة

| الفرع | دوره |
| --- | --- |
| `plan` | الخطط والمسودات والقرارات والأسئلة المفتوحة طوال عمر المشروع. |
| `docs` | التوثيق الرسمي الذي راجعه واعتمده الفريق، مع تمييز المصمم من المنفذ ومن توثيق الإصدارات. |
| `develop` | دمج التطوير اليومي بعد مراجعة التغييرات. |
| `staging` | نسخة مرشحة للاختبار والتحقق قبل الإصدار. |
| `main` | الإصدارات المعتمدة للإنتاج. وجود الفرع الآن لا يعني وجود نشر أو منتج جاهز. |

لبدء قراءة الخطط، انتقل إلى فرع **plan** وافتح `docs/README.md`.

هذه فروع دائمة، خصوصاً **plan**؛ لا يُحذف بعد نقل قرار منه أو إغلاق مهمة. المستندات الحالية مسودات تخطيط، ولم تتحول إلى توثيق رسمي بمجرد رفعها.

راجع [سياسة الفروع والتوثيق](BRANCHING.md) لطريقة نقل القرارات والمراجعة والإصدارات. الفروع لا تُنشئ بيئات استضافة أو نشر تلقائياً.

## الهيكل التقني (فرع develop)

| الجزء | التقنية |
| --- | --- |
| Backend | ASP.NET Core على .NET 10 بأسلوب Clean Architecture (`backend/`) |
| Frontend | Next.js 16 + TypeScript + Tailwind CSS، عربي أولاً RTL (`frontend/web/`) |
| قاعدة البيانات | PostgreSQL 17 عبر EF Core |
| الكاش | Redis 8 |
| تخزين الملفات | متوافق مع S3: RustFS محلياً (صور MinIO الرسمية لم تعد تُنشر)؛ مزوّد الإنتاج لم يُحسم |
| الموبايل | Flutter لاحقاً (`mobile/`) |
| التشغيل | Docker + Docker Compose + Makefile |
| CI/CD | GitHub Actions: فحص وبناء واختبار، فحص أمني، نشر صور موقّعة إلى GHCR |

```text
backend/
  src/
    CrowStation.Domain/          قواعد العمل: كيانات، أحداث، Result/Error. لا يعتمد على أي طبقة أو إطار.
    CrowStation.Application/     حالات الاستخدام (queries/commands) والواجهات المجردة. يعتمد على Domain فقط.
    CrowStation.Infrastructure/  EF Core + PostgreSQL، Redis، S3، فحوص الصحة. ينفّذ واجهات Application.
    CrowStation.Api/             نقاط HTTP، الأخطاء (ProblemDetails)، OpenAPI، الإعدادات. نقطة التشغيل.
  tests/                         Unit + Architecture + Integration (Testcontainers)
frontend/web/                    تطبيق Next.js
mobile/                          محجوز لتطبيق Flutter
compose.yaml  Makefile  .github/workflows/
```

## البدء السريع

المتطلبات: .NET 10 SDK، Node.js 22، Docker مع Compose v2، و`make`. أو افتح المستودع في **Dev Container** وكل شيء جاهز.

```bash
make setup     # .env + git hooks + أدوات .NET + الحزم
make up        # يشغّل كل شيء: API على 8080، الويب على 3000
make help      # قائمة كل الأوامر
```

للتطوير اليومي مع إعادة التحميل التلقائي:

```bash
make infra     # PostgreSQL + Redis + التخزين فقط
make api       # API على http://localhost:8080 (التوثيق التفاعلي: /scalar)
make web       # الويب على http://localhost:3000
```

قبل فتح Pull Request: `make ci` يشغّل محلياً ما يشغّله CI.

لتشغيل المشروع على سيرفر تطوير Debian وفحص الـ API بـ Scalar عبر Tailscale: [دليل سيرفر التطوير](docs/DEV-SERVER.md) (`make server-setup`).

راجع [دليل المساهمة](CONTRIBUTING.md) لطريقة العمل والمراجعة، و[وثيقة البنية](docs/ARCHITECTURE.md) لقواعد الطبقات وقرارات القالب.

