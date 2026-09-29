# Repository Organization Status

Last update: 28 September 2026.

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

Resume question D-01 in the data document. There is no need to reopen it tonight, and this work includes no product implementation or production deployment.
