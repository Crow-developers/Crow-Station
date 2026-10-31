# Crow Station — Analysis and Planning Handoff for the Team

> Version 0.4 · 31 October 2026 · Reference for completing questions and decisions · Analysis phase only

## 1. Start here

**What the team is asked to do:** help the project owner complete the business rules and open questions, and turn the answers into clear, reviewable requirements before designing the database, the API, and the implementation plan. This handoff does not start programming, and it does not turn proposals into approved decisions.

**Exact resume point — H-Q07 (remaining part):** after the platform team decides that the new instructor breached their obligations (H-D55), how is the case settled: what do students get, from which money, and what is the effect on the new instructor? H-Q01 to H-Q04 were settled on 29 October 2026, and H-Q05, H-Q06, and the first part of H-Q07 on 31 October 2026; see sections 4.3 and 4.4 and the [decision cards](DECISION-CARDS.en.md).

**Current reference:** the product and data planning documents are at version 0.3, dated 31 October 2026, and include decisions H-D01 to H-D45 and the section 5 changes. Decisions H-D46 to H-D55 (section 4.4) are newer than both documents and not yet merged into them. If a conflict appears, section 4 of this file takes precedence until the documents are updated.

Read in this order:

1. This file, especially the latest decisions and the resume point.
2. The [product document](PROJECT-BRIEF.en.md): vision, scope, business rules, and journeys, applying the corrections in this handoff.
3. [Data planning](DATA-PLANNING.en.md): proposed entities and relationships, not approved tables.
4. The [branch policy](../BRANCHING.md) and [repository status](../REPOSITORY-STATUS.md).

## 2. The project and the scope of this task

Crow Station is a unified learning platform that brings together courses from the platform owner and other instructors. Students enter a single catalog; each instructor has their own page and a single dashboard for themselves and their team. It is not a set of separate academies with their own domains and branding per instructor. Calling it "multi-tenant" does not settle how technical isolation is done.

| Area | Current status |
| --- | --- |
| Audience | Programming and cybersecurity; Iraq first, with access allowed from abroad. Payment follows the available services and agreements. |
| Channels | Responsive website and mobile app, with Arabic and English interfaces. |
| Team and timeline | The project owner with a team of 10 people with intermediate experience in programming, cybersecurity, and AI tools; September 2027 is a preliminary, revisable launch target. |
| Planning size | About 1,000 users; the split between viewers, code runners, chats, and the actual load needs detail. |
| Technologies with agreed direction | Clean Architecture, ASP.NET Core on .NET 10, PostgreSQL, Flutter for mobile, Docker and Docker Compose. |
| Open technical decisions | Web front end, isolation, MinIO or an alternative, confirming Redis and its role, video streaming, search, code execution, the AI provider, and production operations. |
| Needed now | Answers and business rules, a decision log, and a permissions matrix; then a logical model, API contracts, and an implementation plan once requirements are stable. |
| Not needed now | Programming the product, creating final tables or migrations, launching production environments, or choosing providers without review. |

The first version has a broad scope: accounts, instructors, courses, payments and wallets, video and progress, the desktop editor, tests, grading and certificates, course chat, tracks and mentoring, search and notifications, and the paid assistant service. This is an indicative list; scope details are in the product document.

Crow Store, Crow Hub, and VS are planned as "coming soon" features. Translation, dubbing, and general AI are deferred; **AI code grading and AI review of grading appeals are a required exception from the start**. Rooms and Streak still have undecided launch timing and details; do not automatically place them inside or outside the first version.

## 3. Collaboration rules and approving answers

- We discuss **one question at a time** with the project owner. The team may prepare several topics, but we do not ask them to answer a long list in one go.
- We collect a related group of answers, then document, review, and update Arabic and English in one batch.
- "Yes" after a two-option question is not a clear choice; ask which one is meant. No answer is not agreement.
- When asked "what is best?", the team gives a recommendation with its reasons and impact; it stays a proposal until the project owner explicitly approves it.
- If the project owner changes an earlier decision, we record the change and remove the effect of the cancelled decision from rules, journeys, acceptance criteria, and the data model.
- A settled question is not raised again unless there is a new conflict or an explicit review request. Unsettled details within the same topic may be asked.
- The phrase "from the dashboard" alone does not say who holds the permission, the scope of the setting, or when it takes effect and how it affects existing agreements; fill in these limits when needed.
- Specialist recommendations are input for review; we do not attribute approval to the project owner or a role to a team member without agreement.

| Status | Meaning in the team log |
| --- | --- |
| Agreed | An explicit decision; can be revised later by a new documented decision. |
| Preliminary | An accepted direction with details or final review pending. |
| Proposed | A recommendation that has not been approved. |
| Open | A question without a sufficient answer. |
| Deferred | Discussion or implementation is deferred, stating which. |
| Cancelled or superseded | An earlier decision that was overturned; it stays in the history and is not used as a current rule. |

