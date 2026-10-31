# تشغيل الباك اند على سيرفر تطوير Debian وفحصه بـ Scalar

هذا الدليل يشغّل المشروع كاملاً على جهاز Debian 13 في البيت أو في المكتب، ثم يفتح التوثيق التفاعلي للـ API (Scalar) من جهازك عبر الـ VPN.

| | |
| --- | --- |
| النظام | Debian 13 (trixie) |
| المتطلبات | مستخدم عنده `sudo`، إنترنت، 4 GB رام على الأقل، 20 GB مساحة فارغة |
| ما يعمل على السيرفر | PostgreSQL وRedis وRustFS وAPI (.NET) والويب (Next.js)، كلها داخل Docker |
| طريقة الوصول | Tailscale (موصى به) أو نفق SSH |

> **ملاحظة:** الأوامر التي تبدأ بـ `server$` تُنفَّذ على السيرفر، والتي تبدأ بـ `laptop$` تُنفَّذ على جهازك.

---

## 1. ربط السيرفر بـ Tailscale (موصى به)

يجعل الـ API متاحاً لأجهزتك فقط، دون فتح أي منفذ على الراوتر.

```bash
server$ sudo apt update && sudo apt install -y curl
server$ curl -fsSL https://tailscale.com/install.sh | sh     # السكربت الرسمي: يضيف مستودع Tailscale المناسب لنسخة Debian
server$ sudo tailscale up --hostname=crow-dev
server$ tailscale ip -4        # احفظ هذا العنوان، مثال: 100.101.102.103
```

افتح الرابط الذي يظهر ووافق على الجهاز. ثم ثبّت Tailscale على جهازك وسجّل دخولك بنفس الحساب.

**بدون Tailscale:** يعمل كل شيء أيضاً، لكن الـ API يبقى على `127.0.0.1` وتصل إليه بنفق SSH (القسم 4).

---

## 2. تنزيل المشروع

المستودع خاص، لذلك يحتاج السيرفر صلاحية قراءة. أسهل طريقة مفتاح SSH تضيفه لحسابك في GitHub:

```bash
server$ sudo apt update && sudo apt install -y git make
server$ ssh-keygen -t ed25519 -C "crow-dev-server"
server$ cat ~/.ssh/id_ed25519.pub
```

انسخ الناتج إلى GitHub من **Settings ← SSH and GPG keys ← New SSH key**. ثم:

```bash
server$ git clone git@github.com:Crow-developers/Crow-Station.git
server$ cd Crow-Station
server$ git checkout develop
```

---

## 3. التهيئة والتشغيل بأمر واحد

```bash
server$ make server-setup
```

هذا الأمر يشغّل `sudo ./scripts/dev-server/setup.sh`. يمكن تشغيله أكثر من مرة دون ضرر.

**ما يفعله:**

1. يتحقق أن النظام Debian، ويثبّت `curl` و`jq` و`openssl`.
2. يثبّت Docker Engine وCompose من مستودع Docker الرسمي، إذا لم يكونا مثبتين، ويضبط تدوير السجلات.
3. يضيف مستخدمك إلى مجموعة `docker`.
4. يكتشف عنوان Tailscale. إذا لم يجده يستعمل `127.0.0.1`.
5. يُنشئ ملف `.env` بكلمات سر عشوائية، بصلاحية `600`. لا يُرفع إلى git أبداً.
6. يبني الصور ويشغّل الخدمات، وتعود تلقائياً بعد إعادة تشغيل السيرفر (`compose.server.yaml`).
7. يفحص الـ API ويطبع رابط Scalar.

لاختيار العنوان بنفسك:

```bash
server$ sudo ./scripts/dev-server/setup.sh --bind 127.0.0.1
```

### الحسابات التي يُنشئها

| الحساب | أين | القيمة |
| --- | --- | --- |
| مستخدم Linux الحالي | مجموعة `docker` | يستطيع تشغيل `docker` دون `sudo` بعد تسجيل الخروج والدخول |
| مستخدم قاعدة البيانات | PostgreSQL | `crow_app` بكلمة سر عشوائية (`POSTGRES_PASSWORD`) |
| مفاتيح التخزين | RustFS (S3) | `STORAGE_ACCESS_KEY` و`STORAGE_SECRET_KEY` عشوائية |

كل القيم في `.env` على السيرفر فقط. لعرضها: `cat .env`.

### المنافذ

| الخدمة | المنفذ | يستمع على |
| --- | --- | --- |
| API و Scalar | 8080 | عنوان Tailscale (أو `127.0.0.1`) |
| الويب | 3000 | عنوان Tailscale (أو `127.0.0.1`) |
| PostgreSQL | 5432 | `127.0.0.1` فقط |
| Redis | 6379 | `127.0.0.1` فقط |
| RustFS و لوحته | 9000 و 9001 | `127.0.0.1` فقط |

