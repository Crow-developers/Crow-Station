import { connection } from "next/server";

// Liveness probe for Docker and orchestrators. connection() opts out of prerendering
// so every call is answered by the running server.
export async function GET() {
  await connection();
  return Response.json({ status: "Healthy" });
}
