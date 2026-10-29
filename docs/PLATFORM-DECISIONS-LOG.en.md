# Platform Design Decisions Log — Exploratory Session

**Date:** 19 October 2026  
**Status:** Draft — under review  
**Branch:** `plan-en` (English translation of `PLATFORM-DECISIONS-LOG.ar.md` on plan and plan-ar)  
**Reference:** this file is a complete archive of the strategic questions asked to define the platform's identity, and the project owner's initial positions on them.

---

## 📑 Table of contents
1. [Areas 1-10: Core infrastructure and security](#areas-1-10-core-infrastructure-and-security)
2. [Areas 11-20: Expansion and growth](#areas-11-20-expansion-and-growth)
3. [Areas 21-30: Collaboration and community](#areas-21-30-collaboration-and-community)

---

## Areas 1-10: Core infrastructure and security

### 🛡️ Authentication, identity, and security
| ID | Question | Answer | Status |
|---|---|---|---|
| NQ-01 | Linking accounts with GitHub/GitLab | Yes | Settled |
| NQ-02 | Mandatory multi-factor authentication (MFA) for instructors and administrators | Mandatory, but the method is not defined yet | Needs detail |
| NQ-03 | Managing concurrent sessions | Only phone and computer (no more than two devices); the idle policy is not defined | Needs detail |
| NQ-04 | Account recovery strategy | Both (security questions and identity verification) | Settled |
| NQ-05 | Vulnerability disclosure program (VDP) | Open for discussion | Open |
| NQ-06 | Using a WAF with custom rules | Yes | Settled |

### 💻 Labs and code execution
| ID | Question | Answer | Status |
|---|---|---|---|
| NQ-07 | WebAssembly (WASM) support for running in the browser | Needs review | Open |
| NQ-08 | Providing pre-built environments | Open for discussion | Open |
| NQ-09 | Preventing malicious activity (crypto mining, etc.) | Needs review | Open |
| NQ-10 | Allowing Docker-in-Docker | Open for discussion | Open |
| NQ-11 | Keeping lab state between sessions (persistent storage) | Yes | Settled |
| NQ-12 | Providing a full terminal (Linux shell) | Yes | Settled |

### 📜 Intellectual property and content management
| ID | Question | Answer | Status |
|---|---|---|---|
| NQ-13 | Intellectual property rights for content | As stated in the contract | Settled |
| NQ-14 | Process for responding to DMCA complaints | Not settled yet | Open |
| NQ-15 | Preventing content theft between instructors | Not settled yet | Open |
| NQ-16 | Archiving policy for old courses | They are not removed | Settled |
| NQ-17 | Managing major course versions (v1 → v2) | Not settled yet | Open |

### 🎓 Student learning experience
| ID | Question | Answer | Status |
|---|---|---|---|
| NQ-18 | Personal notes tied to video timestamps | Yes | Settled |
| NQ-19 | Bookmarks and sharing them | Open to discussion | Open |
| NQ-20 | Picture-in-Picture (PiP) support | Yes | Settled |
| NQ-21 | Offline mode on mobile | Not settled yet | Open |
| NQ-22 | Visual progress tracking (progress timeline) | Yes | Settled |

### 🏆 Certificates and advanced testing
| ID | Question | Answer | Status |
|---|---|---|---|
| NQ-23 | Preventing certificate forgery (blockchain/QR) | Open for discussion | Open |
| NQ-24 | Automated proctoring system | Yes | Settled |
| NQ-25 | Advanced anti-cheating mechanisms | Not settled yet | Open |
| NQ-26 | Support for coding challenges and essay questions with AI | Yes | Settled |

### 🤖 Advanced AI
| ID | Question | Answer | Status |
|---|---|---|---|
| NQ-27 | Building a personal AI tutor | Yes | Settled |
| NQ-28 | Using AI to grade text tests | Yes | Settled |
| NQ-29 | Automatic summaries (AI summaries) | Yes | Settled |
| NQ-30 | Smart recommendation system (AI recommendations) | Yes | Settled |

### 🌍 Compliance, privacy, and accessibility
| ID | Question | Answer | Status |
|---|---|---|---|
| NQ-31 | Compliance with GDPR and Iraqi data protection law | Not settled yet | Open |
| NQ-32 | Right to be forgotten policy | Yes | Settled |
| NQ-33 | Compliance with WCAG 2.1 AA accessibility standards | Yes | Settled |
| NQ-34 | Handling minor students (COPPA) | Not settled yet | Open |

### 💬 Community and interaction
| ID | Question | Answer | Status |
|---|---|---|---|
| NQ-35 | A discussion forum for each course | Yes, for the instructor and assistant, with administration access | Settled |
| NQ-36 | Public comments on lessons | Yes, with offensive language blocked | Settled |
| NQ-37 | Reputation system | Not settled yet | Open |
| NQ-38 | Official Discord/Slack channels | Open for discussion | Open |

### 🚀 Marketing, growth, and integrations
| ID | Question | Answer | Status |
|---|---|---|---|
| NQ-39 | Referral program | Yes | Settled |
| NQ-40 | Coupons | We support percentage coupons with a usage limit | Settled |
| NQ-41 | Affiliate program | Yes in the future, open for details | Open |
| NQ-42 | Public developer API and webhooks | Open for discussion | Open |
| NQ-43 | Pricing by region (geo-pricing) | Not settled yet | Open |

### 🛠️ Monitoring, resilience, and continuity
| ID | Question | Answer | Status |
|---|---|---|---|
| NQ-44 | Logging and monitoring strategy (observability) | Open for discussion | Open |
| NQ-45 | RPO and RTO targets for disaster recovery | Open for discussion | Open |
| NQ-46 | Multi-region deployment | Yes | Settled |
| NQ-47 | Communication plan during outages | Open (likely an isolated environment), not settled | Open |

---

## Areas 11-20: Expansion and growth

### 🏢 Corporate training (B2B) and partnerships
| ID | Question | Answer | Status |
|---|---|---|---|
| FQ-01 | Corporate training solutions for companies | Yes | Settled |
| FQ-02 | Corporate reports | Yes | Settled |
| FQ-03 | Recruiting companies' access to top performers' records | Yes | Settled |
| FQ-04 | Partnerships with Iraqi and Arab universities | Yes | Settled |
| FQ-05 | Special discounts for university students | Yes | Settled |

### 🎥 Live streaming and events
| ID | Question | Answer | Status |
|---|---|---|---|
| FQ-06 | Live streaming support | Not settled | Open |
| FQ-07 | Automatic recording of live streams | Not settled | Open |
| FQ-08 | Hosting free webinars and workshops | Yes | Settled |
| FQ-09 | Live Q&A sessions | Yes, the schedule is set later by the course presenter | Settled |
| FQ-10 | Hosting annual tech conferences | Open for discussion | Open |

### 🎮 Gamification and adaptive learning
| ID | Question | Answer | Status |
|---|---|---|---|
| FQ-11 | Points and achievements system | Yes | Settled |
| FQ-12 | Leaderboards | Yes | Settled |
| FQ-13 | Adaptive learning | Yes | Settled |
| FQ-14 | AI-personalized learning tracks | Yes | Settled |
| FQ-15 | Streaks system (consecutive days) | Yes | Settled |

### 💰 Cryptocurrency and alternative payments
| ID | Question | Answer | Status |
|---|---|---|---|
| FQ-16 | Accepting cryptocurrency | Open for discussion | Open |
| FQ-17 | Internal credits system | Open for discussion | Open |
| FQ-18 | Paying in installments | Open for discussion | Open |
| FQ-19 | Bartering services | Not settled yet | Open |
| FQ-20 | Internal digital wallet for instructors | Yes | Settled |

### 🥽 Virtual and augmented reality
| ID | Question | Answer | Status |
|---|---|---|---|
| FQ-21 | Using VR for interactive labs | Open for discussion | Open |
| FQ-22 | AR support for explaining concepts | Not settled yet, open to discussion | Open |
| FQ-23 | 3D network simulation environments | Open for discussion | Open |
| FQ-24 | Virtual tours of data centers | Open for discussion | Open |
| FQ-25 | Collaboration in a 3D virtual space | Open for discussion | Open |

### 🏆 Competitions and hackathons
| ID | Question | Answer | Status |
|---|---|---|---|
| FQ-26 | Periodic programming competitions | Yes | Settled |
| FQ-27 | Cybersecurity CTF competitions | Yes | Settled |
| FQ-28 | Hackathon environment (48 hours) | Not settled yet | Open |
| FQ-29 | Cash prizes for winners | Open for discussion | Open |
| FQ-30 | External company sponsorship of competitions | Open to discussion | Open |

### 🎓 Professional and academic accreditation
| ID | Question | Answer | Status |
|---|---|---|---|
| FQ-31 | Pursuing professional accreditation (CompTIA, ISC2) | Yes | Settled |
| FQ-32 | Providing continuing education hours (CEUs/CPEs) | Yes | Settled |
| FQ-33 | Partnerships with major tech companies (AWS, MS) | Yes | Settled |
| FQ-34 | Issuing special certificates with the instructor's digital signature | Yes | Settled |
| FQ-35 | Public certificate verification (QR/URLs) | Yes | Settled |

### 🛡️ Cyber insurance and crisis management
| ID | Question | Answer | Status |
|---|---|---|---|
| FQ-36 | Getting cyber insurance | Open for discussion | Open |
| FQ-37 | Incident response plan (IRP) | Not settled yet | Open |
| FQ-38 | Periodic penetration testing | Yes, by a specialized team | Settled |
| FQ-39 | Internal vs external security team | Internal, and open to discussion | Open |
| FQ-40 | Handling a leak of student data | Not settled yet | Open |

### 🌍 Foreign markets and expansion
| ID | Question | Answer | Status |
|---|---|---|---|
| FQ-41 | Expanding into other Arab markets | Yes | Settled |
| FQ-42 | Content translated into other languages | Yes, in the future | Settled |
| FQ-43 | Allowing instructors from other countries | Yes | Settled |
| FQ-44 | Partnerships with foreign learning platforms | Yes | Settled |
| FQ-45 | Handling cultural differences | Open for discussion | Open |

### 📚 Libraries and open resources
| ID | Question | Answer | Status |
|---|---|---|---|
| FQ-46 | An open resource library for everyone | Open for discussion | Open |
| FQ-47 | Community contributions to open resources | Not settled yet | Open |
| FQ-48 | Providing free tools for students | Yes | Settled |
| FQ-49 | A public Q&A forum (Stack Overflow-like) | Open for discussion | Open |
| FQ-50 | Ready-made project templates | Yes | Settled |

---

## Areas 21-30: Collaboration and community

### 👥 Group projects and collaboration
| ID | Question | Answer | Status |
|---|---|---|---|
| LQ-01 | Supporting group projects | Yes | Settled |
| LQ-02 | A collaborative workspace | Not settled yet | Open |
| LQ-03 | Managing task splitting (Kanban boards) | Open for discussion | Open |
| LQ-04 | Code review between team members | Yes | Settled |
| LQ-05 | Assessing each member's contribution (Git commits, etc.) | Open for discussion | Open |

### 🛒 Marketplace for projects and templates
| ID | Question | Answer | Status |
|---|---|---|---|
| LQ-06 | Project marketplace | Yes | Settled |
| LQ-07 | Selling project templates | Yes | Settled |
| LQ-08 | Commission rate on project sales | Open for discussion | Open |
| LQ-09 | Ensuring the quality of sold projects | Through review before publishing, and ratings | Settled |
| LQ-10 | Multiple licenses for projects | Yes | Settled |

### 💼 Career services and professional development
| ID | Question | Answer | Status |
|---|---|---|---|
| LQ-11 | Job board for graduates | Yes | Settled |
| LQ-12 | Resume review service | Open for discussion | Open |
| LQ-13 | Mock interviews | Yes | Settled |
| LQ-14 | Job recommendation system | Yes | Settled |
| LQ-15 | Soft-skills development workshops | Yes | Settled |

### 📊 Advanced analytics and insights for instructors
| ID | Question | Answer | Status |
|---|---|---|---|
| LQ-16 | Advanced analytics dashboard for instructors | Yes | Settled |
| LQ-17 | Predictive analytics | Open for discussion | Open |
| LQ-18 | Video heatmaps | Yes | Settled |
| LQ-19 | Smart recommendations for instructors to improve content | Yes | Settled |
| LQ-20 | Benchmarking performance between courses | Yes | Settled |

### 💳 Advanced subscription and membership models
| ID | Question | Answer | Status |
|---|---|---|---|
| LQ-21 | Unlimited annual subscriptions (all-access) | Open for discussion | Open |
| LQ-22 | Freemium model | Yes | Settled |
| LQ-23 | Family memberships | No | Settled |
| LQ-24 | Free trials | Free trials mean the first 3 videos are free and the rest is paid | Settled |
| LQ-25 | Pay-what-you-want model | Not settled yet | Open |

### 🧩 API marketplace and plugins
| ID | Question | Answer | Status |
|---|---|---|---|
| LQ-26 | Plugin store | Open for discussion | Open |
| LQ-27 | Providing SDKs in multiple languages | Open for discussion | Open |
| LQ-28 | Revenue model for the plugin marketplace | Not settled yet | Open |
| LQ-29 | Ensuring plugin security | By testing them | Settled |
| LQ-30 | Webhook support for notifications | Open for discussion | Open |

### 🎧 Audio content and podcasts
| ID | Question | Answer | Status |
|---|---|---|---|
| LQ-31 | Supporting educational podcasts | Open for discussion | Open |
| LQ-32 | Advanced audio player | Yes | Settled |
| LQ-33 | Downloading audio episodes (offline) | Open for discussion | Open |
| LQ-34 | Official platform podcast channel | Yes | Settled |
| LQ-35 | Audio interviews | Not settled yet | Open |

### 🤝 Peer-to-peer learning
| ID | Question | Answer | Status |
|---|---|---|---|
| LQ-36 | Peer review system | No | Settled |
| LQ-37 | Study groups system | Open to discussion | Open |
| LQ-38 | Peer tutoring for rewards | Open for discussion | Open |
| LQ-39 | Virtual study rooms | Yes | Settled |
| LQ-40 | Pair programming system | Yes | Settled |

### 🎓 Alumni network
| ID | Question | Answer | Status |
|---|---|---|---|
| LQ-41 | Building an alumni network | Open for discussion | Open |
| LQ-42 | An alumni directory for employers | Open for discussion | Open |
| LQ-43 | Networking events for alumni | Yes | Settled |
| LQ-44 | Special discounts for alumni | Yes | Settled |
| LQ-45 | Mentorship system | Not settled yet | Open |

### 🌐 Contributing to open-source projects
| ID | Question | Answer | Status |
|---|---|---|---|
| LQ-46 | Encouraging open-source contribution | Yes | Settled |
| LQ-47 | A list of projects needing contributors (Hacktoberfest) | Yes | Settled |
| LQ-48 | Workshops on Git and open-source workflows | Yes | Settled |
| LQ-49 | Rewards for open-source contributors | Open for discussion | Open |
| LQ-50 | An open-source project of the platform's own | Open for discussion | Open |

---

## 📝 Closing notes
- The answers in this session were recorded as a **guiding draft** for the project owner.
- Proposed next steps:
  1. Review questions with the status "Open" or "Needs detail" with the team or specialists.
  2. Extract **binding decisions** from the settled answers and merge them into the decision table in `TEAM-HANDOFF.en.md`.
  3. Update the `PROJECT-BRIEF` and `DATA-PLANNING` documents to reflect these new decisions.
