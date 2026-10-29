# دليل المساهمة — Crow Station

هذا الدليل لطريقة العمل اليومية على الكود. سياسة الفروع الدائمة ونقل القرارات من التخطيط في [BRANCHING.md](BRANCHING.md).

## 1. قبل أن تبدأ

```bash
make setup
```

يقوم بـ: نسخ `.env.example` إلى `.env`، وتفعيل git hooks، وتثبيت أدوات .NET، واستعادة الحزم. أو افتح المستودع في **Dev Container** (VS Code: «Reopen in Container») وسيُنفَّذ تلقائياً.

**لا تبدأ ميزة منتج دون مواصفة معتمدة.** كل ميزة ترتبط بقرار في فرع plan (مثل `H-D41`) أو مواصفة في فرع docs، وتذكره في وصف الـ Pull Request.

## 2. سير العمل

1. أنشئ فرع مهمة قصيراً من `develop`:
   ```bash
   git switch develop && git pull
   git switch -c feat/course-catalog
   ```
   البادئات: `feat/` `fix/` `refactor/` `docs/` `ci/` `chore/`.
2. اعمل بـ commits صغيرة بصيغة **Conventional Commits** (يفحصها hook `commit-msg`):
   ```text
   feat(api): add course catalog endpoint
   fix(web): keep RTL layout on small screens
   ```
3. شغّل الفحوص محلياً قبل الرفع: `make ci` (أو `make backend-test-unit` و`make web-test` للسرعة).
4. افتح Pull Request إلى `develop`. عنوانه يتبع نفس الصيغة لأنه يصبح رسالة الـ commit عند الدمج (Squash).
5. يجب أن ينجح فحص **`CI / ci-success`** وتُحل كل نقاشات المراجعة قبل الدمج.
6. احذف فرع المهمة بعد الدمج. الفروع الدائمة لا تُحذف أبداً.

ترقية الإصدار: `develop` ← PR إلى `staging` (اختبار) ← PR إلى `main` ← وسم `vX.Y.Z` على main يبني صور الإنتاج وينشئ GitHub Release.

## 3. ما يفحصه CI على كل Pull Request

| الفحص | ماذا يفعل |
| --- | --- |
| Backend | restore بملفات القفل، `dotnet format`، بناء بالمحللات (التحذير = خطأ)، اختبارات Unit + Architecture + Integration على PostgreSQL وRedis وRustFS حقيقية، تقرير تغطية |
| Web | ESLint، TypeScript، Prettier، اختبارات Vitest مع تغطية، بناء الإنتاج |
| Docker | hadolint، بناء الصورتين، فحص الثغرات بـ Trivy (يفشل على HIGH/CRITICAL القابلة للإصلاح)، رفع النتائج إلى Code Scanning |
| Smoke | تشغيل النظام كاملاً بـ Docker Compose والتحقق من نقاط الصحة والصفحة الرئيسية |
| Security | CodeQL لـ C# وTypeScript وGitHub Actions، gitleaks للأسرار، Dependency Review للحزم الجديدة ورخصها |
| Workflows | actionlint لملفات GitHub Actions |
| PR title | صيغة Conventional Commits |

الوظائف تتخطى الأجزاء التي لم تتغير، والفحص الجامع `ci-success` هو الوحيد المطلوب في حماية الفروع.

## 4. قواعد الكود

### Backend
- **قاعدة الاعتماد:** Api ← Infrastructure ← Application ← Domain. الاختبارات المعمارية تفشل إذا كسرتها.
- Domain لا يعرف EF Core ولا ASP.NET ولا أي مكتبة خارجية.
- حالة الاستخدام = `IQuery<T>` أو `ICommand` + handler من نوع `internal sealed` في `Application/Features/<Feature>/`. يُسجَّل تلقائياً.
- الأخطاء المتوقعة تُعاد كـ `Result`/`Error`، لا كاستثناءات. الاستثناءات للأخطاء غير المتوقعة فقط.
- نقاط HTTP صغيرة: تستدعي handler وتحوّل النتيجة بـ `ToHttpResult()`.
- كل تغيير في قاعدة البيانات = migration: `make migration NAME=AddCourses` ثم `make migrate`.
- الإعدادات من `appsettings` ومتغيرات البيئة. **لا أسرار في git.**

### Frontend
- Server Components افتراضياً؛ `"use client"` فقط عند الحاجة لتفاعل المتصفح.
- طلبات الـ API من الخادم عبر `src/lib/api-client.ts`.
- العربية أولاً: استخدم خصائص CSS المنطقية (`ms-`/`me-`/`ps-`/`pe-` في Tailwind) بدل left/right.

### الاختبارات
- منطق Domain وApplication: اختبارات Unit.
- نقاط HTTP ومسارات البيانات: اختبارات Integration في `CrowStation.Api.IntegrationTests` (تحتاج Docker).
- مكوّنات الويب والدوال: Vitest + Testing Library.

## 5. إعدادات GitHub المطلوبة من مسؤول المستودع

هذه لا تُضبط من الكود:

1. **حماية الفروع:** إضافة `ci-success` كفحص مطلوب على `develop` و`staging` و`main` في قاعدة الحماية الموجودة.
2. **Code scanning:** تفعيل GitHub Advanced Security / Code scanning (مجاني للمستودعات العامة) حتى تظهر نتائج CodeQL وTrivy.
3. **Dependabot:** تفعيل Dependabot alerts وsecurity updates. ملف `.github/dependabot.yml` يوجّه التحديثات إلى `develop`.
4. **الحزم:** صور Docker تُنشر إلى `ghcr.io/crow-developers/crow-station-api` و`-web`. اجعلها خاصة أو عامة حسب قرار الفريق.
5. **النشر (لاحقاً):** إنشاء بيئات `development` و`staging` و`production` (مع مراجعين إلزاميين للإنتاج)، ثم ضبط المتغير `DEPLOY_ENABLED=true` واستبدال خطوة النشر في `cd.yml`.
6. **CODEOWNERS:** إضافة `.github/CODEOWNERS` بأسماء حسابات الفريق عند تحديدها.