قاعدة البيانات والتخزين لا تُفتح على الشبكة أبداً.

---

## 4. فتح Scalar وفحص الـ API

### عبر Tailscale

افتح في متصفح جهازك:

```text
http://<عنوان Tailscale>:8080/scalar/
```

### عبر نفق SSH (بدون Tailscale)

```bash
laptop$ ssh -N -L 8080:127.0.0.1:8080 <user>@<server-ip>
```

اترك النافذة مفتوحة، وافتح `http://localhost:8080/scalar/`.

### التجربة

1. اختر **Diagnostics ← GET /api/v1/diagnostics/time**.
2. اضغط **Test Request**، ثم **Send**.
3. النتيجة المتوقعة `200` مع وقت السيرفر (`utcNow`).

روابط أخرى:

| الرابط | الفائدة |
| --- | --- |
| `/health/live` | هل التطبيق يعمل |
| `/health/ready` | هل قاعدة البيانات وRedis والتخزين جاهزة |
| `/openapi/v1.json` | ملف OpenAPI (لـ Postman أو لتوليد عميل) |

> Scalar يعمل فقط في وضع `Development`، وهو الوضع المضبوط في `compose.yaml`. لن يظهر في الإنتاج.

---

## 5. الأوامر اليومية

| الأمر | ماذا يفعل |
| --- | --- |
| `make check` | يفحص كل الخدمات ويطبع الروابط |
| `make ps` | حالة الخدمات |
| `make logs` | السجلات مباشرة (`Ctrl+C` للخروج) |
| `make down` | إيقاف الخدمات مع حفظ البيانات |
| `git pull && docker compose up -d --build --wait` | تحديث الكود وإعادة البناء |
| `make reset` | **يحذف كل البيانات** ويوقف الخدمات |

**البدء من الصفر بكلمات سر جديدة:** `make reset`، ثم `rm .env`، ثم `make server-setup`. تغيير كلمة سر قاعدة البيانات في `.env` وحده لا يكفي، لأن القاعدة الموجودة تحتفظ بالكلمة القديمة.

---

## 6. إعطاء Claude وصولاً إلى السيرفر

Claude يعمل في السحابة ولا يصل إلى جهازك مباشرة. للوصول، شغّل Claude Code على السيرفر بوضع Remote Control:

```bash
server$ curl -fsSL https://claude.ai/install.sh | bash
server$ cd ~/Crow-Station
server$ claude                 # مرة واحدة لتسجيل الدخول، ثم /exit
server$ claude remote-control
```

يظهر السيرفر في تطبيق Claude Code. يطلب Claude إذنك قبل استعماله في كل جلسة، ويعمل فقط داخل مجلد `Crow-Station`.

لإبقائه يعمل بعد إغلاق نافذة SSH، شغّله داخل `tmux`:

```bash
server$ sudo apt install -y tmux
server$ tmux new -s claude      # ثم claude remote-control داخلها، والخروج بـ Ctrl+B ثم D
```

---

## 7. الأمان

- لا تستعمل `--bind 0.0.0.0` على سيرفر متصل بالإنترنت. Docker يتجاوز جدار `ufw` للمنافذ المنشورة.
- ملف `.env` يبقى على السيرفر. لا ترسله في المحادثات ولا ترفعه إلى git (هو في `.gitignore`).
- هذا سيرفر تطوير، وليس إنتاجاً. كلمات السر عشوائية لكن الإعدادات للتطوير (`Development`).

---

## 8. حل المشاكل

| المشكلة | الحل |
| --- | --- |
| `permission denied` عند `docker` | سجّل الخروج والدخول بعد إضافتك لمجموعة `docker`، أو استعمل `sudo`. |
| فشل `API ready` في `make check` | `docker compose logs api postgres` لمعرفة الخدمة المتعطلة. |
| Scalar لا يفتح من جهازك | تأكد أن Tailscale متصل على الجهازين (`tailscale status`)، وأن `PUBLIC_BIND_ADDRESS` في `.env` هو عنوان Tailscale. |
| `toomanyrequests` من Docker Hub | انتظر قليلاً، أو أضف `"registry-mirrors": ["https://mirror.gcr.io"]` إلى `/etc/docker/daemon.json` ثم `sudo systemctl restart docker`. |
| الخدمات لم تعد بعد إعادة التشغيل | `make check`، ثم `docker compose up -d --wait` في مجلد المشروع. |
| تغيّر عنوان Tailscale | عدّل `PUBLIC_BIND_ADDRESS` و`WEB_ORIGIN` في `.env`، ثم `docker compose up -d --wait`. |