## 4. Latest decisions — these take precedence over the version 0.2 text when they conflict

This section comes from the project owner's answers in the discussion after version 0.2 was published and until the handoff was requested. The H-D identifiers belong to this handoff; they do not replace the existing O and D identifiers. They were compiled on 8 October 2026; this does not claim every answer was given on that day.

| ID | Current decision | What remains open or what the decision does not mean |
| --- | --- | --- |
| H-D01 | Transferring a course removes all permissions of the previous instructor and their team on the transferred course, while keeping their account, records, expenses, and balance. | It does not delete the instructor's account or remove their permissions on their other courses; the limits on viewing data of past students need defining. |
| H-D02 | A refund follows the original purchase and the shares of its parties: pre-transfer sales follow the old agreement, and later ones follow the new agreement. | Responsibility of the new instructor for breaching an obligation transferred to them is defined in the transfer agreement. The detailed proof and settlement mechanism is not settled. |
| H-D03 | Every purchase has a 7-day period during which its earnings do not move to the available balance. Earnings from an incomplete course stay held until the course is completed and approved. | These are cumulative conditions where they apply; the end of the period alone does not release earnings from an incomplete course. |
| H-D04 | A request submitted within the period is not rejected because of administrative delay. The purchase amount stays held until the request is decided. | This does not mean the refund is automatically accepted; it is assessed under the rules. Review time and the refund execution procedure need detail. |
| H-D05 | A course may be transferred while a refund is pending; the amount stays linked to the instructor the student bought from. An approved refund is paid from the held amount; otherwise the earnings return to the first instructor once the other release conditions are met. | A change of owner does not move the old transaction's earnings to the new owner, and does not drop the request. Details of partial refunds and the remainder will be set by the formula later. |
| H-D06 | Correcting, updating, or replacing a purchased lesson gives the earlier buyer the new version at no extra charge, and they do not lose the purchased content. | There is no approval yet for keeping every old version as a visible archive; the effect of updates on progress is the current question. |
| H-D07 | An independent add-on outside the promised content may be priced, provided it is optional and does not reduce the original course. | How this add-on is sold needs to be tied to the whole-course purchase decision; we do not assume it is a single lesson offered for sale. |
| H-D08 | Moving a lesson to another course does not remove access for those who owned it, does not erase their progress, and does not require them to buy the other course. | It does not automatically give them the rest of the new course's lessons or services. How the lesson appears in their library needs journey design. |
| H-D09 | ~~The same lesson can be used in several courses.~~ **Superseded by H-D40 on 29 October 2026.** | The project owner did not recall this answer when reviewing it, and the repository does not hold its original wording. The rule in force is H-D40. |
| H-D10 | Purchases are for the whole course only, and each course has its own price, even if it contains lessons shared with a course the student owns. | This cancels selling sections and lessons individually, a "complete the course" price, and discounting shared lessons when buying another course. |
| H-D11 | No special notice tells the student that lessons repeat between the course they want to buy and courses they own. | This does not cancel the usual syllabus display or course description. |
| H-D12 | The proposal that the platform pays refunds from its own money and records a debt on the instructor when their balance runs out was not approved; the project owner stressed holding amounts while refund eligibility lasts. | Any possible exceptional case outside the hold, such as liability for a breach after transfer, needs a separate decision; we do not assume the platform funds it or that the student's right is automatically refused. |
| H-D13 | A course sequence may include video lessons, text lessons, and standalone tests. | The meaning of completion and of benefit for refund purposes differs by type and needs a decision; a video watch threshold does not automatically apply to text or tests. |

#### 4.2 Exploratory session decisions — 19 October 2026

This section comes from the full exploratory session log with the project owner: `docs/PLATFORM-DECISIONS-LOG.en.md` (147 questions across 30 areas). Only **settled** decisions are recorded here; items marked "open / not settled / needs detail" stay tracked in the log file and are not repeated here. IDs from H-D14 onward extend the section 4 series and do not replace O, D, or H-Q identifiers.

