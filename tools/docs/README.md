# Generate the Reading Versions

Markdown files in `docs` are the content source. The included HTML files can be read without installing any tools.

To regenerate HTML after editing text, run the following from this directory using Node.js 22.16 or later:

```sh
npm install
npm run build
```

The command regenerates the product document and data planning document. The generator does not contact an external service; only dependency installation requires access to the npm registry. The marked library version is pinned. This is a documentation tool, not application setup for Crow Station.
