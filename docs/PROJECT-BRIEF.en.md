# Crow Station — Product Brief and Planning Decisions

> Version 0.2 · 30 September 2026 · Reference draft for review · Does not authorize implementation

## 1. How to use this document

This document consolidates the project discussion into one reference to organize requirements before design and implementation. It is not a legal contract, a final technical specification, or a promise to launch every idea in the original draft.

Companion document: [Data Planning and Design Decisions](DATA-PLANNING.en.md). It begins analyzing proposed entities and relationships; it does not approve tables or a database count. Both documents are updated together as decisions are resolved.

The source is `Project_Features.pdf`, read in full at the start of the discussion, followed by the project owner's answers and subsequent corrections. Where statements differ, this document uses the latest explicit decision and retains unresolved matters as open questions. The original file was not present in the project directory when this version was prepared; unchanged images of its two pages are preserved in `docs/sources` to keep the available source with the plans.

| Status | Meaning |
| --- | --- |
| Agreed | An explicit decision in the discussion; the project owner may revisit it later. |
| Preliminary | An accepted direction whose details or final approval remain subject to review. |
| Proposed | A recommendation that has not received final approval and is not a binding requirement. |
| Deferred | Implementation or discussion has been postponed; the relevant section specifies which. |
| Open | A question or boundary that has not been resolved. |

**Reading rule:** Requirements in the following sections are agreed unless marked otherwise. The work-plan section proposes a planning sequence; it is not an approved implementation plan.

## 2. Vision, purpose, and product boundaries

Crow Station is a unified learning platform for publishing and purchasing courses, bringing the platform owner's content and other instructors' content together. The learning experience includes video, coding practice, assessments, certificates, communication, and independent learning paths with paid mentoring.

- Each instructor has a profile page, dashboard, content, and delegated team.
- The intent is not to create independent academies with separate branding and domains for each instructor.
- Calling the product Multi-Tenant does not determine the system design. Data organization and isolation have not been approved.
- Launch content focuses on **programming and cybersecurity**.
- Iraq is the initial market, while registration and purchasing from abroad are allowed from the start, including manual payment arrangements.
- The platform interface supports Arabic and English at launch. Instructors choose their content language freely.
- **Crow Station** is the preliminary name and may change if a better name is selected.

### 2.1 Planning assumptions

| Item | Current position |
| --- | --- |
| Target date | September 2027; preliminary and subject to review after estimation. |
| Delivery team | The project owner and a team of 10 people, with intermediate programming and cybersecurity experience, using AI tools. |
| Web frontend experience | Limited, but the owner does not want this to prevent choosing an appropriate technology. |
| Budget | An operating and services budget exists; no amount or cap has been specified, and unlimited funding is not assumed. |
| Initial load | A planning target of approximately 1,000 concurrent students and instructors; the split across video, code execution, and other services is undefined. |
| Launch channels | A responsive website and Android and iOS apps together. |

### 2.2 Definitions that prevent confusion

| Term | Meaning |
| --- | --- |
| Course purchase | An entitlement to the content covered by the purchase, without an expiry date. |
| Platform subscription | A time-limited package unlocking additional features; it does not automatically grant courses. |
| Free preview | One or more pre-purchase videos selected by the instructor and platform; an account is required. |
| Refund request window | The first 7 days after a purchase, during which an exceptional refund may be requested; not a free trial or a guarantee of approval. |
| Learning path | An independent product with a plan, instructions, and mentoring; not simply a course bundle. |
| Room | A hands-on learning lab inspired by TryHackMe, not the course chat. |
| Course chat | A text group discussion restricted to purchasers. |
| Mentoring conversation | A private conversation tied to the paid mentoring service and its duration. |
| Instructor wallet | Available or held earnings; not a decision to create a general student payment wallet. |

## 3. Initial release scope and later phases

### 3.1 Required at launch

- Platform staff assistant service: requests, assignment, fees, grace periods, suspension, and termination with separate permissions.

- Responsive website and mobile apps, with Arabic and English interfaces.
- Accounts, verification at purchase, and administrator, instructor, and staff permissions.
- Instructor admission, contracts, initial review, and course and content management.
- Full and partial purchases, electronic and manual payments, refunds, wallets, and withdrawals.
- Subscription packages, promotions, and their administration settings; exact benefits remain to be resolved.
- Protected video, synchronized viewing progress, student dashboard, playlists, and notes.
- A desktop code editor supporting writing, editing, and execution.
- Quiz, test, and challenge tools; automatic grading, including AI grading for code; and certificates.
- Reviews, reports, and text course chats.
- Independent learning paths and mentoring, including around-the-clock coverage by the platform's mentoring team.
- Advanced semantic and content search across Arabic and English, with filters.
- Notifications, analytics, and audit records; integrations and retention policies will be detailed later.

### 3.2 Deferred or displayed as “Coming soon”

