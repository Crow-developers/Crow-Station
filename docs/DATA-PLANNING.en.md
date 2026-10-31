# Crow Station — Data Planning and Design Decisions

> Version 0.3 · 31 October 2026 · Proposed conceptual analysis · Not a final database schema

## 1. Is the product document enough to design the databases?

**It is enough to begin analysis and draw a conceptual model, but not to approve all final tables, relationships, and constraints.** We have a broad picture of the product's behavior, but some rules that change the meaning of ownership, entitlements, balances, and permissions remain undecided.

This file complements the [product document](PROJECT-BRIEF.en.md); it does not replace it. Its purpose is to translate known decisions into data requirements and identify what can be designed now and what must wait for an answer. It does not create a database, SQL, or migrations, or choose a final number of databases.

| Level | Question it answers | Status |
| --- | --- | --- |
| Product requirements | What can the user do, and under which conditions? | A broad draft exists; some decisions remain open. |
| Conceptual model | What do we need to store, and how are these things related? | Started in this file as a proposal. |
| Logical model | What are the precise relationships, cardinalities, constraints, and lifecycles? | To be completed gradually as consequential decisions are resolved. |
| Physical design | Tables, columns, types, indexes, policies, and partitioning | Approval has not started. |
| Operations | Backup, recovery, load, and monitoring | Requirements and decisions for later. |

**Status rule:** All entity names, relationships, and storage approaches here are analytical proposals, not decisions approved by the project owner. Rules carried over from the product document are marked “Agreed.” Some topics are intentionally open; we will not ask for all answers at once.

**Version 0.3:** applies decisions H-D01 to H-D45 merged into product brief 0.3. Entity names are still proposals, and no tables or migrations for product features have been approved; the code foundation on the develop branch contains an empty `AppDbContext` ready for them.

## 2. The database is not a copy of the application screens

A student dashboard, instructor dashboard, and administration dashboard do not imply three databases. Multiple rooms or instructors do not require a database for each. PostgreSQL is the agreed engine; the number of databases and isolation boundaries remain undecided.

We need to distinguish:

- **Identity:** Who performed the action?
- **Responsibility:** Which course or dashboard are they working on, and with what permission?
- **Ownership:** Who owns the content or financial entitlement?
- **Entitlement:** What has the student purchased and is entitled to access?
- **Storage:** Where are records, files, and indexes physically stored?

For example, an instructor purchasing another instructor's course does not change its ownership or add the buyer to the seller's team; it only grants learning access. A design relying on a single field such as “user type” cannot express these relationships on its own.

### 2.1 Where could each type be stored? — Proposed approach

| Data type | Candidate approach | Undecided details |
| --- | --- | --- |
| Accounts, content, orders, entitlements, and agreements | PostgreSQL as the source of truth | Module boundaries, isolation, and database count. |
| Videos, attachments, and contract copies | S3-compatible file storage (RustFS locally, H-D45), with metadata in PostgreSQL | Production provider, file protection, copies, and versions. |
| Temporary data such as caches and rate limits | Redis for caching (H-D45) | It must not be the sole store of permanent entitlements or money. |
| Semantic search | A derived index that can be rebuilt from approved sources | PostgreSQL or another engine, and the source of video text. |
| Detailed metrics and high-volume logs | Logical separation from core data | Volume, retention, and whether a separate service is needed. |
| Code execution and labs | An execution environment separate from the product database | Isolation, resources, execution lifecycle; rooms are still deferred for discussion. |

There is no decision to add other databases or a particular search engine now.

## 3. Data domain map

The English names provide a shared team vocabulary only; they are not final table names. The last column links each domain to a section of the product document.

