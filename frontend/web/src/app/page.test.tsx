import { render, screen } from "@testing-library/react";
import { describe, expect, it } from "vitest";
import HomePage from "./page";

describe("HomePage", () => {
  it("shows the product name", () => {
    render(<HomePage />);

    expect(screen.getByRole("heading", { level: 1, name: "Crow Station" })).toBeInTheDocument();
  });
});