| Feature | Decision |
| --- | --- |
| Crow Store | Prepare its foundations and display “Coming soon”; sales are inactive at launch. |
| Crow Hub | GitHub-like repositories for students and the platform, with dedicated hosting; display “Coming soon”; details later. |
| VS competitions | Included in the plan, displayed as “Coming soon,” and inactive at launch. |
| General AI features | Deferred, with an explicit exception for code grading and review of grading appeals in the first release. |
| Translation and dubbing | A separate deferred discussion; neither a launch date nor a provider is selected. |
| Rooms and labs | Details and activation date are unresolved; they are not automatically included in or excluded from launch. |
| Streak | Starts with rooms and expands later, so its launch depends on the room decision. |
| General discussion area | Desired, but potentially deferred; course chats are confirmed for launch. |
| Choosing a particular mentor | Later releases; the initial service uses a mentoring team. |
| Course chat attachments | Future feature; the initial release is text-only. |
| Bank integration for withdrawals | A future possibility, not a first-release commitment. |

### 3.3 Currently excluded

No Flutter Web, no student downloads of course videos, no watermark in the current version, and no mobile code editor. There are no general private messages to instructors inside the platform; the agreed exception is mentoring conversations. The store is not a marketplace for third-party sellers.

## 4. Accounts, roles, and permissions

### 4.1 Account journey

1. A visitor can browse only; previews and account interactions are unavailable.
2. After registration, preview videos are available without phone or email verification.
3. At purchase, verifying **one** of the two is sufficient: phone or email.
4. All account types may purchase courses; an instructor or employee does not need a separate student account.

### 4.2 Functional roles

| Role | Responsibility and boundaries |
| --- | --- |
| Platform owner | Sensitive settings, delegation, and approval of delegates' financial changes and package price or benefit changes. |
| Administration | Platform operations, contracts and agreements, appointing teams, and organizing permissions. |
| Review team | Initial content acceptance, deciding review depth, publication approval, and accountability for the decision. |
| Responsible instructor | Manages their dashboard, content, and team; financial matters and withdrawals are exclusive to this person on the instructor-team side. |
| Contributing instructor or delegated employee | May work for multiple instructors with independent permissions and data boundaries in each dashboard. |
| Support | Initially may confirm manual payments and activate access; responsibilities are separated later when specialists are hired. |
| Mentoring team | Responds within assigned paths and services, with conversation history available for continuity. |
| Advertising team | Appointed by administration; exclusively manages and publishes advertisements. |
| Reports team | Sees the reporter's identity and handles the report; the instructor does not see that identity. |
| Student | Purchases, learns, and interacts within their entitlements. |

These are functional roles, not final database Role names. Access boundaries for the platform's own staff require a separate permissions matrix.

### 4.3 Instructor delegation

- Each instructor has one dashboard containing their courses and team.
- A person may assist several instructors simultaneously, with separate permissions and data boundaries for each dashboard.
- The platform defines the permission list from which instructors select grants for each person.
- Instructors can grant and revoke access and delegate access management within permitted limits.
- Delegates cannot grant permissions beyond their own or change the responsible instructor's permissions.
- No financial matters, including balances, financial reports, and withdrawal requests, may be delegated to instructor staff or contributors.
- Several instructors may contribute to one course; earnings go to the responsible instructor, who distributes them outside an automatic split between contributor wallets.

### 4.4 Student sessions

The initial policy permits one signed-in session on one device per student. Signing into the same account on a new device warns the student and signs out the previous device. This is broader than preventing two videos from playing at once and does not apply to instructors. Account-sharing controls may become stricter later. The behavior of an account combining instructor and student roles remains unresolved.

### 4.5 Assistants from the platform's staff

**Agreed — required from the first release:**

- The instructor requests an assistant; the platform proposes a staff member, and the instructor approves before assignment.
- The instructor chooses the assistant's permissions from the platform's list. The prohibition on delegating financial matters still applies.
- This is a paid service with modest fees; no numeric amounts have been approved.
- Offers, prices, and terms are managed from the dashboard, supporting monthly, hourly, task-based, or individually agreed pricing.
- Fees are managed inside the platform; the instructor can track amounts due and deductions from their dashboard.
- Fees are deducted only from the instructor's available balance, never from held amounts.
- If the balance is insufficient, the instructor receives a payment grace period configured from the dashboard. Whether it is global or per offer is unresolved.
- Service continues during the grace period. If it ends without payment, service is suspended and the assistant loses access to this instructor's dashboard, while work and records are preserved.
- After payment, restoring the service and permissions requires administration confirmation; payment alone does not automatically restore access.
- The instructor may terminate the service at any time; amounts due are calculated under the agreed offer terms.
- Termination immediately revokes access even if financial settlement remains pending, while preserving work and records.
- Suspension or termination concerns the service relationship with the relevant instructor; it does not delete the assistant's account or permissions with another instructor.

**Future proposal:** This service might be offered as a job posting; its mechanism and phase are undefined.

