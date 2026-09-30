# Crow Station Documentation

This directory is a reference for product analysis and planning, not a software implementation.

- **Product document:** [Formatted reading version](PROJECT-BRIEF.en.html) · [Editable source](PROJECT-BRIEF.en.md).
- **Data planning:** [Formatted reading version](DATA-PLANNING.en.html) · [Editable source](DATA-PLANNING.en.md).
- **To begin reviewing:** Read vision and scope (2–3), the rules for your area of interest, then the open questions (17).
- **For next steps:** See the documentation completion plan (20). System architecture, data model, and API details are not yet approved.

Product version: **0.2**. Data planning version: **0.2**. Last content update: **30 September 2026**.

Multiple assistant teams, one dashboard per instructor, launch assistant services, and course transfer rules are settled. The next unanswered question: Is the previous instructor's and their team's course-management access revoked immediately after transfer while the previous instructor retains only historical financial records? Other open details are in D-02 and D-13. This batch does not start product implementation.

“Agreed” means a point was resolved in discussion, not that the product was implemented. The document preserves distinctions between proposals, preliminary decisions, and deferrals. Regenerate HTML after editing the source to keep both formats aligned. The current formatter is `tools/docs/build-brief.mjs`; it contains no product code.

To update HTML, run the tool without arguments for the product document and with `data` for the data document, using Node.js 22.16 or later after installing the dependency as described in `tools/docs/README.md`. This tool is not part of the planned application.

These drafts originate on the permanent **plan** branch. Only approved portions move to **docs** under the [branch policy](../BRANCHING.md). See [upload and protection status](../REPOSITORY-STATUS.md).

Matching Arabic version: [plan-ar](https://github.com/Crow-developers/Crow-Station/tree/plan-ar/docs). Complete English translation: [plan-en](https://github.com/Crow-developers/Crow-Station/tree/plan-en/docs). Section and question identifiers are the same across languages.
