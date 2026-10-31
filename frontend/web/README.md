# Crow Station — Web

Next.js (App Router) + TypeScript + Tailwind CSS. Arabic first (`lang="ar" dir="rtl"`).

```bash
npm ci
API_BASE_URL=http://localhost:8080 npm run dev   # or `make web` from the repository root
```

| Script                            | Purpose                                                           |
| --------------------------------- | ----------------------------------------------------------------- |
| `npm run lint`                    | ESLint (Next.js rules, zero warnings)                             |
| `npm run typecheck`               | Generate route types and run `tsc --noEmit`                       |
| `npm run format` / `format:check` | Prettier                                                          |
| `npm test` / `test:coverage`      | Vitest + Testing Library                                          |
| `npm run build`                   | Production build (`output: "standalone"`, used by the Dockerfile) |

Runtime configuration: `API_BASE_URL` (server side) points at the API. It is read at request time, so one image runs in every environment.

`AGENTS.md` is written by Next.js itself and points coding agents at the docs bundled with this exact Next.js version.