**Open:** Deduction dates, priority relative to withdrawal requests, collection retries, settlement for work during grace, the deduction recipient, and how the assistant is paid. A prepaid balance and direct payment to an assistant wallet are not assumed. Settlement details do not change the agreed withdrawal deadline.

## 5. Instructor admission, review, and contracts

1. An instructor joins through an application approved by administration or a direct invitation.
2. After acceptance, the instructor signs a contract containing policies and controls; signing is mandatory before publication.
3. The specialist team reviews the first course or a sample, choosing how much review is needed to evaluate the subject matter and teaching approach.
4. The team can approve publication and is accountable for that decision, without further administrative sign-off.
5. An electronic contract is sufficient to begin publishing. Publishing the third video notifies administration to assess continuation, request a paper contract, or restrict publication.
6. **The third video is an assessment checkpoint, not a technical limit:** the fourth video is not automatically blocked.
7. After approval, instructors can generally add or edit content and publish further courses without prior review, within their permissions and policies.
8. Following a violation, the review team can return the instructor to a mode requiring approval of new content and edits before publication.

Electronic signing details, paper-copy storage, sanction levels, and exactly what belongs in each course contract remain later design and policy questions.

## 6. Courses, content, and access rights

### 6.1 Publication and sales

- The instructor sets the price.
- Publication settings allow full-course sales, separate sections or lessons, or both.
- Purchased content remains accessible without expiry or another payment for the same content.
- The instructor decides how new lessons and updates are offered to existing purchasers, whether within the course or as related additions.
- When completing a course purchase, students buy only what they do not own. The instructor determines completion pricing without charging again for owned content.
- The instructor may impose a learning sequence, such as completing a lesson or passing a test before unlocking the next item.
- Publication may be immediate, on a specified date, or gradual based on time since purchase.
- Preview content consists of one or more videos selected by the instructor and platform. Approval details are unresolved.

**Open:** How do we preserve the original purchased content when it is replaced by a paid update? What defines “completion” for a course with scheduled lessons? We do not assume deletion of the original version or withdrawal of access to it.

### 6.2 Selling before course completion

- Allowed, with guarantees that promised content will be delivered.
- Publication dates and the completion deadline are defined in the contract and clearly shown before purchase.
- All instructor earnings from the course remain held until completion, **including separate sales of an already-published lesson**.
- The instructor must declare completion, and the platform team must verify and approve delivery before earnings are released.
- Delays require administrative approval and notification of revised dates to students.
- If delivery fails or a delay does not suit the student, the student may choose a refund for undelivered content or the offered compensation, even after 7 days.
- Administrative approval of a delay does not remove this right.

### 6.3 Stopping sales and instructor departure

- Existing purchasers retain course access even when the instructor stops selling or leaves.
- The platform takes over service for existing students, including assessments, certificates, and appeals.
- Availability to new purchasers is determined by agreement, with the instructor deciding as the owner.
- Options include continued sales under an agreement, transferring ownership or selling the course to the platform, or stopping availability to new purchasers.
- Removing the sales listing does not remove content from existing purchasers' libraries.

### 6.4 Transferring course ownership to another instructor

- Transfer is allowed with approval from administration, the previous instructor, and the new instructor.
- Course fees and revenue-share terms are agreed anew with the receiving instructor as for a new publication in their account, applying only to sales after transfer.
- Earnings from earlier sales remain with the previous instructor under the previous agreement, including amounts not yet released or withdrawn.
- Resetting means starting independent financial accounting for this course under the new instructor; it does not erase earlier records or earnings or reset either instructor's entire balance.
- Existing students retain purchases and access; transfer does not require them to buy the course again.
- Responsibility for the course and all existing students passes to the new instructor, including assessments, appeals, and conversations.
- **Open — next question:** Is the previous instructor's and their team's management access revoked immediately after transfer, while the previous instructor retains access only to historical financial records? The project owner has not answered this question.
- **Open:** Transfer approval mechanics and effective time, concurrent transactions, and who bears refunds or incomplete-course commitments arising from pre-transfer sales.

## 7. Student experience, video, and code editor

### 7.1 Student dashboard

A professional dashboard brings together courses, purchases, progress, assessments, results, certificates, and playlists. Students can create private playlists combining lessons from different courses they own. A playlist does not grant access to locked or unpurchased lessons.

- **Essential for launch:** save each video's playback position and synchronize it between website and app when returning or changing devices.
- Notes and bookmarks linked to a timestamp within a video.
- These notes and bookmarks are private to the student and are not shared with instructors or other students.

### 7.2 Video and attachments

- Viewing is inside the platform only; students cannot download course videos.
- No watermark for now.
- The goal is to reduce downloading and leakage as far as possible, not to claim that copying can be prevented absolutely.
- Instructors determine whether educational attachments such as PDFs, code, and exercises may be downloaded.
- Advertisements do not appear inside course videos.
- Streaming protection, storage, transcoding, and quality options have not been selected.

### 7.3 Code editor

