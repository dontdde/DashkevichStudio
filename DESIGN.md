# Design

## Intent
Dashkevich Studio should feel like a focused technical workshop: composed, lucid, and built for clients who want capable execution rather than performance theatre.

## Visual Theme
Mood phrase: precision instrument on a black workbench, with indigo light and clean white documentation beside it.

Color strategy: committed restraint. The page uses a near-black architectural background, white document-like content areas, and a saturated indigo primary color for identity and action.

## Color Tokens
Use OKLCH values only.

```css
:root {
  --bg: oklch(0.085 0 0);
  --surface: oklch(0.975 0 0);
  --surface-soft: oklch(0.925 0.010 270);
  --ink: oklch(0.165 0.018 270);
  --muted: oklch(0.430 0.028 270);
  --primary: oklch(0.480 0.180 270);
  --primary-strong: oklch(0.400 0.180 270);
  --accent: oklch(0.710 0.155 160);
  --line: oklch(0.840 0.018 270);
  --dark-ink: oklch(0.950 0 0);
  --dark-muted: oklch(0.760 0.020 270);
}
```

## Typography
Use a system sans stack for speed and maturity: `-apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif`. Avoid decorative or default AI-style display fonts. Use strong size contrast, balanced headings, and compact body copy with a 65-75ch line length.

## Layout
The site should be dense but breathable: a strong first viewport, clear two-direction split, specific service blocks, and one shared case page. Use full-width sections rather than nested cards. Cards are allowed for individual services and case summaries only.

## Components
- Header with simple nav and direct contact action.
- Hero with concise offer, two CTAs, and a visual system panel.
- Direction blocks for Product Development and AI Agents.
- Service matrix.
- Case cards with category labels and concrete deliverables.
- Contact form connected to the studio ASP.NET Core API.
- Footer with email, Telegram, and infrastructure note.

## Motion
Use subtle load and hover motion only. All motion must have a `prefers-reduced-motion` fallback.

## SEO/GEO
Every page needs title, description, canonical URL, Open Graph tags, and structured headings. The home page should include Organization/ProfessionalService JSON-LD. Copy should include clear answer-shaped definitions for "product development" and "AI agents" so AI engines can summarize the studio accurately.
