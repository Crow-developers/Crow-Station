import type { Metadata } from "next";
import "./globals.css";

export const metadata: Metadata = {
  title: {
    default: "Crow Station",
    template: "%s · Crow Station",
  },
  description: "منصة تعليمية للكورسات والتطبيق العملي والمسارات والإرشاد.",
};

// Arabic first (right-to-left). English support will add a locale segment later.
export default function RootLayout({ children }: LayoutProps<"/">) {
  return (
    <html lang="ar" dir="rtl" className="h-full antialiased">
      <body className="flex min-h-full flex-col">{children}</body>
    </html>
  );
}