- Required from the first release on desktop only; unavailable on mobile.
- Appears beside the video, with controls to resize, hide, and show it.
- Supports writing, editing, and execution for practice during the lesson, not saving projects to the account.
- Preliminary languages: Python, C, Java, and JavaScript, with other console languages to be specified later.
- Unlocks after purchasing any course and remains available without a recurring subscription.
- If refunded purchases leave the student owning no courses, the editor locks until another course is purchased.

**Open:** Does purchasing a single lesson unlock the editor? How are code execution sessions isolated and their resources and duration limited?

## 8. Assessments, grading, and certificates

### 8.1 Assessment tools

- Quizzes, tests, and challenges are available in the initial release; instructors choose whether to use them, and they are not mandatory for publishing every course.
- The platform sets strict baseline rules for attempts and passing, with limited instructor flexibility. Values and limits are undefined.
- Grading is automatic from the first release; manual grading is an exception in cases defined by the instructor.
- Questions with predetermined answers are graded automatically without AI.
- Coding answers are graded by AI according to the owner's decision; this is an exception to deferring general AI features.
- The platform has fixed standards, with question-specific criteria possible within them; the exact boundaries remain open.

### 8.2 Appeals

1. The student submits a grading appeal.
2. AI reviews it first.
3. The appeal is not escalated merely because the student is unconvinced.
4. If AI cannot resolve it, a ticket goes only to the course instructor; if that instructor has left, the platform serves the students under the continuity decision.

**Open:** The definition of inability to resolve an appeal, the number of repeat reviews, and the method for auditing grading quality and fairness before relying on it for certificates.

### 8.3 Certificates

- Passing an assessment is required for a course certificate.
- Whether lessons must be watched before taking the assessment is deferred for decision.
- The certificate carries both the platform's and the instructor's names.
- Every certificate has a unique verification code and a public authenticity-check link.
- Verification-page fields and certificate revocation cases are undefined.

## 9. Payments, earnings, and refunds

### 9.1 Pricing and collection

- Instructors set course prices; the platform's percentage is agreed **per course**, not fixed per instructor.
- The percentage applies to the amount actually paid after discounts.
- Instructors can start a promotion directly. The platform's current percentage remains until its reduction is agreed and approved.
- Agreements determine how the cost of platform promotions is shared; neither party is assumed always to bear it.
- All financial terms are recorded inside the platform before application, whether negotiated inside or outside it.
- The Iraqi dinar is the preliminary primary currency; dollar handling depends on the payment service and agreements.
- Electronic and manual payments are supported. For manual payment, the student contacts support to arrange a transfer, and access is activated after receipt is confirmed.
- Manual payment is available to everyone after support arrangements and verification; it is not limited to users outside Iraq.

### 9.2 Instructor wallet and withdrawals

| Earnings type | Availability rule |
| --- | --- |
| Completed course sale | Each new purchase amount remains held until 7 days have passed since that purchase; this is not the first 7 days of the instructor account. |
| Incomplete course or part of one | Held until course completion, the instructor's declaration, and team approval; the purchase window still matters for recent purchases. |
| Learning path sale | Net earnings are available immediately upon payment confirmation because the current agreed policy does not allow path refunds. |

- Any portion of the available balance may be withdrawn.
- The minimum withdrawal is 10 dollars; its dinar equivalent and conversion rate need a decision.
- Processing and, upon approval, transferring funds must both occur within 24 hours of the request; this is not merely a response deadline.
- Money, financial reports, and withdrawals belong exclusively to the responsible instructor on the instructor-team side.
- Handling pending refund requests and appeals before releasing balances is not fully defined.

### 9.3 Course refunds

- The default is no refund after payment, with exceptions approved by administration and potentially delegated to a specialist department later.
- An ordinary exceptional request must be made within 7 days of purchase; submission does not guarantee approval.
- Refunds are partial based on benefit received and calculated using the **number and value of watched lessons**.
- Watching 10% of a lesson's duration is a **preliminary, reviewable** threshold for counting it as watched. Merely opening it is insufficient under this proposal.
- Free preview lessons are excluded from deductions.
- Instructors choose equal content-value allocation or different lesson values.
- **Fixing the value allocation at purchase remains a non-final proposal**, following the owner's request to discuss it with specialists.
- When a refund is executed, access to the relevant content is revoked automatically or manually.
- Failure to deliver content on time is a separate exception allowing a refund or compensation beyond the window, at the student's choice.
- No final financial formula yet covers rounding, allocating discounts to lessons, payment fees, or reversing each party's share.

## 10. Packages, promotions, and approvals

### 10.1 Subscriptions

- Multiple packages, each with its own price and benefits. Details are deferred until the associated services are settled.
- Prices, benefits, grace-period length, and benefits during grace are managed per package in the administration dashboard.
- Package changes preserve the subscriber's current terms until renewal, when the changes take effect.
- Renewal is manual or automatic with user consent and subject to payment-method capabilities.
- Cancellation stops renewal only; it does not forfeit the paid period.
- Package changes are available at renewal or by canceling renewal and purchasing a replacement.
- Users choose to start the replacement immediately or after the current package ends.
- With an immediate start, the old package ends and the new one begins.
- The remaining period's value is credited as a discount on the replacement, under an administration-configurable policy.
- Whether excess value is retained as student credit is an administrative setting, not a fixed rule at present.

