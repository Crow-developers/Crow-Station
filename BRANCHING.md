# Branch and Documentation Policy — Crow Station

## 1. Purpose

Organize planning, specifications, development, and releases without confusing drafts with official decisions or production content. The project owner requested that `plan` remain throughout the project's lifetime, with planning separate from official documentation, development, testing, and production.

## 2. Permanent branches

| Branch | Contents | Integration rule |
| --- | --- | --- |
| `plan` | Product and data documents, proposals, questions, plans, and revision history | Updated continuously. Never merged wholesale into product branches or deleted. |
| `plan-ar` | The same Arabic version as plan with an explicit language name | Updated with plan to the same commit; never deleted. |
| `plan-en` | The matching English translation of the plans | Updated by translating the same decisions; never merged over Arabic files or deleted. |
| `docs` | Approved official specifications, architecture decisions, data model, API, operations guides, and release documentation | A review request adds specific approved material, not all plan drafts. |
| `develop` | Integrated development work | Short-lived task branches are reviewed before merging. |
| `staging` | A release candidate from develop and its testing fixes | Candidate nomination starts with a review request from develop; its scope is controlled during testing. |
| `main` | The state approved for production | Promote a staging candidate after review and tests. Mark releases with version tags. |

Permanent branches are not automatically deleted or rewritten by force push. Temporary task branches may be deleted after merging, according to repository policy.

## 3. How do plans become official documentation?

1. Discuss the idea in `plan` and identify its status: proposed, preliminary, agreed, or deferred.
2. When a clear portion is approved, create a short-lived branch such as `docs/approved-course-policy` from `docs`.
3. Transfer only the approved material, citing its source in plan and a commit ID or discussion link, and write it as an official document.
4. Open a Pull Request to `docs` explaining what changed and why; review the specification before merging.
5. Update the decision's reference in `plan` to the official document, preserving the discussion record, draft, and history.

**Current drafts are not promoted to “official” without approval.** Approval of a specification does not prove implementation; label it clearly as “approved specification” or “matches implementation in release ...”.

Do not merge the entire `docs` branch into code branches merely because it contains a useful document. API specifications, migration files, and code-related documentation retain a clear source in the code branch, while official documentation links their versions to the relevant release tag or commit. Avoid conflicting sources of truth.

## 4. Development and release flow

Task branch from `develop` → review and merge into `develop` → nominate for `staging` → verify and approve → promote to `main`.

- Each task links to an approved specification or explains and updates specification changes as part of review.
- Staging fixes also return to develop so they are not lost in the next release.
- Urgent production fixes start from main and return to staging and develop after approval.
- A `staging` branch does not configure test secrets or servers; `main` does not deploy automatically. CI/CD and environments are later work.
- Reviewer counts and mandatory checks are approved when tests and named team members exist; a nonexistent mandatory check must not block the team.

## 5. Required GitHub protection

These are protection requirements, not a claim that they are already active. Implementation is recorded in `REPOSITORY-STATUS.md` on plan after repository settings are verified.

- Prevent deletion and force pushes on all seven permanent branches.
- In particular, protect plan from automatic cleanup after Pull Requests are merged.
- Require review requests before merging into docs, develop, staging, and main after initial setup.
- Financial documentation and policies are reviewed by the owner or authorized people according to team agreement.
- Do not change public/private visibility or invite contributors without the project owner's direction.

GitHub provides deletion and force-push restrictions, and protection can prevent automatic branch deletion. Availability depends on the account plan, repository type, and administration permissions. [Ruleset rules](https://docs.github.com/en/repositories/configuring-branches-and-merges-in-your-repository/managing-rulesets/available-rules-for-rulesets), [automatic branch deletion](https://docs.github.com/en/repositories/configuring-branches-and-merges-in-your-repository/configuring-pull-request-merges/managing-the-automatic-deletion-of-branches).

## 6. What is uploaded and what stays local

The plan branch includes Markdown, formatted HTML, available source material, and the document generator. It excludes rendering-check screenshots, tmp files, node_modules, passwords, and local environment settings.

## 7. Planning resumption point

Multiple assistant teams, one dashboard per instructor, launch assistant services, and course transfer rules are settled. The next unanswered question: Is the previous instructor's and their team's course-management access revoked immediately after transfer while the previous instructor retains only historical financial records? Other open details are in D-02 and D-13. This batch does not start product implementation.

## 8. Arabic and English parity

- `plan` is the original Arabic reference; `plan-ar` is an identical reference updated with it to the same commit.
- `plan-en` contains the same documents in English, using `.en.md` and `.en.html` instead of `.ar.md` and `.ar.html`.
- Translation does not change decision status or resolve open questions. Preserve section numbers, O, D, and AC identifiers, tables, first-release scope, and deferrals.
- Original source images remain unchanged in both versions; they are not newly created English documents.
- When Arabic changes, push the same commit to plan and plan-ar, translate the differences into plan-en, and cite the source commit in the update description. Review parity and regenerate HTML before pushing.
- Do not merge language branches directly into one another; transfer and translate decisions to avoid replacing one language with the other. Synchronization is a documented workflow, not an automatic service.
- Official documentation retains one approval route through docs; additional planning languages do not change production rules.
