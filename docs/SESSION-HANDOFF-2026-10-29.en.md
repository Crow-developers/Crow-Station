# Work Session Handoff — 29 October 2026

> A summary of what was done today on the planning branches, so the team can see the latest changes before the next session. Arabic version: `SESSION-HANDOFF-2026-10-29.ar.md` on the plan and plan-ar branches.

## 1. Summary

- Four questions from the handoff list were settled: **H-Q01 to H-Q04**, producing five new decisions: **H-D39 to H-D43**.
- Decision **H-D09** ("the same lesson can be used in several courses") was superseded by **H-D40**.
- A new file records answers: [decision cards](DECISION-CARDS.en.md), one card per question using the section 9 template of the team handoff.
- The English translation of the team handoff and the exploratory session log is now complete on plan-en.
- **Next resume point: H-Q05** — how do we calculate refunds for a course with video, text, and tests, and what lesson values are approved?

## 2. New decisions

| ID | Decision | Question |
| --- | --- | --- |
| H-D39 | Updating a lesson the student completed does not cancel their completion. It stays complete, the student sees a notice that it was updated, and reviewing the new version is optional. | H-Q01 |
| H-D40 | Each lesson belongs to exactly one course. Copying a lesson to another course creates an independent lesson with its own progress and updates. **Supersedes H-D09.** | H-Q02 |
| H-D41 | A paid add-on is shown on the course page at a separate price, and only students who own the course can buy it. | H-Q03 |
| H-D42 | An incomplete course can only be sold with a declared syllabus (the list of upcoming lessons) and an expected completion date, both shown before purchase. The course is complete when the whole list is published and approved by the platform. | H-Q04 |
| H-D43 | If the completion date is missed: one justified extension, with students notified. If still late after that: the student may request a refund from the held amount, and sales of the course stop. | H-Q04 |

Full details, and what stays open for each decision, are in the [decision cards](DECISION-CARDS.en.md) and section 4.3 of the [team handoff](TEAM-HANDOFF.en.md).

## 3. Important note about H-D09

When H-Q02 was asked, the project owner answered that a lesson cannot be in more than one course, and said they did not recall answering anything that allowed it. The repository does not hold the original answer text for H-D09; it was recorded in the handoff as a summary of an earlier discussion. After the options were explained with an example, the project owner chose independent copies, so H-D09 is recorded as "superseded" and kept in the history.

**For the team:** if anyone has a source for the original H-D09 answer, please attach it to the handoff discussion for review.

## 4. Files changed

| Branch | File | Change |
| --- | --- | --- |
| plan, plan-ar | `docs/TEAM-HANDOFF.ar.md` and `.html` | Version 0.2: new section 4.3, H-D09 superseded, H-Q01–H-Q04 struck through in section 7, two new rows in section 5, resume point H-Q05. |
| plan, plan-ar | `docs/DECISION-CARDS.ar.md` | New file: four decision cards. |
| plan, plan-ar | `docs/README.md` | Link to the decision cards and the new resume point. |
| plan-en | `docs/TEAM-HANDOFF.en.md` and `.html` | New file: full translation of handoff 0.2. |
| plan-en | `docs/PLATFORM-DECISIONS-LOG.en.md` | New file: translation of the exploratory session log (147 questions). |
| plan-en | `docs/DECISION-CARDS.en.md` | New file: translation of the four decision cards. |
| plan-en | `tools/docs/build-brief.mjs`, `package.json`, `README.md` | The English HTML tool also generates the handoff via `npm run build`. |
| All | `SESSION-HANDOFF-2026-10-29.*.md` and `REPOSITORY-STATUS.md` | This file, and an entry in the repository status. |

**Verification:** heading counts, table rows, and all identifiers (H-D, H-Q, NQ, FQ, LQ, O, D, AC) match between Arabic and English.

## 5. Not done yet

- **The product and data planning documents** are still at version 0.2 in both languages. The handoff decisions (H-D01–H-D43) have not been merged into them; the required merge list is in section 5 of the team handoff.
- **Questions H-Q05 to H-Q24** are still open.
- **Follow-up questions** opened by today's decisions, including: the maximum extension length, who in the platform approves course completion, refund rules for add-ons, and who owns a copied lesson when course ownership is transferred.
- Nothing was moved to the docs branch or the development branches; everything above is planning drafts.

## 6. How the session worked

- Each question was asked on its own with specific options and a clear recommendation, and the project owner chose among them; when an answer was unclear, a clarification with an example was requested before recording it.
- Answers were recorded first in Arabic on plan and plan-ar, then translated to plan-en.
- Documentation was prepared by Claude at the project owner's request. It has not yet been reviewed by a team member.

## 7. Team's next step

1. Read this file, then section 4.3 of the team handoff and the decision cards.
2. Comment on the handoff discussion on GitHub with any note or conflict you see, especially about H-D09.
3. The coordinator asks the project owner **H-Q05** in the next session.