| ID | Current decision | What remains open or what the decision does not mean |
| --- | --- | --- |
| H-D14 | Identity and access: students' accounts link to GitHub/GitLab automatically; MFA is mandatory for instructors and administrators; account recovery uses both security questions and identity verification; concurrent sessions are limited to two devices (phone and computer). | The MFA method is not defined; the idle sign-out policy is open. |
| H-D15 | Edge protection: a WAF with custom rules for paid courses. | Rule tuning is deferred to implementation. |
| H-D16 | Labs: lab state is saved between sessions, and a full terminal (Linux shell) is provided. | Isolation technology, resource limits, Docker-in-Docker, and preventing malicious activity are open (NQ-07–NQ-10). |
| H-D17 | Content and ownership: content ownership follows the instructor's contract; old courses are not removed from the platform. | The DMCA process, protection against internal theft, and the major-version policy (v1→v2) are open. |
| H-D18 | Learning experience: personal notes tied to video timestamps, Picture-in-Picture, and a visual progress timeline. | Offline mode on mobile and sharing bookmarks are open. |
| H-D19 | Tests and certificates: automated proctoring for critical tests; programming questions with automated grading and text questions with initial AI grading; publicly verifiable certificates. | Anti-cheating details and anti-forgery technology are open. |
| H-D20 | Advanced AI: a personal AI tutor, initial AI grading of text questions, automatic summaries, and smart recommendations. | Quality criteria and appeal escalation stay tied to O-11 and H-Q13. |
| H-D21 | Privacy and accessibility: commitment to the right to be forgotten and to WCAG 2.1 AA. | Data storage location and compliance with GDPR and Iraqi law are open; handling minors is open. |
| H-D22 | Community: a discussion forum for each course, run by the instructor and assistant, with administration access; public comments on lessons with offensive-language filtering. | The reputation system and Discord/Slack channels are open. |
| H-D23 | Growth: a referral program, and percentage coupons with a usage limit. | An affiliate program (future), a public API, and geographic pricing are open. |
| H-D24 | Operational resilience: multi-region deployment. | RPO/RTO targets and the outage communication plan are open. |
| H-D25 | Corporate training (B2B): an enterprise solution with a reporting dashboard, employer access to top performers' records, university partnerships, and student discounts. | The student consent mechanism, access scope, and contract pricing are open (tied to O-22). |
| H-D26 | Live events: free webinars and workshops, and live Q&A sessions on a schedule set by the course presenter. | Live streaming of paid courses and automatic recording are open. |
| H-D27 | Gamification: points and badges, leaderboards, adaptive learning, personalized tracks, and a streak system. | Launch timing of Streaks and Rooms stays tied to O-01 and H-Q21; approval does not mean launching in the first version. |
| H-D28 | Wallet and competitions: an internal digital wallet for instructors to manage their earnings; periodic programming competitions and CTF competitions. | Cryptocurrency, installments, an internal currency, hackathons, and prize funding are open. |
| H-D29 | Accreditation: pursue professional accreditation and continuing education hours (CEUs/CPEs), partnerships with major companies, and digitally signed instructor certificates. | The timeline and choice of partners are deferred to planning. |
| H-D30 | Security: periodic penetration tests by specialists, and building an internal security team. | Cyber insurance, the incident response plan, and the breach notification mechanism are open. |
| H-D31 | Geographic expansion: other Arab markets, accepting instructors from other countries, partnerships with foreign platforms, and content translation in the future. | How to handle cultural differences is open. |
| H-D32 | Open resources: free tools for students and ready-made project templates. | An open library, community contributions, and a public Q&A forum are open. |
| H-D33 | Collaboration and marketplace: group projects with code review between members; a marketplace for projects and templates with review before publishing, ratings, and multiple licenses; platform plugins are not published before testing. | The collaborative editor, task splitting, contribution assessment, commission rate, and plugin revenue model are open. |
| H-D34 | Career services: a job board for graduates, mock interviews, job recommendations, and soft-skills workshops. | A CV review service is open. |
| H-D35 | Instructor analytics: an advanced analytics dashboard, video heatmaps, smart improvement recommendations, and benchmarking between courses. | Predictive dropout analytics are open. |
| H-D36 | Subscription and preview: a freemium model; a free preview limited to the first three videos of a course, with the rest paid; **no** family memberships; **no** peer assessment system. | Unlimited annual subscriptions and pay-what-you-want are open; the preview does not break the whole-course purchase rule (H-D10). |
| H-D37 | Audio and peer learning: an advanced audio player and an official podcast channel; virtual study rooms and pair programming. | Offline podcast downloads, audio interviews, study groups, and peer teaching are open. |
| H-D38 | Alumni and open source: networking events and discounts for alumni; encouraging open-source contribution with a project list and Git workshops. | The alumni network structure, a mentorship system, and contributor rewards are open. |

**Note:** the decisions in this section expand the platform's feature scope and do not change the financial rules (refunds, holds, ownership transfer) in 4.1, and they do not close questions in the O, D, and H-Q logs. The list of required updates in section 5 is extended when these decisions are merged into the product and data planning documents in a later documentation batch.

**Important sequence:** the project owner first agreed to count a shared lesson once, then explicitly changed this to paying for each course separately. The decision in force is H-D10. Similarly, the decision that lesson updates are free limits the instructor's earlier freedom to price each update.

#### 4.3 Decisions from the 29 October 2026 session — updates, progress, sales, and technical foundation

The project owner answered H-Q01 to H-Q04 in a written session on 29 October 2026. The full cards, using the section 9 template, are in the [decision cards](DECISION-CARDS.en.md).