### 10.2 Promotions and audiences

- Offers are not limited to school and university students; administration defines audience categories and conditions in the dashboard.
- Multiple discounts can be combined; administration sets compatibility and the overall maximum.
- How category membership is verified has not been defined.

### 10.3 Change permissions

Sensitive financial settings belong primarily to the owner. Marketing or other teams may be delegated access, but their changes require the owner's approval before application, including package prices and benefits. This does not conflict with instructors discounting their courses directly; reducing the platform's percentage still requires agreement.

**Open:** The effect of credit and switching-policy changes on existing subscriptions, notice of renewal at a new price, and the boundaries of settings that may be changed.

## 11. Learning paths and mentoring

### 11.1 The learning path as an independent product

- Available for sale by the platform and instructors from the initial release.
- Includes the roadmap, instructions, learning sequence, follow-up, and mentoring.
- Creating one does not require existing platform courses; it may reference internal content or external resources and courses.
- Courses are priced separately; buying a path does not automatically grant ownership of them.
- Under the earlier decision, instructors' paths are limited to their own content, while the platform can combine different instructors' content. The extent to which external resources apply to instructor paths needs clarification.
- No refunds for learning path purchases under the currently agreed policy.
- No prior content review is required before selling an instructor's path.
- The platform's percentage is agreed separately for each path.

### 11.2 Service duration and communication

- Mentoring is included in the path price for an announced, agreed period beginning **at purchase**.
- When that period ends, the path and instructions remain accessible and mentoring stops; it may be extended for a fee.
- Private conversation history remains, but new messages stop until renewal.
- Communication uses a private in-platform conversation and may also take place externally, such as on WhatsApp.
- There is no one-hour daily limit; that proposal was explicitly canceled.
- The platform's mentoring team provides round-the-clock coverage; any available member may reply with access to history.
- Letting students select a particular mentor is deferred to later releases.
- When instructors and their teams provide mentoring, they define working hours, announced before purchase.
- Coverage of 24/7 does not itself establish a maximum response time; that has not been approved.

### 11.3 Mentoring for instructor paths

- May be delivered by the instructor and their team, the platform team, or both together.
- Announcing the provider in advance is not yet a universally approved requirement; arrangements remain situation-dependent.
- If an offer includes platform-team mentoring, its agreement and fees must be approved **before selling that service**.
- Platform-team fees are a fixed amount per student, set separately per path according to importance, difficulty, and agreement.
- They are deducted automatically from the instructor's earnings in addition to the platform's percentage.
- Net path-sale earnings become available immediately upon payment confirmation.

**Open:** Renewal duration and price, discounts' effect on revenue covering mentoring fees, documenting external communication, and service guarantees when continuation is impossible.

## 12. Community, reviews, and reports

### 12.1 Course chat

- An open group conversation in which purchasers, the instructor, and their team can write and reply.
- Completely hidden from non-purchasers, including preview-only viewers.
- Text-only in the initial release; images and files are future additions.
- Instructors can delete messages and mute or ban participants from the conversation for abuse.
- Access details for single-lesson purchasers after refunds or partial entitlement revocation need clarification.
- External contact details instructors place in the course are visible to everyone, including visitors.

### 12.2 Reviews

- Eligibility starts after completing at least the first video, including a free preview without a purchase.
- Stars and written comments are available, and participation is optional; the required relationship between comments and stars needs detail.
- Ratings below 3/5 are reviewed before publication; 3 and above are published directly.
- Lowering an already-published rating below 3 temporarily hides it and returns it to review.
- Students may edit ratings and comments, and instructors may reply publicly.
- Acceptance and rejection criteria are deferred; negative criticism is not assumed to be automatically rejected or accepted.

### 12.3 Reports

- A separate process reports course problems or instructor violations.
- The reporter's identity is available only to the responsible team, not to the instructor.
- Students track their reports and answer team questions within the platform.
- Priority levels, handling deadlines, and escalation are undefined.

## 13. Search, notifications, advertising, and analytics

### 13.1 Search

- Advanced from the start, based on meaning and content rather than exact text matching alone.
- Supports Arabic and English and searches across languages; an Arabic query can find relevant English content.
- Covers discoverable content, including unpurchased items, while keeping paid content locked and private data undisclosed.
- Does not require showing the information's timestamp within a video.
- Filters include subject area, level, language, price, instructor, and rating.
- The indexed text source, understanding of video content, search engine, and quality evaluation method are unresolved.

### 13.2 Notifications

Required channels include in-platform, mobile push, email, and SMS. Users control optional notifications such as promotions, while essential security and payment notifications remain mandatory. Event-to-channel mapping, preferences, sending limits, and providers will be defined later; this does not mean sending every notification through every channel.

