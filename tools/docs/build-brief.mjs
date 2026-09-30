import { readFile, writeFile } from 'node:fs/promises';
import { createRequire } from 'node:module';
const { marked } = createRequire(import.meta.url)('marked');
import { fileURLToPath } from 'node:url';

const isData = process.argv[2] === 'data';
const basename = isData ? 'DATA-PLANNING.en' : 'PROJECT-BRIEF.en';
const version = '0.2';
const title = isData ? 'Data Planning and Design Decisions' : 'Product Brief and Planning Decisions';
const heroTitle = isData ? 'Data planning<br>before table design' : 'Product brief<br>and planning decisions';
const description = isData ? 'From product requirements to entities, relationships, and lifecycles. A conceptual proposal showing what we know and what still needs a decision, without assuming the database design is complete.' : 'One platform for learning, practice, and mentoring. An organized reference for agreed decisions and what remains to be resolved before design and implementation.';
const tags = isData ? ['Proposed design for review','PostgreSQL · Isolation undecided','No tables or migrations yet'] : ['Draft for review','Iraq first · Worldwide access','Website + mobile app'];
const stats = isData ? [['18 domains','Proposed entity map'],['13 decisions','Data discussion sequence'],['Conceptual model','Not a final schema']] : [['September 2027','Preliminary launch target'],['1,000 users','Concurrent-user planning target'],['22 topics','Open question register']];
const source = new URL(`../../docs/${basename}.md`, import.meta.url);
const destination = new URL(`../../docs/${basename}.html`, import.meta.url);
const markdown = await readFile(source, 'utf8');
let rendered = marked.parse(markdown, { gfm: true });
const sections = [];
rendered = rendered.replace(/<h2>([\s\S]*?)<\/h2>/g, (_, title) => {
  const id = `section-${sections.length + 1}`;
  sections.push({ id, title });
  return `<h2 id="${id}">${title}</h2>`;
});
rendered = rendered.replace(/<h1>[\s\S]*?<\/h1>/, '').replace(/<table>/g, '<div class="table-wrap"><table>').replace(/<\/table>/g, '</table></div>');
rendered = rendered.replace(/href="(PROJECT-BRIEF\.en|DATA-PLANNING\.en)\.md"/g, 'href="$1.html"');
const nav = sections.map(s => `<a href="#${s.id}">${s.title}</a>`).join('\n');
const html = `<!doctype html>
<html lang="en" dir="ltr">
<head>
<meta charset="UTF-8"><meta name="viewport" content="width=device-width, initial-scale=1">
<meta name="color-scheme" content="light">
<title>Crow Station | ${title}</title>
<style>
:root{--ink:#172d3b;--muted:#5e6e79;--paper:#fff;--bg:#f1f4f4;--line:#dce5e6;--accent:#087d7e;--light:#eef8f6}
*{box-sizing:border-box}html{scroll-behavior:smooth;scroll-padding-top:28px}body{margin:0;background:var(--bg);color:var(--ink);font-family:'Segoe UI',Tahoma,Arial,sans-serif;font-size:16px;line-height:1.95}
.layout{display:grid;grid-template-columns:275px minmax(0,1fr);max-width:1550px;margin:auto;min-height:100vh}
aside{position:sticky;top:0;height:100vh;overflow:auto;padding:28px 20px;border-right:1px solid var(--line);background:#f8faf9}
.brand{font-size:24px;font-weight:750;letter-spacing:-.6px;line-height:1.4;direction:ltr;text-align:left}.brand small{display:block;font-size:12px;color:var(--accent);letter-spacing:2px;margin-bottom:8px;text-transform:uppercase}
.nav-label{font-size:13px;color:var(--muted);margin:25px 0 8px}nav a{display:block;color:#405663;text-decoration:none;padding:7px 11px;margin:2px 0;border-radius:7px;font-size:13px;line-height:1.7;border-left:3px solid transparent}nav a:hover,nav a.active{background:#e4f1ee;color:#075c5b;border-left-color:var(--accent)}
main{min-width:0;padding:44px clamp(24px,4vw,68px) 75px}.hero{padding:32px 36px;background:#102f3a;color:white;border-radius:15px;margin-bottom:26px;position:relative;overflow:hidden;border-top:5px solid #39bca6}.eyebrow{color:#9ddfd3;font-size:13px;letter-spacing:.3px}.hero h1{font-size:clamp(27px,3vw,37px);line-height:1.6;margin:12px 0}.hero p{color:#cfdee2;margin:0;max-width:720px}.tags{display:flex;flex-wrap:wrap;gap:8px;margin-top:22px}.tags span{font-size:12px;border:1px solid #42606a;border-radius:20px;padding:3px 12px;color:#e1efed}.summary{display:grid;grid-template-columns:repeat(3,1fr);gap:12px;margin-bottom:30px}.summary div{background:white;border:1px solid var(--line);border-radius:10px;padding:15px 20px}.summary strong{display:block;color:var(--accent);font-size:21px;line-height:1.7}.summary span{font-size:12px;color:var(--muted)}
article{background:var(--paper);padding:10px clamp(20px,3vw,42px) 36px;border:1px solid var(--line);border-radius:12px}article>h2{font-size:24px;line-height:1.7;border-top:1px solid var(--line);padding-top:32px;margin:42px 0 19px;color:#075f64}article>h2:first-of-type{margin-top:24px;border-top:0;padding-top:0}h3{font-size:19px;margin:28px 0 12px;color:#254657}p{margin:12px 0}ul,ol{padding-left:25px;padding-right:0;margin:14px 0}li{padding-left:4px;margin:7px 0}li::marker{color:var(--accent)}strong{font-weight:650}blockquote{margin:23px 0;background:var(--light);padding:11px 18px;border-left:4px solid var(--accent);border-radius:6px;color:#315c59;font-size:14px}blockquote p{margin:0}code{font-family:Consolas,monospace;font-size:.86em;direction:ltr;unicode-bidi:isolate;display:inline-block;background:#edf1f3;border-radius:4px;padding:0 5px;overflow-wrap:anywhere}.table-wrap{overflow-x:auto;margin:20px 0;border:1px solid var(--line);border-radius:8px}table{width:100%;border-collapse:collapse;font-size:14px;line-height:1.9;text-align:left}th{background:#edf5f4;color:#18595a;font-weight:650}td,th{padding:12px 15px;border-bottom:1px solid var(--line);vertical-align:top}td:first-child{font-weight:600;min-width:110px}tr:last-child td{border-bottom:0}tbody tr:nth-child(even){background:#fafcfc}a{color:#076f78}footer{font-size:12px;color:var(--muted);margin-top:22px;text-align:center}.tools{display:flex;gap:12px;align-items:center;justify-content:space-between;margin-bottom:18px;font-size:12px;color:var(--muted)}button{font-family:inherit;background:white;border:1px solid var(--line);border-radius:6px;padding:7px 16px;cursor:pointer;color:var(--ink)}button:hover{border-color:var(--accent)}
@media(max-width:1000px){.layout{grid-template-columns:230px minmax(0,1fr)}aside{padding:22px 13px}main{padding:24px 20px}article{padding:10px 23px 25px}.hero{padding:25px}}
@media(max-width:720px){.layout{display:block}aside{position:relative;height:auto;max-height:270px;border-right:0;border-bottom:1px solid var(--line);padding:17px 20px}.brand{font-size:20px}.brand small{display:none}.nav-label{margin-top:10px}nav{display:grid;grid-template-columns:1fr 1fr}nav a{font-size:12px}main{padding:18px 12px}.hero{padding:23px}.hero h1{font-size:27px}.summary{gap:7px}.summary div{padding:11px}.summary strong{font-size:17px}article{padding:5px 17px 25px}article>h2{font-size:21px}body{font-size:15px}table{min-width:510px}}
@media print{@page{size:A4;margin:18mm}body{background:white;font-size:10.5pt;line-height:1.7}.layout{display:block}aside,.tools,.summary{display:none}main{padding:0;max-width:none}.hero{background:white;color:#102f3a;border:1px solid #c4d6d7;border-top:4px solid var(--accent);padding:18px}.hero p,.eyebrow,.tags span{color:#365661}.hero h1{font-size:25pt}.tags{margin-top:10px}.tags span{border-color:#aac2c6}article{padding:0;border:0}article>h2{font-size:17pt;break-after:avoid}h3{font-size:13pt;break-after:avoid}table{font-size:9pt;min-width:0}tr{break-inside:avoid}.table-wrap{overflow:visible;border-radius:0}thead{display:table-header-group}td,th{padding:6px 8px}blockquote{break-inside:avoid}footer{font-size:9pt}}
</style>
</head>
<body>
<div class="layout">
<aside aria-label="Document contents"><div class="brand"><small>${isData ? 'Data planning / 02' : 'Product planning / 01'}</small>Crow Station</div><div class="nav-label">Document contents · ${sections.length} sections</div><nav>${nav}</nav></aside>
<main>
<div class="tools"><span>Planning reference · 30 September 2026</span><button onclick="window.print()">Print document</button></div>
<header class="hero"><div class="eyebrow">CROW STATION · Version ${version}</div><h1>${heroTitle}</h1><p>${description}</p><div class="tags">${tags.map(tag=>`<span>${tag}</span>`).join('')}</div></header>
<div class="summary">${stats.map(([value,label])=>`<div><strong>${value}</strong><span>${label}</span></div>`).join('')}</div>
<article>${rendered}</article>
<footer>Crow Station · Planning document for review · Source: ${basename}.md</footer>
</main></div>
<script>
const links = Array.from(document.querySelectorAll('nav a'));
const observer = new IntersectionObserver(entries => {
  for (const entry of entries) if (entry.isIntersecting) {
    for (const link of links) link.classList.toggle('active', link.getAttribute('href') === '#' + entry.target.id);
  }
}, {rootMargin:'0px 0px -65% 0px',threshold:0});
document.querySelectorAll('article h2').forEach(section => observer.observe(section));
</script>
</body></html>`;
await writeFile(destination, html, 'utf8');
console.log(JSON.stringify({ path:fileURLToPath(destination), sections:sections.length, markdownCharacters:markdown.length, htmlCharacters:html.length }));