| ID | Current decision | What remains open or what the decision does not mean |
| --- | --- | --- |
| H-D39 | Updating a lesson the student has completed does not cancel their completion. The lesson stays complete, the student sees a notice that it was updated, and reviewing the new version is optional. Certificates issued before the update stay valid. | The form and channel of the notice, the effect of an update on a lesson not yet completed, and the definition of completion itself (H-Q12) are open. |
| H-D40 | Each lesson belongs to exactly one course. The instructor may copy a lesson to another course; the copy is an independent lesson with its own progress and updates, and editing one copy does not change the other. Supersedes H-D09. | Does not change H-D08 (moving a lesson) or H-D10 (each course priced independently). Who owns copies when course ownership is transferred, and whether quizzes and attachments are copied automatically, are open. |
| H-D41 | A paid add-on (H-D07) is shown on the course page as an optional extra with a separate price, and only students who own the course can buy it. It is not sold on its own to non-buyers. | Whether it can be bought together with the course in one transaction, its refund rules, and whether it moves with a course ownership transfer are open. |
| H-D42 | An incomplete course can only be sold with a declared syllabus: the list of upcoming lessons and an expected completion date, shown to the student before purchase. The course is complete when the whole list is published and approved by the platform, at which point the earnings release rule (H-D03) applies. | Limits on changing the declared syllabus after sale, who in the platform approves completion, and the review time are open. |
| H-D43 | If the declared completion date is missed, the instructor is entitled to one justified extension, and students are notified. If still late after the extension, the student may request a refund from the held amount, and sales of the course stop. | The maximum extension length, the refund request window, and resuming sales after completion are open. The refund calculation was settled in H-D49. |
| H-D44 | The web front end is Next.js with TypeScript, chosen by the project owner when building the code foundation on the develop branch. | The mobile app stays Flutter and is built later; English support in the web app is not designed yet. |
| H-D45 | Local environment: PostgreSQL, Redis for caching, and S3-compatible file storage through RustFS instead of MinIO, because official MinIO images are no longer published. | The production storage and hosting provider and the isolation approach are open (H-Q23). |

#### 4.4 Decisions from the 31 October 2026 session — refunds and instructor breach after transfer

The project owner answered H-Q05, H-Q06, and the first part of H-Q07 in a written session on 31 October 2026. The full cards are in the [decision cards](DECISION-CARDS.en.md). **These decisions take precedence over section 9.3 of product document 0.3 until it is updated.**

| ID | Current decision | What remains open or what the decision does not mean |
| --- | --- | --- |
| H-D46 | A lesson counts as "used" for refund calculation as follows: a **video** after 10% of its duration is watched; a **text lesson** as soon as it is opened; a **test** as soon as it is started. Free preview lessons are not deducted. | The 10% video threshold is still provisional and needs specialist review. |
| H-D47 | The course price is one total value and is not split across lessons. The instructor does not set a value per lesson. This cancels the rule "the instructor chooses an equal or different value per lesson" and the proposal to "fix the value distribution at purchase". | Does not change H-D10 (whole-course purchase). |
| H-D48 | A refund within the 7-day window = course price × (lessons not used ÷ number of lessons in the course at purchase). Lessons added after purchase are not counted. Example: 100,000 IQD and 20 lessons, the student used 4, so 80,000 IQD is refunded before fees (H-D50). | Rounding, whether preview lessons count in the lesson total, and refund rules for paid add-ons (H-D41) are open. |
| H-D49 | A refund for a course still late after its one extension (H-D43) uses the same formula as H-D48, but the total is the full **declared syllabus**, and unpublished lessons count as not used. Example: a 20-lesson syllabus, 8 published and all used by the student, so 60,000 of 100,000 IQD is refunded. The right to request stays valid after the 7-day window. | The deadline for requesting this refund, the maximum extension length, and resuming sales after completion are open. |
| H-D50 | The payment gateway fee that is not returned is deducted from the student's refund, and this is stated clearly in the refund policy. **Exception:** for a late-course refund (H-D49), the instructor's share bears the fee and the student receives the full calculated amount. | Handling manual payments, showing the fee to the student before purchase, and specialist legal review (O-22) are open. |
| H-D51 | The platform answers a refund request (accept or reject) within **3 working days** of submission. If the team is late, an internal alert goes to the administration, and the request is not rejected because of the delay (H-D04). | Working days and public holidays, who reviews the request, and delegating to a specialist department are open. |
| H-D52 | The student's access to the course stops **as soon as the refund request is submitted**, and the amount is calculated from what was used at that moment. If the request is rejected, access returns immediately. | The effect on the chat, paid add-ons, and the certificate is open. |
| H-D53 | The amount is refunded to the **same payment method** used for the purchase. If the transfer fails, the amount goes to the student's platform wallet and can be withdrawn. | Manual payments and the retry period before moving to the wallet are open. |
| H-D54 | After a partial refund, the instructor's share of the remainder is released **as soon as the refund is executed**, if the other hold conditions are met (for example course completion, H-D03). | Does not change the hold on an incomplete course. |
| H-D55 | If students complain about the new instructor after a course transfer (for example deleted lessons or stopping work on the course), **the platform team decides** whether a breach happened. It reviews the complaints and the course change log (what changed, when, and who changed it), hears from the new instructor, then decides. The log shows the change happened after the transfer, so the previous instructor is not held responsible. | Settlement after a proven breach (what students get, from which money, and the effect on the new instructor), the review deadline, and the right to appeal are open (rest of H-Q07). |