### 13.3 Advertising

Includes promotion of platform courses and external organizations. Management and publication belong exclusively to an administration-appointed team, with no advertisements inside videos. Placements, pricing, and advertiser acceptance rules are open.

### 13.4 Analytics

The instructor dashboard shows sales, views, student progress, and assessment results, both aggregated and for individual students within the instructor's courses. Financial data remains restricted to the responsible instructor. Administration analytics, report exports, and permissions for non-financial analytics need further detail.

## 14. Rooms and future services

### 14.1 Room decisions made before deferring discussion

- Labs resemble TryHackMe rooms and contain a Machine accessed through a VPN to be selected later.
- Recording session and activity data is requested, but the meaning of “all activities,” its mechanisms, and retention duration remain unresolved.
- Platform-produced rooms are accessible through a platform subscription.
- An instructor can design a room and set its policies or ask the platform to design it and associate it with the course.
- Instructors pay hosting fees for their own rooms and choose whether student access is free or paid.
- Student room payments and hosting fees are handled within the platform system; no additional percentage commission for instructor rooms has been approved.
- Revenue from administration-produced rooms belongs to the platform.
- Access policies and their relationship to course purchase depend on the room and its owner under the agreement; requiring both course purchase and subscription is not a universal rule.
- A separate Machine instance per student was **only a proposal** and was not approved before discussion was deferred.

### 14.2 Other services

| Service | Recorded scope |
| --- | --- |
| Crow Store | Physical and digital products and platform brands only, with no third-party sellers. Prepare the foundation and show “Coming soon.” |
| Crow Hub | A GitHub-like service for students and the platform, with dedicated hosting, inactive at launch. |
| VS | Competitions in the plan without a defined mechanism; “Coming soon.” |
| Streak | Applies to rooms first, then expands; no calculation rule is approved yet. |
| Translation and dubbing | Deferred for discussion, not an instructor requirement. |

## 15. Protection and audit

- Protection covers content, particularly video, account-sharing prevention, and checking content rights and violations.
- Suspected leakage or sharing generates an alert for team investigation; suspicion alone does not automatically suspend an account.
- No watermark for now.
- Recording user and instructor activity is an explicit requirement; before design it must become a defined event list, audit record, permission model, and retention period.
- The rights-checking mechanism does not automatically override instructor approval and first-review policy; subsequent monitoring remains open for design.
- Protection details must not be presented as an absolute guarantee against leakage.

## 16. Technology direction and what is settled

| Component | Decision or status |
| --- | --- |
| Code organization | Clean Architecture. |
| Backend and API | ASP.NET Core on .NET 10. |
| Database | PostgreSQL. |
| Mobile app | Flutter, not Flutter Web for the website. |
| Web frontend | A separate technology not yet selected; criteria include practicality, performance, maintainability, and user experience. |
| Redis | Interpreted as what the owner meant by “rides”; its name and role need confirmation. |
| File storage | MinIO or an alternative; the provider is undecided. |
| Runtime deployment | Docker and Docker Compose are the agreed direction; production, scaling, and availability design is not yet detailed. |
| Instructor data isolation | Unresolved; choosing PostgreSQL does not determine shared databases, separate schemas, or separate databases. |
| AI | Its grading use is agreed; provider, model, policies, and cost remain open. |
| Video, search, and code execution | Functional requirements are agreed; architecture and provider choices remain open. |

The original file's “Two DB / one database per room” proposal is not approved. Clean Architecture is not equated with microservices; service distribution is a separate later decision.

## 17. Open questions and potential conflicts

This prioritization organizes discussion; it does not authorize decisions on the project owner's behalf.