| Domain | Candidate entities | Purpose | Product reference |
| --- | --- | --- | --- |
| Identity | User, ContactMethod, Verification, Session | A person's account, contact methods, verification, and sessions | 4 |
| Instructor team | InstructorProfile, Workspace, Membership, PermissionGrant | Separate the person from the dashboard they manage and their permissions within it | 4–5 |
| Admission and contracts | Application, Invitation, ContractVersion, Signature, ReviewDecision | Evidence of admission, contracting, review, and restrictions | 5 |
| Content | Course, Section, Lesson (in one course, with an optional reference to the lesson it was copied from), ContentRevision, MediaAsset | Course structure, content revisions, and files (H-D13, H-D40) | 6–7 |
| Publishing and delivery | ReleaseRule, DeclaredSyllabus, PlannedLesson, CompletionDeadline, DeadlineExtension, CompletionApproval | Scheduling, declared syllabus, extension, and approval of completion (H-D42, H-D43) | 6 |
| Products and offers | Offer (for the whole course), AddOn, PriceVersion, CommercialAgreement | What is sold, at what price, share, and terms (H-D10, H-D41) | 6, 9–11 |
| Purchases and payments | Order, OrderItem, PaymentAttempt, PaymentConfirmation, ManualPaymentReview | Orders, attempts, and manual or electronic confirmation | 9 |
| Access | Entitlement, EntitlementCoverage, AccessRevocation | A student's right to content or a service, its origin, and revocation | 6–11 |
| Settlements | Settlement, Allocation, Hold, LedgerEntry, Withdrawal, RefundCase | Calculation of earnings, holds, refunds, and withdrawals | 9–11 |
| Subscriptions | Plan, PlanVersion, Subscription, RenewalConsent, ChangeRequest | Package duration, version, renewal, switching, and grace periods | 10 |
| Promotions | Promotion, AudienceRule, CombinationRule, Redemption | Discount eligibility, stacking, and application to purchases | 10 |
| Learning paths | LearningPath, PathStep, ResourceReference, PathPurchase | Independent instructions and links to internal or external resources | 11 |
| Mentoring | MentoringAgreement, ServicePeriod, TeamAssignment, Conversation | Mentoring service period, team, conversation, and renewal | 11 |
| Student progress | PlaybackProgress, WatchedCoverage, LessonCompletion, UpdateNotice, Playlist, PlaylistItem, PrivateNote | Resume position, viewing, completion with the lesson version, update notices, playlists, and private notes (H-D39) | 7 |
| Educational assessment | Assessment, QuestionVersion, Attempt, Answer, GradeRevision, Appeal | Test attempts, grading, criteria, and appeals | 8 |
| Certificates | Certificate, VerificationToken | Issuance and public verification without exposing the student's profile | 8 |
| Community | CourseConversation, Message, ModerationAction, Review, ReviewRevision, Report | Conversations, reviews, moderation decisions, and reports | 12 |
| Shared services | Notification, DeliveryAttempt, Preference, AdCampaign, AuditEvent, ApprovalRequest | Notifications, advertising, auditing, and approvals | 10, 13, 15 |
| B2B Training | B2BOrganization, B2BMembership, B2BReporting, B2BContract | Company accounts, their employee memberships, their progress reports, and training agreements | 2.3 |
| Achievements and Gamification | Achievement, Badge, LeaderboardEntry, StreakLog | Points and badges system, leaderboards, and streak logging | 14.3 |
| Collaborative Projects | GroupProject, ProjectTeam, TeamMembership, ProjectSubmission | Team projects, their reviews, and deliverables | 14.4 |
| Lab State | LabSessionState, LabEnvironmentTemplate, LabResourceQuota | Preserve lab state between sessions, templates, and resource limits | NQ-11, NQ-16 |
| External Integrations | OAuthConnection, ExternalIdentity | Link GitHub/GitLab accounts and external identities | NQ-01 |

Detailed tables for Crow Store, Crow Hub, VS, or rooms will not be created now. We retain their expected boundaries and links and design them when their functions are resolved; displaying “Coming soon” alone does not require a complete commerce or repository database.

## 4. Identity and instructor dashboard boundaries

### 4.1 What we know

**Agreed:** An account can purchase content regardless of its role. An instructor appoints an instructor or employee within their dashboard, grants permissions, and revokes them. Financial authority cannot be delegated to the instructor's team. Several instructors may contribute to a course, but it has one financially responsible instructor.

### 4.2 Proposed relationships

- One account has multiple contact methods and sessions over time; at most two concurrent devices, a phone and a computer (H-D14). How device type is represented and what happens with an additional device need detail.
- An instructor profile is linked to the account, while a workspace represents the management scope for their content.
- A membership links a person's account to a workspace and carries permission grants and their activation and revocation dates.
- Participation in teaching a course is separate from staff membership and permission to view financial information.
- Course ownership transfers are recorded historically so changing the current owner does not change the beneficiary of an earlier purchase.
- Linking student account to GitHub/GitLab via OAuthConnection provides external proof of projects and facilitates displaying them in their profile. The relationship is optional and does not replace official contact methods (Email/Phone).

