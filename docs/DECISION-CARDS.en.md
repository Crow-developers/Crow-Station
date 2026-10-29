# Crow Station — Decision Cards

> Record of the project owner's answers using the section 9 template of the team handoff. Identifiers match the Arabic version `DECISION-CARDS.ar.md` on the plan and plan-ar branches.

One card is added for each question that is settled. Old cards are never deleted; when a decision changes, a new card is added and the old one is marked "superseded".

**Note:** the H-Q and H-D identifiers come from the Arabic team handoff (`TEAM-HANDOFF.ar.md` on plan). That handoff has not been translated to English yet.

## H-Q01 — Effect of a lesson update on completion

| Field | Value |
| --- | --- |
| Identifier | H-Q01 → decision H-D39. Related: D-03, D-10, H-D06. |
| Question asked | An instructor publishes a free update to a lesson the student has already completed. What happens to the student's progress on that lesson? (Certificates issued before the update stay valid in every option.) Options: stays complete with a notice / instructor decides the update type / any update clears completion / only the assessment is retaken. |
| Project owner's answer | Chose: "Stays complete with a notice (recommended)". |
| Answer and documentation dates | Answer: 29 October 2026. Documented: 29 October 2026. |
| Status | Agreed. |
| Operational wording | When an instructor publishes an updated version of a lesson that a student has completed, the lesson stays complete for that student, the student sees a notice that the lesson was updated, and reviewing the new version is optional. |
| Earlier decision affected | Nothing revoked. Complements H-D06 (updates are free and the buyer keeps the content). |
| Exceptions and open points | Form and channel of the notice; effect of an update on a lesson the student started but did not finish; the definition of completion itself (H-Q12). |
| Data, permissions and money impact | Proposal: store each completion with the lesson version that was completed, and add an "updated since you completed it" state to drive the notice. No financial impact. |
| Acceptance example | When a student completes a lesson and the instructor then publishes an updated version, the result is: the lesson still shows as complete, the course completion percentage does not change, and a "This lesson was updated" notice appears. |
| Review | Card prepared by Claude at the project owner's request. No reviewer assigned. Decision approved by the project owner. |
| Places to update | Handoff 4.3, 7 and 12; product document 6.1 and 7; data planning 5, D-03 and D-10; Arabic version. |

## H-Q02 — A lesson in more than one course

| Field | Value |
| --- | --- |
| Identifier | H-Q02 → decision H-D40, superseding H-D09. Related: D-03, D-10, H-D08, H-D10. |
| Question asked | A single lesson can appear in more than one course (H-D09). If the student completes it in course A and is also enrolled in course B, where the same lesson appears, does it count as complete in B too? |
| Project owner's answer | "A lesson cannot be in more than one course, and if that happens, each course has its own progress." Asked about H-D09, they said: "I don't remember answering anything like that." After an example (an instructor who needs an "Installing Python" lesson in two courses), they chose: "Option 2: independent copy". |
| Answer and documentation dates | Answer: 29 October 2026. Documented: 29 October 2026. |
| Status | Agreed. |
| Operational wording | Each lesson belongs to exactly one course. If an instructor wants the same content in another course, they copy the lesson, which creates an independent lesson with its own progress and updates. Editing one copy does not change the other, and the student completes each copy within its own course. |
| Earlier decision affected | H-D09 ("the same lesson can be used in several courses") is superseded. The project owner did not recall that answer, and the repository does not hold its original wording. H-D08 (moving a lesson) and H-D10 (each course priced independently) are unchanged. |
| Exceptions and open points | Who owns the copy when course ownership is transferred; whether quizzes and attachments are copied automatically; whether copying between different instructors is allowed. |
| Data, permissions and money impact | Proposal: one course per lesson, with an optional field pointing to the original lesson it was copied from. Progress is recorded per lesson, and therefore per course. No cross-course discount (H-D10). |
| Acceptance example | When an instructor copies the "Installing Python" lesson from "Python Basics" to "Data Analysis", and the student had completed it in the first course, the result is: the lesson shows as not complete in the second course, and editing the first course's copy does not change the second's. |
| Review | Card prepared by Claude at the project owner's request. No reviewer assigned. Decision approved by the project owner. |
| Places to update | Handoff 4, 4.3, 5, 7 and 12; product document 6.1; data planning 5 and D-03; Arabic version. |