## 5. Reconciling older documents before building any design on them

**This list was applied in version 0.3 of the product and data documents (31 October 2026).** The table remains as a record of what changed and why.

| Location | Required correction | Decision reference |
| --- | --- | --- |
| Product document: terminology, 3.1, 6.1, and journey 18.1 | Remove partial sales and the "complete the course" price; purchases are for the whole course and each course has its own price. | H-D10 |
| Data planning: 5, D-03 and D-10; product document: 6.1 and 7 | Tie each lesson to one course, and add copying as an independent lesson that records its source; keep the student's completion when a lesson is updated, with an "updated" notice state. | H-D39, H-D40 |
| Product document: 6.1, 6.2 and 9.2; data planning: D-04 | Add the course-linked paid add-on, the declared syllabus with a date for incomplete courses, and the single extension followed by refunds and stopping sales. | H-D41–H-D43 |
| Product document: 6.1 and O-03 | Update the rules that corrections, updates, and replacements are free, and separate the independent add-on from the promised content. | H-D06, H-D07 |
| Product document: 6.2 and 9.2 | Remove examples of selling a single lesson from an incomplete course; keep holding all earnings from an incomplete course. | H-D03, H-D10 |
| Product document: 6.4 and O-15 | Turn the question about the old team's access into a settled decision, keeping the account and financial history. | H-D01 |
| Product document: 6.4, 9.3 and O-05 | Document refunds during transfer, preventing rejection because of administrative delay, and continuing the hold. | H-D02–H-D05 |
| Product document: 7.3 | Close the question of unlocking the editor by buying a single lesson, since it no longer applies; do not remove the code execution isolation question. | H-D10 |
| Data planning: 4.5, 5, 11 and 13.1 | Reset display and entitlement around the course purchase, with a continuing right to a moved lesson; separate content reuse from course pricing. | H-D01, H-D06–H-D10 |
| Data planning: 7.2 and D-06 | The hold on a request submitted on time is a settled decision, not just a proposal awaiting approval. | H-D04 |
| Data planning: 5.1 and 10; product document: 7 and 8 | Record mixed content types as a settled decision, keeping completion and refund rules for each type open. | H-D13 |
| Data planning: D-02 and D-03 | Update the settled parts while keeping unanswered details of transfer, progress, and versions open. | H-D01, H-D02, H-D05–H-D10 |
| Acceptance criteria: AC-07, AC-22 and purchase-related criteria | Change the AC-07 example to buying an incomplete course; extend AC-22 to permissions and holds; add cases for updates and reuse. | H-D01–H-D11 |
| README, branch policy, repository status, and HTML versions | Update the old resume point; keep question identifiers and document superseded items, and regenerate the reading pages. | H-Q01 |
| Product document: scope and vision | Add the scope of corporate training (B2B) and reporting dashboards, live events (webinars, Q&A), competitions (hackathons, CTFs), open resources (free tools, templates), and the project marketplace. | H-D25, H-D26, H-D28, H-D32, H-D33 |
| Product document: business rules and journeys | Add gamification rules (streaks, leaderboards, points) and adaptive learning. Define the free preview rules (first 3 videos) without breaking the whole-course purchase rule (H-D10). Add the GitHub/GitLab account linking journey and the group project joining journey. | H-D27, H-D36, H-D14, H-D33 |
| Product document: AI | Expand the AI scope to include a personal AI tutor, automatic summaries, smart course recommendations, and initial grading of text (essay) questions. | H-D20 |
| Product document: security and privacy | Document the WAF, the right to be forgotten, WCAG 2.1 AA compliance, automated proctoring for tests, and the dual account recovery strategy. | H-D15, H-D21, H-D19, H-D14 |
| Data planning: entities | Add new entities needed to support the decisions: B2B Organization, B2B User, Achievement/Badge, Group Project, Project Marketplace Item, Lab Session State. | H-D25, H-D27, H-D33, H-D16 |
| Data planning: relationships | Update user relationships to link GitHub/GitLab (OAuth mapping). Add the relationship between a course and its earlier versions (versioning). Link certificates to the external verification system. | H-D14, H-D17, H-D19 |
| Data planning: security and retention rules | Add fields for the status of right-to-be-forgotten requests (Deletion Request Status) and accessibility requirements for metadata. Define the lab lifecycle (saving state between sessions). | H-D21, H-D16 |

**Lesson-based refunds stay after single-lesson sales were cancelled.** A lesson is a counting unit inside the course, not a price unit (H-D47, H-D48). The 10% video threshold still needs specialist review.

