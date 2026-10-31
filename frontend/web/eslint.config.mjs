import { defineConfig, globalIgnores } from "eslint/config";
import nextVitals from "eslint-config-next/core-web-vitals";
import nextTs from "eslint-config-next/typescript";
import prettier from "eslint-config-prettier/flat";

const eslintConfig = defineConfig([
  ...nextVitals,
  ...nextTs,
  // Turns off stylistic rules that conflict with Prettier. Must come last.
  prettier,
  globalIgnores([".next/**", "out/**", "build/**", "coverage/**", "test-results/**", "next-env.d.ts"]),
]);

export default eslintConfig;
