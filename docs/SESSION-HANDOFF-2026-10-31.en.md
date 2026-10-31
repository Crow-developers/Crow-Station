# Work Session Handoff — 31 October 2026

> Summary of what was done since the 29 October handoff, so the team can see the latest changes before the next session. Arabic version: `SESSION-HANDOFF-2026-10-31.ar.md` on the plan and plan-ar branches. Previous handoff: [29 October](SESSION-HANDOFF-2026-10-29.en.md).

## 1. Summary

- **The product and data planning documents are now version 0.3** in both languages, with decisions H-D01 to H-D45 merged.
- **H-Q05** (refund calculation), **H-Q06** (refund processing), and **the first part of H-Q07** (who decides a breach by the new instructor) were settled. They produced ten decisions: **H-D46 to H-D55**.
- Two technical decisions were added: **H-D44** (Next.js web front end) and **H-D45** (local environment: PostgreSQL, Redis, RustFS).
- **The code foundation** is on `develop` and `staging`: Clean Architecture with .NET 10 and Next.js, Compose, and CI/CD. CI and CD are green.
- **Next resume point: the rest of H-Q07**. After the platform team proves a breach, how is the case settled?

## 2. New decisions

| ID | Decision | Question |
| --- | --- | --- |
| H-D44 | The web front end is Next.js with TypeScript. | — |
| H-D45 | Local environment: PostgreSQL, Redis for caching, and RustFS for file storage (S3-compatible). | — |
| H-D46 | A lesson counts as used: a video after 10% of its duration, a text lesson as soon as it is opened, a test as soon as it is started. Free preview is not deducted. | H-Q05 |
| H-D47 | The course price is one total value; the instructor does not set a value per lesson. | H-Q05 |
| H-D48 | Refund = price × (lessons not used ÷ lessons in the course at purchase). | H-Q05 |
| H-D49 | A course still late after its extension: the same formula on the number of lessons in the declared syllabus. | H-Q05 |
| H-D50 | The non-refundable payment fee is deducted from the student, except for a late course, where the instructor's share bears it. | H-Q05 |
| H-D51 | The platform answers a refund request within 3 working days. | H-Q06 |
| H-D52 | Access stops as soon as the request is submitted, and returns immediately if it is rejected. | H-Q06 |
| H-D53 | The amount goes back to the same payment method; if the transfer fails, it goes to the student's wallet. | H-Q06 |
| H-D54 | After a partial refund, the instructor's share of the remainder is released on execution if the other conditions are met. | H-Q06 |
| H-D55 | The platform team decides whether the new instructor breached their obligations after a transfer, by reviewing complaints and the course change log and hearing from the instructor. | H-Q07 |

**Calculation example:** a 100,000 IQD course with 20 lessons and a 2,000 fee. The student used 4 lessons, so the refund is 80,000 − 2,000 = 78,000 IQD.

Full details, and what remains open in each decision, are in the [decision cards](DECISION-CARDS.en.md) and section 4.4 of the [team handoff](TEAM-HANDOFF.en.md).

## 3. Important note about H-D47

The product document said the instructor splits the course value across lessons, equally or with different values, and proposed fixing that split at purchase. When asked, the project owner said: "The course price is not calculated by the number of videos; it is a total value for the course." Both rules were cancelled, and a lesson is now a counting unit inside the course, not a price unit.

**Decisions H-D46 to H-D55 take precedence over section 9.3 of product document 0.3** until the next version.

## 4. Files changed

| Branch | File | Change |
| --- | --- | --- |
| plan, plan-ar, plan-en | `docs/PROJECT-BRIEF.*` and `docs/DATA-PLANNING.*` | Version 0.3 in both languages, with the section 3.2 table fixed and the corporate training section moved to 2.3. |
| plan, plan-ar, plan-en | `docs/TEAM-HANDOFF.*` | Version 0.4: new section 4.4, H-D44 and H-D45, H-Q05 and H-Q06 struck through, resume point the rest of H-Q07. |
| plan, plan-ar, plan-en | `docs/DECISION-CARDS.*` | Three new cards: H-Q05, H-Q06, and H-Q07 (first part). |
| plan, plan-ar, plan-en | `docs/README.md`, `tools/docs/build-brief.mjs`, and `REPOSITORY-STATUS.md` | Links, version numbers, and the resume point. |
| develop, staging | All code | The code foundation and CI/CD (through pull requests). |

**Check:** heading counts, table row counts, and all identifiers match between Arabic and English.

## 5. Not done yet

- **The rest of H-Q07 and questions H-Q08 to H-Q24** are still open.
- **Merging H-D46 to H-D55 into the product document** (9.2, 9.3, O-04, O-05, AC-08) and data planning (D-02, D-05, D-06), in the next version.
- **Follow-up questions:** rounding; whether preview lessons count in the lesson total; paid add-on refunds; manual payments; specialist review of the 10% threshold and the policy.
- **GitHub tasks for the repository owner:** delete the `claude/dev-foundation` and `claude/fix-cd-version` branches; make `ci-success` a required check; turn on Dependency graph and Dependabot alerts; provide team usernames for CODEOWNERS.
- **CI/CD machine:** Ali Adil was asked to prepare a Debian 13 VM with Tailscale or OpenVPN and key-only SSH. The project owner has the setup guide.

## 6. How the session worked

- Each question was asked on its own in Arabic, with a numeric example, options, and a recommendation, and the project owner chose among them. When the owner answered in free text (H-D47), the text was recorded as given.
- Answers were recorded in Arabic on plan and plan-ar, then translated to plan-en.
- Documentation prepared by Claude at the project owner's request. Not yet reviewed by a team member.

## 7. Team's next step

1. Read this file, then section 4.4 of the team handoff and the decision cards.
2. Comment on the handoff discussion on GitHub with any notes, especially about the refund formula (H-D48) and the payment fee (H-D50).
3. The coordinator asks the project owner **the rest of H-Q07** in the next session.