| ID | Topic | Decision needed |
| --- | --- | --- |
| O-01 | Launch scope | Rooms and Streak timing, activating packages without rooms, and the final decision on the general discussion area. |
| O-02 | Operating figures | Concurrent video viewers, code runners, and chat users within the 1,000-user target. |
| O-03 | Permanent access and paid updates | Preserving purchased content when replaced or updated; boundaries for missing or rights-infringing content. |
| O-04 | Refund policy | The 10% threshold, lesson and discount formula, and fixing values at purchase after specialist review. |
| O-05 | Requests within the window | Preventing funds from being released before resolving a refund requested within 7 days but not yet processed. |
| O-06 | Guarantees and completion | Defining a completed course, scheduled lessons, publication commitments, and documented team approval. |
| O-07 | Paths and mentoring | No refunds and immediate earnings for an ongoing service: how to handle mentoring not delivered or discontinued. |
| O-08 | Path resources | Limiting instructors' paths to their own content versus allowing external resources: the precise scope of each type. |
| O-09 | Mentoring responsibility | Provider identification before sale, collaboration arrangements, response deadlines despite 24/7 coverage, and renewal prices and periods. |
| O-10 | Mentoring cost | What happens when a discounted price cannot cover the fixed fee and platform percentage. |
| O-11 | AI and appeals | Grading criteria and quality measurement, inability-to-resolve criteria triggering a ticket, and attempt limits. |
| O-12 | Assessment rules | Passing grades, attempts, entry conditions, and settings instructors may change. |
| O-13 | Reviews | Rejection criteria, text-edit review, and comments without stars if allowed. |
| O-14 | Audit and privacy | Recorded events, retention, staff access, and lab and conversation logs. |
| O-15 | Multiple-role accounts and teams | Student device policy for multiple-role accounts; previous instructor and team access after ownership transfer. Multiple assistant teams and one dashboard per instructor are settled. |
| O-16 | Funds and currencies | Payment gateways, currency differences, rounding and fees, withdrawal-threshold conversion, and failed transfers. |
| O-17 | Package changes | Protecting purchasers' terms when changing switching, credit, and owner-controlled policies. |
| O-18 | Apps | A separate student app or an initially combined app with roles; mobile administration and instructor functions. |
| O-19 | Search | Index sources, limits on paid-content snippets, and cross-language result quality measurement. |
| O-20 | Architecture | Web frontend, isolation, video storage, search, code execution, Redis, backups, and recovery. |
| O-21 | Non-functional requirements | Response times, availability, accessibility, storage limits, support, and monitoring plans. |
| O-22 | Contracts and policies | Specialist review of contracts, content rights, refunds, cancellation, and data policies before publication approval. |

## 18. Core journeys for review

### 18.1 Buying a course and learning

Browse → register → free preview → select content → verify phone or email → electronic payment or manual arrangement with support → payment confirmation → entitlement granted → synchronized viewing progress, practice, and assessments → certificate upon passing its assessment under conditions to be completed later.

### 18.2 New instructor

Application or invitation → administrative acceptance → electronic contract and initial content review before publication → team approval → publication → administration notified at the third video → administrative assessment without automatically blocking the fourth → continuation, paper contract request, or restrictions determined by responsible staff.

### 18.3 A course sold before completion

Commitments and schedule in the contract and sales page → purchase with content status disclosed → all course earnings held → scheduled publication → completion declared → team verification and approval → release of eligible earnings. Delay requires approval and notification and preserves the student's refund-or-compensation choice.

### 18.4 Buying a path with platform mentoring

Independent path, instructions, and announced mentoring period → fee agreement approved for an instructor path → purchase and payment confirmation → mentoring period starts immediately → platform percentage and fixed mentoring fee calculated → net earnings made available to the instructor → communication with any available member → period ends and new messages stop while history remains → paid renewal if desired.

### 18.5 Withdrawing earnings

Responsible instructor with available funds → request for part of the balance meeting the minimum → review → approval and transfer within 24 hours of the request. Rejection, failure, and appeal details need later documentation.

## 19. Preliminary product acceptance criteria

These examples can later become test scenarios; they do not mean the functionality has been implemented or tested successfully now.

| ID | Scenario and expected outcome |
| --- | --- |
| AC-01 | Visitors browse but cannot preview; unverified accounts can preview, while purchase requires one contact method to be verified. |
| AC-02 | No initial publication before signing and approval; publishing the third video alerts administration without automatically blocking the fourth. |
| AC-03 | An instructor's employee cannot access financial matters, request withdrawals, raise their own permissions, or alter the responsible instructor. |
| AC-04 | Student sign-in on a new device produces a warning and signs out the previous session under policy. |
| AC-05 | Students resume video from the saved position when moving from the website to the app after sign-in. |
| AC-06 | Stopping new course sales does not revoke existing purchasers' access. |
| AC-07 | Buying a lesson in an incomplete course does not make earnings withdrawable before completion and approval. |
| AC-08 | Free preview viewing does not reduce refunds; the viewing threshold and formula will be tested after approval. |
| AC-09 | Net path earnings are available immediately after payment, less platform mentoring fees where applicable. |
| AC-10 | Mentoring expiry blocks new messages while retaining history and the path; renewal restores communication. |
| AC-11 | A rating below 3 awaits review; reducing a published rating below 3 temporarily hides it. |
| AC-12 | Instructors cannot see reporter identities, and non-purchasers cannot see course chat. |
| AC-13 | Private notes and bookmarks are not visible to other accounts or public search. |
| AC-14 | A package price change does not alter current-period terms; a delegate's change is not applied before owner approval. |
| AC-15 | The editor works on desktop after entitlement and is absent on mobile; execution is isolated according to a later security design. |
| AC-16 | Grading appeals are reviewed automatically and sent to the instructor only when the later-approved inability-to-resolve criterion is met. |
| AC-17 | Arabic queries find relevant English content in agreed reference examples without exposing private content. |
| AC-18 | One assistant works with multiple instructors under separate permissions; access to one dashboard does not expose another's data. |
| AC-19 | A platform assistant is assigned only after the instructor requests help and approves the candidate; the instructor grants permissions without financial delegation. |
| AC-20 | Assistant fees use only available funds; insufficiency starts a dashboard-configured grace period, whose unpaid expiry blocks access and preserves records. |
| AC-21 | Payment after suspension does not restore permissions without administration confirmation; termination immediately revokes access while financial settlement remains separate. |
| AC-22 | Course transfer preserves purchases and assigns teaching responsibility to the new instructor; earlier earnings remain with the previous instructor and later sales use the new agreement without erasing history. |

