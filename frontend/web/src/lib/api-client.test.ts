import { afterEach, describe, expect, it, vi } from "vitest";
import { ApiError, apiBaseUrl, apiGet } from "./api-client";

describe("apiBaseUrl", () => {
  it("trims trailing slashes", () => {
    expect(apiBaseUrl({ API_BASE_URL: "http://api:8080/" })).toBe("http://api:8080");
  });

  it("fails loudly when not configured", () => {
    expect(() => apiBaseUrl({})).toThrow("API_BASE_URL is not configured.");
  });
});

describe("apiGet", () => {
  afterEach(() => {
    vi.unstubAllEnvs();
    vi.unstubAllGlobals();
  });

  it("returns parsed JSON on success", async () => {
    vi.stubEnv("API_BASE_URL", "http://api:8080");
    const fetchMock = vi.fn().mockResolvedValue(Response.json({ utcNow: "2026-10-29T12:00:00Z" }));
    vi.stubGlobal("fetch", fetchMock);

    const result = await apiGet<{ utcNow: string }>("/api/v1/diagnostics/time");

    expect(result.utcNow).toBe("2026-10-29T12:00:00Z");
    expect(fetchMock).toHaveBeenCalledWith("http://api:8080/api/v1/diagnostics/time", expect.anything());
  });

  it("throws ApiError on a failed response", async () => {
    vi.stubEnv("API_BASE_URL", "http://api:8080");
    vi.stubGlobal("fetch", vi.fn().mockResolvedValue(new Response("nope", { status: 503 })));

    await expect(apiGet("/api/v1/x")).rejects.toBeInstanceOf(ApiError);
  });
});