## 6. Stable decisions team members need

This is an operational summary to help follow the questions; it is not a substitute for the full product document.

| Topic | What must be preserved |
| --- | --- |
| Student and account | Visitors browse; registering allows previews; buying requires verifying one method: phone or email. Buying is available to all roles. |
| Sessions | At most two concurrent devices per account, a phone and a computer (H-D14), replacing the earlier one-device policy. Behavior for an additional device and multi-role accounts needs detail (H-Q17). |
| Student rights | Buying a course grants access with no expiry, and stopping sales or the instructor leaving does not remove access for earlier buyers. |
| Instructor and team | One dashboard per instructor; an assistant can work in several dashboards with separate permissions. The platform defines the list of permissions and the instructor grants them. Money cannot be delegated to the instructor's team. |
| Onboarding | Acceptance, a contract, and a first review; accepting the electronic contract is enough to start, and the third publication alerts the administration to decide whether to continue or require a paper contract. It is not an automatic block after the third video. |
| Assistants | A service from the first version: the instructor requests, the platform nominates, and the instructor approves and sets permissions. Offers and fees come from the dashboard, deducted only from the available balance. |
| Assistant service cycle | A payment grace period set from the dashboard; the service continues during it, then permissions stop if unpaid. Reactivation after payment needs the administration. The instructor can end the service immediately, keeping the work and settling per the offer. |
| Earnings and withdrawal | The platform's share is agreed per course and taken from what was actually paid; any available portion can be withdrawn with a minimum of 10 dollars, with approval and transfer within 24 hours. The dinar equivalent and fees are not settled. |
| Course transfer | Approval from the administration and both parties; the new agreement applies only to later sales, and earlier sales belong to the old instructor. All responsibility for students moves to the new instructor. Resetting does not erase the history or the instructor's wallet. |
| Video and editor | Viewing inside the platform with no video download and currently no watermark; the editor is for computers, beside the video, not for mobile or saving projects. |
| Educational assessment | Quiz, test, and challenge tools; automated grading with AI for code from the start. Appeals are reviewed automatically, then by the instructor when that fails. A certificate requires passing a test. |
| Tracks and mentoring | A track is sold separately from courses. Mentoring lasts for a period starting at purchase; when it ends, sending stops while the chat and the track remain. The current track policy has no refunds, and the net amount is available immediately after payment; failure to deliver the service is an open question. |
| Student privacy | Notes and bookmarks are private to the student; the reporter's identity goes to the responsible team; private chats and notes are not public search content. |

## 7. Proposed question list, in order

The order and priority are **proposals to organize the team's work**. H-Q is the handoff question identifier, and the last column links it to the existing logs. Other topics in O or D are not deleted just because they are grouped here. Start with H-Q01, then discuss each item as a separate question or split it into follow-up questions.