**Agreed — D-01:** A person may assist several instructors simultaneously with separate permissions per dashboard; the platform can supply staff assistants.

**Agreed:** Each instructor has one dashboard; they request a platform assistant, approve the candidate, and define permissions. Service and transfer details follow below.

### 4.3 Isolation alternatives

| Alternative | Meaning | Initial assessment for this project |
| --- | --- | --- |
| Shared database with ownership and permission scope per record | Accounts, catalog, and purchases are shared; private data is restricted to its scope | A leading candidate for study because the product is unified, not an approved choice. |
| Schema per instructor | Organizational separation within the same database | Requires repeated change management and cross-instructor relationships; not chosen merely because of the term Multi-Tenant. |
| Separate database per instructor | Greater separation of data and operations | Adds coordination for purchases, search, and paths spanning instructors; not requested as a business requirement. |

**Technical fact:** PostgreSQL schemas do not provide strict security isolation by themselves; access depends on privileges. Row-level security (RLS) can be considered as an additional layer, with attention to exceptions for owners and roles allowed to bypass it. This informs comparison of alternatives and does not approve a particular isolation mechanism. [Schemas](https://www.postgresql.org/docs/current/ddl-schemas.html), [row security policies](https://www.postgresql.org/docs/current/ddl-rowsecurity.html).

Isolation must cover files, search, background jobs, and reports, not just instructor dashboard queries. The basic test: membership in one instructor's dashboard must not expose another dashboard's data or files.

### 4.4 Assistant service — effects on data

**Agreed:** The instructor's request, platform proposal, and instructor approval precede assignment; the instructor sets permissions. Service fees use only their available balance. Service continues during the dashboard-configured grace period, then access stops if unpaid. Records are retained and administration must confirm restoration after payment. The instructor can terminate immediately, with settlement under the offer. The service is in the first release.

**Analytical proposal:** AssistantOffer, AssistantRequest, ServiceAssignment, ServiceCharge, and ServiceLifecycleEvent entities linked to the assistant's membership, instructor, offer, and accepted terms. Record request, nomination, approval, permission grants, deductions, grace, suspension, termination, and restoration decisions. These are not approved table names.

**Data integrity proposal:** Deductions and withdrawals share protection against double spending of available funds; execution priority needs a decision. Stopping service in one dashboard does not remove other memberships. Separating service records, membership, and financial movements preserves work and history when access is revoked.

**Open:** Deduction timing and retries, grace-period fee settlement, scope of grace configuration, and receipt and onward payment of fees to assistants. An assistant payment account or prepaid balance is not assumed.

### 4.5 Course ownership transfer — financial history and responsibility

**Agreed:** Administration and both instructors approve, later sales use a new agreement, and earlier earnings remain with the previous instructor. The new instructor assumes all course and existing-student responsibilities while purchases and access are preserved. Resetting starts the course's accounting under the new instructor; it does not delete history or reset an entire instructor wallet.

**Analytical proposal:** A CourseOwnershipTransfer record contains both parties, approvals, effective time, and references to the previous and new agreements. Each sale's earnings remain linked to the beneficiary and agreement at sale, rather than inferred solely from the current owner. Service responsibility changes are separate from ownership of earlier earnings.

**Agreed — H-D01, H-D02, H-D05:** the previous instructor's and team's access to the transferred course is removed immediately, while their account, records, and balance are kept. A refund follows the original purchase and its agreement, and a pending refund stays linked to the instructor the student bought from.

**Analytical proposal:** the transfer closes the previous instructor's team memberships on this course at the effective time without deleting them, and a hold tied to an earlier transaction stays with its original beneficiary.

**Open:** transactions concurrent with the transfer (H-Q08), and proving and settling a breach by the new instructor (H-Q07).

## 5. Content, entitlements, and sales

**Agreed:** purchases are for the whole course only, and each course has its own price (H-D10). Students keep purchased content without expiry and get lesson updates at no charge (H-D06) while keeping their completion (H-D39). Each lesson belongs to one course, and copying creates an independent lesson (H-D40). A paid add-on is tied to a course and only its owners can buy it (H-D41). A moved lesson stays with those who owned it (H-D08).

The proposed model separates three things:

1. **Content:** A course, then sections, lessons, revisions, and files.
2. **Commercial offer:** what is sold: the whole course, or a paid add-on tied to it, with price and terms.
3. **Buyer entitlement:** What this particular student actually received through a particular purchase.

An order status of “paid” proves the course purchase; the entitlement covers lessons later added to it and their updates, while a paid add-on is a separate entitlement. A playlist organizes available content; it does not grant a new entitlement.

### 5.1 Content questions: settled and remaining

- **Moving and reusing lessons:** settled; one course per lesson with independent copies (H-D40), and moving does not remove the right (H-D08).
- **An old version when the new one becomes paid:** no longer applies; updates are free (H-D06). Keeping a visible archive of old versions was not approved.
- **Commitments of an incomplete course:** settled; a declared syllabus with a lesson list and date (H-D42), and anything outside it is an optional paid add-on (H-D41).
- **Buying a part unlocks the chat and editor:** no longer applies after partial sales were cancelled (H-D10).
- **Lesson types:** settled; video, text, and standalone tests (H-D13).

**Still open:** representing lesson versions in progress, who owns copied lessons after an ownership transfer, and the definition of completion for each type (H-Q12).

### 5.2 Proposed publishing lifecycle

Content draft → ready for publication after checking instructor permissions → published. Availability for new purchases is separate from access for existing buyers, and course completion is separate from publication status. “Complete,” “published,” and “available for sale” are not combined into one status.

### 5.3 Proposed data impact

- `Lesson` belongs to one `Course`, with an optional field pointing to the lesson it was copied from.
- `LessonCompletion` records the lesson version at completion; the "updated since you completed it" state is derived by comparing versions and does not cancel completion.
- `AddOn` is tied to a course and has a price, and owning the course is a condition for buying it.
- `DeclaredSyllabus`, `PlannedLesson`, the expected completion date, and `DeadlineExtension` (count and reason), plus a "late" course state that stops sales.

These are analytical proposals, not approved tables.

## 6. Orders, payments, and refunds

### 6.1 Proposed relationships

- A user may create multiple orders, each containing purchase items. Actual support for a cart with multiple products still requires a decision.
- An order item links to an offer, content coverage, price, currency, and discounts as applied to the transaction.
- A payment attempt may fail and another succeed; the payment attempt is therefore distinct from the order.
- Electronic confirmation or manual payment review provides payment evidence linked to an external reference or an authorized person's decision.
- Qualifying confirmation creates an entitlement and financial settlement; a duplicate provider notification does not create another entitlement or balance credit.
- A refund request is linked to the original content and transaction; recording approval is separate from executing the transfer and revoking access.

### 6.2 Candidate lifecycles

| Process | Proposed states | Boundaries to resolve |
| --- | --- | --- |
| Order | Awaiting payment, confirmed, canceled | Cancellation, expiry, and mixed carts. |
| Payment attempt | Started, processing, succeeded, failed, indeterminate result | Reconciling provider statements with notifications. |
| Manual payment review | Submitted, under review, accepted, rejected | Responsible person, reason, evidence links, and reuse of evidence. |
| Refund | Requested, under review, rejected or approved, executing, completed or execution failed | Timing of earnings holds and access revocation, and the retry process. |

These state names do not establish a new acceptance policy. For example, “transfer failed” does not mean the student's approved refund entitlement has ended.

### 6.3 Historical facts versus freezing a policy

**Technical proposal:** Preserve purchase facts as they occurred: amounts paid, currencies, discounts, and the agreement used. This does not resolve the proposal to **freeze lesson weights for refund calculations**, which the project owner wants to review with specialists. Even if a later policy uses adjusted values, we must know the original facts and the policy applied to the calculation.

## 7. Earnings, holds, and withdrawals

### 7.1 Why is a WalletBalance field insufficient?

A single balance does not explain where money came from, the platform share, mentoring fees, or amounts pending because of a recent purchase or incomplete course. A record of movements, settlements, and holds is therefore proposed, with a derived balance and a financial ledger reviewed by specialists before formulas are approved.

| Proposed component | Data it represents |
| --- | --- |
| Settlement | Sale item, payment, beneficiary, agreement, and currency. |
| Value allocation | Platform share, instructor earnings, mentoring fees, and allocation of discount costs. |
| Hold | Reason, amount, start, release condition, and the actor or event that released it. |
| Financial movement | Signed amount or accounting entry and its reference, without silently overwriting history. |
| Withdrawal request | Amount, currency, beneficiary account, approval, transfer, and result. |

**Data integrity proposal:** Reserve a withdrawal request's amount within the available balance until the request is resolved, preventing two concurrent requests from spending the same balance. Do not treat it as paid before transfer confirmation. This does not change the agreed 24-hour deadline.

### 7.2 Hold reasons are not a single status

**Agreed (H-D03):** a recent purchase amount waits 7 days; all earnings from an incomplete course wait for completion, the instructor's declaration, and team approval, and the conditions stack. A path sale makes net earnings available immediately upon payment. A course still late after its single extension allows a refund from the held amount (H-D43).

**Proposal:** Multiple hold reasons can apply to the same amount, which is released only when all applicable conditions are satisfied. **Agreed (H-D04):** a refund request submitted within the window is a hold reason that lasts until the request is decided, and it is not rejected because of administrative delay. **Agreed (H-D12):** the platform does not fund refunds and record a debt on the instructor.

### 7.3 Currencies and settlements

The proposal is to record payment currency, settlement currency, amounts, and the exchange rate, source, and time when currencies differ. Dinar and dollar figures must not be added into a single numeric balance without documented conversion. Rounding, fees, and the equivalent of the 10-dollar minimum remain open; no default figures are invented.

## 8. Packages and policies managed from the dashboard

**Agreed:** Package prices, benefits, grace periods, switching, and credits are managed from the dashboard; changes by delegates require owner approval. Subscribers retain the terms of their current period until renewal.

Proposed relationships:

- A package has versions; a subscription links to the version defining its period's terms.
- A request to change price or benefits is separate from the effective version and records the requester, approver, decision, and effective time.
- Automatic renewal consent is recorded separately from current access validity.
- Canceling renewal does not cancel a paid entitlement and is not equivalent to immediately switching to an alternative.
- Switching records the old and new packages, start time, remaining-period calculation, discount, and policy for any value difference.

**Design proposal:** Configurable financial settings have types, bounds, permissions, and version history; they are not merely free text in a generic settings table. Whether a change applies to existing or future subscribers follows the business decision for each policy.

## 9. Learning paths and mentoring: permanent access and a temporary service

**Agreed:** A path remains available after mentoring expires; mentoring begins at purchase, lasts a defined period, and can be renewed. Any team member may respond, and the conversation remains readable without sending messages after the service expires.

The proposal therefore separates:

1. Entitlement to read the path and its instructions.
2. The mentoring service period, with its start, end, and service-hours terms.
3. The platform mentoring agreement when applicable: a fixed fee per student and service scope.
4. Team assignment and responding members, without requiring one fixed mentor per student.
5. Conversation history and messages, linked to the beneficiary and service and retained after the period ends.

Renewal adds a service period and its payment under rules not yet detailed; it does not unnecessarily resell the right to read the path. Automatic storage of WhatsApp messages within the platform is not assumed.

**Open:** If a student renews early, is the period added after the current end or started immediately? Can the mentoring provider change during the period, and under what agreement?

## 10. Viewing, tests, and private content

### 10.1 Progress

**Proposal:** Separate the last playback position from the amount actually watched. Scrubbing to the end of a video does not prove everything before it was watched. This matters for refunds, reviews, and progress measurement.

Candidate data includes the student, lesson, video version, stopping position, and watched ranges or an auditable summary, depending on acceptable data volume. The definition of a completed lesson, measurement precision, and retention remain open. The 10% rule concerns the refund proposal and does not automatically become the course completion criterion.

### 10.2 Grading and appeals

The proposal links each attempt to the question versions and grading criteria used, preserving the answer, result, regrading history, and reason for changes. For AI, retain model references, criteria version, and the structured result needed for review; storing internal reasoning or provider secrets is not requested.

Regrading must not silently erase the previous grade. Certificate eligibility depends on an approved result and verification conditions, not merely the model's response text. These are proposed design controls; grading quality, inability to resolve a case, and escalation remain undefined.

### 10.3 Privacy scope

| Data | Agreed access or required boundary |
| --- | --- |
| Notes and markers | Only the student who owns them; the instructor's teaching role does not grant access. |
| Progress and results | The student and the instructor within their courses; team delegation requires specific permissions. |
| Group conversation | Buyers, the instructor, and their team according to permissions. |
| Reporter identity | Only the responsible reporting team, not the instructor being reported. |
| Public certificate | Only necessary verification information; actual fields are not defined. |
| Financial information | The responsible instructor and authorized platform personnel, not the instructor's team. |

Logging private notes verbatim in an audit log that employees can read would conflict with privacy. The proposal is to record the modification event without copying private content, with exceptions and permissions defined later.

## 11. Core relationships before a detailed ERD

This is a list of conceptual relationships, not a final diagram of all entities.

| Relationship | Candidate cardinality | Status |
| --- | --- | --- |
| User and purchase orders | One user has multiple orders | Directly inferred from repeat purchases. |
| User and instructor teams | Through memberships with permissions | Multiple teams for one person are agreed in D-01; permissions are separate for each dashboard. |
| Course and participating instructors | Multiple contributors and one financially responsible instructor at a given time | Meaning agreed; historical representation of responsibility changes is proposed. |
| Course, sections, and lessons | One lesson in one course; a copy is an independent lesson | Settled (H-D40). |
| Offer and content coverage | An offer for the whole course, and a paid add-on tied to a course | Settled (H-D10, H-D41); version representation is proposed. |
| Purchase and entitlements | A course purchase creates an entitlement that includes its updates; an add-on is a separate entitlement | Settled (H-D06, H-D41). |
| Settlement and holds | A settlement can be affected by multiple hold reasons | Proposed design to protect release conditions. |
| Package, versions, and subscription | A package has versions; a subscription follows a version | Proposed to preserve current-period terms. |
| Path purchase and mentoring periods | Path entitlement is independent of service and renewal periods | Expresses the decision that service expires while the path remains. |
| Attempt, grades, and appeal | One attempt has grade and appeal history according to policy | Appeal and attempt counts are undefined. |

We will not approve keys or unique constraints that prevent a relationship before confirming it is unnecessary; nor will we leave relationships unconstrained merely because they might expand later.

## 12. Data planning questions — discussion order

We will ask one question at a time and update this table and the product document after each answer. D identifiers concern data design and link to the product document's O register where topics overlap.

| ID | Question or decision | Impact | Status |
| --- | --- | --- | --- |
| D-01 | Can a person act as a delegate for several instructors at the same time? | Account-to-membership relationships and permission scope | Settled: yes, with separate permissions; the instructor requests, approves, and grants permissions to a platform assistant. |
| D-02 | Instructor responsibility, workspace boundaries, and content ownership transfers | Isolation and financial history | One dashboard, transfer finances, student responsibility, and removal of the previous instructor's and team's access settled (H-D01, H-D02, H-D05); concurrent transactions and breaches remain open (H-Q07, H-Q08). |
| D-03 | Entitlements when a lesson is updated or moved and paid content is added | Content versions and purchase coverage | Mostly settled (H-D06, H-D08, H-D39, H-D40, H-D41). Remaining: version representation and ownership of copies after transfer. |
| D-04 | Incomplete courses: what was promised, and how is delivery proven? | Commitments, publishing schedule, and financial holds | Mostly settled (H-D42, H-D43). Remaining: who approves completion and limits on changing the syllabus. |
| D-05 | Refund formula and fixed or adjustable lesson values | Value history, policies, and settlements | Requires specialists; O-04. |
| D-06 | A refund request still under review when funds become due for release | Holds, withdrawals, and concurrency | Settled: the hold lasts until the request is decided (H-D04). Remaining: review time and execution (H-Q06). |
| D-07 | Cart, payments, currencies, amounts, and fees | Orders, attempts, and settlement | Open; O-16. |
| D-08 | Package structure, feature identifiers, and policy change history | Settings versions and subscription rights | Open; O-01 and O-17. |
| D-09 | Mentoring renewal, team changes, and failure to deliver service | Service periods, assignment, and compensation | Open; O-07 and O-09. |
| D-10 | Lesson completion definition, test criteria, and appeals | Progress events, grades, and certificates | Open; O-11 and O-12. |
| D-11 | Retention, deletion, auditing, and private content | Archiving, privacy, and backups | Open; O-14 and O-22. |
| D-12 | Approval of isolation approach, storage map, and operations | Physical design and service boundaries | After ownership and permissions are settled; O-20 and O-21. |
| D-13 | Assistant service lifecycle, fees, and settlement | Memberships, charges, balance, and access suspension | Core rules settled; collection timing, grace settlement, and assistant payment remain open; O-05 and O-15. |
| D-14 | Resource limits per lab session (CPU, RAM, Network, execution duration) | LabSessionState and LabResourceQuota entities | Open |
| D-15 | Achievement relationship to the entity it is earned from (course, lesson, assessment, project) | Achievement and Badges design and multiple sources | Open |
| D-16 | Group projects and their deliverables data model | GroupProject, ProjectSubmission, TeamMembership | Open |
| D-17 | B2BOrganization lifecycle and its relationship to User and B2BMembership | Institutional training model and its agreements | Open |

## 13. Checking design sufficiency before writing tables

### 13.1 Scenarios the model must represent

- An instructor purchases a course without a second identity or exposure of the seller's data.
- A revoked employee delegation does not retain old permissions through a session or cached data.
- Someone who owns a course buys another course with similar content; they pay its full price, and each entitlement is recorded separately (H-D10).
- Content withdrawn from new sales remains available to previous buyers.
- A platform-share change does not erase the allocation of a settlement made before the change.
- Duplicate payment confirmation does not duplicate balance credits or entitlements.
- Two concurrent withdrawal requests cannot spend the same amount twice.
- An incomplete course does not pay out earnings before completion and approval, even after 7 days (H-D03).
- Updating a lesson does not cancel the completion of students who completed it (H-D39).
- Copying a lesson does not carry completion over to the other course (H-D40).
- Transferring a course with a pending refund does not move the amount to the new owner (H-D05).
- Mentoring expiry does not delete the path or conversation history.
- A package change does not alter benefits for a subscriber's current period.
- Editing a review to below 3 changes its publication status and preserves what is needed for review.
- Regrading an answer preserves the reason for the grade change and its relationship to the certificate.
- Building a search index does not expose students' notes, reports, or private conversations.

### 13.2 Conditions for moving to the logical model

Each core domain needs a data owner, identity definition, relationships and cardinality, states and transitions, associated financial or permission rules, history and deletion policy, and acceptance scenarios. A stable domain can be approved without waiting for every future feature, but we cannot declare the entire project model complete while consequential questions remain open.

### 13.3 Next deliverables

1. **Permission and data-scope matrix:** Who can read and modify what, and in which dashboard.
2. **Logical model and ERD for approved modules:** Explicit cardinality and constraints, with open questions tracked.
3. **Data dictionary:** Field definitions, types, required values, sources, and retention rationale.
4. **Architecture decision records:** Compare isolation, storage, search, and code execution before approving them.
5. **API contracts:** Based on approved operations and lifecycles, not direct exposure of tables.
6. **Implementation and verification plan:** Phases, dependencies, responsibilities, acceptance criteria, and time estimates.

## 14. Version history

**0.3 — 31 October 2026:** Applied decisions H-D01 to H-D45: cancelled partial sales and rebuilt section 5 around whole-course purchase and paid add-ons, one course per lesson with independent copies, completion kept after updates, the declared syllabus and extension, holds until a refund request is decided, removal of the previous instructor's access after transfer, the two-device limit, and Redis and S3 storage. Updated D-02, D-03, D-04, D-06, and the scenarios. No tables have been approved yet.

**0.2 — 30 September 2026:** Settled D-01 and part of D-02; added proposed assistant-service and ownership-transfer analysis and D-13. Updated decision status without approving tables or isolation. Previous instructor and team access after transfer remains unanswered.

**0.1 — 28 September 2026:** Responds to the request to continue documentation toward data planning. Adds candidate entities and relationships, separates identity, permissions, ownership, and entitlements, and provides initial lifecycles and an ordered question register. No isolation approach, database count, or final logical/physical model has been approved. No tables, migration files, or product code were created.
