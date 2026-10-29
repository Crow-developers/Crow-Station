/**
 * Minimal typed client for the Crow Station API, used from Server Components and Route Handlers.
 * API_BASE_URL is read at request time so one Docker image works in every environment.
 */
export class ApiError extends Error {
  constructor(
    readonly status: number,
    readonly path: string,
    readonly body: string,
  ) {
    super(`API request to ${path} failed with status ${status}`);
    this.name = "ApiError";
  }
}

export function apiBaseUrl(env: Record<string, string | undefined> = process.env): string {
  const value = env.API_BASE_URL;
  if (!value) {
    throw new Error("API_BASE_URL is not configured.");
  }
  return value.replace(/\/+$/, "");
}

export async function apiGet<T>(path: string, init?: RequestInit): Promise<T> {
  const response = await fetch(`${apiBaseUrl()}${path}`, {
    ...init,
    headers: { Accept: "application/json", ...init?.headers },
  });

  if (!response.ok) {
    throw new ApiError(response.status, path, await response.text());
  }

  return (await response.json()) as T;
}
