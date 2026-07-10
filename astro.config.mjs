import { defineConfig } from 'astro/config';
import sitemap from '@astrojs/sitemap';

export default defineConfig({
  site: 'https://dashkevichstudio.by',
  integrations: [sitemap()],
  output: 'static'
});