| ID | Question to settle | Why we need it | Reference |
| --- | --- | --- | --- |
| H-Q01 | ~~When a lesson is updated, is the earlier completion kept or must the new version be completed?~~ **Settled: H-D39.** | Designing progress versions without cancelling an earlier right; the actual resume point. | D-03, D-10 |
| H-Q02 | ~~Is progress on the same lesson shared across the courses it appears in, or measured within each course?~~ **Settled: H-D40.** | Separating reuse from each course's completion and certificate. | D-03, D-10 |
| H-Q03 | ~~How is an independent add-on sold while selling single lessons and sections is not allowed?~~ **Settled: H-D41.** | Defining the limits of the paid product without assuming partial resale. | O-03, D-03 |
| H-Q04 | ~~How does the instructor define the promised content before selling an incomplete course, and what proves it was delivered?~~ **Settled: H-D42, H-D43.** | Distinguishing delivering the promise from a new add-on, and approving the release of funds. | O-06, D-04 |
| H-Q05 | ~~How do we calculate refunds for a course with video, text, and tests, and what lesson values are approved?~~ **Settled: H-D46 to H-D50.** | The threshold, formula, and deductions; rounding and specialist review are still open. | O-04, D-05 |
| H-Q06 | ~~What is the refund processing deadline, what are its stages, and when is access removed and the remainder released?~~ **Settled: H-D51 to H-D54.** | Settling operations after the decision to continue the hold, including failed transfers. | O-05, O-16, D-06 |
| H-Q07 | How is a breach by the new instructor proven, and who carries out its settlement under the transfer contract? **Partly settled: H-D55** (the platform team decides the breach). Remaining: settlement. | Not confusing the owner of old sales with the party responsible for a later breach. | D-02, O-22 |
| H-Q08 | When is a transfer executed, and how do we handle a purchase, payment, or refund that happens at the same time? | Linking each transaction to the correct owner and agreement. | D-02, D-07 |
| H-Q09 | What is the cycle for collecting assistant fees, settling the payment grace period, and paying the assistant? | Avoiding assumptions about who collects, funding, or deductions that were not agreed. | D-13, O-05 |
| H-Q10 | Who can change assistant offers and the grace period, and who does the change apply to? | Defining the scope of dashboard settings and protecting existing agreements. | D-13, O-15 |
| H-Q11 | Is there a cart combining several courses and tracks, and in which currency and with which payment rules? | Orders, manual confirmation, fees, and settlement. | D-07, O-16 |
| H-Q12 | What are the rules for a completed lesson, passing, attempts, and access to tests? | Progress, certificates, and fair treatment of updates. | D-10, O-12 |
| H-Q13 | What is the quality standard for AI grading, and what happens when it cannot decide an appeal? | Escalation to the instructor, measuring accuracy, and attempt limits. | O-11, D-10 |
| H-Q14 | What happens if the promised mentoring service is not delivered? | Reconciling the no-refund policy with immediate earnings and an ongoing service. | O-07, O-09, D-09 |
| H-Q15 | How is mentoring renewed early, and how are response time, the service provider, and track resources defined? | Service periods, commitments, external resources, and their cost. | O-08–O-10, D-09 |
| H-Q16 | What are the plan features at launch, and the rules for changing them, grace, and balance? | Fixing products and the scope of admin settings before modeling subscriptions. | O-01, O-17, D-08 |
| H-Q17 | How does the student device policy apply to an instructor account that is also learning? | Preventing unintended removal of work permissions. | O-15, D-01 |
| H-Q18 | What are the acceptance criteria for ratings, reports, and edits, and how long does review take? | Moderation, the decision log, and team permissions. | O-13, O-14 |
| H-Q19 | What data is recorded, kept, and deleted, and who can see it? | Private notes, auditing, backups, and user requests. | O-14, O-22, D-11 |
| H-Q20 | What are the search sources, and what paid content is shown? | Quality of Arabic and English search, content protection, and privacy. | O-19, D-12 |
| H-Q21 | What are the limits of the first version for mobile, rooms, the public area, and deferred features? | Closing the scope before estimating implementation and cost. | O-01, O-18 |
| H-Q22 | What is the real load, and what are the targets for speed, availability, recovery, and code execution limits? | Testing technical options and estimating resources and budget. | O-02, O-20, O-21, D-12 |
| H-Q23 | What is the architectural choice once ownership, permissions, and load are stable? | Web front end, isolation, video, search, storage, and operations; a comparison and approval document. | O-20, D-12 |
| H-Q24 | Which contracts and policies need specialist review before they are approved for publication? | Clear terms for rights, fees, cancellation, and data. | O-22 |

## 8. Splitting work within the team — an editable proposal

No actual names or responsibilities have been assigned yet. One person can cover more than one role, and research can be split internally, with one coordinator collecting questions for the project owner.

| Track | Proposed owner | Deliverable |
| --- | --- | --- |
| Decision coordination | An analysis coordinator chosen by the team | The question table, answer minutes, and tracking of conflicts and changes. |
| Content and student experience | A product analyst with an education representative | Update, progress, and certificate policies, and student journeys with their edge cases. |
| Money and agreements | An analyst with the finance specialist and the project owner | Worked examples for refunds, holds, transfers, and assistants, with clear inputs and outputs. |
| Permissions and security | A security representative with a data analyst | An access matrix, permission revocation, data scope, auditing, and privacy. |
| Data and architecture | A data designer and an architect | A logical model and technical alternatives based on the decisions, separating proposed from approved. |
| Document and language review | A requirements reviewer and a translation reviewer | Matching Arabic and English, and correct links, identifiers, and acceptance criteria. |

The team prepares alternatives and impacts, and the project owner settles product rules. Internal approval details for specialists are agreed with them; this file does not assume authorized names or binding delivery dates.

## 9. Template for recording an answer or decision

Copy this card for each question. **The documentation date is separate from the date the answer was given.** If information is not available, write "not specified" instead of making it up.

| Field | What the team records |
| --- | --- |
| Identifier | H-Qxx, and links to related O / D / H-D items. |
| Question asked | One question with specific wording, with an example when needed. |
| Project owner's answer | The text or an exact quote; a link to the discussion or its available reference. |
| Answer and documentation dates | Each date separately, or "not specified" if unknown. |
| Status | Open / proposed / preliminary / agreed / deferred / superseded. |
| Operational wording | The actor, condition, action, and result, without adding assumptions. |
| Earlier decision affected | Its identifier and the part cancelled or restricted; "none" if nothing is affected. |
| Exceptions and open points | Limits of the decision and necessary follow-up questions. |
| Data, permissions and money impact | Fields or states that need modeling, as proposals until approved. |
| Acceptance example | "When ... and if ... the result is ..."; describes required behavior, not an implemented test. |
| Review | The card's author, the reviewer, and who actually approved the decision; do not fill in names that were not agreed. |
| Places to update | Files, sections, and acceptance criteria affected, in both languages. |

