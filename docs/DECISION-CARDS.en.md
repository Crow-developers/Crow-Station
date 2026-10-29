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
