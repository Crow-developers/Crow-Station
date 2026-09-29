# Crow Station Documentation

This directory is a reference for product analysis and planning, not a software implementation.

- **Product document:** [Formatted reading version](PROJECT-BRIEF.en.html) · [Editable source](PROJECT-BRIEF.en.md).
- **Data planning:** [Formatted reading version](DATA-PLANNING.en.html) · [Editable source](DATA-PLANNING.en.md).
- **To begin reviewing:** Read vision and scope (2–3), the rules for your area of interest, then the open questions (17).
- **For next steps:** See the documentation completion plan (20). System architecture, data model, and API details are not yet approved.

Product version: **0.1.1**. Data planning version: **0.1**. Last content update: **28 September 2026**.

In data planning, begin with section 3 for the entity map, then section 12 for the order of missing decisions. The current question, D-01, asks whether a person can be a delegate for more than one instructor at the same time. No assumed answer has been recorded.

“Agreed” means a point was resolved in discussion, not that the product was implemented. The document preserves distinctions between proposals, preliminary decisions, and deferrals. Regenerate HTML after editing the source to keep both formats aligned. The current formatter is `tools/docs/build-brief.mjs`; it contains no product code.

To update HTML, run the tool without arguments for the product document and with `data` for the data document, using Node.js 22.16 or later after installing the dependency as described in `tools/docs/README.md`. This tool is not part of the planned application.

These drafts originate on the permanent **plan** branch. Only approved portions move to **docs** under the [branch policy](../BRANCHING.md). See [upload and protection status](../REPOSITORY-STATUS.md).

Matching Arabic version: [plan-ar](https://github.com/Crow-developers/Crow-Station/tree/plan-ar/docs). Complete English translation: [plan-en](https://github.com/Crow-developers/Crow-Station/tree/plan-en/docs). Section and question identifiers are the same across languages.
