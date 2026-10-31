# Crow Station Documentation

**Latest changes — 31 October 2026:** H-Q05 and H-Q06 (refund calculation and processing) settled with decisions H-D46–H-D54 in the [handoff](TEAM-HANDOFF.en.md) and the [decision cards](DECISION-CARDS.en.md). Resume point: H-Q07. Previous handoff: [29 October session](SESSION-HANDOFF-2026-10-29.en.md).

**For the team — start here:** [Formatted handoff](TEAM-HANDOFF.en.html) · [Editable handoff source](TEAM-HANDOFF.en.md), version 0.4, 31 October 2026. Decisions H-D46–H-D54 are newer than version 0.3 of the documents below. Current resume point: **H-Q07** (proving and settling a breach by the new instructor). The full exploratory session log is in [PLATFORM-DECISIONS-LOG.en.md](PLATFORM-DECISIONS-LOG.en.md).

This directory is a reference for product analysis and planning, not a software implementation.

- **Product document:** [Formatted reading version](PROJECT-BRIEF.en.html) · [Editable source](PROJECT-BRIEF.en.md).
- **Data planning:** [Formatted reading version](DATA-PLANNING.en.html) · [Editable source](DATA-PLANNING.en.md).
- **Decision cards:** [The project owner's answers, one question at a time](DECISION-CARDS.en.md), starting 29 October 2026. Settled so far: H-Q01 to H-Q04 (H-D39 to H-D43; H-D40 supersedes H-D09).
- **To begin reviewing:** Read vision and scope (2–3), the rules for your area of interest, then the open questions (17).
- **For next steps:** See the documentation completion plan (20). System architecture, data model, and API details are not yet approved.

Product version: **0.3**. Data planning version: **0.3**. Last content update: **31 October 2026**. Both include decisions H-D01 to H-D45.

Multiple assistant teams, one dashboard per instructor, launch assistant services, and course transfer rules are settled. The next unanswered question: Is the previous instructor's and their team's course-management access revoked immediately after transfer while the previous instructor retains only historical financial records? Other open details are in D-02 and D-13. This batch does not start product implementation.

“Agreed” means a point was resolved in discussion, not that the product was implemented. The document preserves distinctions between proposals, preliminary decisions, and deferrals. Regenerate HTML after editing the source to keep both formats aligned. The current formatter is `tools/docs/build-brief.mjs`; it contains no product code.

To update HTML, run the tool without arguments for the product document and with `data` for the data document, using Node.js 22.16 or later after installing the dependency as described in `tools/docs/README.md`. This tool is not part of the planned application.

These drafts originate on the permanent **plan** branch. Only approved portions move to **docs** under the [branch policy](../BRANCHING.md). See [upload and protection status](../REPOSITORY-STATUS.md).

Matching Arabic version: [plan-ar](https://github.com/Crow-developers/Crow-Station/tree/plan-ar/docs). Complete English translation: [plan-en](https://github.com/Crow-developers/Crow-Station/tree/plan-en/docs). Section and question identifiers are the same across languages.
