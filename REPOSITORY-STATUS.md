# Repository Organization Status

Last update: 30 September 2026.

## Completed locally

- Initialized Git in the project directory.
- Created permanent branches `main`, `docs`, `develop`, `staging`, and `plan` from a shared foundation.
- The shared foundation contains only the project introduction, branch policy, and file-tracking settings.
- The `plan` branch holds current documents, their formatting tools, and available source material.
- Product branches are not a finished application or actual deployment, and `docs` does not yet contain approved official specifications.

## Completed on GitHub

- Repository: [Crow-developers/Crow-Station](https://github.com/Crow-developers/Crow-Station), connected as `origin`.
- Uploaded `main`, `docs`, `develop`, `staging`, and `plan`; `main` is the default branch.
- Replaced the old main reference with clean history after checking the previous SHA; the reference inspection before the reset found no other old branches or tags.
- Product branches contain none of the old implementation. Plans are in [plan](https://github.com/Crow-developers/Crow-Station/tree/plan), and the shared organization foundation is `271bd08`.
- Automatic deletion after merging is disabled.
- Active rules without bypass exceptions prevent permanent-branch deletion and force pushes. Ruleset ID: `24138328`.
- Active rules require Pull Requests into `docs`, `develop`, `staging`, and `main`, with review conversations resolved before merging. Ruleset ID: `24138330`.
- The required approval count is currently zero, and no CI checks are mandatory yet. Reviewers and checks will be selected when the team is configured; requiring a PR is distinct from requiring independent reviewer approval.
- `plan` accepts ordinary direct updates, but active rules prevent deletion or history rewriting.

These settings were read back from GitHub after application to verify them. A repository administrator can later change the rules themselves; permanent branch retention is a continuing policy to preserve when settings change.

## Replacing the old work

The project owner requested a complete reset of the old work. The previous main reference was `e39b1b145ee4a6ade3784209baad5c771ccc6596`. A local mirror was saved in `tmp/repository-backup/Crow-Station-before-reset.git` before replacement. This directory is ignored and is not uploaded to GitHub. The repository retains its URL and existing public visibility.

Here, reset means replacing active history and branch contents; it does not claim to erase copies already cloned by others or all unreferenced Git objects GitHub may retain. The repository itself and its membership settings were not deleted.

## Next session

Multiple assistant teams, one dashboard per instructor, launch assistant services, and course transfer rules are settled. The next unanswered question: Is the previous instructor's and their team's course-management access revoked immediately after transfer while the previous instructor retains only historical financial records? Other open details are in D-02 and D-13. This batch does not start product implementation.

## Arabic and English planning versions — 29 September 2026

- Uploaded plan-ar as an Arabic copy of plan at the same commit.
- Uploaded plan-en with complete translations of the product document, data planning, and repository guides, preserving the structure and providing HTML pages with English reading direction.
- The planning-content translation source is Arabic commit 3b383ef; this repository-status update does not change product decisions.
- Verified parity of section numbers, tables, lists, numeric values, question identifiers, and acceptance criteria, plus working document links and desktop and mobile rendering.
- Original source images remain unchanged in both branches.
- Ruleset 24138328 now includes plan-ar and plan-en; all seven branches are protected against deletion and force pushes without bypass exceptions.
- Plans were not merged into main; docs, develop, staging, and main remain unchanged.
- Future language updates require review and translation under BRANCHING.md; synchronization is not automatic.

## Decision batch — 30 September 2026

Product and data planning documents are version 0.2. This batch consolidates assistant-service and course-transfer answers, preserving the final unanswered question. Arabic is published on plan and plan-ar and English on plan-en; no merge to main or promotion of drafts to official documentation.
