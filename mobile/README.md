# Mobile (Flutter)

Reserved for the Crow Station mobile app. Flutter was chosen for mobile (product brief, section 16); the web front end is Next.js.

The app will be scaffolded once the first API endpoints are approved and stable. When it is added:

- `flutter create` here, with the package name agreed by the team.
- A `mobile` job in `.github/workflows/ci.yml` (format, analyze, test) and a `mobile` filter in the `changes` job.
- A Dependabot entry for `pub`.