Settled example: a refund request submitted on day six stays unprocessed until day eight. **Required result:** it is not rejected because of administrative delay, and the funds for this transaction are not released before it is decided. **What we do not conclude:** that the refund is automatically accepted, or that a specific review deadline was approved.

## 10. Next batch deliverables and readiness criteria

The next batch is not "answer everything". Choose a related group; a good start is content and progress, then refunds and transfers, while keeping the list of remaining questions.

1. **Reconcile version 0.2:** merge the section 4 decisions and handle every item in section 5; give the next version its number and date when it is actually complete.
2. **Answer log:** a card for each new decision, linking superseded decisions to their replacements and open questions to their follow-up point.
3. **Updated permissions matrix:** before and after ownership transfer, and during, suspension, and termination of the assistant service, with the scope of each dashboard.
4. **Acceptance scenarios:** a whole-course purchase with no discount for shared content, a free update, a moved lesson with a preserved right, a refund request processed late, and a transfer during a hold.
5. **Proposed data impact:** relationships, states, financial history, and progress; no final SQL or migrations before the model is approved.
6. **Two matching versions:** review Arabic and English, then generate HTML and check links and the table of contents, then push a documented batch to the planning branches.

A batch is ready for review when its active rules do not conflict, every decision has a source, status, and impact, and every unanswered question is clearly visible. Closing future features is not required to approve a stable scope, and it is not acceptable to declare that all database planning is complete while impactful questions are still open.

## 11. Working with the repository and handing over between team members

| Branch | Role |
| --- | --- |
| plan | The permanent original Arabic reference for plans. |
| plan-ar | An Arabic copy matching plan when a batch is published, at the same commit. |
| plan-en | A full English translation of the same batch; decisions and identifiers match, but the file language does not. |
| docs | Official documentation after review and approval, not an automatic copy of planning drafts. |
| develop / staging / main | Development, testing, and production; planning drafts are not moved into them wholesale. |

Agreed working rule: no deleting permanent branches and no force push. No direct merge between the language branches that replaces one language with the other. Synchronizing the two languages is review and translation, not an automated process.

**Proposed collaboration style:** each topic starts from the latest plan in a temporary task branch such as `planning/content-progress`, and its material is reviewed through a merge request into plan. The team decides who coordinates updating plan-ar and translating into plan-en; several people do not edit the same section without coordination. This is a proposal for organizing the team, not a claim that a new protection rule was added on GitHub.

At each internal handover: state the last question and its answer, the next question, the files changed, what was not pushed, and the reference of the Arabic version that was translated. If translation is incomplete, say so clearly and do not describe the branches as synchronized.

## 12. Status of this handoff and the team's first step

| Item | Status when this file was prepared |
| --- | --- |
| Planning baseline read | Product and data documents at version 0.3. |
| Latest decisions | H-D01 to H-D45 merged into version 0.3 of both documents; H-D46 to H-D55 in section 4.4, not yet merged. |
| Conflicts with the old version | Resolved in version 0.3; section 5 is now a historical record. |
| Resume question | H-Q07 (remaining part): settling a proven breach by the new instructor. H-Q01 to H-Q06 and the first part of H-Q07 are settled. |
| Model and API approval | Not complete; product implementation does not start with this handoff. |
| This file | English translation of the Arabic handoff `TEAM-HANDOFF.ar.md` at plan commit 69dd9da, with an HTML reading version. |

**Team's next step:** the coordinator reads sections 4 and 5, then asks the project owner the rest of H-Q07, and records the answer using the section 9 card. After a suitable group of answers, the team reviews the changes and updates the core documents in both languages.

**Version log — 0.1, 8 October 2026:** first handoff file for completing the questions, including the latest discussion decisions, superseded decisions, a document repair map, a proposed question log, and collaboration roles with approval and handover templates. The work order and roles are proposals; only the project owner's answers are recorded as approved decisions.

**Version log — 0.2, 29 October 2026:** settled H-Q01 (H-D39), H-Q02 (H-D40), H-Q03 (H-D41), and H-Q04 (H-D42, H-D43); superseded H-D09; moved the resume point to H-Q05; added decision cards.


**Version log — 0.3, 31 October 2026:** added H-D44 (Next.js) and H-D45 (local environment), corrected the sessions row to follow H-D14, and recorded that section 5 was applied in version 0.3 of the product and data documents.

**Version log — 0.4, 31 October 2026:** settled H-Q05 with decisions H-D46 to H-D50: what "used" means for each lesson type, the course price as one total value, the refund formula by share of lessons, the late-course refund, and the payment fee. Settled H-Q06 with decisions H-D51 to H-D54: a 3-working-day response deadline, access stopped on request, refunds to the same payment method, and release of the remainder on execution. Settled the first part of H-Q07 with decision H-D55: the platform team decides a breach by the new instructor. Resume point: the rest of H-Q07.