## H-Q03 — How a paid add-on is sold

| Field | Value |
| --- | --- |
| Identifier | H-Q03 → decision H-D41. Related: H-D07, H-D10, O-03, D-03. |
| Question asked | You decided earlier (H-D07) that an instructor can sell a paid "add-on" outside the promised course content, and that selling a single lesson or section is not allowed (H-D10). Example: after the "Python Basics" course, the instructor wants to sell "10 extra solved exercises". How is this add-on sold? Options: an add-on tied to the course / a standalone product for anyone / published as a new course / no paid add-ons. |
| Project owner's answer | Chose: "An add-on tied to the course (recommended)". |
| Answer and documentation dates | Answer: 29 October 2026. Documented: 29 October 2026. |
| Status | Agreed. |
| Operational wording | The instructor shows the add-on on the course page as an optional extra with a separate price. Only students who own the course can buy it, and it is not sold on its own to non-buyers. |
| Earlier decision affected | Nothing revoked. Defines the form of H-D07, consistent with H-D10 (no selling single lessons or sections). |
| Exceptions and open points | Whether it can be bought together with the course in one transaction; refund rules for the add-on; whether it moves with a course ownership transfer. |
| Data, permissions and money impact | Proposal: an "add-on" entity tied to one course with a price, and a purchase condition: owning the course. Its earnings follow the period and hold rules (H-D03). |
| Acceptance example | When a student who does not own "Python Basics" tries to buy the "10 solved exercises" add-on, the result is: they cannot buy it before buying the course. |
| Review | Card prepared by Claude at the project owner's request. No reviewer assigned. Decision approved by the project owner. |
| Places to update | Handoff 4.3, 5 and 7; product document 6.1; data planning D-03; Arabic version. |

## H-Q04 — Promised content in an incomplete course

| Field | Value |
| --- | --- |
| Identifier | H-Q04 → decisions H-D42 and H-D43. Related: H-D03, O-06, D-04. |
| Question asked | An instructor sells an incomplete course (for example 5 of 20 lessons published). Their earnings are held until the course is complete (H-D03). How is the "promised content" defined so we know when the course is complete? Options: a declared syllabus with a date / a declared syllabus without a date / the instructor declares completion. Follow-up: the declared date passed and the instructor has not published all the lessons; what happens? Options: one extension, then refunds / immediate refunds / the administration decides each case. |
| Project owner's answer | Chose: "A declared syllabus with a date (recommended)", then "One extension, then refunds (recommended)". |
| Answer and documentation dates | Answer: 29 October 2026. Documented: 29 October 2026. |
| Status | Agreed. |
| Operational wording | Before selling an incomplete course, the instructor lists the upcoming lessons and an expected completion date, shown to the student before purchase. The course is complete when the whole list is published and approved by the platform. If the date is missed, the instructor is entitled to one justified extension and students are notified. If still late after the extension, the student may request a refund from the held amount, and sales of the course stop. |
| Earlier decision affected | Nothing revoked. Defines when the "course completed and approved" condition in H-D03 is met. |
| Exceptions and open points | Limits on changing the syllabus after sale; who in the platform approves completion and the review time; the maximum extension length; the refund request window; whether the refund is full or partial depending on what was published (H-Q05); resuming sales after completion. |
| Data, permissions and money impact | Proposal: fields for the declared syllabus (planned lessons), the expected completion date, the number of extensions and the extension reason, and a "late" course state. Amounts stay held until completion or refund. |
| Acceptance example | When the instructor declares completion by 1 December, extends once to 15 December, and 15 December passes without all lessons published, the result is: sales of the course stop, and the student can request a refund from the held amount. |
| Review | Card prepared by Claude at the project owner's request. No reviewer assigned. Decision approved by the project owner. |
| Places to update | Handoff 4.3, 5, 7 and 12; product document 6.2 and 9.2; data planning D-04; Arabic version. |