## 20. Proposed documentation completion plan

This document does not start implementation. The following sequence turns it into a reviewable, implementable specification package.

| Phase | Output | Exit criterion |
| --- | --- | --- |
| 1. Decision review | Correct this document and prioritize open questions | Approve vision and scope without fundamental conflicts. |
| 2. Product requirements | Detailed PRD, journeys, exceptional cases, and permissions matrix | Every core capability has clear rules and acceptance criteria. |
| 3. Architecture design | Module boundaries, web technology, isolation, integrations, runtime, and protection | Review alternatives, cost, and risks and approve decisions. |
| 4. Data model | Entities, relationships, lifecycles, constraints, and history | Cover entitlements, money, and content without hidden financial assumptions. |
| 5. API contracts | Documented permissions, requests, responses, errors, and events | APIs align with the data model and website and app journeys. |
| 6. Implementation plan | Phases, responsibilities, dependencies, estimates, and acceptance criteria | Realistically review the September 2027 target against team resources. |

Proposed functional areas for study: identity and permissions; catalog and publication; entitlements and video; orders and money; subscriptions and promotions; assessments and certificates; paths and mentoring; community and support; search and notifications; administration and audit. These are responsibility boundaries, not a decision to create microservices.

## 21. Traceability to the original feature file

| Item | Original idea | Location and discussion outcome |
| --- | --- | --- |
| 1 | Name | Section 2: Crow Station provisionally. |
| 2 | Road Maps | Section 11: an independent product with mentoring from launch. |
| 3 | Publish Own Course | Section 5: acceptance or invitation, contract, and initial review, rather than anonymous open publication. |
| 4 | Code Editor | Section 7: console execution beside the video on desktop. |
| 5 | 3 Learning Tracks | Beginner/intermediate/professional levels proposed in the source; final structure is unapproved. A level filter is required. |
| 6 | AI / API | Section 8: code grading and appeals initially; other uses deferred. |
| 7 | Challenges | Section 8: optional instructor tools available at launch. |
| 8 | Rooms | Section 14: labs; details and launch remain to be decided. |
| 9 | VS | “Coming soon.” |
| 10 | Chat | Section 12: text conversation per course; general chat may be deferred. |
| 11 | Crow Hub | Section 14: GitHub-like repositories, “Coming soon.” |
| 12 | Teacher Work / Multi-Tenant | Sections 2, 4, 13, and 16: unified platform, instructors, and analytics; isolation undecided. |
| 13 | User Registration | Section 4: account and preview, then verification at purchase. |
| 14 | Offers | Section 10: flexible promotions, combined discounts, and financial agreements. |
| 15 | AD | Section 13: administrative team, no in-video advertisements. |
| 16 | Dubbing + Translation | Discussion deferred. |
| 17 | Bilingual Support | Arabic and English interfaces from the start. |
| 18 | Crow Store (Card) | Expanded into a store for platform products only; “Coming soon.” |
| 19 | Student Offers | Multiple administrable audience categories, not only students. |
| 20 | Search Engine | Section 13: semantic, content, and cross-language search with filters. |
| 21 | Anti-Corruption | Section 15: content and account protection and verification before action. |
| 22 | Streak | Starts with rooms; details later. |
| 23 | Teacher Dashboard | Sections 4, 6, 9, and 13: content, team, analytics, and permission-controlled finances. |
| 24 | Notification | Section 13: multiple channels and preferences, with essential notifications mandatory. |
| Note | Currencies | Dinar as the preliminary primary currency; dollars according to payment services and agreements. |
| Note | Video validity and protection | Entitlement has no expiry; download prevention is agreed, while streaming-link validity is a later technical decision. |
| Note | Two DB / a database per room | An unapproved source proposal; PostgreSQL is approved and isolation remains open. |

## 22. Version history

**0.2 — 30 September 2026:** Settled multiple assistant team memberships and one dashboard per instructor. Documented paid platform assistants from the first release, including fees, grace periods, suspension, and termination. Approved course transfer with new financial terms for later sales, preserved previous earnings, and transferred student responsibility. Previous instructor and team access after transfer remains open. Product implementation has not started.

**0.1.1 — September 28, 2026:** Added the companion data-planning reference after the request to continue analysis. No business decisions changed; all new entities and relationships in the companion document are proposals under review.

**0.1 — September 28, 2026:** First structured consolidation of discussion decisions through confirmation of the planning user count and the request to prepare this document. No product implementation, approved database schema, or approved API contracts are included in this release. Future updates will explicitly record decision changes rather than silently changing their meaning.
